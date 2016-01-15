using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptIt.SettingPanels
{
    public interface ISettingPanel
    {
        void SaveSet();
        void LoadSet();
    }
}
