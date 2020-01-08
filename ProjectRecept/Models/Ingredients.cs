using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ProjectRecept.Models
{
    class Ingredients
    {
        public bool Butter { get; private set; }
        public bool Cinnamon { get; private set; }
        public bool Sugar { get; private set; }
        public bool Chocolate { get; private set; }

        public Uri CheckSpecifiedIngredients()
        {
            // blir alltid false, varför?
            var specifiedIngredients = new List<string>();
            bool[] checkboxes = new bool[]
            { Butter, Cinnamon, Sugar, Chocolate };

            foreach (var ingredient in checkboxes)
            {
                if (ingredient == true)
                {
                    specifiedIngredients.Add(ingredient.ToString());
                }
            }

            string choosenIngredients = "";
            foreach (var specified in specifiedIngredients)
            {
                choosenIngredients += specified + ",";
            }

            string uri = "";           
            if (String.IsNullOrEmpty(choosenIngredients))
            {
                uri = "http://www.recipepuppy.com/api/";
            }
            else
            {
                var ingredients = choosenIngredients.TrimEnd(',');
                uri = "http://www.recipepuppy.com/api/?i=" + ingredients;
            }
            Uri requestUri = new Uri(uri);

            return requestUri;
        }
    }
}
