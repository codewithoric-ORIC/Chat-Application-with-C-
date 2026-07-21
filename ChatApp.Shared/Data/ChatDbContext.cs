
using Microsoft.Data.Sqlite;
using ChatApp.Shared.Models;

namespace ChatApp.Shared.Data;

public class ChatDbContext
{
    private const string DbPath = "chat.db";

    public ChatDbContext()
    {
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var connection = GetConnection();
        connection.Open();

        var createUsersTable = @"
            CREATE TABLE IF NOT EXISTS Users (
                Username TEXT PRIMARY KEY,
                PasswordHash TEXT NOT NULL
            );";
        using var cmd1 = new SqliteCommand(createUsersTable, connection);
        cmd1.ExecuteNonQuery();

        var createMessagesTable = @"
            CREATE TABLE IF NOT EXISTS Messages (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                SenderUsername TEXT NOT NULL,
                ReceiverUsername TEXT,
                Content TEXT NOT NULL,
                Timestamp TEXT NOT NULL,
                IsPrivate INTEGER NOT NULL
            );";
        using var cmd2 = new SqliteCommand(createMessagesTable, connection);
        cmd2.ExecuteNonQuery();
    }

    public SqliteConnection GetConnection()
    {
        return new SqliteConnection($"Data Source={DbPath}");
    }

    public bool RegisterUser(string username, string passwordHash)
    {
        try
        {
            using var connection = GetConnection();
            connection.Open();

            var cmd = new SqliteCommand("INSERT INTO Users (Username, PasswordHash) VALUES (@username, @hash)", connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@hash", passwordHash);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public string? GetUserPasswordHash(string username)
    {
        using var connection = GetConnection();
        connection.Open();

        var cmd = new SqliteCommand("SELECT PasswordHash FROM Users WHERE Username = @username", connection);
        cmd.Parameters.AddWithValue("@username", username);
        var result = cmd.ExecuteScalar();
        return result?.ToString();
    }

    public void SaveMessage(Message message)
    {
        using var connection = GetConnection();
        connection.Open();

        var cmd = new SqliteCommand(@"
            INSERT INTO Messages (SenderUsername, ReceiverUsername, Content, Timestamp, IsPrivate)
            VALUES (@sender, @receiver, @content, @timestamp, @isPrivate)", connection);
        cmd.Parameters.AddWithValue("@sender", message.SenderUsername);
        cmd.Parameters.AddWithValue("@receiver", (object?)message.ReceiverUsername ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@content", message.Content);
        cmd.Parameters.AddWithValue("@timestamp", message.Timestamp.ToString("o"));
        cmd.Parameters.AddWithValue("@isPrivate", message.IsPrivate ? 1 : 0);
        cmd.ExecuteNonQuery();
    }

    public List<Message> GetMessageHistory(string? user1 = null, string? user2 = null, bool isPrivate = false)
    {
        var messages = new List<Message>();
        using var connection = GetConnection();
        connection.Open();

        SqliteCommand cmd;
        if (isPrivate && user1 != null && user2 != null)
        {
            cmd = new SqliteCommand(@"
                SELECT SenderUsername, ReceiverUsername, Content, Timestamp, IsPrivate
                FROM Messages
                WHERE IsPrivate = 1 AND
                      ((SenderUsername = @user1 AND ReceiverUsername = @user2) OR
                       (SenderUsername = @user2 AND ReceiverUsername = @user1))
                ORDER BY Timestamp ASC", connection);
            cmd.Parameters.AddWithValue("@user1", user1);
            cmd.Parameters.AddWithValue("@user2", user2);
        }
        else
        {
            cmd = new SqliteCommand(@"
                SELECT SenderUsername, ReceiverUsername, Content, Timestamp, IsPrivate
                FROM Messages
                WHERE IsPrivate = 0
                ORDER BY Timestamp ASC", connection);
        }

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            messages.Add(new Message
            {
                SenderUsername = reader.GetString(0),
                ReceiverUsername = reader.IsDBNull(1) ? null : reader.GetString(1),
                Content = reader.GetString(2),
                Timestamp = DateTime.Parse(reader.GetString(3)),
                IsPrivate = reader.GetInt32(4) == 1
            });
        }

        return messages;
    }
}

