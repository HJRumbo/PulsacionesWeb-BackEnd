using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using pulsacionesdotnet.Models;

namespace pulsacionesdotnet.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public IConfiguration Configuration { get; }
        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _userService = new UserService(connectionString);
        }

        /*[HttpGet]
        public IEnumerable<UserViewModel> Gets()
        {
            var users = _userService.ConsultarTodos().Select(u=> new UserViewModel(u));
            return users;
        }

        [HttpGet("{identificacion}")]
        public ActionResult<UserViewModel> Get(string identificacion)
        {
            var persona = _personaService.BuscarxIdentificacion(identificacion);
            if (persona == null) return NotFound();
            var personaViewModel = new PersonaViewModel(persona);
            return personaViewModel;
        }*/

        [HttpPost]
        public ActionResult<UserViewModel> Post(UserInputModel userInput)
        {
            User user = MapearUser(userInput);
            var response = _userService.Guardar(user);
            if (response.Error) 
            {
                return BadRequest(response.Mensaje);
            }
            return Ok(response.User);
        }

        /*[HttpDelete("{identificacion}")]
        public ActionResult<string> Delete(string identificacion)
        {
            string mensaje = _personaService.Eliminar(identificacion);
            return Ok(mensaje);
        }*/
        private User MapearUser(UserInputModel userInput)
        {
            var user = new User
            {
                UserName = userInput.UserName,
                FirstName = userInput.FirstName,
                LastName = userInput.LastName,
                Password = userInput.Password,
                MobilePhone = userInput.MobilePhone,
                Email = userInput.Email
                
            };
            return user;
        }

        /*[HttpPut("{identificacion}")]
        public ActionResult<string> Put(string identificacion, Persona persona)
        {
            throw new NotImplementedException();
        }*/
    }
}