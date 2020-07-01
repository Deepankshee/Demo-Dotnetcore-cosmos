using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Business.Providers;
using Demo.Common.Cryptography;
using Demo.Common.ViewModels;
using Demo.Common.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Demo.Common.Constants;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService = null;
        private readonly ILoggerManager logger = null;

        public AuthController(IAuthService authService, ILoggerManager logger)
        {
            this.authService = authService;
            this.logger = logger;
        }      

        [HttpPost]
        [Route("Validate")]
        public async Task<IActionResult> Validate([FromBody]AuthUserInputModel authUser)
        {
            Response response = new Response();
            try
            {
                authUser.Password = Cryptography.Encrypt(authUser.Password);
                var result = await authService.AuthenticateUser(authUser);
                if (!string.IsNullOrEmpty(result.Token))
                    response.Data = result;
                else
                    response.Message = ResponseMessages.AuthErrorMessage;
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError( ", User: " + authUser.LoginName + ", Error :" + ex.Message);
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
    }
}