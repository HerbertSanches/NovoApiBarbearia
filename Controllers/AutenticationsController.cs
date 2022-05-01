using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NovoApiBarbearia.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using NovoApiBarbearia.Data;

namespace NovoApiBarbearia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AutenticationsController(IConfiguration configuration, AppDbContext context)
        {
            _context = context;
            _config = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Funcionario funcionario)
        {
            bool resultado = ValidarUsuario(funcionario);
            if (resultado == true) {
                var tokenString = GerarTokenJwt();
                return Ok(new { token = tokenString });
            }
            else {
                return Unauthorized();
            }
        }

        private bool ValidarUsuario(Funcionario funcionario)
        {
            //descriptografar c.Senha!!
            var resultado = _context.Funcionario.Where(c => c.Cpf == funcionario.Cpf && c.Senha == funcionario.Senha);
            
            if (resultado.Any() == true) {
                return true;
            }
            else {
                return false;
            }
        }
        private string GerarTokenJwt()
        {
            var expiry = DateTime.Now.AddMinutes(60);
            var securityKey = new SymmetricSecurityKey
                              (Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials
                              (securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                                             expires: DateTime.Now.AddMinutes(120),
                                             signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

    }
}
