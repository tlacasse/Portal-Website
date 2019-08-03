using System;
using System.Linq;
using System.Reflection;

namespace Portal.Data.Web {

    public enum Setting {
        PortalGridCurrentWidth,
        PortalGridCurrentHeight,
        [ReadOnly]
        PortalGridMaxSize,
        [ReadOnly]
        PortalGridMinSize
    }

    public class ReadOnlyAttribute : Attribute {
    }

    public static class SettingExtensions {

        public static bool IsReadonlySetting(this Setting setting) {
            MemberInfo member = setting.GetType().GetMember(setting.ToString()).Single();
            return member.HasAttribute<ReadOnlyAttribute>();
        }

    }

}
