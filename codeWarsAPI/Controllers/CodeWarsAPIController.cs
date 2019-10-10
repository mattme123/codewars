using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using codeWarsAPI.Models;
using codeWarsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace codeWarsAPI.Controllers
{
    [ApiController]
    [Route("api/codewars")]
    public class CodeWarsAPIController : ControllerBase
    {
        private readonly CodeWarsApiService _service;
        public CodeWarsAPIController(CodeWarsApiService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public object PerformCheckAndAdd([FromBody] CodeWarsAddModel cw)
        {
            return _service.PerformCheckAndAddAsync(cw);
        }
    }
}
