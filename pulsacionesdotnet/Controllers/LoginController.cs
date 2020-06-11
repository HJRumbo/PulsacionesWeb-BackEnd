using Datos;
using Entity;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using pulsacionesdotnet.Config;
using pulsacionesdotnet.Models;
using pulsacionesdotnet.Service;

namespace pulsacionesdotnet.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private JwtService _jwtService;
        private UserService _userService;
        public IConfiguration Configuration { get; }
        public LoginController(IConfiguration configuration, IOptions<AppSetting> appSettings)
        {
            Configuration = configuration;
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _userService = new UserService(connectionString);
            _jwtService = new JwtService(appSettings);
        }

        [AllowAnonymous]
        [HttpPost()]
        public IActionResult Login(LoginInputModel model)
        {
            var user = _userService.Validate(model.UserName, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("Acceso Denegado", "Username or password is incorrect");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }
            var response = _jwtService.GenerateToken(user);

            return Ok(response);
        }

    }
}