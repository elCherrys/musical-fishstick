using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_de_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            // Load previously saved preferences
            DarkModeSwitch.IsToggled = Preferences.Get("isDarkMode", false);
            NotificationsSwitch.IsToggled = Preferences.Get("notificationsEnabled", true);
            LocationAccessSwitch.IsToggled = Preferences.Get("locationAccessEnabled", true);
        }

        private void OnDarkModeToggled(object sender, ToggledEventArgs e)
        {
            // Save the preference for dark mode
            Preferences.Set("isDarkMode", e.Value);

            // Apply the dark or light theme based on the toggle value
            Application.Current.UserAppTheme = e.Value ? OSAppTheme.Dark : OSAppTheme.Light;
        }

        private void OnSaveSettingsClicked(object sender, EventArgs e)
        {
            // Save additional settings as needed
            Preferences.Set("notificationsEnabled", NotificationsSwitch.IsToggled);
            Preferences.Set("locationAccessEnabled", LocationAccessSwitch.IsToggled);

            DisplayAlert("Settings", "Ajustes guardados correctamente!", "OK");
        }
    }
}
