using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SitemapGenerator.Contexts;
using SitemapGenerator.Models.Entities;
using SitemapGenerator.ViewModels;

namespace SitemapGenerator.Services
{
    public class UserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;

        public UserService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IConfiguration configuration, DataContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _dataContext = dataContext;
        }

        public async Task<bool> Register(RegisterViewModel model)
        {
            try
            {
                UserEntity userEntity = model;

                _dataContext.Users.Add(userEntity);
                await _dataContext.SaveChangesAsync();
                return true;
            } catch { return false; }
        }
    }
}
