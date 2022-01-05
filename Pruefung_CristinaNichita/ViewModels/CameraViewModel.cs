using System;
using System.Collections.Generic;
using System.Text;

using Pruefung_CristinaNichita.Models;
using Pruefung_CristinaNichita.Services;

namespace Pruefung_CristinaNichita.ViewModels
{
    public class CameraViewModel : ViewModel
    {
        public CameraViewModel(OpenUserService userService)
        {
            this.userService = userService;
        }

        public OpenUserService userService;
    }
}
