using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Domain.Exceptions.NotFound.Posts
{
    public class CategoryNotFound(int id) : NotFoundException($"Category With Id {id} Not Found !!")
    {
    }
}
