using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XactTodo
{
    public class SettingViewModel
    {

        public AppSettings AppSettings => AppSettings.Instance;

        internal void Save()
        {
            throw new NotImplementedException();
        }
    }
}
