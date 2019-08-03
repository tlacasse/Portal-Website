using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Portal.Data.Web {

    public class WebsiteState : IWebsiteState {

        private Dictionary<Setting, string> Settings { get; }

        private string SettingsPath { get; }

        public WebsiteState(IReadOnlyDictionary<Setting, string> defaults) {
            SettingsPath = GetPath("Data/settings.json");
            if (!File.Exists(SettingsPath)) {
                Settings = new Dictionary<Setting, string>();
            } else {
                string json = File.ReadAllText(SettingsPath);
                Settings = JsonConvert.DeserializeObject<Dictionary<Setting, string>>(json);
            }
            SetSettingDefaultsIfNeeded(defaults);
            SaveSettings();
        }

        public string WebsitePath => HttpContext.Current.Server.MapPath("~");

        public string GetPath(string relativePath) {
            return Path.Combine(WebsitePath, relativePath);
        }

        public string GetSetting(Setting name) {
            return Settings[name];
        }

        public int GetSettingInt(Setting name) {
            return int.Parse(Settings[name]);
        }

        public void SetSetting(Setting name, object value) {
            if (name.IsReadonlySetting()) {
                throw new InvalidOperationException(name + " is read-only.");
            }
            Settings[name] = value.ToString();
            SaveSettings();
        }

        private void SaveSettings() {
            string json = JsonConvert.SerializeObject(Settings);
            File.WriteAllText(SettingsPath, json);
        }

        private void SetSettingDefaultsIfNeeded(IReadOnlyDictionary<Setting, string> defaults) {
            SetIfNeeded(Setting.PortalGridCurrentWidth, defaults);
            SetIfNeeded(Setting.PortalGridCurrentHeight, defaults);
        }

        private void SetIfNeeded(Setting setting, IReadOnlyDictionary<Setting, string> defaults) {
            if (!Settings.ContainsKey(setting)) {
                Settings.Add(setting, defaults[setting]);
            }
        }

    }

}
