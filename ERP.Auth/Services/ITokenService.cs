using ERP.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Auth.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
