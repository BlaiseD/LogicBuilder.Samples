﻿using Contoso.XPlatform.Flow;
using Foundation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Contoso.XPlatform
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAppLogger, AppLogger>();
        }

        protected override MauiApp CreateMauiApp()
        {
            MauiApp mauiApp = MauiProgram.CreateMauiApp();
            MauiProgram.Init(ConfigureServices);
            return mauiApp;
        }
    }
}