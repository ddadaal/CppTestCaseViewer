using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CppTestCaseViewer
{
    class Problem
    {

        static string descriptionPath = Path.Combine("question", "description");

        static string testcasesPath = "test_cases";

        public string ID { get; set; }
        public string Description { get; set; }
        public IEnumerable<TestCase> TestCases { get; set; }

        public override string ToString()
        {
            return $"Problem {ID}:{Description}\n\nTest cases:\n{TestCases.Select(x => x.ToString()).Aggregate("", (x, y) => x + y + "\n")}"
                .Replace("\n", Environment.NewLine);
        }

        public void Export(string exportFilePath)
        {
            using (var writer = new StreamWriter(exportFilePath, false))
            {
                writer.WriteLine(this.ToString());
            }

        }

        public static Problem Parse(string decompressedFolder, string qid)
        {
            var problem = new Problem
            {
                ID = qid,
                Description = File.ReadAllText(decompressedFolder + descriptionPath)
            };

            List<TestCase> cases = new List<TestCase>();

            string testcasesFullPath = decompressedFolder + testcasesPath;
            int totalFileNum = Directory.EnumerateFiles(testcasesFullPath).Count();
            
            for(int i=1;i<=totalFileNum/2;i++)
            {
                cases.Add(TestCase.Parse(testcasesFullPath,i.ToString()));
            }

            problem.TestCases = cases;

            return problem;
        }

    }
}
