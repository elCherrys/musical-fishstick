using App_de_Android.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_de_Android
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.LoginScreen());

        }

        public void NavigateToHomePage()
        {
            MainPage = new NavigationPage(new Views.TabbedPage1());  // Cambia a la pantalla principal con el TabbedPage
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


         // Método que actualiza los recursos de la aplicación según el tema
        public void ApplyTheme(OSAppTheme theme)
        {
            if (theme == OSAppTheme.Dark)
            {
                Application.Current.Resources["BackgroundColor"] = Application.Current.Resources["BackgroundColorDark"];
                Application.Current.Resources["TextColor"] = Application.Current.Resources["TextColorDark"];
            }
            else
            {
                Application.Current.Resources["BackgroundColor"] = Application.Current.Resources["BackgroundColorLight"];
                Application.Current.Resources["TextColor"] = Application.Current.Resources["TextColorLight"];
            }
        }
    }
}
