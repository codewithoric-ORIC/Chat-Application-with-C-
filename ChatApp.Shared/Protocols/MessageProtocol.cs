
using System.Text.Json;

namespace ChatApp.Shared.Protocols;

public static class MessageProtocol
{
    public static byte[] Serialize(object data)
    {
        var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(data);
        var lengthBytes = BitConverter.GetBytes(jsonBytes.Length);
        // Ensure little-endian (BitConverter is system-dependent, but we'll use little-endian)
        if (!BitConverter.IsLittleEndian)
        {
            Array.Reverse(lengthBytes);
        }
        var result = new byte[lengthBytes.Length + jsonBytes.Length];
        Buffer.BlockCopy(lengthBytes, 0, result, 0, lengthBytes.Length);
        Buffer.BlockCopy(jsonBytes, 0, result, lengthBytes.Length, jsonBytes.Length);
        return result;
    }

    public static T? Deserialize<T>(byte[] data)
    {
        return JsonSerializer.Deserialize<T>(data);
    }
}

