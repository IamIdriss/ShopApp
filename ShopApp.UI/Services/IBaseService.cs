using ShopApp.UI.Models;
using ShopApp.UI.Models.Dtos;

namespace ShopApp.UI.Services
{
    public interface IBaseService:IDisposable
    {
        public ResponseDto ResponseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);

    }
}
