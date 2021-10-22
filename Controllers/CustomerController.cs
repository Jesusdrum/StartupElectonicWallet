using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StartupElectonicWallet.Services;
using StartupElectonicWallet.DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace StartupElectonicWallet.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ElectronicWalletContext _context;

        public CustomerController(ElectronicWalletContext apiContext)
        {
            this._context = apiContext;
        }


        // GET: api/<CustomerController>
        [HttpGet]
        public ActionResult Get()
        {
            Services.Customer customer_ = null;
            List<Models.Customer> customer = null;
            try
            {
                customer_ = new Services.Customer(_context);
                customer = customer_.GetCustomer();
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Services.Customer customer_ = null;
            Models.Customer customer = null;
            try { 
            customer_ = new Services.Customer(_context);
                customer = customer_.GetCustomer(id);
                return Ok(customer);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        // POST api/<CustomerController>
        [HttpPost]
        public ActionResult Post([FromBody] Models.Customer Customer)
        {
            Services.Customer customer_ = null;
            Models.Common.Response response = null;
            try
            {
                customer_ = new Services.Customer(_context);
                response = customer_.AddCustomer(Customer);
                if (response.IsSuccess)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
