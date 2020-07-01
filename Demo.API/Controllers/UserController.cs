using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Business.Providers;
using Demo.Common.Constants;
using Demo.Common.Logger;
using Demo.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService = null;
        private readonly ILoggerManager logger = null;

        public UserController(IUserService userService, ILoggerManager logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpPost]
        [Authorize]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody]UserViewModel user)
        {
            Response response = new Response();
            try
            {
                if (await userService.Create(user))
                 response.Message = ResponseMessages.UserAddSuccess;
                return Ok(response);
            }
            catch(Exception ex)
            {
                logger.LogError(", User: " + user.LoginName + ", Error :" + ex.Message);
                response.Message = ex.Message;
                return BadRequest(response);
            }           
        }

        [HttpPost]
        [Authorize]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody]UserViewModel user)
        {
            Response response = new Response();
            try
            {
                if(await userService.Update(user))
                response.Message = ResponseMessages.UserAddSuccess;
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(", User: " + user.LoginName + ", Error :" + ex.Message);
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
    }
}