using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_de_Android.Views
{
    public partial class News : ContentPage
    {
        private List<NewsItem> newsItems;

        public News()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            LoadNewsAsync();
        }

        private async Task LoadNewsAsync()
        {
            newsItems = await FetchNewsFromApiAsync();
            if (newsItems != null)
            {
                // Asigna cada artículo de noticias a un Frame, ajustando la lógica según tu API
                UpdateFrameWithNewsData(newsItems);
            }
        }

        public class NewsItem
        {
            public int IdNew { get; set; }
            public string NewsTitle { get; set; }
            public string NewsImage { get; set; }
            public string NewsSubtitle { get; set; }
            public string NewsDescription { get; set; }
        }

        private void UpdateFrameWithNewsData(List<NewsItem> newsItems)
        {
            if (newsItems.Count >= 3)
            {
                // Primer Frame
                Frame1Image.Source = newsItems[0].NewsImage;
                Frame1Label.Text = newsItems[0].NewsTitle;

                // Segundo Frame
                Frame2Image.Source = newsItems[1].NewsImage;
                Frame2Label.Text = newsItems[1].NewsTitle;

                // Tercer Frame
                Frame3Image.Source = newsItems[2].NewsImage;
                Frame3Label.Text = newsItems[2].NewsTitle;
            }
        }

        private async void OnFrameTapped(object sender, EventArgs e)
        {
            var frame = sender as Frame;
            var tapGesture = frame?.GestureRecognizers.FirstOrDefault() as TapGestureRecognizer;
            var frameIdString = tapGesture?.CommandParameter?.ToString();

            if (int.TryParse(frameIdString, out int frameId))
            {
                var newsItem = newsItems.FirstOrDefault(item => item.IdNew == frameId);
                if (newsItem != null)
                {
                    ModalTitle.Text = "Detalle de Noticia";
                    ModalImage.Source = newsItem.NewsImage;
                    ModalSubtitle.Text = newsItem.NewsSubtitle;
                    ModalDescription.Text = newsItem.NewsDescription;

                    // Mostrar el modal
                    ModalContainer.IsVisible = true;
                    await ModalContainer.FadeTo(1, 250);
                }
            }
        }

        private async void CloseModal(object sender, EventArgs e)
        {
            await ModalContainer.FadeTo(0, 250); // Fade out modal
            ModalContainer.IsVisible = false;

            // Restaurar el menú inferior (TabbedPage)
            var tabbedPage = Application.Current.MainPage as TabbedPage;
            if (tabbedPage != null)
            {
                tabbedPage.IsVisible = true;
            }
        }

        private void OnBackgroundTapped(object sender, EventArgs e)
        {
            if (ModalContainer.IsVisible)
            {
                CloseModal(null, null);
            }
        }

        private async Task<List<NewsItem>> FetchNewsFromApiAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "https://myowndomain.lol:5001/api/news/get";
                    string response = await client.GetStringAsync(apiUrl);
                    return JsonConvert.DeserializeObject<List<NewsItem>>(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching news: {ex.Message}");
                return null;
            }
        }
    }
}
