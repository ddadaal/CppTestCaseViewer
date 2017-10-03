// Decompiled with JetBrains decompiler
// Type: NanjingUniversity.CppPlugin.Util.EncryptUtil
// Assembly: CppPlugin, Version=1.0.8.0, Culture=neutral, PublicKeyToken=1cd8cf5671fb4165
// MVID: 4B8FC434-AC69-4E09-B6BC-7195624703C6
// Assembly location: C:\Users\viccrubs\Documents\Tencent Files\540232834\FileRecv\plugins\CppPlugin.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NanjingUniversity.CppPlugin.Util
{
    internal class EncryptUtil
    {
        private static string aeskey = "hYOTz5Il8IzWQSVk";

        public static byte[] decrypt(byte[] input)
        {
            byte[] numArray1 = Convert.FromBase64String(Encoding.UTF8.GetString(input));
            AesManaged aesManaged = new AesManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                Key = Encoding.UTF8.GetBytes(EncryptUtil.aeskey)
            };
            byte[] numArray2 = new byte[16];
            Array.Copy((Array)numArray1, 0, (Array)numArray2, 0, 16);
            aesManaged.IV = numArray2;
            byte[] buffer1 = new byte[numArray1.Length - 16];
            Array.Copy((Array)numArray1, 16, (Array)buffer1, 0, numArray1.Length - 16);
            CryptoStream cryptoStream = new CryptoStream((Stream)new MemoryStream(buffer1), aesManaged.CreateDecryptor(), CryptoStreamMode.Read);
            byte[] buffer2 = new byte[buffer1.Length];
            cryptoStream.Read(buffer2, 0, buffer2.Length);
            cryptoStream.Close();
            return buffer2;
        }


    }
}
