using CruiseDAL;
using CruiseDAL.DataObjects;
using CruiseDAL.V3.Models;
using CruiseDesign.Design_Pages;
using CruiseDesign.Services;
using FluentAssertions;
using NSubstitute;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

using ILogger = Microsoft.Extensions.Logging.ILogger;
using Stratum = CruiseDAL.V2.Models.Stratum;
using LogMatrix = CruiseDAL.V2.Models.LogMatrix;
using CruiseDesign.Test.DatabaseUtil;

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
            var mockDialogService = Substitute.For<IDialogService>();
            mockDialogService.AskSelectCreateProductionV3Template(Arg.Any<string>());
            var logger = Substitute.For<ILogger>();

            var cruisePath = GetTempFilePath("CreateProduction_Cruise.cruise");

            var init = new DatabaseInitializer_V2();
            using var cruiseDb = init.CreateDatabaseFile(cruisePath);

            var lm = new LogMatrix { ReportNumber = "1234", SEDmaximum = 2.2, SEDminimum = 1.1 };
            cruiseDb.Insert(lm);

            var prodFilePath = GetTempFilePath("CreateProduction_Prod.cruise");
            var stCodes = cruiseDb
               .From<Stratum>()
               .Query()
               .Select(x => x.Code).ToArray();

            var dataFiles = new CreateProduction.CreateProdParams
            {
                ProductionFilePath = prodFilePath,
                SelectedStratumCodes = stCodes,
            };

            var fileContext = new CruiseDesignFileContext
            {
                DesignDb = cruiseDb,
            };

            if (File.Exists(prodFilePath)) File.Delete(prodFilePath);

            CreateProduction.CreateProductionFile(dataFiles, fileContext, true, false, mockDialogService, logger);

            File.Exists(prodFilePath).Should().BeTrue();

            var prodDb = new DAL(prodFilePath);

            var lmAgain = prodDb.From<LogMatrix>().Query().Single();
            lmAgain.Should().BeEquivalentTo(lm);
        }

        [Fact]
        public void CreateProductionFiles_Test_V3()
        {
            var mockDialogService = Substitute.For<IDialogService>();
            mockDialogService.AskSelectCreateProductionV3Template(Arg.Any<string>());
            var logger = Substitute.For<ILogger>();

            var cruisePath = GetTempFilePath("CreateProduction_Test_V3_Cruise.cruise");

            var init = new DatabaseInitializer_V2();
            using var cruiseDb = init.CreateDatabaseFile(cruisePath);

            var prodFilePath = GetTempFilePath("CreateProduction_Test_V3_Prod.crz3");
            var stCodes = cruiseDb
               .From<Stratum>()
               .Query()
               .Select(x => x.Code).ToArray();

            var dataFiles = new CreateProduction.CreateProdParams
            {
                ProductionFilePath = prodFilePath,
                SelectedStratumCodes = stCodes,
            };

            var fileContext = new CruiseDesignFileContext
            {
                DesignDb = cruiseDb,
            };

            if (File.Exists(prodFilePath)) File.Delete(prodFilePath);

            CreateProduction.CreateProductionFile(dataFiles, fileContext, true, false, mockDialogService, logger);

            File.Exists(prodFilePath).Should().BeTrue();

            using var prodDb = new CruiseDatastore_V3(prodFilePath);
            VerifyProdFile(prodDb);
        }

        [Fact]
        public void CreateProductionFiles_V3_ShouldFail()
        {
            var mockDialogService = Substitute.For<IDialogService>();
            mockDialogService.AskSelectCreateProductionV3Template(Arg.Any<string>());
            var logger = Substitute.For<ILogger>();

            var cruisePath = GetTempFilePath("CreateProductionFiles_V3_ShouldFail.cruise");

            var init = new DatabaseInitializer_V2();
            using var cruiseDb = init.CreateDatabaseFile(cruisePath);

            var stCodes = cruiseDb
               .From<Stratum>()
               .Query()
               .Select(x => x.Code).ToArray();

            cruiseDb.Execute("UPDATE TreeDefaultValue SET TreeGrade = 0.0;");

            var prodFilePath = GetTempFilePath("CreateProductionFiles_V3_ShouldFail.crz3");

            var dataFiles = new CreateProduction.CreateProdParams
            {
                ProductionFilePath = prodFilePath,
                SelectedStratumCodes = stCodes,
            };

            var fileContext = new CruiseDesignFileContext
            {
                DesignDb = cruiseDb,
            };

            if (File.Exists(prodFilePath)) File.Delete(prodFilePath);

            CreateProduction.CreateProductionFile(dataFiles, fileContext, true, false, mockDialogService, logger);

            File.Exists(prodFilePath).Should().BeFalse();
        }

        [Fact]
        public void CreateProductionFiles_TestMeth()
        {
            var mockDialogService = Substitute.For<IDialogService>();
            mockDialogService.AskSelectCreateProductionV3Template(Arg.Any<string>());
            var logger = Substitute.For<ILogger>();

            var designPath = GetTestFile("99996_TestMeth_Timber_Sale_08072021.design");
            var reconPath = GetTestFile("99996_TestMeth_TS.cruise");

            using var designDb = new DAL(designPath);
            //using var reconDb = new

            var prodFilePath = GetTempFilePath("CreateProductionFiles_TestMeth_Prod.cruise");
            var stCodes = designDb
               .From<Stratum>()
               .Query()
               .Select(x => x.Code).ToArray();

            var dataFiles = new CreateProduction.CreateProdParams
            {
                ProductionFilePath = prodFilePath,
                SelectedStratumCodes = stCodes,
            };

            var fileContext = new CruiseDesignFileContext
            {
                DesignDb = designDb,
                ReconFilePath= reconPath,
            };

            if (File.Exists(prodFilePath)) File.Delete(prodFilePath);

            CreateProduction.CreateProductionFile(dataFiles, fileContext, true, false, mockDialogService, logger);

            File.Exists(prodFilePath).Should().BeTrue();

            //using var prodDb = new CruiseDatastore_V3(prodFilePath);
            //VerifyProdFile(prodDb);
        }

        [Fact]
        public void CreateProductionFiles_TestMeth_V3()
        {
            var mockDialogService = Substitute.For<IDialogService>();
            mockDialogService.AskSelectCreateProductionV3Template(Arg.Any<string>());

            var logger = Substitute.For<ILogger>();

            var designPath = GetTestFile("99996_TestMeth_Timber_Sale_08072021.design");
            var reconPath = GetTestFile("99996_TestMeth_TS.cruise");

            using var designDb = new DAL(designPath);
            //using var reconDb = new

            var prodFilePath = GetTempFilePath("CreateProductionFiles_TestMeth_Prod.crz3");
            var stCodes = designDb
               .From<Stratum>()
               .Query()
               .Select(x => x.Code).ToArray();

            var dataFiles = new CreateProduction.CreateProdParams
            {
                ProductionFilePath = prodFilePath,
                SelectedStratumCodes = stCodes,
            };

            var fileContext = new CruiseDesignFileContext
            {
                DesignDb = designDb,
                ReconFilePath = reconPath,
            };

            if (File.Exists(prodFilePath)) File.Delete(prodFilePath);

            CreateProduction.CreateProductionFile(dataFiles, fileContext, true, false, mockDialogService, logger);

            File.Exists(prodFilePath).Should().BeTrue();

            using var prodDb = new CruiseDatastore_V3(prodFilePath);
            VerifyProdFile(prodDb);
        }


        [Fact]
        public void CreateProductionFile_V3_WithV3TemplateFile()
        {
            var v3TemplatePath = GetTestFile("R2 Template 2017.02.28.crz3");

            var mockDialogService = Substitute.For<IDialogService>();
            mockDialogService.AskSelectCreateProductionV3Template(Arg.Any<string>())
                .Returns(v3TemplatePath);

            var mockLogger = Substitute.For<ILogger>();


            var designPath = GetTestFile("99996_TestMeth_Timber_Sale_08072021.design");
            var reconPath = GetTestFile("99996_TestMeth_TS.cruise");

            using var designDb = new DAL(designPath);

            var prodFilePath = GetTempFilePath("CreateProductionFile_V3_WithV3TemplateFile_Prod.crz3");
            var stCodes = designDb
               .From<CruiseDAL.V2.Models.Stratum>()
               .Query()
               .Select(x => x.Code).ToArray();

            var dataFiles = new CreateProduction.CreateProdParams
            {
                ProductionFilePath = prodFilePath,
                SelectedStratumCodes = stCodes,
            };

            var fileContext = new CruiseDesignFileContext
            {
                DesignDb = designDb,
                ReconFilePath = reconPath,
            };

            if (File.Exists(prodFilePath)) File.Delete(prodFilePath);

            CreateProduction.CreateProductionFile(dataFiles, fileContext, true, false, mockDialogService, mockLogger);

            File.Exists(prodFilePath).Should().BeTrue();

            using var prodDb = new CruiseDatastore_V3(prodFilePath);
            using var templateDb = new CruiseDatastore_V3(v3TemplatePath);
            VerifyTemplateData(prodDb, templateDb);
            VerifyProdFile(prodDb);
        }

        [Fact]
        public void CreateProductionFile_V3_WithV3Template()
        {
            var v3TemplatePath = GetTempFilePath(nameof(CreateProductionFile_V3_WithV3Template) + ".crz3t");
            var templateInit = new TemplateDatabaseInitializer();
            using var templateDb = templateInit.CreateDatabaseFile(v3TemplatePath);

            var mockDialogService = Substitute.For<IDialogService>();
            mockDialogService.AskSelectCreateProductionV3Template(Arg.Any<string>())
                .Returns(v3TemplatePath);

            var mockLogger = Substitute.For<ILogger>();


            var designPath = GetTestFile("99996_TestMeth_Timber_Sale_08072021.design");
            var reconPath = GetTestFile("99996_TestMeth_TS.cruise");

            using var designDb = new DAL(designPath);

            var prodFilePath = GetTempFilePath("CreateProductionFile_V3_WithV3Template_Prod.crz3");
            var stCodes = designDb
               .From<CruiseDAL.V2.Models.Stratum>()
               .Query()
               .Select(x => x.Code).ToArray();

            var dataFiles = new CreateProduction.CreateProdParams
            {
                ProductionFilePath = prodFilePath,
                SelectedStratumCodes = stCodes,
            };

            var fileContext = new CruiseDesignFileContext
            {
                DesignDb = designDb,
                ReconFilePath = reconPath,
            };

            if (File.Exists(prodFilePath)) File.Delete(prodFilePath);

            CreateProduction.CreateProductionFile(dataFiles, fileContext, true, false, mockDialogService, mockLogger);

            File.Exists(prodFilePath).Should().BeTrue();

            using var prodDb = new CruiseDatastore_V3(prodFilePath);
            VerifyTemplateData(prodDb, templateDb);
            VerifyProdFile(prodDb);
        }


        protected void VerifyProdFile(CruiseDatastore_V3 prodDb)
        {
            var result = prodDb.QueryGeneric2("SELECT t.* FROM Tree as t LEFT JOIN TallyLedger AS tl USING (TreeID) where tl.rowid is null", null).ToArray();

            result.Should().BeEmpty(result.Length.ToString());
        }

        protected void VerifyTemplateData(CruiseDatastore_V3 cruiseDb, CruiseDatastore_V3 template)
        {
            var templateTFH = template.From<TreeFieldHeading>().Query();
            foreach(var tfh in templateTFH)
            {
                cruiseDb.From<TreeFieldHeading>().Where("Field = @p1 AND Heading = @p2").Count(tfh.Field, tfh.Heading).Should().Be(1);
            }

            var templateLFH = template.From<LogFieldHeading>().Query();
            foreach(var lfh in templateLFH)
            {
                cruiseDb.From<LogFieldHeading>().Where("Field = @p1 and Heading = @p2").Count(lfh.Field, lfh.Heading).Should().Be(1);
            }

            var stTmlts = template.From<StratumTemplate>().Query();
            foreach (var stt in stTmlts)
            {
                cruiseDb.From<StratumTemplate>().Where("StratumTemplateName = @p1").Count(stt.StratumTemplateName).Should().Be(1);
            }
        }

        [Fact]
        public void Issue_CrashOn_findSgStatCN100()
        {
            var designPath = GetTestFile("JohnneysHazardousFuelReduction_20230313.design");
            var reconPath = GetTestFile("JohnneysHazardousFuelReduction_20230313.cruise");

            using var designDb = new DAL(designPath);

            var prodPath = GetTempFilePath("Issue_CrachOn_findSgStatCN100_Prod.cruise");

            var stCodes = designDb
               .From<Stratum>()
               .Query()
               .Select(x => x.Code).ToArray();

            var dataFiles = new CreateProduction.CreateProdParams
            {
                ProductionFilePath = prodPath,
                SelectedStratumCodes = stCodes,
            };

            var fileContext = new CruiseDesignFileContext
            {
                DesignDb = designDb,
                ReconFilePath = reconPath,
            };

            if (File.Exists(prodPath)) File.Delete(prodPath);

            var cdStratumStats = designDb.From<StratumStatsDO>()
               .Join("Stratum AS s", "USING (Stratum_CN)")
               .Where("StratumStats.Used = 1")
               .OrderBy("s.Code").Query().ToArray();

            foreach (var stStat in cdStratumStats)
            {
                stStat.StratumStats_CN.Should().NotBeNull();
                Output.WriteLine($"StStateCode{stStat.Code} StStat_CN:{stStat.StratumStats_CN}");

                var method = "100";
                if (true)
                {
                    if (stStat.Method == "PNT" || stStat.Method == "PCM")
                        method = "PNT";
                    else if (stStat.Method == "FIX" || stStat.Method == "FCM")
                        method = "FIX";
                    else if (stStat.Method == "FIXCNT")
                        method = "FIXCNT";
                }

                var mySgStats = designDb.From<SampleGroupStatsDO>().Where("StratumStats_CN = @p1").Query(stStat.StratumStats_CN);

                foreach (var sgStats in mySgStats)
                {
                    Output.WriteLine($"SgStatCde{sgStats.Code}");

                    sgStats.Code.Should().NotBeNullOrEmpty();

                    long? sgStatsCN100pct;
                    if (stStat.SgSetDescription == "Comparison Cruise" || method == "FIXCNT")
                        sgStatsCN100pct = CreateProduction.findSgStatCN100(designDb, stStat.Stratum_CN, sgStats.Code, sgStats.SgSet, stStat.Method);
                    else
                        sgStatsCN100pct = CreateProduction.findSgStatCN100(designDb, stStat.Stratum_CN, sgStats.Code, sgStats.SgSet, "100");

                    sgStatsCN100pct.Should().NotBeNull($"St: {stStat.Code} Sg:{sgStats.Code}");
                }


            }

            var mockDialogService = Substitute.For<IDialogService>();
            mockDialogService.AskSelectCreateProductionV3Template(Arg.Any<string>());
            var logger = Substitute.For<ILogger>();
            CreateProduction.CreateProductionFile(dataFiles, fileContext, true, false, mockDialogService, logger);
        }
    }
}