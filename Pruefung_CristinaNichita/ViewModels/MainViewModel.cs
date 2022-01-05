using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Essentials;

using Pruefung_CristinaNichita.Models;
using Pruefung_CristinaNichita.Services;
using Pruefung_CristinaNichita.Views;



namespace Pruefung_CristinaNichita.ViewModels
{

    public class MainViewModel : ViewModel
    {
        public MainViewModel(OpenUserService userService) 
        { 
            this.userService = userService;
            userService.EventUser += (sender, item) => UserGroup.Add(CreateForecastItem(item));
            userService.EventUserUpdated += (sender, item) => Task.Run(async () => await this.LoadData());
            Task.Run(async () => await this.LoadData());
        }

        private ForecastItem selectedUser;

        public ForecastItem SelectedUser
        {
            get => selectedUser;
            set => Set(ref selectedUser, value);
        }

        public ICommand OpenUser => new Command(async () =>
        {
            if(this.selectedUser != null)
            {
                await NavigationToUser(SelectedUser);
            }
            await Task.Run(async () => await this.LoadData());
        });

        private async Task NavigationToUser(ForecastItem giveUser)
        {
            if(giveUser != null)
            {
                var getDetailView = Resolver.Resolve<DetailView>();
                var getDetailViewModel = getDetailView.BindingContext as DetailViewModel;

                if (getDetailViewModel != null)
                {
                    getDetailViewModel.FullUser = giveUser;
                    await Navigation.PushAsync(getDetailView);
                }
            }
        }

        public ICommand Camera => new Command(async () =>
        {
            var CameraView = Resolver.Resolve<CameraView>();
            await Navigation.PushAsync(CameraView);
        });

        /*
        public MainViewModel(IUserService userService)
        {
            this.userService = userService;
        }
        */

        /*
        public ICommand Camera => new Command(async () =>
        {
            var CameraView = Resolver.Resolve<CameraView>();
            await Navigation.PushAsync(CameraView);
        });
        */

        public ICommand AddUser => new Command(async () =>
        {
            var DetailView = Resolver.Resolve<DetailView>();
            await Navigation.PushAsync(DetailView);
            await Task.Run(async () => await this.LoadData());
        });

        OpenUserService userService;

        public ObservableCollection<ForecastItem> user;
        public ObservableCollection<ForecastItem> User
        {
            get => user;
            set => Set(ref user, value);     
        }

        public ObservableCollection<ForecastGroup> userGroup;

        public ObservableCollection<ForecastGroup> UserGroup
        {
            get => userGroup;
            set => Set(ref userGroup, value);
        }

        public async Task LoadData()
        {
            var forecast = await this.userService.GetUser();
            var itemGroup = forecast.Select(i => CreateForecastItem(i));

            userGroup = new ObservableCollection<ForecastGroup>(itemGroup);

            User = new ObservableCollection<ForecastItem>(forecast);

            /*
            foreach (var item in forecast.Items)
            {
                if (!itemGroup.Any())
                {
                    itemGroup.Add(
                        new ForecastGroup(new List<ForecastItem>() { item })
                        { Info = item.Info });
                    continue;
                }
                var group = itemGroup.SingleOrDefault(x => x.Info == item.Info);
                if (group == null)
                {
                    itemGroup.Add(
                        new ForecastGroup(new List<ForecastItem>() { item })
                        { Info = item.Info });
                    continue;
                }
                group.Items.Add(item);
            }
            */
        }

        private ForecastGroup CreateForecastItem(ForecastItem forecastItem)
        {
            var forecastGroup = new ForecastGroup(new List<ForecastItem>() { forecastItem });
            forecastGroup.ItemStatusChanged += ItemStatusChanged;

            return forecastGroup;
        }

        private void ItemStatusChanged(object sender, EventArgs e)
        {
        }

        private string scanResultText;

        public string ScanResultText
        {
            get => scanResultText;
            set => Set(ref scanResultText, value);
        }

        public ICommand Load => new Command(async() =>
        {
            for(int i = 0; i<10; i++)
            {
                var randomUser = await this.userService.GetForecast();
                //var userList = await this.userService.GetUser();

                foreach(var eachUser in randomUser.Items)
                
                    //if(!userList.Contains(eachUser))
                    {
                        await this.userService.AddUser(eachUser);
                    }
                
            }

           //this.userService.EventUser += (sender, item) => UserGroup.Add(CreateForecastItem(item));
            
           await Task.Run(async () => await this.LoadData());

        });

        public ICommand Delete => new Command(async () =>
        {
            await this.userService.DeleteAllItems();
            await Task.Run(async () => await this.LoadData());
        });
    }
}

