using System;
using System.IO;
using System.Security.Cryptography;

public class EncryptionModule
{
    public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(data, 0, data.Length);
        cs.FlushFinalBlock();

        return ms.ToArray();
    }

    public static byte[] GenerateKey() => Aes.Create().Key;
    public static byte[] GenerateIV() => Aes.Create().IV;
}
