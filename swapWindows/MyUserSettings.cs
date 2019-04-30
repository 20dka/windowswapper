using System;
using System.Configuration;
using System.Text;

namespace swapWindows
{
    class MyUserSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("3057")]
        public decimal secondX
        {
            get
            {
                return ((decimal)this["secondX"]);
            }
            set
            {
                this["secondX"] = (decimal)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("163")]
        public decimal secondY
        {
            get
            {
                return ((decimal)this["secondY"]);
            }
            set
            {
                this["secondY"] = (decimal)value;
            }
        }

    }
}
