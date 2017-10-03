// Decompiled with JetBrains decompiler
// Type: NanjingUniversity.CppPlugin.Util.ZipUtil
// Assembly: CppPlugin, Version=1.0.8.0, Culture=neutral, PublicKeyToken=1cd8cf5671fb4165
// MVID: 4B8FC434-AC69-4E09-B6BC-7195624703C6
// Assembly location: C:\Users\viccrubs\Documents\Tencent Files\540232834\FileRecv\plugins\CppPlugin.dll

using System.IO;
using System.IO.Compression;

namespace NanjingUniversity.CppPlugin.Util
{
    internal class ZipUtil
    {
        public static void zipDirectory(string zipFileFullPath, string desDirFullPath)
        {
            if (!Directory.Exists(zipFileFullPath))
                return;
            if (File.Exists(desDirFullPath))
                File.Delete(desDirFullPath);
            DirectoryInfo parent = Directory.GetParent(desDirFullPath);
            if (!parent.Exists)
                parent.Create();
            ZipFile.CreateFromDirectory(zipFileFullPath, desDirFullPath);
        }

        public static void unZipFiles(string zipFileFullPath, string desDirFullPath)
        {
            if (!File.Exists(zipFileFullPath))
                return;
            if (!Directory.Exists(desDirFullPath))
                Directory.CreateDirectory(desDirFullPath);
            ZipFile.ExtractToDirectory(zipFileFullPath, desDirFullPath);
        }
    }
}
