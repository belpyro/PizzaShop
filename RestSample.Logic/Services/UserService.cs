using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using Fody;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RestSample.Logic.Extensions;
using RestSample.Logic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestSample.Logic.Services
{
    [ConfigureAwait(false)]
    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            this._mapper = mapper;
        }

        public async Task<Result> Register(NewUserDto model)
        {
            // validation username existing
            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            var result2 = await _userManager.AddToRoleAsync(user.Id, "user");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);

            await _userManager.SendEmailAsync(user.Id, "Confirm your email", $"click on https://localhost:44444/api/user/email/confirm?userId={user.Id}&token={token}");

            return Result.Combine(result.ToFunctionalResult(), result2.ToFunctionalResult());
        }

        public async Task<Result> ChangePassword(string userId, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(userId, token, newPassword);
            return result.ToFunctionalResult();
        }

        public async Task<Result> ConfirmEmail(string userId, string token)
        {
            var data = await _userManager.ConfirmEmailAsync(userId, token);
            return data.ToFunctionalResult();
        }

        public async Task<Result> ResetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new ValidationException("User doesn't exist");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
            await _userManager.SendEmailAsync(user.Id, "Reset your password", $"Click on yourhost/api/users/password/reset?userId={user.Id}&token={token}");
            return Result.Success();
        }

        public async Task<Result<IReadOnlyCollection<UserDto>>> GetAllUsers()
        {
            //bad practice! don't use it in production. only as sample
            var users = await _userManager.Users.ProjectToListAsync<UserDto>(_mapper.ConfigurationProvider);
            return Result.Success((IReadOnlyCollection<UserDto>)users.AsReadOnly());
        }
    }
}