using GraduationProject.DTO;
using GraduationProject.DTO.DTOAccount;
using GraduationProject.Models;
using GraduationProject.Services.IAuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
 
namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<User> usermanger;
        private readonly IConfiguration configuration;
        private readonly IAuthService _authService;
        Context context = new Context();
        public static IWebHostEnvironment _env;

        public AccountController(IWebHostEnvironment env, UserManager<User> usermanger,
            IConfiguration configuration, IAuthService authService)
        {
            _authService = authService;
            this.usermanger = usermanger;
            this.configuration = configuration;
            _env = env;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }
        //[Authorize]
        [HttpPost("login")] //api/Account/Login
        public async Task<IActionResult> Login([FromForm] Login userDto)
        {
            if (ModelState.IsValid)
            {  //check + create token  
                User user = await usermanger.FindByNameAsync(userDto.UserName);
                if (user != null)
                {
                    bool found = await usermanger.CheckPasswordAsync(user, userDto.Password);
                    if (found)
                    {
                        //claims token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        //get role
                        var roles = await usermanger.GetRolesAsync(user);
                        foreach (var item in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item));

                        }
                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
                        //    signingCredentials 
                        SigningCredentials signing = new SigningCredentials(securityKey,
                        SecurityAlgorithms.HmacSha256
                        );


                        JwtSecurityToken MyToken = new JwtSecurityToken(
                        issuer: configuration["JWT:ValidIssuer"],//url for web api << provider
                        audience: configuration["JWT:ValidAudiance"], // url consumer << angular
                        claims: claims,
                        expires: DateTime.Now.AddDays(30),
                        signingCredentials: signing
                        );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(MyToken),
                            expiration = MyToken.ValidTo
                        });
                    }
                }

                return Unauthorized();
            }
            return Unauthorized();
        }
        [HttpPost("ForgetPassword")]

        public async Task<IActionResult> ForgetPassword([FromForm] ForgetPassword _user)
        {
            User user = await usermanger.FindByNameAsync(_user.UserName);
            var token = await usermanger.GeneratePasswordResetTokenAsync(user);
            if (user != null)
            {

                var result = await usermanger.ResetPasswordAsync(user, token, _user.ChangePass);
                //ChangePasswordAsync(user, user.PasswordHash, NewPassword);
                //  GeneratePasswordResetTokenAsync(user);
                if (result.Succeeded)
                {

                    return Ok("New PassWord Add Done");
                }
                else
                {
                    var Errors = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        Errors += $"{error.Description}  +  ";
                    }
                    return BadRequest(Errors);
                }


            }
            return Unauthorized();
        }
        [HttpPost("changePassword")]
        public async Task<IActionResult> changePassword([FromForm] ChangePassword _user)
        {
            User user = await usermanger.FindByNameAsync(_user.UserName);
            if (user != null)
            {

                var result = await usermanger.ChangePasswordAsync(user, _user.OldPassword, _user.NewPassword);
                //ChangePasswordAsync(user, user.PasswordHash, NewPassword);
                //  GeneratePasswordResetTokenAsync(user);
                if (result.Succeeded)
                {

                    return Ok("Password  Change Succeeded");
                }
                else
                {
                    var Errors = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        Errors += $"{error.Description}  +  ";
                    }
                    return BadRequest(Errors);
                }


            }
            return Unauthorized();
        }

        [HttpGet("ShowUser")]
        public async Task<IActionResult> getAccount()
        {
            var userId =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await usermanger.FindByIdAsync(userId);
            ShowUser showUser = new ShowUser();
            showUser.UserName = user.UserName;
            showUser.FirstName = user.FirstName;
            showUser.LastName = user.LastName;
            showUser.Phone = user.Phone;
            showUser.Email = user.Email;
            //  showUser.Password = user.PasswordHash;//correct?
            showUser.Street = user.Street;
            showUser.City = user.City;
            return Ok(showUser);
        }

        [HttpGet("GetAuthenticatedUser")]
        public IActionResult GetAuthenticatedUser()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    // User is authenticated
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    // Your code here
                    return Ok($"Authenticated user with ID: {userId}");
                }
                else
                {
                    return Unauthorized("Not authenticated");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred during registration.");

            }
        }

        //  [Authorize(Roles = "Admin")]
        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromForm] TokenRequestModel model)
        {
            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync([FromForm] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }

        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _authService.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
                return BadRequest(result);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        [HttpPost("revokeToken")]
        public async Task<IActionResult> RevokeToken([FromForm] RevokeToken model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required!");

            var result = await _authService.RevokeTokenAsync(token);

            if (!result)
                return BadRequest("Token is invalid!");

            return Ok();
        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
