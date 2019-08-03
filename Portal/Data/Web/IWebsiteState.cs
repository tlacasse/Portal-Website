
namespace Portal.Data.Web {

    public interface IWebsiteState {

        string WebsitePath { get; }

        string GetPath(string relativePath);

        void SetSetting(Setting name, object value);

        string GetSetting(Setting name);

        int GetSettingInt(Setting name);

    }

}
