using Datos;
using Entity;
using System;
using System.Collections.Generic;

namespace Logica
{
    public class UserService
    {
        private readonly ConnectionManager _conexion;
        private readonly UserRepository _repositorio;

        public UserService(string connectionString)
        {
            _conexion = new ConnectionManager(connectionString);
            _repositorio = new UserRepository(_conexion);
        }

        public GuardarUserResponse Guardar(User user)
        {
            try
            {
                user.Estado="AC";
                _conexion.Open();
                _repositorio.Guardar(user);
                _conexion.Close();
                return new GuardarUserResponse(user);
            }
            catch (Exception e)
            {
                return new GuardarUserResponse($"Error de la Aplicacion: {e.Message}");
            }
            finally { _conexion.Close(); }
        }

        public User Validate(string userName, string password)
        {
            _conexion.Open();
            User user = _repositorio.Validate(userName, password);
            _conexion.Close();
            return user;
        }
        
    }
    

    public class GuardarUserResponse 
    {
        public GuardarUserResponse(User user)
        {
            Error = false;
            User = user;
        }
        public GuardarUserResponse(string mensaje)
        {
            Error = true;
            Mensaje = mensaje;
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public User User { get; set; }
    }

}