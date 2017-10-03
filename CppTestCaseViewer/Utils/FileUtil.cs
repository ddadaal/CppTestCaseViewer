// Decompiled with JetBrains decompiler
// Type: NanjingUniversity.CppPlugin.Util.FileUtil
// Assembly: CppPlugin, Version=1.0.8.0, Culture=neutral, PublicKeyToken=1cd8cf5671fb4165
// MVID: 4B8FC434-AC69-4E09-B6BC-7195624703C6
// Assembly location: C:\Users\viccrubs\Documents\Tencent Files\540232834\FileRecv\plugins\CppPlugin.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace NanjingUniversity.CppPlugin.Util
{
    public class FileUtil
    {

        private const int BUFFER_SIZE = 1024;

        public static byte[] readFileInBytes(string filePath)
        {
            try
            {
                return File.ReadAllBytes(filePath);
            }
            catch (Exception ex)
            {
                return new byte[0];
            }
        }


        public static byte[] readInputStreamIntoBytes(Stream inputStream)
        {
            long num1 = 0;
            if (inputStream.CanSeek)
            {
                num1 = inputStream.Position;
                inputStream.Position = 0L;
            }
            try
            {
                IList<byte[]> numArrayList = (IList<byte[]>)new List<byte[]>();
                int count = 0;
                byte[] buffer = new byte[1024];
                int num2;
                while ((num2 = inputStream.Read(buffer, 0, 1024)) > 0)
                {
                    count += num2;
                    numArrayList.Add(buffer);
                }
                byte[] numArray = new byte[count];
                int index;
                for (index = 0; index < numArrayList.Count - 1; ++index)
                    Buffer.BlockCopy((Array)numArrayList[index], 0, (Array)numArray, index * 1024, numArrayList[index].Length);
                Buffer.BlockCopy((Array)numArrayList[index], 0, (Array)numArray, index * 1024, count);
                return numArray;
            }
            finally
            {
                if (inputStream.CanSeek)
                    inputStream.Position = num1;
            }
        }

        public static bool saveFileWithByte(string saveFilePath, byte[] content)
        {
            try
            {
                FileUtil.CreateDirectoryWithFilePath(saveFilePath);
                File.WriteAllBytes(saveFilePath, content);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static bool saveFile(string saveFilePath, Stream inputStream)
        {
            long num = 0;
            if (inputStream.CanSeek)
            {
                num = inputStream.Position;
                inputStream.Position = 0L;
            }
            try
            {
                FileUtil.CreateDirectoryWithFilePath(saveFilePath);
                FileStream fileStream = new FileStream(saveFilePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
                byte[] buffer = new byte[1024];
                int count;
                while ((count = inputStream.Read(buffer, 0, 1024)) > 0)
                    fileStream.Write(buffer, 0, count);
                fileStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (inputStream.CanSeek)
                    inputStream.Position = num;
            }
        }

        public static bool delAllFile(string path)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                if (directoryInfo.Exists)
                {
                    directoryInfo.Delete(true);
                    return true;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static bool delAllFileWithEX(string path, List<string> exceptions)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                if (directoryInfo.Exists)
                {
                    foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                        directory.Delete(true);
                    foreach (FileInfo file in directoryInfo.GetFiles())
                    {
                        if (!exceptions.Contains(file.Name))
                            file.Delete();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static bool copy(string src, string dst)
        {
            try
            {
                FileUtil.CreateDirectoryWithFilePath(dst);
                File.Copy(src, dst);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool copyDir(string src, string dst)
        {
            if (!Directory.Exists(dst))
                Directory.CreateDirectory(dst);
            if (!Directory.Exists(src))
                return false;
            DirectoryInfo directoryInfo = new DirectoryInfo(src);
            foreach (FileInfo file in directoryInfo.GetFiles())
                FileUtil.copy(file.FullName, Path.Combine(dst, file.Name));
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                FileUtil.copyDir(directory.FullName, Path.Combine(dst, directory.Name));
            return true;
        }

        private static void CreateDirectoryWithFilePath(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Directory.Exists)
                return;
            fileInfo.Directory.Create();
        }
    }
}
