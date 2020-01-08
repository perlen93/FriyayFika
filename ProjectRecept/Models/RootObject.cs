using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ProjectRecept.Models
{
    public class RootObject
    {
        public string Title { get; set; }
        public double Version { get; set; }
        public string Href { get; set; }
        public List<Result> Results { get; set; }

        public async Task<string> GetResponseBodyAsync(Uri requestUri)
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


        public async Task<Uri> GetRandomRecipieURLAsync(Uri requestUri)
        {
            try
            {
                var rootObj = await DeserializeRecipieAsync(requestUri);

                if (rootObj != null)
                {
                    Random rnd = new Random();
                    int randomIndex = rnd.Next(rootObj.Results.Count);

                    var randomItem = rootObj.Results[randomIndex].Href;
                    var randomRecepieURL = new Uri(randomItem);
                    return randomRecepieURL;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                string message = "No internetconnection or response from API. Contact app owner for further assistance.";
                var messageDialog = new MessageDialog(message);
                await messageDialog.ShowAsync();
                return null;
            }
        }

        public async Task<RootObject> DeserializeRecipieAsync(Uri requestUri)
        {
            string httpResponseBody = await GetResponseBodyAsync(requestUri);

            if (!String.IsNullOrEmpty(httpResponseBody))
            {
                var rootObj = JsonConvert.DeserializeObject<RootObject>(httpResponseBody);

                RootObject root = new RootObject
                {
                    Title = rootObj.Title,
                    Version = rootObj.Version,
                    Href = rootObj.Href,
                    Results = rootObj.Results
                };
                return root;
            }
            return null;
        }

    }

    //tror inte denna behövs
    //public async void GetRecepieAsync(object sender, RoutedEventArgs e)
    //{
    //    try
    //    {
    //        var recipeURL = await GetRandomRecipieURLAsync();
    //    }
    //    catch
    //    {        
    //        string message = "No internetconnection or response from API. Contact app owner for further assistance.";
    //        var messageDialog = new MessageDialog(message);
    //        await messageDialog.ShowAsync();
    //    }

    //    // GÖRA OM DENNA TILL Task<string> ?? Så vi kan returnera en URL att jobba med
    //    // eller ska denna bara redirecta?

    //    //RecipeView recipiePage = new RecipeView();
    //    //this.Content = recipiePage;
    //    // denna fungerar inte för navigation/push Async är för xamarin
    //    // await Navigation.PushAsync(new RecipeView(recipeURL));

    //    // hur få in denna targetUri med till recipiePage?
    //    // Uri targetUri = new Uri(recipeURL);
    //    // ha den som inparameter för ReciepeView? 

    //    // här kan vi anv. oss av reseptURL o skicka den till ReipieView 
    //    // där webView1 borde reppar den view vi vill skicka
    //    // Kör Try catchen är egentligen till för när vi ska visa upp websida i xaml
    //    // DENNA METOD ÄR INTE KLAR!!
    //}


}

