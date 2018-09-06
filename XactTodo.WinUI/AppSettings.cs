using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using XactTodo.Utils;

namespace XactTodo
{
    public class AppSettings
    {
        private string rootUrl_Api;
        private string userName;
        private string password;

        private AppSettings()
        {
            var items = AppSettingManager.Instance.Read();
            var kv = items.Where(p => p.Key.Equals(nameof(rootUrl_Api), StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            //if (kv != null)
            {
                rootUrl_Api = kv.Value;
            }
            //用户账号
            kv = items.Where(p => p.Key.Equals(nameof(userName), StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            userName = kv.Value;
            //密码
            kv = items.Where(p => p.Key.Equals(nameof(password), StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            password = kv.Value;
        }

        private static AppSettings _instance;
        public static AppSettings Instance => _instance ?? (_instance = new AppSettings());

        public string RootUrl_Api
        {
            get => rootUrl_Api;
            set
            {
                rootUrl_Api = value;
                AppSettingManager.Instance.Save(nameof(RootUrl_Api), rootUrl_Api);
            }
        }

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                AppSettingManager.Instance.Save(nameof(UserName), userName);
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                AppSettingManager.Instance.Save(nameof(Password), password);
            }
        }

        private string ToTitleCase(string s)
        {
            return string.IsNullOrEmpty(s) ? s : (s.Substring(0, 1).ToUpper() + s.Substring(1));
        }
    }
}
