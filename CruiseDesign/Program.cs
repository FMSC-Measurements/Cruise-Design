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
using System.Windows.Forms;

namespace CruiseDesign
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
#if !DEBUG
            Microsoft.AppCenter.AppCenter.Start(Secrets.CRUISEDESIGN_APPCENTER_KEY_WINDOWS,
                               typeof(Microsoft.AppCenter.Analytics.Analytics), typeof(Microsoft.AppCenter.Crashes.Crashes));
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var windowProvider = new WindowProvider();
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => ConfigureServices(context, services, windowProvider))
                .ConfigureLogging(ConfigureLogging).Build();
            ServiceProvider = host.Services;

            //if (args.Length != 0)
            //{
            //    var fileContextProvider = ServiceProvider.GetRequiredService<ICruiseDesignFileContextProvider>();
            //    var newFileContext = new CruiseDesignFileContext()
            //    { DesignFilePath = Convert.ToString(args[0]) };

            //    newFileContext.SetReconFilePathFromDesign();

            //    if (newFileContext.OpenDesignFile(ServiceProvider.GetRequiredService<ILogger>()))
            //    {
            //        fileContextProvider.CurrentFileContext = newFileContext;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Unable to open the design file", "Information");
            //    }
            //}

            windowProvider.MainWindow = new CruiseDesignMain(args,
                ServiceProvider,
                ServiceProvider.GetRequiredService<ILogger<CruiseDesignMain>>(),
                ServiceProvider.GetRequiredService<ICruiseDesignFileContextProvider>(),
                ServiceProvider.GetRequiredService<IDialogService>());

            Application.Run(windowProvider.MainWindow);
        }

        private static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder builder)
        {
            builder.AddAppCenterLogger();
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services, WindowProvider windowProvider)
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
            services.AddSingleton<ICruiseDesignFileContextProvider, CruiseDesignFileContextProvider>();
            services.AddSingleton<IWindowProvider>(windowProvider);
        }
    }
}