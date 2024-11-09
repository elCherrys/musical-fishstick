using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace App_de_Android.Utilities
{
    public static class ApiService
    {
        private static async Task<string> GetTokenAsync()
        {
            return await SecureStorage.GetAsync("authToken");
        }

        public static async Task<HttpResponseMessage> MakeAuthenticatedRequest(string apiUrl, HttpMethod method, HttpContent content = null)
        {
            var httpClient = new HttpClient();
            var token = await GetTokenAsync();

            var request = new HttpRequestMessage(method, apiUrl);

            if (content != null)
            {
                request.Content = content;
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await httpClient.SendAsync(request);
        }
    }
}
