using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WarehouseSystemWebAPI.Models;
using WarehouseSystemWebAPI.Services;

namespace WarehouseSystemWebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountService accountService;
        IConfiguration configuration;

        public AccountController(IAccountService _accountService,IConfiguration _configuration)
        {
            accountService = _accountService;
            configuration = _configuration;
        }

        [Authorize(Roles = "Admin")]
        [Route("NewUser")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUp signUp)
        {
            var result = await accountService.CreateAccount(signUp);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, result.Errors);
            }
        }
        [Authorize]
        [Route("LogOut")]
        [HttpGet]
        public async Task LogOut()
        {
            await accountService.LogOut();
        }
        [Authorize(Roles = "Admin")]
        [Route("UserList")]
        [HttpGet]
        public IActionResult UserList()
        {
            List<UsersDTO> users = accountService.UsersList();
            return Ok(users);
        }
        [Authorize(Roles = "Admin")]
        [Route("UserRoles")]
        [HttpGet]
        public async Task<IActionResult> UserRoles(string UserId)
        {
            List<UserRoles> userRoles = await accountService.UserRole(UserId);
            return Ok(userRoles);
        }
        [Authorize(Roles = "Admin")]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserRoles(List<UserRoles> userRoles) {
            await accountService.UpdateRoles(userRoles);
            userRoles = await accountService.UserRole(userRoles[0].UserId);

            return Ok(userRoles);
        }
        [Authorize(Roles = "Admin")]
        [Route("NewRole")]
        [HttpPost]
        public async Task<IActionResult> NewRole(Role role)
        {
        var result=  await  accountService.InsertRole(role);
            if (result.Succeeded)
            {
                return Ok();
            }

            else
            {
                return StatusCode(500, result.Errors);
            }
            
        }
        [Authorize(Roles = "Admin")]
        [Route("RoleList")]
        [HttpGet]
        public IActionResult RoleList()
         {
         List<Role> roles=  accountService.RoleList();
            return Ok(roles);
         }
        [Authorize(Roles = "Admin")]
        [Route("Active")]
        [HttpGet]
        public async Task Active(string userId)
        {
            await accountService.Activate(userId);
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(SignIn signIn)
        {
            var result=await accountService.SignIn(signIn);
            var user = await accountService.GetUserInfo(signIn.Email);
            if (user.Active == true)
            {
                if (result.Succeeded)
                {
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, signIn.Email),
                    new Claim("UniqueValue", Guid.NewGuid().ToString())
                };

                    //var user = await accountService.GetUserInfo(signIn.Email);
                    var roles = accountService.GetRoles(user);
                    foreach (var item in roles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, item));
                    }
                    var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
                    var token = new JwtSecurityToken(
                        issuer: configuration["JWT:ValidIssuer"],
                        audience: configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddDays(15),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return RedirectToAction("actives", "Account");
            }
        }
        public bool actives()
        {
            return false;

        }
        [Authorize]
        [Route("Created")]
        [HttpGet]
        public async Task<IActionResult> Createdby(string name)
        {
            return Ok( await accountService.GetUserInfo(name));
        }
        [Authorize]
        [Route("GetRoles")]
        [HttpGet]
        public async Task<List<string>> GetRoles(string username)
        {
            var user = await accountService.GetUserInfo(username);
            return   accountService.GetRoles(user);
        }

    }


    }


