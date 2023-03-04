using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Data.Models;
using NLayer.Service;
using System.Data;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommercialActivitityController : ControllerBase
    {
        private readonly ICommercialActivityService _commercialActivityService;

        public CommercialActivitityController(ICommercialActivityService commercialActivityService)
        {
            _commercialActivityService = commercialActivityService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "admin,manager")]
        public IActionResult GetAll()
        {
            var commercialactivity = _commercialActivityService.GetAll();
            return Ok(commercialactivity);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "admin,manager")]
        public IActionResult Update(CommercialActivity commercialActivity)
        {
            _commercialActivityService.Update(commercialActivity);
            return Ok();
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "admin,manager")]
        public IActionResult Add(CommercialActivity commercialActivity)
        {
            _commercialActivityService.Add(commercialActivity);
            return Ok();
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int commercialActivityId)
        {
            _commercialActivityService.Delete(commercialActivityId);
            return Ok();
        }
    }
}
