using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupElectonicWallet.Models.Common
{
    public class Response
    {
        public Boolean IsSuccess { get; set; }
        public string Message { get; set; }
        public DateTime DateTimeResponse { get; set; }
    }
}
