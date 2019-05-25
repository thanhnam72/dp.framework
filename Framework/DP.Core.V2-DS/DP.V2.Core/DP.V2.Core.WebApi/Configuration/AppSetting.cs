using DP.V2.Core.WebApi.Dependencies;
using Microsoft.Extensions.Configuration;
using System;

namespace DP.V2.Core.WebApi.Configuration
{
    public static class AppSetting
    {
        public static string GetString(string key)
        {
            try
            {
                IConfiguration config = DependencyProvider.Resolve<IConfiguration>();
                return config.GetSection("AppSetting").GetSection(key).Value;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public static T GetObject<T>(string key)
        {
            IConfiguration config = DependencyProvider.Resolve<IConfiguration>();
            return (T)config.GetSection("AppSetting").GetValue<T>(key);
        }
    }
}
