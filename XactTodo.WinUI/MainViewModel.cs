using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XactTodo.Models;
using XactTodo.Utils;

namespace XactTodo
{
    public class MainViewModel
    {
        private const string KEY_AUTHORIZATION = "authorization";
        private readonly HttpClient client;
        private bool loading;

        public MainViewModel()
        {
            try
            {
                client = HttpClientFactory.CreateClient();
                var url_unfinished = $"{AppSettings.Instance.RootUrl_Api}/api/v1/Matter/unfinished";
                client.DefaultRequestHeaders.Add(KEY_AUTHORIZATION, "TEST");
                var response = client.GetAsync(url_unfinished).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var matters = response.Content.ReadAsAsync<IEnumerable<Matter>>().Result;
                    //var jo = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(rlt.Content.ReadAsStringAsync().Result);
                    //var matters = JsonConvert.DeserializeObject<IEnumerable<Matter>>(rlt.Content.ReadAsStringAsync().Result);
                    this.Matters = new ObservableCollection<Matter>(matters);
                }
           }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "警告", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            this.ShowSettingsCommand = new RelayCommand(ShowSettingsDialog, IsMainWindowLoaded);
            this.MatterFinishCommand = new RelayCommand(MatterFinished);
            this.AddMatterCommand = new RelayCommand(AddMatter);
            this.ShowMatterCommand = new RelayCommand(ShowMatter);
            this.ReloadMattersCommand = new RelayCommand(ReloadMatters);
        }
        public ObservableCollection<Matter> Matters { get; private set; }

        public ICommand ShowSettingsCommand { get; private set; }
        public ICommand MatterFinishCommand { get; private set; }
        public ICommand AddMatterCommand { get; private set; }
        public ICommand ShowMatterCommand { get; private set; }
        public ICommand ReloadMattersCommand { get; private set; }

        private bool IsMainWindowLoaded()
        {
            return Application.Current.MainWindow != null && Application.Current.MainWindow.IsLoaded;
        }

        private void ShowSettingsDialog()
        {
            SettingWindow.Show();
        }

        private void AddMatter()
        {
            var matter = MatterWindow.AddMatter();
        }

        private void ShowMatter(object sender)
        {
            if (sender == null)
                return;
            var matter = ((ListBoxItem)sender).Content as Matter;
            MatterWindow.ShowMatter(matter);
        }

        private void ReloadMatters()
        {
            if (loading) return;
            try
            {
                loading = true;

                var response = client.GetAsync("api/matters").Result;
                response.EnsureSuccessStatusCode(); // 有错误码时报出异常

                var matters = response.Content.ReadAsAsync<IEnumerable<Matter>>().Result;
                this.Matters = new ObservableCollection<Matter>(matters);
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // 这个异常指明了一个解序列化请求体的问题。
                MessageBox.Show(jEx.Message);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                loading = false;
            }
        }

        private void MatterFinished(object sender)
        {
            var lstMatters = (Application.Current.MainWindow as MainWindow).lstMatters;
            var matter = ((ListBoxItem) lstMatters.ContainerFromElement((Control)sender)).Content as Matter;
            MessageBox.Show(matter?.Subject);
        }

    }
}