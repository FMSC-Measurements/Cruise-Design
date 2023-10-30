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
