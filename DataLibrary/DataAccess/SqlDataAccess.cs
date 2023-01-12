using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using System.IO;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "KutuphaneDB")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }


        /// <summary>
        /// Veritabanı üzerinde verilen sql'i çalıştırır. Veri tabanına veri kaydetmek için kullanılabilir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns>SQL başarıyla tamamlanırsa etkilenen satır sayısını, tamamlanamazsa 0 döner.</returns>
        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    return cnn.Execute(sql, data);
                }
                catch
                {
                    return 0;
                }
            }
        }
        public static int SaveDataId<T>(string sql, string sqlId, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    if (cnn.Execute(sql, data) != 0)
                    {
                        return cnn.Query<int>(sqlId, data).First();
                    }
                    else return 0;
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Veri tabanında veri aramk için kullanılabilir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int CheckData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<int>(sql, data).First();
            }
        }


        private static string CipherDecrypter(string cipherText)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                AsymmetricCipherKeyPair keyPair;

                using (var reader = File.OpenText(@"D:\Projects\DataLibrary\Keys\private-key.pem"))
                    keyPair = (AsymmetricCipherKeyPair)new PemReader(reader).ReadObject();

                var decryptEngine = new Pkcs1Encoding(new RsaEngine());

                decryptEngine.Init(false, keyPair.Private);

                return Encoding.UTF8.GetString(decryptEngine.ProcessBlock(cipherBytes, 0, cipherBytes.Length));
            }
            catch
            {
                return null; // Null
            }
        }
        public static bool EncryptedLoginControl<T>(string sql, T data, string parola)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                string DBcipherText = null;
                try
                {
                    DBcipherText = cnn.Query<string>(sql, data).First();
                }
                catch
                {
                    return false;
                }
                if (DBcipherText == null) return false;

                if (CipherDecrypter(DBcipherText) == CipherDecrypter(parola))
                {
                    return true;
                }
                return false;

            }
        }
        public static int EncryptedLoginControlId<T>(string sql, string sqlId, T data, string parola)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                string DBcipherText = null;
                try
                {
                    DBcipherText = cnn.Query<string>(sql, data).First();
                }
                catch
                {
                    return 0;
                }
                if (DBcipherText == null) return 0;

                if (CipherDecrypter(DBcipherText) == CipherDecrypter(parola))
                {
                    return cnn.Query<int>(sqlId, data).First();
                }
                return 0;

            }
        }


        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }
    }
}

