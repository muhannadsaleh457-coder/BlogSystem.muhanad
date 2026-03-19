using BlogSystem.muhanad.Shared.Dtos.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Abstractions.Mails
{
    public interface IMailService
    {
       Task<bool> SendEmail(EmailDto email);
    }
}
