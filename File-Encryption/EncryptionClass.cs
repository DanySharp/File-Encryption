using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace File_Encryption
{
    class EncryptionClass
    {
        ThreadClass threadin = new ThreadClass();
        public void EncryptionFile(string inputadd, string outputadd, string password)
        {
            threadin.StartThread();
            byte[] EncToByte = File.ReadAllBytes(inputadd);
            byte[] bytepass = Encoding.UTF8.GetBytes(password);

            bytepass = SHA256.Create().ComputeHash(bytepass);
            byte[] encByte = Encryption(EncToByte, bytepass);

            File.WriteAllBytes(outputadd, encByte);
            threadin.EndThread();

        }


        public byte[] Encryption(byte[] ToEncrypted, byte[] passwordbyte)
        {
            byte[] Encryptedbyte = null;
            byte[] saltbyte = new byte[] { 1,2,3,4,5,6,7,8};

            using (MemoryStream ms=new MemoryStream())
            {
                using(RijndaelManaged AESm=new RijndaelManaged() )
	            {
		        AESm.BlockSize=128;
                    AESm.KeySize=256;
                    var key=new Rfc2898DeriveBytes(passwordbyte,saltbyte,1000);
                    AESm.Key=key.GetBytes(AESm.KeySize/8);
                    AESm.IV=key.GetBytes(AESm.BlockSize/8);
                    AESm.Mode=CipherMode.CBC;

                    using(var cs=new CryptoStream (ms,AESm.CreateEncryptor(),CryptoStreamMode.Write))
                   	{
		 cs.Write(ToEncrypted,0,ToEncrypted.Length);
                        cs.Close();
                   	}

                    Encryptedbyte=ms.ToArray();

	            }


          }
            return Encryptedbyte;
        }





        ///decrypt
        ///
        public byte[] Decryption(byte[] todecrypt, byte[] passwordbyte)
        {
            byte[] decryptbyte = null;
            byte[] saltbyte = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AESm = new RijndaelManaged())
                {
                    AESm.BlockSize = 128;
                    AESm.KeySize = 256;
                    var key = new Rfc2898DeriveBytes(passwordbyte, saltbyte, 1000);
                    AESm.Key = key.GetBytes(AESm.KeySize / 8);
                    AESm.IV = key.GetBytes(AESm.BlockSize / 8);
                    AESm.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AESm.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(todecrypt, 0, todecrypt.Length);
                        cs.Close();
                    }

                    decryptbyte = ms.ToArray();

                }


            }
            return decryptbyte;
        }

        public void DecryptFile(string inputadd, string outputadd, string password)
        {
            threadin.StartThread();
            byte[] decryptfile = File.ReadAllBytes(inputadd);
            byte[] bytepass = Encoding.UTF8.GetBytes(password);

            bytepass = SHA256.Create().ComputeHash(bytepass);
            byte[] encByte = Decryption(decryptfile, bytepass);

            File.WriteAllBytes(outputadd, encByte);
            threadin.EndThread();

        }
    }
}
