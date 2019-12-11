using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProjectRecept
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public class Result
    {
        public string title { get; set; }
        public string href { get; set; }
        public string ingredients { get; set; }
        public string thumbnail { get; set; }
    }

    public class RootObject
    {
        public string title { get; set; }
        public double version { get; set; }
        public string href { get; set; }
        public List<Result> results { get; set; }
    }

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private async void Generate_Click(object sender, RoutedEventArgs e)
        {
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
            var headers = httpClient.DefaultRequestHeaders;
            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            var input = "";
            if (Ingredient.IsChecked == true)
            {
                input = Ingredient.Content.ToString();
            }

            Uri requestUri = new Uri("http://www.recipepuppy.com/api/" + input);

            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            var rootObj = JsonConvert.DeserializeObject<RootObject>(httpResponseBody);
            var message = "";

            RootObject root = new RootObject
            {
                title = rootObj.title,
                version = rootObj.version,
                href = rootObj.href,
                results = rootObj.results
            };

            foreach (var recepie in root.results)
            {
                message = recepie.href;
            }

            var messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }
    }
}
