using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

using Pruefung_CristinaNichita.Services;
using Pruefung_CristinaNichita.Models;
using Pruefung_CristinaNichita.Views;

namespace Pruefung_CristinaNichita.ViewModels
{
    public class DetailViewModel : ViewModel
    {
        public DetailViewModel(OpenUserService userService)
        {
            this.userService = userService;
            FullUser = new ForecastItem();
        }

        public ICommand Save => new Command(async() =>
        {
            if(FullUser.Name != null)
            {
                await userService.AddOrUpdateUser(FullUser);
            }

            var MainView = Resolver.Resolve<MainView>();
            await Navigation.PushAsync(MainView);
        });

        private OpenUserService userService;

        public ForecastItem FullUser { get; set; }
        
    }



}
