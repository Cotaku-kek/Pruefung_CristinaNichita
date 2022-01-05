using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pruefung_CristinaNichita.Views;

namespace Pruefung_CristinaNichita
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(Resolver.Resolve<Views.MainView>());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
