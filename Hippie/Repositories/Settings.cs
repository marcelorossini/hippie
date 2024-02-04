using Hippie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace Hippie.Repositories
{
    public static class Settings
    {
        private static string _configFileName = "config.json";
        private static string _configFileNamePath = Path.Combine(Directory.GetCurrentDirectory(), _configFileName);
        public static Setting Current = new Setting();

        public static Setting Read()
        {
            if (Current.DefaultDir == null)
            {
                if (File.Exists(_configFileNamePath))
                {
                    string json = File.ReadAllText(_configFileNamePath);
                    Current = JsonConvert.DeserializeObject<Setting>(json);
                    return Current;
                }
            }            
            return new Setting();
        }

        public static void Save(Setting setting)
        {
            Current = setting;
            string json = JsonConvert.SerializeObject(setting);
            File.WriteAllText(_configFileNamePath, json);
        }
    }
}
