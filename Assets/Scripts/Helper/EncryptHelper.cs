using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class EncryptHelper
{
    private static readonly string K = "S3cr3t_M3d1eval_StUd3nt_K3y_2026"; // 32 chars
    private static readonly string I = "Sp4c3_B4s3_IV_!!"; // 16 chars

    public static string Encrypt(string text) {
        using (Aes aes = Aes.Create()) {
            aes.Key = Encoding.UTF8.GetBytes(K);
            aes.IV = Encoding.UTF8.GetBytes(I);
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream()) {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)) {
                    using (var sw = new StreamWriter(cs)) sw.Write(text);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }

    public static string Decrypt(string cipher) {
        using (Aes aes = Aes.Create()) {
            aes.Key = Encoding.UTF8.GetBytes(K);
            aes.IV = Encoding.UTF8.GetBytes(I);
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream(Convert.FromBase64String(cipher))) {
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read)) {
                    using (var sr = new StreamReader(cs)) return sr.ReadToEnd();
                }
            }
        }
    }
}