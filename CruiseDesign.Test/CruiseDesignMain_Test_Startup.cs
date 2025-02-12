using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CruiseDesign.Test
{
    public class CruiseDesignMain_Test_Startup : TestBase
    {
        public CruiseDesignMain_Test_Startup(ITestOutputHelper output) : base(output)
        {
        }


        [Fact(Skip = "Because test launches UI. Remove skip to run test manually")]
        public void ProgramStarts()
        {
            CruiseDesign.Program.Main(new string[0]);
        }

        [Fact(Skip = "Because test launches UI. Remove skip to run test manually")]
        public void ProgramStarts_WithArgs()
        {
            var testFilePath = GetTestFile("99996_TestMeth_Timber_Sale_08072021.design");
            var args = new[] { testFilePath };

            CruiseDesign.Program.Main(args);
        }

        [Fact(Skip = "Because test launches UI. Remove skip to run test manually")]
        public void ProgramStarts_WithArgs_ReadOnlyFile()
        {
            var testFilePath = GetTestFile("99996_TestMeth_Timber_Sale_08072021.design");

            var readOnlyFilePath = Path.Combine(TestTempPath, "ProgramStarts_WithArgs_ReadOnlyFile.design");
            if (!File.Exists(readOnlyFilePath))
            {
                File.Copy(testFilePath, readOnlyFilePath);
                File.SetAttributes(readOnlyFilePath, FileAttributes.ReadOnly);
            }

            var args = new[] { readOnlyFilePath };

            CruiseDesign.Program.Main(args);
        }

    }
}
