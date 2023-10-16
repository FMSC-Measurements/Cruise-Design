using CruiseDAL.V3.Sync.Syncers;
using CruiseDesign.Design_Pages;
using CruiseDesign.Historical_setup;
using CruiseDesign.ProductionDesign;
using CruiseDesign.Reports;
using CruiseDesign.Services;
using CruiseDesign.Services.Logging;
using CruiseDesign.Stats;
using CruiseDesign.Strata_setup;
using FMSC.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CruiseDesign
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            

#if !DEBUG
            Microsoft.AppCenter.AppCenter.Start(Secrets.CRUISEDESIGN_APPCENTER_KEY_WINDOWS,
                               typeof(Microsoft.AppCenter.Analytics.Analytics), typeof(Microsoft.AppCenter.Crashes.Crashes));
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .ConfigureLogging(ConfigureLogging).Build();
            ServiceProvider = host.Services;

            Application.Run(new CruiseDesignMain(args));
        }

        private static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder builder)
        {
            builder.AddAppCenterLogger();
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            //example
            //services.AddTransient<IMyService, MyService>();
            //services.AddTransient<MyForm>();

            // register all forms
            services.AddTransient<CreateProduction>();
            services.AddTransient<DesignMain>();
            services.AddTransient<DesignSaveForm>();
            services.AddTransient<MethodSelect>();
            services.AddTransient<Processing>();

            services.AddTransient<SaleSetupPage>();

            services.AddTransient<ProductionDesignMain>();
            services.AddTransient<ReportAdditional>();
            services.AddTransient<ReportForm>();
            services.AddTransient<ReportViewer>();

            services.AddTransient<userSelectStratum>();

            services.AddTransient<TDV_Select>();
            services.AddTransient<Working>();

            services.AddTransient<TestBox1>();
            services.AddTransient<CostSetupForm>();
            services.AddTransient<CruiseDesignMain>();
            services.AddTransient<HistoricalSetupWizard>();

            services.AddTransient<StrataSetupWizard>();
            services.AddTransient<WaitForm>();

            // register other services
            services.AddSingleton<IDialogService, DialogService>();

        }

    }
}
