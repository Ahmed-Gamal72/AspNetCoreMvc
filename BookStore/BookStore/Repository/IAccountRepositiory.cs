using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface IAccountRepositiory
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);
    }
}