using Newtonsoft.Json;
using ShopApp.UI.Models;
using ShopApp.UI.Models.Dtos;
using System.Text;

namespace ShopApp.UI.Services
{
    public class BaseService : IBaseService

    {
        private readonly IHttpClientFactory _httpClient;
        public ResponseDto ResponseModel { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.ResponseModel = new ResponseDto();
            _httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = _httpClient.CreateClient("ShopAppAPI");
                var message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data)
                        , Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage apiResponse = await client.SendAsync(message);
                var apiContent =await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto =JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponseDto;


            }
            catch (Exception e)
            {

                var dto = new ResponseDto
                { IsSuccess = false,
                DisplayMessage="Error occurs",
                    ErrorMessages = new() { Convert.ToString(e.Message) }
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
