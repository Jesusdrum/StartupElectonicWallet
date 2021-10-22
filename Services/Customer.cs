using StartupElectonicWallet.DataAccess;
using StartupElectonicWallet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StartupElectonicWallet.Services
{
    public class Customer
    {
        private readonly ElectronicWalletContext _context;

        public Customer(ElectronicWalletContext context)
        {
            _context = context;

        }
        public StartupElectonicWallet.Models.Customer GetCustomer(int customerId)
        {
            StartupElectonicWallet.Models.Customer customer = null;

            try
            {
                if (customerId != 0) 
                    customer = _context.Customer.FirstOrDefault(r => r.CustomerID == customerId);
                if (customer != null)
                    customer.AccountCollection = _context.Account.Where(r => r.CustomerId == customerId).ToList();
            }
            catch
            {
                throw new Exception();
            }
            return customer;
        }

        public List<StartupElectonicWallet.Models.Customer> GetCustomer()
        {
            List<StartupElectonicWallet.Models.Customer> customer = null;

            try
            {
                    customer = _context.Customer.ToList();
            }
            catch
            {
                throw new Exception();
            }
            return customer;
        }

        public StartupElectonicWallet.Models.Common.Response AddCustomer(Models.Customer Customer_)
        {
            StartupElectonicWallet.Models.Customer customer = null;
            StartupElectonicWallet.Models.Common.Response response= null;
            try
            {
                response = new Models.Common.Response();
                customer = (StartupElectonicWallet.Models.Customer)_context.Customer.FirstOrDefault(r => r.CustomerIdentifier == Customer_.CustomerIdentifier || r.CustomerID==Customer_.CustomerID);
                if (customer != null)
                {
                    // No permitido, el cliente existe
                    response.DateTimeResponse = DateTime.Now;
                    response.IsSuccess = false;
                    response.Message = "Ya existe el cliente";
                    return response;
                } 
                   // Se registra el cliente
                    _context.Customer.Add(Customer_);
                    _context.SaveChanges();

                //Se envía una respuesta satisfactoria
                response.DateTimeResponse = DateTime.Now;
                response.IsSuccess = true;
                response.Message = "Registro guardado";
            }
            catch
            {
                throw new Exception();
            }
            return response;
        }
    }
}
