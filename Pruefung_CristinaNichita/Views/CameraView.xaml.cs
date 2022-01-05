using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Pruefung_CristinaNichita.ViewModels;
using Pruefung_CristinaNichita.Services;

namespace Pruefung_CristinaNichita.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraView : ContentPage
    {
        public CameraView()
        {
            InitializeComponent();
            cameraViewModel = new CameraViewModel(new OpenUserService());
            cameraViewModel.Navigation = Navigation;
            BindingContext = cameraViewModel;
        }

        public void resultScan(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var user = await cameraViewModel.userService.GetUser();
                var userGet = user.Find(x => x.Id == Int32.Parse(result.Text));

                if (userGet != null)
                {
                    var detailView = Resolver.Resolve<DetailView>();
                    var detailViemModel = detailView.BindingContext as DetailViewModel;

                    if (detailViemModel != null)
                    {
                        detailViemModel.FullUser = userGet;
                        await Navigation.PushAsync(detailView);
                    }
                }
            });
        }

        CameraViewModel cameraViewModel;
    }
}