using Microsoft.AspNetCore.Http;

namespace Application.UserFeedback.User;

public interface IUserService : ICustomerService<UserModel>
{
}