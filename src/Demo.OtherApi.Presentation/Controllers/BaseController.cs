using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Demo.OtherApi.BusinessLogic.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.OtherApi.Presentation.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly IBusinessManagerService _businessManagerService;
        public BaseController(IBusinessManagerService businessManagerService)
        {
            _businessManagerService = businessManagerService;
        }

        public IBusinessManagerService DomainManager
        {
            get
            {
                return _businessManagerService;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Conflict(object value)
        {
            return this.StatusCode(StatusCodes.Status409Conflict, value);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error(object value)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, value);
        }
        
    }
}
