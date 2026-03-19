using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Domain.Exceptions.BadRequest.Auth
{
    public class LoginBadRequest() : BadRequestException("Invalid Login !!")
    {
    }
}
