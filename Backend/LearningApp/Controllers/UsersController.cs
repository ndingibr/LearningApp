using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using LearningApp.Core.Entities;
using LearningApp.Infrastructure;

namespace LearningApp.API
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private ISecurityRepository<Users> _usersRepo;
        private ISecurityRepository<UserRole> _userRoleRepo;
        private ISecurityRepository<Roles> _roles;
        private IConfiguration _config;

        public UsersController(ISecurityRepository<Users> usersRepo, 
            ISecurityRepository<UserRole> usersRolesRepo, 
            ISecurityRepository<Roles> roles,
            IConfiguration config)
        {
            _userRoleRepo = usersRolesRepo;
            _roles = roles;
            _usersRepo = usersRepo;
            _config = config;
        }

        [HttpGet("")]
        public IActionResult Users()
        {
            var users = _usersRepo.GetAll();
            return Ok(users);
        }

        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            var users = _roles.GetAll();
            return Ok(users);
        }
        
        [HttpGet("user")]
        public IActionResult GetUser(string email)
        {
            var user = from users in _usersRepo.GetAll()
                       join userRoles in _userRoleRepo.GetAll()
                       on users.Id equals userRoles.UserId
                       join roles in _roles.GetAll()
                       on userRoles.RoleId equals roles.Id
                       where users.Email == email
                       select new
                       {
                           users.FirstName,
                           users.LastName,
                           users.Email,
                           users.PhoneNumber,
                           roles.Name,
                           roles.Id,
                           users.Police_Station
                       };

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _usersRepo.GetAll()
                .FirstOrDefault(x => x.Email == model.Email);

            if (user.Password != model.Password)
                return BadRequest("Wrong password");

            var role = (from userRoles in _userRoleRepo.GetAll().Where(x => x.UserId == user.Id).ToList()
                        join roles in _roles.GetAll() on userRoles.RoleId equals roles.Id
                        select new
                        {
                            roles.Name
                        }).FirstOrDefault();
            
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    //new Claim(ClaimTypes.Role, role.ToString())
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
              _config["Tokens:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [AllowAnonymous]
        [Route("ActivateUser")]
        [HttpPost]
        public IActionResult ActivateUser([FromBody]SignUpModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors)
                    .Select(modelError => modelError.ErrorMessage).ToList());

            var user = new Users()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                Police_Station = model.Police_Station,
                IsActive = model.IsActive
            };

            _usersRepo.Insert(user);

            var savedUser = _usersRepo.GetAll().FirstOrDefault(x => x.Email == model.Email);

            var role = new UserRole
            {
                UserId = savedUser.Id,
                RoleId = model.Role
            };

            _userRoleRepo.Insert(role);

            return Ok();
        }

        [AllowAnonymous]
        [Route("SignUp")]
        [HttpPost]
        public IActionResult SignUp([FromBody]SignUpModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors)
                    .Select(modelError => modelError.ErrorMessage).ToList());

            var existingUser = _usersRepo.GetAll().Where(x => x.Email == model.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("", "User already exists!");
                return BadRequest();
            }

            var user = new Users()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                Police_Station = model.Police_Station,
                IsActive = false
            };

            _usersRepo.Insert(user);

            var savedUser = _usersRepo.GetAll().FirstOrDefault(x => x.Email == model.Email);

            var role = new UserRole
            {
                UserId = savedUser.Id,
                RoleId = model.Role
            };

            _userRoleRepo.Insert(role);
       
            return Ok();
        }

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword([FromBody]SetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _usersRepo.GetAll()
                .FirstOrDefault(x => x.Email == model.Email);

            if (user.Password != model.OldPassword)
                return BadRequest();

            user.Password = model.NewPassword;

            _usersRepo.Update(user);
            
            return Ok();
        }

        private string GenerateToken(string username, int expireMinutes = 20)
        {
            var symmetricKey = Convert.FromBase64String(_config["Tokens:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                        new Claim(ClaimTypes.Name, username)
                    }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
    }
}
