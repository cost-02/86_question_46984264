using System;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;

public class Encryptor
{
    public static string EncryptString(string text, string passphrase)
    {
        byte[] data = Encoding.UTF8.GetBytes(text);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(passphrase));
            using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider()
            {
                Key = keys,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            })
            {
                ICryptoTransform transform = tripDes.CreateEncryptor();
                byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(results, 0, results.Length);
            }
        }
    }

    public static void InsertDataIntoSQL(string connectionString, string encryptedData)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO YourTable (EncryptedColumn) VALUES (@EncryptedData)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@EncryptedData", encryptedData);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }

    public static void main(string[] args)
    {
        string connectionString = "your_connection_string";
        string dataToEncrypt = "Dato decrittografato da Java";
        string passphrase = "la_tua_passphrase";

        string encryptedData = EncryptString(dataToEncrypt, passphrase);
        InsertDataIntoSQL(connectionString, encryptedData);
        Console.WriteLine("Dato ricrittografato e inserito in SQL Server.");
    }
}
