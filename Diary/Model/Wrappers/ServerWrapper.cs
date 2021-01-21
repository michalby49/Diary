using Diary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Model.Wrappers
{
    class ServerWrapper
    {
        public static string Address
        {
            get
            {
                return Settings.Default.Address;
            }
            set
            {
                Settings.Default.Address = value;
            }
        }
        public static string Name
        {
            get
            {
                return Settings.Default.Name;
            }
            set
            {
                Settings.Default.Name = value;

            }
        }
        public static string DataBaseName
        {
            get
            {
                return Settings.Default.DataBaseName;
            }
            set
            {
                Settings.Default.DataBaseName = value;
            }
        }
        public static string DataBaseLogin
        {
            get
            {
                return Settings.Default.DataBaseLogin;
            }
            set
            {
                Settings.Default.DataBaseLogin = value;
            }
        }
        public static string DataBasePassword
        {
            get
            {
                return Settings.Default.DataBasePassword;
            }
            set
            {
                Settings.Default.DataBasePassword = value;
            }
        }
        public static bool NeedRestart { get; set; }
    }
}
