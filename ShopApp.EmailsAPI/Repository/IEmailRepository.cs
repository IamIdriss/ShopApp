using ShopApp.EmailsAPI.Models.Dtos;

namespace ShopApp.EmailsAPI.Repository
{
    public interface IEmailRepository
    {
        Task SendAndLogEmail(PaymentUpdateMessageDto messageDto);
    }
}
