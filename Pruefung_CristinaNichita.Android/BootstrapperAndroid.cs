using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pruefung_CristinaNichita.Droid
{
    public class BootstrapperAndroid : Bootstrapper
    {
        public static void init()
        {
            var instance = new BootstrapperAndroid();
        }
    }
}