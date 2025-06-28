using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProviderAPI.DTO;
using ProviderAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProviderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }

        [HttpPost("Register")]  // api/Account/Register
        public async Task<IActionResult> Register(RegisterDTO userFromReq)
        {
            if (ModelState.IsValid) 
            { 
                // create user ==> save in DB
                ApplicationUser user = new ApplicationUser();
                user.UserName = userFromReq.UserName;
                user.Email = userFromReq.Email;

                IdentityResult result = await userManager.CreateAsync(user, userFromReq.Password);
                if (result.Succeeded) 
                {
                    return Ok("Registered Successfully!");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Register Error", error.Description);
                }
            }
            return BadRequest(ModelState);
        }


        [HttpPost("Login")]  // api/Account/Login
        public async Task<IActionResult> Login(LoginDTO userFromReq)
        {
            if (ModelState.IsValid)
            {
                // check user in DB !?
                ApplicationUser userFromDB = await userManager.FindByNameAsync(userFromReq.UserName);

                if (userFromDB != null)
                { 
                    // check password
                    bool found = await userManager.CheckPasswordAsync(userFromDB, userFromReq.Password);
                    if (found == true) 
                    {
                        // Generate token
                        // 1- Design

                        // customize claims list
                        List<Claim> userClaims = new List<Claim>();

                        // Create unique token generated id
                        userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        // Get id, userName
                        userClaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDB.Id));
                        userClaims.Add(new Claim(ClaimTypes.Name, userFromDB.UserName));

                        var userRoles = await userManager.GetRolesAsync(userFromDB);
                        foreach (var roleName in userRoles)
                        {
                            userClaims.Add(new Claim(ClaimTypes.Role, roleName));
                        }


                        // Create signingCredentials
                        // 1- Key => Symmetric Security Key
                        SymmetricSecurityKey signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecretKey"]));

                        // algorithm
                        SigningCredentials signingCred = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);


                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer: config["JWT:IssuerIP"],  // wgo made token
                            audience: config["JWT:AudienceIP"],  // who uses token => consumer
                            expires: DateTime.Now.AddDays(1),  // token expiration time
                            claims: userClaims,
                            signingCredentials: signingCred
                        );

                        // Generate Token
                        // send in response
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(myToken) ,
                            expiration = DateTime.Now.AddDays(1),
                        });
                    }
                }
                ModelState.AddModelError("Password", "Invalid userName or password");
            }
            return BadRequest(ModelState);
        }
    }
}
