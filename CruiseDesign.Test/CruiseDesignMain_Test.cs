using CruiseDAL;
using CruiseDesign.Historical_setup;
using CruiseDesign.Services;
using CruiseDesign.Strata_setup;
using CruiseDesign.Test.DatabaseUtil;
using FluentAssertions;
using NSubstitute;
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
    public class CruiseDesignMain_Test : TestBase
    {
        public CruiseDesignMain_Test(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void CreateNewFileFromHistorical()
        {
            var fileContextProvider = new CruiseDesignFileContextProvider();

            var logger = MakeMockLogger();
            var dialogService = MakeMockDialogService();
            // setup dialog service
            dialogService.ShowDialog<SaleSetupPage>().Returns(System.Windows.Forms.DialogResult.OK);

            var designPath = GetTempFilePath(nameof(CreateNewFileFromHistorical) + ".design");
            if (File.Exists(designPath)) { File.Delete(designPath); }

            CruiseDesignMain.CreateNewFileFromHistorical(designPath, fileContextProvider, logger, dialogService);

            // verify SaleSetupPage Shown
            dialogService.Received().ShowDialog<SaleSetupPage>();

            File.Exists(designPath).Should().BeTrue();

            var fileContext = fileContextProvider.CurrentFileContext;
            fileContext.Should().NotBeNull();
            fileContext.DesignDb.Should().NotBeNull();
            fileContext.DesignFilePath.Should().Be(designPath);
        }

        [Fact]
        public void CreateNewFromRecon_UsingV2_NoDesignFile()
        {
            var fileContextProvider = new CruiseDesignFileContextProvider();

            var logger = MakeMockLogger();
            var dialogService = MakeMockDialogService();
            // setup dialog service
            dialogService.ShowDialog<Working>().Returns(System.Windows.Forms.DialogResult.OK);

            var reconFilePath = GetTempFilePath(nameof(CreateNewFromRecon_UsingV2_NoDesignFile) + ".cruise");
            var designFilePath = GetTempFilePath(nameof(CreateNewFromRecon_UsingV2_NoDesignFile) + ".design");
            if (File.Exists(reconFilePath)) { File.Delete(reconFilePath); }
            if (File.Exists(designFilePath)) { File.Delete(designFilePath); }


            var reconInit = new DatabaseInitializer_V2();

            var reconDb = reconInit.CreateDatabaseFile(reconFilePath);

            CruiseDesignMain.CreateNewFromRecon(reconFilePath, fileContextProvider, logger, dialogService);

            // verify SaleSetupPage Shown
            //dialogService.Received().ShowDialog<Working>();

            File.Exists(reconFilePath).Should().BeTrue();
            File.Exists(designFilePath).Should().BeTrue();

            var fileContext = fileContextProvider.CurrentFileContext;
            fileContext.Should().NotBeNull();
            fileContext.ReconFilePath.Should().Be(reconFilePath);
            fileContext.DesignDb.Should().NotBeNull();
        }

        [Fact]
        public void CreateNewFromRecon_UsingV2_ExistingDesignFile()
        {
            var fileContextProvider = new CruiseDesignFileContextProvider();

            var logger = MakeMockLogger();
            var dialogService = MakeMockDialogService();
            // setup dialog service
            dialogService.ShowDialog<Working>().Returns(System.Windows.Forms.DialogResult.OK);

            var reconFilePath = GetTempFilePath(nameof(CreateNewFromRecon_UsingV2_ExistingDesignFile) + ".cruise");
            var designFilePath = GetTempFilePath(nameof(CreateNewFromRecon_UsingV2_ExistingDesignFile) + ".design");
            if (File.Exists(reconFilePath)) { File.Delete(reconFilePath); }
            if (File.Exists(designFilePath)) { File.Delete(designFilePath); }

            var reconInit = new DatabaseInitializer_V2();

            var reconDb = reconInit.CreateDatabaseFile(reconFilePath);
            var designDb = new DAL(designFilePath, true);

            CruiseDesignMain.CreateNewFromRecon(reconFilePath, fileContextProvider, logger, dialogService);

            File.Exists(reconFilePath).Should().BeTrue();
            File.Exists(designFilePath).Should().BeTrue();

            var fileContext = fileContextProvider.CurrentFileContext;
            fileContext.Should().NotBeNull();
            fileContext.ReconFilePath.Should().Be(reconFilePath);
            fileContext.DesignDb.Should().NotBeNull();
        }

        [Fact]
        public void CreateNewFromRecon_UsingV3_NoDesignFile()
        {
            var fileContextProvider = new CruiseDesignFileContextProvider();

            var logger = MakeMockLogger();
            var dialogService = MakeMockDialogService();
            // setup dialog service
            dialogService.ShowDialog<Working>().Returns(System.Windows.Forms.DialogResult.OK);

            var reconFilePath = GetTempFilePath(nameof(CreateNewFromRecon_UsingV3_NoDesignFile) + ".crz3");
            var designFilePath = GetTempFilePath(nameof(CreateNewFromRecon_UsingV3_NoDesignFile) + ".design");
            var processFilePath = GetTempFilePath(nameof(CreateNewFromRecon_UsingV3_NoDesignFile) + ".process");
            if (File.Exists(reconFilePath)) { File.Delete(reconFilePath); }
            if (File.Exists(designFilePath)) { File.Delete(designFilePath); }
            if (File.Exists(processFilePath)) { File.Delete(processFilePath); }


            var reconInit = new DatabaseInitializer_V3();
            var reconDb = reconInit.CreateDatabaseFile(reconFilePath);

            var processDb = new DAL(processFilePath, true);

            new DownMigrator().MigrateFromV3ToV2(reconInit.CruiseID, reconDb, processDb);
            
            CruiseDesignMain.CreateNewFromRecon(reconFilePath, fileContextProvider, logger, dialogService);

            // verify SaleSetupPage Shown
            //dialogService.Received().ShowDialog<Working>();

            File.Exists(reconFilePath).Should().BeTrue();
            File.Exists(designFilePath).Should().BeTrue();
            File.Exists(processFilePath).Should().BeTrue();

            var fileContext = fileContextProvider.CurrentFileContext;
            fileContext.Should().NotBeNull();
            fileContext.ReconFilePath.Should().Be(processFilePath);
            fileContext.DesignDb.Should().NotBeNull();
            fileContext.V3FilePath.Should().Be(reconFilePath);
        }

    }
}
