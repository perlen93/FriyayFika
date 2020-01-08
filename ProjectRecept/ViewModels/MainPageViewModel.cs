using ProjectRecept.Models;
using ProjectRecept.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRecept.ViewModels
{
    class MainPageViewModel
    {
        // behövs denna?
        // public event PropertyChangedEventHandler PropertyChanged;

        public RootObject RootObject { get; set; }
        public Ingredients Ingredients { get; set; }

        public Uri RecepieUrl { get; set; }
        public MainPageViewModel()
        {
            RootObject = new RootObject();
            Ingredients = new Ingredients();
        }

        public async Task GenerateFriyayFika()
        {
            // kör koden så att det finns en recipie url
            var specifiedIngredients =  Ingredients.CheckSpecifiedIngredients();         
            RecepieUrl = await RootObject.GetRandomRecipieURLAsync(specifiedIngredients);
            // var hej = RootObject.Results.First<Result>().Href;
            // Uri heej = new Uri(hej);
           
            // men varför redirectar den inte till recipieView??
            var recipieView = new RecipeView();
            recipieView.Navigate(RecepieUrl);


            // hur navigera till RecipieView?!
            this.Frame.Navigate(typeof(RecipeView));




        }
    }
}
