using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CppTestCaseViewer
{
    class TestCase
    {
        public string ID { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }

        public override string ToString()
        {
            string endsWithNewLine = Output.EndsWith("\n") ? "(Ends with a new line)" : "(Ends without a new line)";

            return $"No.{ID}:\nInput: {Input}\n\nOutput: {Output} \n {endsWithNewLine} \n".Replace("\n",Environment.NewLine);
        }

        public static TestCase Parse(string testcaseRootPath, string id)
        {
            string inPath = ToFullPath(testcaseRootPath, $"test_{id}.in");
            string outPath = ToFullPath(testcaseRootPath, $"test_{id}.out");

            if (!File.Exists(inPath))
            {
                inPath = ToFullPath(testcaseRootPath, $"testcase_{id}.in");
                outPath = ToFullPath(testcaseRootPath, $"testcase_{id}.out");
            }

            if (!File.Exists(inPath))
            {
                inPath = ToFullPath(testcaseRootPath, $"test{id}.in");
                outPath = ToFullPath(testcaseRootPath, $"test{id}.out");
            }

            


            return new TestCase()
            {
                ID = id,
                Input = File.ReadAllText(inPath),
                Output = File.ReadAllText(outPath)
            };

        }

        static string ToFullPath(string testcaseRootPath, string fileName)
        {
            return testcaseRootPath + "\\" + fileName;
        }
    }
}
