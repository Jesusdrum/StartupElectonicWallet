using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StartupElectonicWallet.Models;
using StartupElectonicWallet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupElectonicWallet.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IJwtAuthenticationService _authService;

        public LoginController(ILogger<LoginController> logger, IJwtAuthenticationService authService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authService = authService;
        }

        /// GET: api/
        ///<summary>
        /// Status
        ///</summary>
        ///<Return>Estatus</Return>
        [AllowAnonymous]
        [HttpGet]
        public object Get()
        {
            var responseObject = new { Status = "Running" };
            _logger.LogInformation($"Status: {responseObject.Status}");

            return responseObject;
        }

        /// POST: api/Authenticate
        ///<summary>
        /// Login de API
        ///</summary>
        /// <remarks>
        /// Sample request:
        ///     POST api/Authenticate
        ///     {        
        ///       "Username": "wsUser",
        ///       "Password": "123456"    
        ///     }
        /// </remarks>
        ///<param name="user">Json: Username, Password</param>
        ///<Return name="Token">Bearer: Token</Return>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthInfo user)
        {
            var token = _authService.Authenticate(user.Username, user.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}

