using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StartupElectonicWallet.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StartupElectonicWallet.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerAccountController : ControllerBase
    {
        private readonly ElectronicWalletContext _context;

        public CustomerAccountController(ElectronicWalletContext apiContext)
        {
            this._context = apiContext;
        }

        [HttpGet]
        public ActionResult Get()
        {
            Services.CustomerServices CustomerServices_ = null;
            List<Models.Account> account = null;
            try
            {
                CustomerServices_ = new Services.CustomerServices(_context);
                account = CustomerServices_.GetAccount();
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Services.CustomerServices CustomerServices_ = null;
            Models.Account account = null;
            try
            {
                CustomerServices_ = new Services.CustomerServices(_context);
                account = CustomerServices_.GetAccount(id);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: CustomerServicesController/Create
        [HttpPost]
        public ActionResult Post([FromBody]Models.Account Account)
        {
            Services.CustomerServices customerServices_ = null;
            Models.Common.Response response = null;
            try
            {
                customerServices_ = new Services.CustomerServices(_context);
                response = customerServices_.AddAccount(Account);
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
