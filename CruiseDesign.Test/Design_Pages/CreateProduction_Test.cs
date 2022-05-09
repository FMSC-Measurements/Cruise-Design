using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using CruiseDesign.Design_Pages;
using CruiseDAL.V2.Models;
using CruiseDAL;

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

         CreateProduction.CreateProductionFile(dataFiles, true, false);


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

         CreateProduction.CreateProductionFile(dataFiles, true, false);


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

         CreateProduction.CreateProductionFile(dataFiles, true, false);


      }
   }
}
