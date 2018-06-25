using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MediatrExercise.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddDefaultConfigurationFiles(this IConfigurationBuilder builder, string applicationPath)
        {
            var appData = Path.Combine(applicationPath, "App_Data");
            var appSettings = Path.Combine(appData, "appsettings.json");
            var appSettingsDebug = Path.Combine(appData, "appsettings.debug.json");
            var routes = Path.Combine(appData, "routes.json");
            return builder
                .AddJsonFile(appSettings, optional: false, reloadOnChange: true)
                .AddJsonFileIfDebug(appSettingsDebug, optional: true, reloadOnChange: true)
                .AddJsonFile(routes, optional: false, reloadOnChange: false);
        }

        private static IConfigurationBuilder AddJsonFileIfDebug(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
#if DEBUG
            return builder.AddJsonFile(path, optional, reloadOnChange);
#else
            return builder;
#endif
        }
    }
}