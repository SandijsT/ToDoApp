using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.DTOs;
using ToDo.Interfaces;
using ToDo.Models.Entities;

namespace ToDo.Controllers
{
    public class UsersController : BaseController
    {
        private readonly TaskContext _context;
        private readonly ITokenService _tokenService;

        public UsersController(TaskContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public IActionResult Register()
        {
            if(User?.Identity != null && User.Identity.IsAuthenticated )
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        
        [HttpPost, ActionName("Register")]
        public async Task<IActionResult> RegisterConfirm([Bind("Username, Password")] RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken!");
                
            using var hmac = new HMACSHA512();

            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _tokenService.CreateToken(user);
            Response.Cookies.Append("auth", token);
            return RedirectToAction("Index", "TaskList");
        }

        public IActionResult Login()
        {
            if(User?.Identity != null && User.Identity.IsAuthenticated )
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        
        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> LoginConfirm(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x =>
                x.Username.ToLower().Equals(loginDto.Username.ToLower()));
            if (user == null) return Unauthorized("Invalid username!");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            if (computedHash.Where((t, i) => t != user.PasswordHash[i]).Any())
            {
                return Unauthorized("Incorrect password!");
            }
            
            var token = _tokenService.CreateToken(user);

            Response.Cookies.Append("auth", token);
            return RedirectToAction("Index", "TaskList");
        }
        
        public ActionResult Logout()
        {
            Response.Cookies.Append("auth", "");
            return RedirectToAction("Index", "Home");
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username.ToLower().Equals(username.ToLower()));
        }
        
    }
}