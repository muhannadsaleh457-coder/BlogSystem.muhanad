using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Domain.Exceptions.NotFound.Auth
{
    public class UserNotFound(string email) : NotFoundException($"User With Email {email} Not Found !!")
    {
    }
}
