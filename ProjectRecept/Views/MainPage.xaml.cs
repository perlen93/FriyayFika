using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectRecept.Models;
using ProjectRecept.ViewModels;
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
    public sealed partial class MainPage : Page
    {
        private readonly MainPageViewModel _viewModel = new MainPageViewModel();
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = _viewModel;
        }

        private async void OnClick_ValidatePassword(object sender, RoutedEventArgs e)
        {
            await _viewModel.GenerateFriyayFika();
            //var x = new RecipeView();
            //x.Navigate();
        }

        //    private async Task<string> GetResponseBodyAsync(Uri requestUri)
        //    {
        //        Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
        //        var headers = httpClient.DefaultRequestHeaders;
        //        string header = "ie";
        //        if (!headers.UserAgent.TryParseAdd(header))
        //        {
        //            throw new Exception("Invalid header value: " + header);
        //        }

        //        header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
        //        if (!headers.UserAgent.TryParseAdd(header))
        //        {
        //            throw new Exception("Invalid header value: " + header);
        //        }

        //        Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
        //        string httpResponseBody = "";

        //        try
        //        {
        //            httpResponse = await httpClient.GetAsync(requestUri);
        //            httpResponse.EnsureSuccessStatusCode();
        //            httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
        //        }
        //        catch (Exception ex)
        //        {
        //            httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
        //        }

        //        return httpResponseBody;
        //    }

        //    public async Task<string> GetRandomRecipieURLAsync()
        //    {
        //        var inputIngredient = CheckSpecifiedIngredients();
        //        string uri = "";
        //        var message = "";

        //        if (String.IsNullOrEmpty(inputIngredient))
        //        {
        //            uri = "http://www.recipepuppy.com/api/" + inputIngredient;
        //        }
        //        else
        //        {
        //            var ingredients = inputIngredient.TrimEnd(',');
        //            uri = "http://www.recipepuppy.com/api/?i=" + ingredients;
        //        }
        //        Uri requestUri = new Uri(uri);
        //        var rootObj = await DeserializeRecipieAsync(requestUri);

        //        if (rootObj != null)
        //        {
        //            Random rnd = new Random();
        //            int randomIndex = rnd.Next(rootObj.Results.Count);

        //            var randomItem = rootObj.Results[randomIndex].Href;
        //            message = randomItem;

        //            var messageDialog = new MessageDialog(message);
        //            await messageDialog.ShowAsync();
        //        }

        //        return message;
        //    }

        //    public async Task<RootObject> DeserializeRecipieAsync(Uri requestUri)
        //    {
        //        string httpResponseBody = await GetResponseBodyAsync(requestUri);

        //        if (!String.IsNullOrEmpty(httpResponseBody))
        //        {
        //            var rootObj = JsonConvert.DeserializeObject<RootObject>(httpResponseBody);

        //            RootObject root = new RootObject
        //            {
        //                Title = rootObj.Title,
        //                Version = rootObj.Version,
        //                Href = rootObj.Href,
        //                Results = rootObj.Results
        //            };
        //            return root;
        //        }
        //        return null;
        //    }

        //    public async void GetRecepieAsync(object sender, RoutedEventArgs e)
        //    {
        //        string message;
        //        try
        //        {
        //            var recipeURL = await GetRandomRecipieURLAsync();
        //        }
        //        catch
        //        {
        //            message = "No internetconnection or response from API. Contact app owner for further assistance.";
        //            var messageDialog = new MessageDialog(message);
        //            await messageDialog.ShowAsync();
        //        }

        //        // GÖRA OM DENNA TILL Task<string> ?? Så vi kan returnera en URL att jobba med
        //        // eller ska denna bara redirecta?

        //        RecipeView recipiePage = new RecipeView();
        //        this.Content = recipiePage;
        //        // denna fungerar inte för navigation/push Async är för xamarin
        //        // await Navigation.PushAsync(new RecipeView(recipeURL));

        //        // hur få in denna targetUri med till recipiePage?
        //        // Uri targetUri = new Uri(recipeURL);
        //        // ha den som inparameter för ReciepeView? 

        //        // här kan vi anv. oss av reseptURL o skicka den till ReipieView 
        //        // där webView1 borde reppar den view vi vill skicka
        //        // Kör Try catchen är egentligen till för när vi ska visa upp websida i xaml
        //        // DENNA METOD ÄR INTE KLAR!!
        //    }

        //    public string CheckSpecifiedIngredients()
        //    {
        //        var specifiedIngredients = new List<string>();
        //        CheckBox[] checkboxes = new CheckBox[]
        //        { Butter, Cinnamon, Sugar, Chocolate };

        //        foreach (var ingredient in checkboxes)
        //        {
        //            if (ingredient.IsChecked == true)
        //            {
        //                specifiedIngredients.Add(ingredient.Content.ToString());
        //            }
        //        }

        //        string choosenIngredients = "";
        //        foreach (var specified in specifiedIngredients)
        //        {
        //            choosenIngredients += specified + ",";
        //        }
        //              string uri = "";
        //                var message = "";

        //        if (String.IsNullOrEmpty(inputIngredient))
        //        {
        //            uri = "http://www.recipepuppy.com/api/" + inputIngredient;
        //        }
        //        else
        //        {
        //            var ingredients = inputIngredient.TrimEnd(',');
        //            uri = "http://www.recipepuppy.com/api/?i=" + ingredients;
        //        }

        //        return uri;
        //    }
    }
}
