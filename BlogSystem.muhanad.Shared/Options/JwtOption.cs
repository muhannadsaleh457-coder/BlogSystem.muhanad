using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Shared.Options
{
    public class JwtOption
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double Lifetime { get; set; }
        public string SigningKey { get; set; }
        
    }
}
