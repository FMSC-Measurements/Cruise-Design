using CruiseDAL;
using CruiseDAL.DataObjects;
using CruiseDesign.Stats;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruiseDesign.Services
{
    public class CruiseDesignFileContext : IDisposable
    {
        public string ReconFilePath { get; set; }
        public string DesignFilePath { get; set; }
        public string ProcessFilePath { get; set; }

        public string V3FilePath { get; set; }

        public bool CanCreateNew { get; set; }
        public bool IsProductionFile { get; set; }

        public bool DoesReconExist => !string.IsNullOrEmpty(ReconFilePath);

        public bool IsUsingV3File => !string.IsNullOrEmpty(V3FilePath);


        public DAL DesignDb { get; set; }

        public bool SetProcessFilePathFromV3Cruise(string cruisePath)
        {
            var processFilePath = Path.ChangeExtension(cruisePath, ".process");
            if(File.Exists(processFilePath))
            {
                ProcessFilePath = processFilePath;
                return true;
            }
            return false;
        }

        public bool SetReconFilePathFromDesign()
        {
            return SetReconFilePathFromDesign(DesignFilePath);
        }

        public bool SetReconFilePathFromDesign(string designPath)
        {
            var reconPathV2 = Path.ChangeExtension(designPath, ".cruise");
            if(File.Exists(reconPathV2))
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

        public bool SetDesignFilePathFromRecon()
        {
            var reconPath = ReconFilePath;

            return SetDesignFilePathFromRecon(reconPath);
        }

        protected bool SetDesignFilePathFromRecon(string reconPath)
        {
            var designPath = Path.ChangeExtension(reconPath, ".design");
            if(File.Exists(designPath))
            {
                DesignFilePath = designPath;
                return true;
            }
            return false;
        }

        

        public bool OpenDesignFile(ILogger logger, bool? canCreateNew = null)
        {
            try
            {
                DesignDb = new DAL(DesignFilePath, canCreateNew ?? CanCreateNew);
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

        public bool OpenDesignFile(string designPath, ILogger logger, bool? canCreateNew = null)
        {
            DesignFilePath = designPath;
            SetReconFilePathFromDesign(designPath);

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
            if(DesignDb != null)
            {
                DesignDb.Dispose();
                DesignDb = null;
            }
        }
    }

    public class CruiseDesignFileContextProvider : ICruiseDesignFileContextProvider
    {
        public event EventHandler FileContextChanged;

        CruiseDesignFileContext _currentFileContext;

        public CruiseDesignFileContext CurrentFileContext
        {
            get => _currentFileContext;
            set
            {
                if(_currentFileContext != null)
                {
                    _currentFileContext.Dispose();
                }

                _currentFileContext = value;
                FileContextChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public interface ICruiseDesignFileContextProvider
    {
        CruiseDesignFileContext CurrentFileContext { get; set; }

        event EventHandler FileContextChanged;
    }
}
