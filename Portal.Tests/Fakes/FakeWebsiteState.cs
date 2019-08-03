﻿using Portal.Data.Web;

namespace Portal.Tests.Fakes {

    public class FakeWebsiteState : IWebsiteState {

        public string WebsitePath => "website";

        public string GetPath(string relativePath) {
            return WebsitePath + "/" + relativePath;
        }

        public string GetSetting(Setting name) {
            throw new System.NotImplementedException();
        }

        public int GetSettingInt(Setting name) {
            throw new System.NotImplementedException();
        }

        public void SetSetting(Setting name, object value) {
            throw new System.NotImplementedException();
        }

    }

}
