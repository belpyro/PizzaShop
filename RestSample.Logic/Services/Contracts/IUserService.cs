using CSharpFunctionalExtensions;
using RestSample.Logic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestSample.Logic.Services
{
    public interface IUserService
    {
        Task<Result> Register(NewUserDto model);

        Task<Result> ConfirmEmail(string userId, string token);

        Task<Result> ResetPassword(string email);

        Task<Result> ChangePassword(string userId, string token, string newPassword);

        Task<Result<IReadOnlyCollection<UserDto>>> GetAllUsers();

        Task<Maybe<UserDto>> GetUser(string username, string password);
    }
}