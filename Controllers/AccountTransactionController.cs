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
    public class AccountTransactionController : ControllerBase
    {
        private readonly ElectronicWalletContext _context;

        public AccountTransactionController(ElectronicWalletContext apiContext)
        {
            this._context = apiContext;
        }


        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Services.CustomerServices CustomerServices_ = null;
            List<Models.Transaction> transactionCollection = null;
            try
            {
                CustomerServices_ = new Services.CustomerServices(_context);
                transactionCollection = CustomerServices_.GetTransaction(id);
                return Ok(transactionCollection);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: CustomerServicesController/Create
        [HttpPost]
        public ActionResult Post([FromBody]Models.Transaction Transaction)
        {
            Services.CustomerServices customerServices_ = null;
            Models.Common.Response response = null;
            try
            {
                customerServices_ = new Services.CustomerServices(_context);
                response = customerServices_.AddTransaction(Transaction);
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
