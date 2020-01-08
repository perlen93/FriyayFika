using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ProjectRecept;
using System.Threading.Tasks;
using ProjectRecept.Models;
using ProjectRecept.ViewModels;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectRecept.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class RecipeView : Page
    {
        private readonly MainPageViewModel _viewModel = new MainPageViewModel();

        public RecipeView()
        {            
            this.InitializeComponent();
            DataContext = _viewModel;
        }

        public void Navigate(Uri RecepieUrl)
        {
            webView1.Navigate(RecepieUrl);
        }
    }
}
