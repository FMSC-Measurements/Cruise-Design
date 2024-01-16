using CruiseDesign.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using NSubstitute;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using System.IO;
using CruiseDAL;

namespace CruiseDesign.Test
{
    public class CruiseDesignMain_Test_OpenExistingDesignFile : TestBase
    {
        public CruiseDesignMain_Test_OpenExistingDesignFile(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData(270)]
        [InlineData(260)]
        [InlineData(258)]

        public void OpenExistingDesignFile_PathTooLong(int desiredDirLength)
        {
            var fileName = "a.design";
            var dir = base.TestTempPath;
            var baseDirLength = dir.Length;
            var neededExtraDirLength = desiredDirLength - baseDirLength - fileName.Length; 

            var extraDir = new String('a', neededExtraDirLength);

            var targetPath = Path.Combine(dir, extraDir, "a.design");
            Output.WriteLine("TargetPath::::(" + targetPath.Length + ")" + targetPath);

            var fileContextProvider = new CruiseDesignFileContextProvider();

            var logger = MakeMockLogger();
            var dialogService = MakeMockDialogService();

            if (File.Exists(targetPath)) { File.Delete(targetPath); }

            CruiseDesignMain.OpenExistingDesignFile(targetPath, fileContextProvider, logger, dialogService);

            // verify error message shown
            dialogService.Received().ShowMessage(Arg.Is("File Path Too Long"), Arg.Any<string>());
        }

        [Theory]
        [InlineData(".cruise")]
        [InlineData(".design")]
        [InlineData(".crz3")]
        [InlineData(".process")]
        public void OpenExistingDesignFile_NonExistantFile(string extention)
        {
            var fileContextProvider = new CruiseDesignFileContextProvider();

            var logger = MakeMockLogger();
            var dialogService = MakeMockDialogService();
            var path = GetTempFilePath( nameof(OpenExistingDesignFile_NonExistantFile) + extention);

            if(File.Exists(path)) { File.Delete(path); }

            CruiseDesignMain.OpenExistingDesignFile(path, fileContextProvider, logger, dialogService);

            // verify error message shown
            dialogService.Received().ShowMessage(Arg.Is("Selected File Does Not Exist"), Arg.Any<string>());
            dialogService.Received(1).ShowMessage(Arg.Any<string>(), Arg.Any<string>());
        }

        [Theory]
        [InlineData(".abc")]
        public void OpenExistingDesignFile_InvalidFileExtention(string extention)
        {
            var fileContextProvider = new CruiseDesignFileContextProvider();

            var logger = MakeMockLogger();
            var dialogService = MakeMockDialogService();
            var path = GetTempFilePath(nameof(OpenExistingDesignFile_InvalidFileExtention) + extention);

            if (File.Exists(path)) { File.Delete(path); }
            File.Create(path);

            CruiseDesignMain.OpenExistingDesignFile(path, fileContextProvider, logger, dialogService);

            // verify error message shown
            dialogService.Received().ShowMessage(Arg.Is("Unable to open file. Unrecognized extension"), Arg.Any<string>());
        }

        [Fact]
        public void OpenExistingDesignFile_V2CruiseWithDesignFile()
        {
            var fileContextProvider = new CruiseDesignFileContextProvider();

            var logger = MakeMockLogger();
            var dialogService = MakeMockDialogService();
            var cruisePath = GetTempFilePath(nameof(OpenExistingDesignFile_V2CruiseWithDesignFile) + ".cruise");
            var designPath = GetTempFilePath(nameof(OpenExistingDesignFile_V2CruiseWithDesignFile) + ".design");
            if (File.Exists(cruisePath)) { File.Delete(cruisePath); }
            if (File.Exists(designPath)) { File.Delete(designPath); }

            using var cruiseDb = new DAL(cruisePath, true);
            using var processDb = new DAL(designPath, true);

            CruiseDesignMain.OpenExistingDesignFile(cruisePath, fileContextProvider, logger, dialogService);

            // verify no messages received
            dialogService.DidNotReceiveWithAnyArgs().ShowMessage(default, default);

            // verify file context
            var fileContext = fileContextProvider.CurrentFileContext;
            fileContext.Should().NotBeNull();
            fileContext.DesignFilePath.Should().Be(cruisePath);
            fileContext.IsProductionFile.Should().BeTrue();
            fileContext.ReconFilePath.Should().BeNull();
            fileContext.DesignDb.Should().NotBeNull();
        }

        [Fact]
        public void OpenExistingDesignFile_V3CruiseMissingProcessFile()
        {
            var fileContextProvider = new CruiseDesignFileContextProvider();

            var logger = MakeMockLogger();
            var dialogService = MakeMockDialogService();
            var cruisePath = GetTempFilePath(nameof(OpenExistingDesignFile_V3CruiseMissingProcessFile) + ".crz3");
            var processPath = GetTempFilePath(nameof(OpenExistingDesignFile_V3CruiseMissingProcessFile) + ".process");
            if (File.Exists(cruisePath)) { File.Delete(cruisePath); }
            if (File.Exists(processPath)) { File.Delete(processPath); }

            using var cruiseDb = new CruiseDatastore_V3(cruisePath, true);

            CruiseDesignMain.OpenExistingDesignFile(cruisePath, fileContextProvider, logger, dialogService);

            // verify warning messages received
            dialogService.Received().ShowMessage(Arg.Is("Cruise file not processed. Cannot continue."), Arg.Any<string>());

            // verify file context
            var fileContext = fileContextProvider.CurrentFileContext;
            fileContext.Should().BeNull();
        }

        [Fact]
        public void OpenExistingDesignFile_V3CruiseWithProcessFile()
        {
            var fileContextProvider = new CruiseDesignFileContextProvider();

            var logger = MakeMockLogger();
            var dialogService = MakeMockDialogService();
            var cruisePath = GetTempFilePath(nameof(OpenExistingDesignFile_V3CruiseWithProcessFile) + ".crz3");
            var processPath = GetTempFilePath(nameof(OpenExistingDesignFile_V3CruiseWithProcessFile) + ".process");
            if (File.Exists(cruisePath)) { File.Delete(cruisePath); }
            if (File.Exists(processPath)) { File.Delete(processPath); }

            using var cruiseDb = new CruiseDatastore_V3(cruisePath, true);
            using var designDb = new DAL(processPath, true);

            CruiseDesignMain.OpenExistingDesignFile(cruisePath, fileContextProvider, logger, dialogService);

            // verify no messages received
            dialogService.DidNotReceiveWithAnyArgs().ShowMessage(default, default);

            // verify file context
            var fileContext = fileContextProvider.CurrentFileContext;
            fileContext.Should().NotBeNull();
            fileContext.DesignFilePath.Should().Be(processPath);
            fileContext.IsProductionFile.Should().BeTrue();
            fileContext.ReconFilePath.Should().BeNull();
            fileContext.DesignDb.Should().NotBeNull();
            fileContext.DesignDb.Path.Should().Be(processPath);
        }

        [Fact]
        public void OpenExistingDesignFile_V2DesignFile()
        {
            var fileContextProvider = new CruiseDesignFileContextProvider();

            var logger = MakeMockLogger();
            var dialogService = MakeMockDialogService();
            //var cruisePath = GetTempFilePath(nameof(OpenExistingDesignFile_V2DesignFile) + ".cruise");
            var designPath = GetTempFilePath(nameof(OpenExistingDesignFile_V2DesignFile) + ".design");
            //if (File.Exists(cruisePath)) { File.Delete(cruisePath); }
            if (File.Exists(designPath)) { File.Delete(designPath); }

            //using var cruiseDb = new DAL(cruisePath, true);
            using var designDb = new DAL(designPath, true);

            CruiseDesignMain.OpenExistingDesignFile(designPath, fileContextProvider, logger, dialogService);

            // verify no messages received
            dialogService.DidNotReceiveWithAnyArgs().ShowMessage(default, default);

            // verify file context
            var fileContext = fileContextProvider.CurrentFileContext;
            fileContext.Should().NotBeNull();
            fileContext.DesignFilePath.Should().Be(designPath);
            fileContext.IsProductionFile.Should().BeFalse();
            fileContext.ReconFilePath.Should().BeNull();
            fileContext.DoesReconExist.Should().BeFalse();
            fileContext.DesignDb.Should().NotBeNull();
        }

        [Fact]
        public void OpenExistingDesignFile_V2DesignFileWithV2Recon()
        {
            var fileContextProvider = new CruiseDesignFileContextProvider();

            var logger = MakeMockLogger();
            var dialogService = MakeMockDialogService();
            var reconCruisePath = GetTempFilePath(nameof(OpenExistingDesignFile_V2DesignFileWithV2Recon) + ".cruise");
            var designPath = GetTempFilePath(nameof(OpenExistingDesignFile_V2DesignFileWithV2Recon) + ".design");
            if (File.Exists(reconCruisePath)) { File.Delete(reconCruisePath); }
            if (File.Exists(designPath)) { File.Delete(designPath); }

            using var cruiseDb = new DAL(reconCruisePath, true);
            using var designDb = new DAL(designPath, true);

            CruiseDesignMain.OpenExistingDesignFile(designPath, fileContextProvider, logger, dialogService);

            // verify no messages received
            dialogService.DidNotReceiveWithAnyArgs().ShowMessage(default, default);

            // verify file context
            var fileContext = fileContextProvider.CurrentFileContext;
            fileContext.Should().NotBeNull();
            fileContext.DesignFilePath.Should().Be(designPath);
            fileContext.IsProductionFile.Should().BeFalse();
            fileContext.ReconFilePath.Should().Be(reconCruisePath);
            fileContext.DoesReconExist.Should().BeTrue();
            fileContext.DesignDb.Should().NotBeNull();
        }

        [Fact]
        public void OpenExistingDesignFile_DesignFileWithV3Recon()
        {
            var fileContextProvider = new CruiseDesignFileContextProvider();

            var logger = MakeMockLogger();
            var dialogService = MakeMockDialogService();
            var reconCruisePath = GetTempFilePath(nameof(OpenExistingDesignFile_DesignFileWithV3Recon) + ".crz3");
            var designPath = GetTempFilePath(nameof(OpenExistingDesignFile_DesignFileWithV3Recon) + ".design");
            var processPath = GetTempFilePath(nameof(OpenExistingDesignFile_DesignFileWithV3Recon) + ".process");
            if (File.Exists(reconCruisePath)) { File.Delete(reconCruisePath); }
            if (File.Exists(designPath)) { File.Delete(designPath); }
            if (File.Exists(processPath)) { File.Delete(processPath); }

            using var reconCruiseDb = new CruiseDatastore_V3(reconCruisePath, true);
            using var processDb = new DAL(processPath, true);
            using var designDb = new DAL(designPath, true);

            CruiseDesignMain.OpenExistingDesignFile(designPath, fileContextProvider, logger, dialogService);

            // verify no messages received
            dialogService.DidNotReceiveWithAnyArgs().ShowMessage(default, default);

            // verify file context
            var fileContext = fileContextProvider.CurrentFileContext;
            fileContext.Should().NotBeNull();
            fileContext.DesignFilePath.Should().Be(designPath);
            fileContext.IsProductionFile.Should().BeFalse();
            fileContext.ReconFilePath.Should().Be(processPath);
            fileContext.DoesReconExist.Should().BeTrue();
            fileContext.V3FilePath.Should().Be(reconCruisePath);
            fileContext.IsUsingV3File.Should().BeTrue();
            fileContext.DesignDb.Should().NotBeNull();
        }

    }
}
