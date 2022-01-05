using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pruefung_CristinaNichita.Views;
using Pruefung_CristinaNichita.ViewModels;
using Pruefung_CristinaNichita.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pruefung_CristinaNichita.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
            var mainViewModel = new MainViewModel(new OpenUserService());
            mainViewModel.Navigation = Navigation;
            BindingContext = mainViewModel;
        }
    }
}