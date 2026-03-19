using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Shared.Dtos.Exceptions
{
    public class ExceptionResponse
    {
        public string message { get; set; }
        public int statusCode { get; set; }
    }
}
