using CruiseDAL;
using CruiseDAL.V2.Models;
using CruiseDesign.Design_Pages;
using FluentAssertions;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

//using CruiseDAL.V3.Models;

namespace CruiseDesign.Test.Design_Pages
{
    public class CreateProduction_Test : TestBase
    {
        public CreateProduction_Test(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void CreateProductionFiles_Test()
        {
            var cruisePath = GetTempFilePath("CreateProduction_Cruise.cruise");

            var init = new DatabaseInitializer_V2();
            using var cruiseDb = init.CreateDatabaseFile(cruisePath);

            var prodFilePath = GetTempFilePath("CreateProduction_Prod.cruise");
            var stCodes = cruiseDb
               .From<Stratum>()
               .Query()
               .Select(x => x.Code).ToArray();

            var dataFiles = new CreateProduction.DataFiles
            {
                CruiseDesignDb = cruiseDb,
                ProductionFilePath = prodFilePath,
                SelectedStratumCodes = stCodes,
            };

            if (File.Exists(prodFilePath)) File.Delete(prodFilePath);

            CreateProduction.CreateProductionFile(dataFiles, true, false);

            File.Exists(prodFilePath).Should().BeTrue();
        }

        [Fact]
        public void CreateProductionFiles_Test_V3()
        {
            var cruisePath = GetTempFilePath("CreateProduction_Test_V3_Cruise.cruise");

            var init = new DatabaseInitializer_V2();
            using var cruiseDb = init.CreateDatabaseFile(cruisePath);

            var prodFilePath = GetTempFilePath("CreateProduction_Test_V3_Prod.crz3");
            var stCodes = cruiseDb
               .From<Stratum>()
               .Query()
               .Select(x => x.Code).ToArray();

            var dataFiles = new CreateProduction.DataFiles
            {
                CruiseDesignDb = cruiseDb,
                ProductionFilePath = prodFilePath,
                SelectedStratumCodes = stCodes,
            };

            if (File.Exists(prodFilePath)) File.Delete(prodFilePath);

            CreateProduction.CreateProductionFile(dataFiles, true, false);

            File.Exists(prodFilePath).Should().BeTrue();
        }

        [Fact]
        public void CreateProductionFiles_V3_ShouldFail()
        {
            var cruisePath = GetTempFilePath("CreateProductionFiles_V3_ShouldFail.cruise");

            var init = new DatabaseInitializer_V2();
            using var cruiseDb = init.CreateDatabaseFile(cruisePath);

            var stCodes = cruiseDb
               .From<Stratum>()
               .Query()
               .Select(x => x.Code).ToArray();

            cruiseDb.Execute("UPDATE TreeDefaultValue SET TreeGrade = 0.0;");

            var prodFilePath = GetTempFilePath("CreateProductionFiles_V3_ShouldFail.crz3");

            var dataFiles = new CreateProduction.DataFiles
            {
                CruiseDesignDb = cruiseDb,
                ProductionFilePath = prodFilePath,
                SelectedStratumCodes = stCodes,
            };

            if (File.Exists(prodFilePath)) File.Delete(prodFilePath);

            CreateProduction.CreateProductionFile(dataFiles, true, false);

            File.Exists(prodFilePath).Should().BeFalse();
        }

        [Fact]
        public void CreateProductionFiles_TestMeth()
        {
            var designPath = GetTestFile("99996_TestMeth_Timber_Sale_08072021.design");
            var reconPath = GetTestFile("99996_TestMeth_TS.cruise");

            using var designDb = new DAL(designPath);
            //using var reconDb = new

            var prodFilePath = GetTempFilePath("CreateProductionFiles_TestMeth_Prod.cruise");
            var stCodes = designDb
               .From<Stratum>()
               .Query()
               .Select(x => x.Code).ToArray();

            var dataFiles = new CreateProduction.DataFiles
            {
                CruiseDesignDb = designDb,
                ProductionFilePath = prodFilePath,
                SelectedStratumCodes = stCodes,
                ReconFilePath = reconPath,
                HasReconData = true,
            };

            if (File.Exists(prodFilePath)) File.Delete(prodFilePath);

            CreateProduction.CreateProductionFile(dataFiles, true, false);

            File.Exists(prodFilePath).Should().BeTrue();
        }

        [Fact]
        public void CreateProductionFiles_TestMeth_V3()
        {
            var designPath = GetTestFile("99996_TestMeth_Timber_Sale_08072021.design");
            var reconPath = GetTestFile("99996_TestMeth_TS.cruise");

            using var designDb = new DAL(designPath);
            //using var reconDb = new

            var prodFilePath = GetTempFilePath("CreateProductionFiles_TestMeth_Prod.crz3");
            var stCodes = designDb
               .From<Stratum>()
               .Query()
               .Select(x => x.Code).ToArray();

            var dataFiles = new CreateProduction.DataFiles
            {
                CruiseDesignDb = designDb,
                ProductionFilePath = prodFilePath,
                SelectedStratumCodes = stCodes,
                ReconFilePath = reconPath,
                HasReconData = true,
            };

            if (File.Exists(prodFilePath)) File.Delete(prodFilePath);

            CreateProduction.CreateProductionFile(dataFiles, true, false);

            File.Exists(prodFilePath).Should().BeTrue();
        }
    }
}