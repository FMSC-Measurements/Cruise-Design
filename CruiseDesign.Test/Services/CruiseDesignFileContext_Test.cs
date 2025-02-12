using CruiseDesign.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CruiseDesign.Test.Services
{
    public class CruiseDesignFileContext_Test : TestBase
    {
        public CruiseDesignFileContext_Test(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData(270)]
        [InlineData(260)]
        [InlineData(258)]

        public void EnsurePathValid_PathTooLong(int desiredDirLength)
        {
            var fileName = "a.design";
            var dir = base.TestTempPath;
            var baseDirLength = dir.Length;
            var neededExtraDirLength = desiredDirLength - baseDirLength - fileName.Length;

            var extraDir = String.Concat(Enumerable.Range(1, neededExtraDirLength).Select(x => (x % 10)));

            var targetPath = Path.Combine(dir, extraDir, "a.design");
            Output.WriteLine("TargetPath::::(" + targetPath.Length + ")" + targetPath);

            var logger = MakeMockLogger();
            var dialogService = MakeMockDialogService();

            if (File.Exists(targetPath)) { File.Delete(targetPath); }

            CruiseDesignFileContext.EnsurePathValid(targetPath, logger, dialogService);

            // verify error message shown
            dialogService.Received().ShowMessage(Arg.Is("File Path Too Long"), Arg.Any<string>());
        }
    }
}
