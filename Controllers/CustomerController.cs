using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_ADO_DotNet.Models.BO;

namespace WebApi_ADO_DotNet.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        #region Declaration
        Customer _oCustomer = new Customer();
        List<Customer> _oCustomers = new List<Customer>();
        #endregion


        //[Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Gets()
        {
            List<Customer> _oCustomers = new List<Customer>();
            Customer oCustomer = new Customer();
            _oCustomers = Customer.Gets();
            return _oCustomers.ToList();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            _oCustomer = new Customer();
            _oCustomer = Customer.Get(id);
            return _oCustomer;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Save(Customer oCustomer)
        {
            _oCustomer = new Customer();
            _oCustomer = oCustomer.Save(oCustomer);
            return _oCustomer;
        }

        [HttpDelete("{id}")]
        public  ActionResult<Customer> Delete(int id)
        {
            
            _oCustomer = new Customer();
            _oCustomer.Message = _oCustomer.Delete(id);
            return _oCustomer;
        }

    }
}