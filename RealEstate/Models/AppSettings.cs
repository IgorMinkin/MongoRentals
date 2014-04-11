using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Dynamic;
using Newtonsoft.Json;

namespace RealEstate.Models
{
    public class AppSettings
    {
        private static readonly Lazy<IDictionary<string,object>> Settings = new Lazy<IDictionary<string, object>>(ReadSettings);

        private static IDictionary<string, object> ReadSettings()
        {
            var settings = new Dictionary<string, object>();
            var appSettingsKeys = ConfigurationManager.AppSettings.AllKeys;

            foreach (var appSettingsKey in appSettingsKeys)
            {
                settings[appSettingsKey] = ConfigurationManager.AppSettings[appSettingsKey];
            }

            return settings;
        } 

        public string ToJson()
        {
            return JsonConvert.SerializeObject(Settings.Value);
        }
    }
}