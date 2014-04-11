using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Dynamic;
using Newtonsoft.Json;

namespace RealEstate.Models
{
    public class AppSettings
    {
        private static readonly IDictionary<string,object> Settings = new Dictionary<string, object>();

        static AppSettings ()
        {
            var appSettingsKeys = ConfigurationManager.AppSettings.AllKeys;

            foreach (var appSettingsKey in appSettingsKeys)
            {
                Settings[appSettingsKey] = ConfigurationManager.AppSettings[appSettingsKey];
            }
        }

        public string ToJSON()
        {
            var settings = JsonConvert.SerializeObject(Settings);
            Debug.WriteLine(settings);
            return settings;
        }
    }
}