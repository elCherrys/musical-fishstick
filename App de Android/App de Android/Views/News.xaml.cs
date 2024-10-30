using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_de_Android.Views
{
    public partial class News : ContentPage
    {
        public News()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        // Override the OnBackButtonPressed method
        protected override bool OnBackButtonPressed()
        {
            // Check if the modal is visible
            if (ModalContainer.IsVisible)
            {
                // Close the modal if it is open
                CloseModal(null, null);
                return true; // Indicate that the back button press has been handled
            }

            // Allow the default back button behavior if the modal is not visible
            return base.OnBackButtonPressed();
        }


        private async void OnFrameTapped(object sender, EventArgs e)
        {
            // Obtener el Frame que fue presionado
            var frame = sender as Frame;

            // Obtener el CommandParameter desde el GestureRecognizer
            var tapGesture = frame?.GestureRecognizers.FirstOrDefault() as TapGestureRecognizer;
            var frameIdString = tapGesture?.CommandParameter?.ToString();

            if (int.TryParse(frameIdString, out int frameId))
            {
                // Ocultar el menú inferior (TabbedPage)
                var tabbedPage = Application.Current.MainPage as TabbedPage;
                if (tabbedPage != null)
                {
                    tabbedPage.IsVisible = false;  // Oculta el TabbedPage
                }

                // Configurar el contenido del modal según el frameId
                switch (frameId)
                {
                    case 1:
                        ModalTitle.Text = "Detalle de Noticia";
                        ModalImage.Source = "https://img.freepik.com/fotos-premium/tazon-fuente-fresa-fresca-rojo_185193-45914.jpg";
                        ModalSubtitle.Text = "Frescura Y Sabor Irresistible";
                        ModalDescription.Text = "Disfruta de los mejores frutos rojos de la temporada, perfectos para tus recetas saludables.";
                        break;
                    case 2:
                        ModalTitle.Text = "Detalle de Noticia";
                        ModalImage.Source = "https://img.freepik.com/fotos-premium/dibujo-verano-imagenes-fondo-espacio-copia_1179130-568427.jpg";
                        ModalSubtitle.Text = "Frutas Cítricas";
                        ModalDescription.Text = "Las naranjas frescas están llenas de vitamina C y son el snack perfecto para cualquier momento del día.";
                        break;
                    case 3:
                        ModalTitle.Text = "Detalle de Noticia";
                        ModalImage.Source = "https://img.freepik.com/foto-gratis/ilustracion-verduras-frutas-bonitas_23-2151859026.jpg";
                        ModalSubtitle.Text = "Llenas De Sabor Y Salud";
                        ModalDescription.Text = "Nuestras verduras frescas son cultivadas localmente para garantizar el mejor sabor y nutrición.";
                        break;
                    default:
                        break;
                }

                // Mostrar el modal
                if (ModalContainer != null)
                {
                    ModalContainer.IsVisible = true;
                    await ModalContainer.FadeTo(1, 250); // Fade in modal
                }
            }
            else
            {
                Console.WriteLine("Error: No se pudo convertir el CommandParameter a un entero.");
            }
        }

        // Método para cerrar el modal y restaurar el TabbedPage
        private async void CloseModal(object sender, EventArgs e)
        {
            await ModalContainer.FadeTo(0, 250); // Fade out modal
            ModalContainer.IsVisible = false;

            // Restaurar el menú inferior (TabbedPage)
            var tabbedPage = Application.Current.MainPage as TabbedPage;
            if (tabbedPage != null)
            {
                tabbedPage.IsVisible = true;  // Vuelve a mostrar el TabbedPage
            }
        }
        private void OnBackgroundTapped(object sender, EventArgs e)
        {
            if (ModalContainer.IsVisible)
            {
                CloseModal(null, null); // Close the modal
            }
        }

    }
}