using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XactTodo.Exceptions;

namespace XactTodo
{
    public static class HttpClientFactory
    {
        private static HttpClient client;
        private static AppSettings AppSettings => AppSettings.Instance;

        static HttpClientFactory()
        {
        }

        public static HttpClient CreateClient()
        {
            if (client == null)
            {
                //初始化HttpClient实例变量
                client = new HttpClient();
                if (string.IsNullOrEmpty(AppSettings.RootUrl_Api))
                {
                    throw new MissingSettingException("缺少应用程序配置项[API服务器地址]，请在系统设置中设定该配置项。");
                }
                client.BaseAddress = new Uri(AppSettings.RootUrl_Api);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            return client;
        }

    }
}
