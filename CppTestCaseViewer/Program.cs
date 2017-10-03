using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NanjingUniversity.CppPlugin.Util;

namespace CppTestCaseViewer
{
    class Program
    {
        public const string Root = ".\\";
        public static string TestCasesFolder = Root + "testcases\\";
        public static string DecompressedFolder =Root+ "decompressed\\";
        public static string ZipFolder = Root + "zips\\";
        public static string ExportedFolder = Root + "export\\";

        static void Main(string[] args)
        {
            Output("Program started.");

            if (!Directory.Exists(DecompressedFolder))
            {
                Directory.CreateDirectory(DecompressedFolder);
            }

            if (!Directory.Exists(ZipFolder))
            {
                Directory.CreateDirectory(ZipFolder);
            }
            if (!Directory.Exists(ExportedFolder))
            {
                Directory.CreateDirectory(ExportedFolder);
            }


            Directory.Delete(ExportedFolder);
            Directory.CreateDirectory(ExportedFolder);

            Output("Export folder deleted and recreated.");

            IEnumerable<string> qids = Directory.EnumerateFiles(TestCasesFolder).Select(x => x.Split('\\').Last());

            Output($"{qids.Count()} problem(s) detected.");

            foreach (var qid in qids)
            {
                
                Output("Parsing problem {qid}...");
                string fileFullPath = TestCasesFolder +  qid;
                string zipFullPath = ZipFolder + qid + ".zip";
                string decompressedFolderPath = DecompressedFolder +"\\"+ qid;
                string exportPath = ExportedFolder + qid;
                if (!Directory.Exists(exportPath))
                {
                    Directory.CreateDirectory(exportPath);
                }
                
                byte[] content = EncryptUtil.decrypt(FileUtil.readFileInBytes(fileFullPath));
                Output("Decrypted.");

                FileUtil.saveFileWithByte(zipFullPath, content);
                ZipUtil.unZipFiles(zipFullPath, decompressedFolderPath);
                Output("Zip exported and unzipped.");

                string unzippedInnerDir = Directory.EnumerateDirectories(decompressedFolderPath).First();

                string testcasesDir = unzippedInnerDir + "\\test_cases";

                string targetDir = ExportedFolder + qid + "\\";

                foreach(var s in Directory.EnumerateFiles(testcasesDir))
                {
                    string fileName = Path.GetFileName(s);
                    File.Copy(s, targetDir + fileName);
                }

                //var problem = Problem.Parse(unzippedInnerDir+"\\", qid);
                Output("Test cases exported.");
                Output("=======================================");
            }

            Output($"Test cases for all {qids.Count()} problems have been exported. Check them out on {ExportedFolder}!");
            Console.ReadLine();

        }

        public static void Output(string content)
        {
            Console.WriteLine(content);
        }
        
    }
}
