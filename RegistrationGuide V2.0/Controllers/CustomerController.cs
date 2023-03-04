using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NLayer.Data.Models;
using NLayer.Service;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "admin,manager")]
        public IActionResult GetAll()
        {
            var customer = _customerService.GetAll();
            return Ok(customer);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "admin,manager")]
        public IActionResult Update(Customer customer)
        {

            _customerService.Update(customer);
            return Ok(customer);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "admin,manager")]
        public IActionResult Create(Customer customer)
        {
            _customerService.Add(customer);
            return Ok(customer);
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int customerid)
        {
            _customerService.Delete(customerid);
            return Ok();
        }
        [HttpGet]
        public  IActionResult IsMoreThanOneNameFromNumber(string number)
        {
          return  Ok(_customerService.GetNamesFromNumber(number));
          
        }

    }
}
