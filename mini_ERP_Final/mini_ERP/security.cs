using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;

namespace TeamProject_test_v1
{
    internal class security
    {
        private static security instance_=new security();
        public static security getinstance_() { return instance_; }
        //SqZPLFWfur = 암호화 벡터키
        public string getpassword(string password)//암호화
        {
            byte[] DataToEncrypt=Encoding.UTF8.GetBytes(password);
            string EncryptToData = Encrypt(DataToEncrypt, "SqZPLFWfur");//login_Settings.Default.pass_vector
            return EncryptToData;
        }
        public string AesToPass(string pass)//복호화
        {
            byte[] encode_pass = Convert.FromBase64String(pass);
            string DecryptToData = Decrypt(encode_pass, "SqZPLFWfur");
            return DecryptToData;
        }

        public static Rfc2898DeriveBytes createkey(string password)
        {
            byte[] KeyBytes=Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = SHA512.Create().ComputeHash(KeyBytes);

            Rfc2898DeriveBytes result = new Rfc2898DeriveBytes(KeyBytes, saltBytes, 200000);
            return result;
        }
        public static Rfc2898DeriveBytes createvector(string vector)
        {
            byte[] vectorBytes=Encoding.UTF8.GetBytes(vector);
            byte[] saltbyte=SHA512.Create().ComputeHash(vectorBytes);

            Rfc2898DeriveBytes result = new Rfc2898DeriveBytes(vectorBytes, saltbyte, 200000);

            return result;
        }
        //G994n5ObLssWF4y6g7E0AqJiXksq5MhT = 암호화 원본솥팅 벡터키
        public static string Encrypt(byte[] origin, string password)
        {
            Aes aes = Aes.Create();
            Rfc2898DeriveBytes key = createkey(password);
            Rfc2898DeriveBytes vector = createvector("G994n5ObLssWF4y6g7E0AqJiXksq5MhT");//login_Settings.Default.RFC_vector

            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key.GetBytes(32);
            aes.IV = vector.GetBytes(16);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using(MemoryStream ms = new MemoryStream())
            {
                using(CryptoStream cs =  new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(origin, 0, origin.Length);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public static string Decrypt(byte[] origin, string password)
        {
            Aes aes = Aes.Create();
            Rfc2898DeriveBytes key = createkey(password);
            Rfc2898DeriveBytes vector = createvector("G994n5ObLssWF4y6g7E0AqJiXksq5MhT");

            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key.GetBytes(32);
            aes.IV = vector.GetBytes(16);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                {
                    cs.Write(origin, 0, origin.Length);
                }
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
