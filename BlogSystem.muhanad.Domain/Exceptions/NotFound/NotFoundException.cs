using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Domain.Exceptions.NotFound
{
    public class NotFoundException(string message) : Exception(message)
    {
    }
}
