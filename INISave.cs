using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CaptIt
{
    public class INISave
    {
        class API
        {
            [DllImport("kernel32")]
            public static extern int GetPrivateProfileString(string section, string key, string default_, StringBuilder result, int size, string path);
            [DllImport("kernel32")]
            public static extern long WritePrivateProfileString(string section, string key, string val, string path);
        }

        public string path;
        public INISave(string path)
        {
            this.path = path;
        }

        public void WriteSetting(string section, string key, string value)
        {
            API.WritePrivateProfileString(section, key, value, path);
        }

        public string GetSetting(string section, string key)
        {
            StringBuilder sb = new StringBuilder(255);

            API.GetPrivateProfileString(section, key, "", sb, sb.Capacity, path);
            return sb.ToString();
        }
    }
}
