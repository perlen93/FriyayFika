using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectRecept.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

        private async Task<string> GetResponseBodyAsync(Uri requestUri)
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

            return httpResponseBody;
        }

        private async Task<string> GetRecepieAsync(object sender, RoutedEventArgs e)
        { 
            var inputIngredient = CheckSpecifiedIngredients();
            string uri = "";

            if (String.IsNullOrEmpty(inputIngredient))
            {
                uri = "http://www.recipepuppy.com/api/" + inputIngredient;
            }
            else
            {
                var ingredients = inputIngredient.TrimEnd(',');
                uri = "http://www.recipepuppy.com/api/?i=" + ingredients;
            }
            Uri requestUri = new Uri(uri);

            string httpResponseBody = await GetResponseBodyAsync(requestUri);

            if (!String.IsNullOrEmpty(httpResponseBody))
            {
                var rootObj = JsonConvert.DeserializeObject<RootObject>(httpResponseBody);
                var message = "";

                RootObject root = new RootObject
                {
                    title = rootObj.title,
                    version = rootObj.version,
                    href = rootObj.href,
                    results = rootObj.results
                };

                Random rnd = new Random();
                int randomIndex = rnd.Next(rootObj.results.Count);
                var randomItem = rootObj.results[randomIndex].href;
                message = randomItem;               
               
                var messageDialog = new MessageDialog(message);
                await messageDialog.ShowAsync();

                return message;
                // nu returnerar den URL strängen. Denna behöver läggas in 
                // koppla så knappen redriectar till RecipeVIew
                // .Navigate(typeof(RecipeView)); 
            }
        }

        public string CheckSpecifiedIngredients()
        {
            var specifiedIngredients = new List<string>();
            CheckBox[] checkboxes = new CheckBox[] 
            { Butter, Cinnamon, Sugar, Chocolate };

            foreach (var ingredient in checkboxes)
            {
                if (ingredient.IsChecked == true)
                {
                   specifiedIngredients.Add(ingredient.Content.ToString());
                }
            }                   

            string choosenIngredients = "";
            foreach (var specified in specifiedIngredients)
            {
                choosenIngredients += specified + ",";
            }

            return choosenIngredients;
        }


    }
}
