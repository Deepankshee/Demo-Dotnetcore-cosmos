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
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService = null;
        private readonly ILoggerManager logger = null;

        public ProductController(IProductService productService, ILoggerManager logger)
        {
            this.productService = productService;
            this.logger = logger;
        }

        [HttpPost]
        [Authorize]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody]ProductViewModel product)
        {
            Response response = new Response();
            try
            {
                product.UserId = new Guid(User.FindFirst(ClaimType.Id)?.Value);
                if (await productService.Create(product))
                   response.Message = ResponseMessages.ProductAddSuccess;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                logger.LogError(", product: " + product.Name + ", Error :" + ex.Message);
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            Response response = new Response();
            try
            {
                var userId = (User.FindFirst(ClaimType.Id)?.Value);
                var result = await productService.GetProductsByUserId(userId);
                response.Data = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                logger.LogError("product Error :" + ex.Message);
                return BadRequest(response);
            }
        }
    }
}