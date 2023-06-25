using ShopApp.UI.Models;
using ShopApp.UI.Models.Dtos;

namespace ShopApp.UI.Services
{
    public interface IBaseService:IDisposable
    {
        
        Task<T> SendAsync<T>(ApiRequest apiRequest);

    }
}
