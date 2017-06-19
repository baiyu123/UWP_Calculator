using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace CalculatorUWP
{
    static class Setting
    {
        public static void SetDarkTheme() {
            StoreString("Theme", "Dark");
        }

        public static void SetLightTheme() {
            StoreString("Theme","Light");
        }

        //theme data storage
        private static void StoreString(string key, string value) {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            localSettings.Values["Theme"] = value;
        }

        //theme data retrieval
        public static string GetThemeString() {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            return (string)localSettings.Values["Theme"];
        }
        

        /*public static void LoadTheme() {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            string theme = (string)localSettings.Values["Theme"];
            if (theme == "Dark")
            {
                App.Current.RequestedTheme = ApplicationTheme.Dark;
            }
            else {
                App.Current.RequestedTheme = ApplicationTheme.Light;
            }
        }*/
    }
}
