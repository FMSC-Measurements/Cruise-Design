using CruiseDAL;
using CruiseDAL.DataObjects;
using CruiseDesign.Stats;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CruiseDesign.Services
{
    public class CruiseDesignFileContext : IDisposable
    {
        public string ReconFilePath { get; set; }
        public string DesignFilePath { get; set; }
        public string ProcessFilePath { get; set; }

        public string V3FilePath { get; set; }

        public bool IsProductionFile { get; set; }

        public bool DoesReconExist => !string.IsNullOrEmpty(ReconFilePath);

        public bool IsUsingV3File => !string.IsNullOrEmpty(V3FilePath);

        public DAL DesignDb { get; set; }

        public bool SetProcessFilePathFromV3Cruise()
        {
            return SetProcessFilePathFromV3Cruise(V3FilePath);
        }

        public bool SetProcessFilePathFromV3Cruise(string cruisePath)
        {
            var processFilePath = GetProcessFilePathFromV3Cruise(cruisePath);
            if (File.Exists(processFilePath))
            {
                ProcessFilePath = processFilePath;
                return true;
            }
            return false;
        }

        public static string GetProcessFilePathFromV3Cruise(string cruisePath)
        {
            return Path.ChangeExtension(cruisePath, ".process");
        }

        public bool SetReconFilePathFromDesign()
        {
            return SetReconFilePathFromDesign(DesignFilePath);
        }

        public bool SetReconFilePathFromDesign(string designPath)
        {
            var reconPathV2 = Path.ChangeExtension(designPath, ".cruise");
            if (File.Exists(reconPathV2))
            {
                ReconFilePath = reconPathV2;
                return true;
            }

            var reconPathProcess = Path.ChangeExtension(designPath, ".process");
            if (File.Exists(reconPathProcess))
            {
                ReconFilePath = reconPathProcess;
                return true;
            }

            return false;
        }

        public bool SetDesignFilePathFromRecon(bool setIfNotExists = false)
        {
            return SetDesignFilePathFromRecon(ReconFilePath, setIfNotExists);
        }

        protected bool SetDesignFilePathFromRecon(string reconPath, bool setIfNotExists = false)
        {
            var designPath = GetDesignFilePathFromRecon(reconPath);
            var designExists = File.Exists(designPath);
            if (designExists || setIfNotExists)
            {
                DesignFilePath = designPath;
            }
            return designExists;
        }

        public static string GetDesignFilePathFromRecon(string reconPath)
        {
            return Path.ChangeExtension(reconPath, ".design");
        }

        public bool SetV3FilePathFromDesignFilePath()
        {
            var designFilePath = DesignFilePath;
            var v3FilePath = Path.ChangeExtension(designFilePath, ".crz3");
            if(File.Exists(v3FilePath))
            {
                V3FilePath = v3FilePath;
                return true;
            }
            return false;
        }

        public bool OpenDesignFile(ILogger logger, bool canCreateNew = false)
        {
            try
            {
                DesignDb = new DAL(DesignFilePath, canCreateNew);
                return true;
            }
            catch (System.IO.IOException e1)
            {
                logger.LogError(e1, "unable to open design file: IOException");
                return (false);
            }
            catch (System.Exception e1)
            {
                logger.LogError(e1, "unable to open design file: Exception");
                return (false);
            }
        }

        public bool OpenDesignFile(string designPath, ILogger logger, bool canCreateNew = false)
        {
            DesignFilePath = designPath;
            return OpenDesignFile(logger, canCreateNew);
        }

        public bool CheckIsDesignFileProcessed()
        {
            return CheckIsDesignFileProcessed(DesignDb);
        }

        public static bool CheckIsDesignFileProcessed(DAL db)
        {
            // if NULL, return false
            if (db.From<LCDDO>().Count() == 0)
                return (false);

            // else, return true
            return (true);
        }

        public Task<bool> ProcessFileAsync()
        {
            return Task.Run(() => ProcessFile(this));
        }

        public bool ProcessFile()
        {
            return ProcessFile(this);
        }

        public static bool ProcessFile(CruiseDesignFileContext fileContext)
        {
            DAL designDb = fileContext.DesignDb;
            string reconFilePath = fileContext.ReconFilePath;
            bool isProdFile = fileContext.IsProductionFile;
            return ProcessFile(designDb, reconFilePath, isProdFile);
        }

        private static bool ProcessFile(DAL designDb, string reconFilePath, bool isProdFile)
        {
            var err = 0;
            if (isProdFile)
            {
                getProductionStatistics getProd = new getProductionStatistics();
                getProd.getProductionStats(designDb, err);
            }
            else
            {
                getPopulationStatistics getStats = new getPopulationStatistics();
                getStats.getPopulationStats(reconFilePath, designDb, reconFilePath != null, err);
            }

            return err == 0;
        }



        public void Dispose()
        {
            if (DesignDb != null)
            {
                DesignDb.Dispose();
                DesignDb = null;
            }
        }
    }
}