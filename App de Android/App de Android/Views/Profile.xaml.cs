using System;
using Xamarin.Forms;

namespace App_de_Android.Views
{
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
        }

        // Métodos para manejar los eventos de clic de los botones
        private void Boton1_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Botón 1", "Has presionado el botón 1: Hola, Mundo", "OK");
        }

        private void Boton2_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Botón 2", "Has presionado el botón 2: Bienvenido", "OK");
        }

        private void Boton3_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Botón 3", "Has presionado el botón 3: Presiona aquí", "OK");
        }

        private void Boton4_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Botón 4", "Has presionado el botón 4: Xamarin es genial", "OK");
        }

        private void Boton5_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Botón 5", "Has presionado el botón 5: ¡Prueba completada!", "OK");
        }
    }
}
