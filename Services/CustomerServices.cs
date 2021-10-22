using Microsoft.Extensions.Configuration;
using StartupElectonicWallet.DataAccess;
using StartupElectonicWallet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupElectonicWallet.Services
{
    public partial  class CustomerServices
    {
        private readonly ElectronicWalletContext _context;

        public CustomerServices(ElectronicWalletContext context)
        {
            _context = context;

        }
        public StartupElectonicWallet.Models.Common.Response AddAccount (Models.Account Account)
        {
            Models.Account account = null;
            Models.Customer customer = null;
            Models.Transaction transaction = null;
           StartupElectonicWallet.Models.Common.Response response = null;
            //Se maneja la transacción
            using (var transac = new System.Transactions.TransactionScope())
            {
                try
                {
                    response = new Models.Common.Response();
                    if (Account != null)
                    {
                        // Se colocan los valores iniciales al crear la cuenta
                        Account.DateCreate = DateTime.Now;

                        if (Account.CustomerId == 0)
                        {
                            response.DateTimeResponse = DateTime.Now;
                            response.IsSuccess = false;
                            response.Message = "Debe indicar un cliente.";
                            return response;
                        }

                        customer = (StartupElectonicWallet.Models.Customer)_context.Customer.FirstOrDefault(r => r.CustomerID == Account.CustomerId);
                        if (customer == null)
                        {
                            response.DateTimeResponse = DateTime.Now;
                            response.IsSuccess = false;
                            response.Message = "El cliente no existe.";
                            return response;
                        }


                        account = (StartupElectonicWallet.Models.Account)_context.Account.FirstOrDefault(r => r.AccountId == Account.AccountId);
                        if (account != null)
                        {
                            response.DateTimeResponse = DateTime.Now;
                            response.IsSuccess = false;
                            response.Message = "La cuenta ya existe.";
                            return response;
                        }
                            
                            _context.Account.Add(Account);
                            _context.SaveChanges();

                            if (Account.AvailableAmount != 0)
                            {
                                // se registra el monto inicial

                            }

                            response.DateTimeResponse = DateTime.Now;
                            response.IsSuccess = true;
                            response.Message = "Registro guardado.";


                        transac.Complete();
                    }
                }
                catch
                {
                    throw new Exception();
                }
            }
            return response;
        }

        public StartupElectonicWallet.Models.Account GetAccount(int AccountId)
        {
            StartupElectonicWallet.Models.Account account = null;
            try
            {
                if (AccountId != 0)
                    account = _context.Account.FirstOrDefault(r => r.AccountId == AccountId);
                if (account != null)
                {
                    account.TransactionCollection = _context.Transaction.Where(r => r.AccountId == AccountId || r.AccountToId == AccountId).ToList();
                }
            }
            catch
            {
                throw new Exception();
            }
            return account;
        }

        public List<StartupElectonicWallet.Models.Account> GetAccount()
        {
            List<StartupElectonicWallet.Models.Account> account = null;
            try
            {
                    account = _context.Account.ToList();
            }
            catch
            {
                throw new Exception();
            }
            return account;
        }

        public StartupElectonicWallet.Models.Common.Response UpdateAccount(Models.Account Account)
        {
            StartupElectonicWallet.Models.Account account = null;
            StartupElectonicWallet.Models.Common.Response response= null;
            try
            {
                response = new Models.Common.Response();
                account = (StartupElectonicWallet.Models.Account)_context.Account.FirstOrDefault(r => r.AccountId == Account.AccountId);
                if (account != null)
                {
                    response.DateTimeResponse = DateTime.Now;
                    response.IsSuccess = false;
                    response.Message = "Cuenta no encontrada.";
                    return response;
                }
                    _context.Account.Add(account);
                    _context.SaveChanges();

                    response.DateTimeResponse = DateTime.Now;
                    response.IsSuccess = true;
                    response.Message = "Registro actualizado.";

            }
            catch
            {
                throw new Exception();
            }
            return response;
        }

        public StartupElectonicWallet.Models.Common.Response AddTransaction(Models.Transaction Transaction)
        {
            StartupElectonicWallet.Models.Account account = null;
            StartupElectonicWallet.Models.Common.Response response = null;
            decimal amount = 0;
            using (var transaction = new System.Transactions.TransactionScope())
            {
                try
                {
                    response = new Models.Common.Response();
                    if (Transaction == null)
                    {
                        response.DateTimeResponse = DateTime.Now;
                        response.IsSuccess = false;
                        response.Message = "Datos no válidos.";
                        return response;
                    }

                        // TransactionType
                        // DP: Depósito
                        // RT: Retiro/Pago
                        // TF: Transferencia
                        // Validamos la cuenta
                        account = (StartupElectonicWallet.Models.Account)_context.Account.FirstOrDefault(r => r.AccountId == Transaction.AccountId);
                        // Validamos que la cuenta exista y que se encuentre activa
                        if(account!=null && account.StatusId == 1)
                        {
                            if (Transaction.TransactionType == "DP")//Deposito//AppSetting;
                            {
                                if (Transaction.AccountId!=0 && Transaction.Amount > 0)
                                {
                                        //Se registra la transacción
                                        _context.Transaction.Add(Transaction);
                                        _context.SaveChanges();
                                        amount = account.AvailableAmount + Transaction.Amount;
                                        account.AvailableAmount = amount;
                                        _context.Account.Update(account);
                                        _context.SaveChanges();

                                }
                                else
                                {
                                    //Operación no permitida
                                    response.DateTimeResponse = DateTime.Now;
                                    response.IsSuccess = false;
                                    response.Message = "Cuenta no válida o el monto de la transacción debe ser mayor a 0.";
                                    return response;
                                }

                            }
                            else if (Transaction.TransactionType == "RT")//Retiro//AppSetting;
                            {
                                amount = account.AvailableAmount - Transaction.Amount;
                                if (amount < 0)
                                {
                                    //Operación no permitida
                                    response.DateTimeResponse = DateTime.Now;
                                    response.IsSuccess = false;
                                    response.Message = "Fondos insuficientes.";
                                    return response;

                                }
                                else
                                {
                                    _context.Transaction.Add(Transaction);
                                    _context.SaveChanges();
                                    account.AvailableAmount = amount;
                                    _context.Account.Update(account);
                                    _context.SaveChanges();

                                }

                            }
                            else if (Transaction.TransactionType == "TF")//Transferencia//AppSetting;
                            {

                                    //Se revisa la cuenta de destino
                                    //
                                    if(Transaction.AccountToId== 0)
                                    {
                                        //Operación no permitida
                                        response.DateTimeResponse = DateTime.Now;
                                        response.IsSuccess = false;
                                        response.Message = "Debe indicar la cuenta de destino.";
                                        return response;
                                    }

                                    Models.Account accountTo = (StartupElectonicWallet.Models.Account)_context.Account.FirstOrDefault(r => r.AccountId == Transaction.AccountToId);
                                    // Se valida que exista y esta activa
                                    if (accountTo != null && accountTo.StatusId == 1)
                                    {
                                        decimal amountFrom = account.AvailableAmount - Transaction.Amount;

                                        if (amountFrom < 0)
                                        {
                                            //Operación no permitida
                                            response.DateTimeResponse = DateTime.Now;
                                            response.IsSuccess = false;
                                            response.Message = "Fondos insuficientes.";
                                            return response;
                                        }
                                        else
                                        {
                                            amount = accountTo.AvailableAmount + Transaction.Amount;
                                            //registramos la tranzacción
                                            _context.Transaction.Add(Transaction);
                                            _context.SaveChanges();

                                            // debito en cuenta origen
                                            account.AvailableAmount = amountFrom;
                                            _context.Account.Update(account);
                                            _context.SaveChanges();

                                            // Depositamos en la cuenta destino
                                            accountTo.AvailableAmount = amount;
                                            _context.Account.Update(accountTo);
                                            _context.SaveChanges();

                                        }

                                    }
                                    else
                                    {
                                        //Operación no permitida
                                        response.DateTimeResponse = DateTime.Now;
                                        response.IsSuccess = false;
                                        response.Message = "Cuanta no existe o se encuentra inactiva.";
                                        return response;
                                    }

                            }
                            transaction.Complete();

                            //Se preprara la respuesta
                            response.DateTimeResponse = DateTime.Now;
                            response.IsSuccess = true;
                            response.Message = "Registro procesado.";
                    }
                        else
                        {
                            //Cuenta invalida
                            response.DateTimeResponse = DateTime.Now;
                            response.IsSuccess = false;
                            response.Message = "Cuenta invalida.";
                            return response;
                        }
                }
                catch
                {
                    throw new Exception();
                }
            }
            return response;
        }
        public List<StartupElectonicWallet.Models.Transaction> GetTransaction(int accountId)
        {
            List<StartupElectonicWallet.Models.Transaction> transaction = null;
            try
            {
                if (accountId != 0)
                    transaction = _context.Transaction.Where(r => r.AccountId == accountId || r.AccountToId== accountId).ToList();
            }
            catch
            {
                throw new Exception();
            }
            return transaction;
        }
    }
}
