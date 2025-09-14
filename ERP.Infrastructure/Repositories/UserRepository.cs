using ERP.Infrastructure.Data;
using ERP.Infrastructure.Entities;
using ERP.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ERPDbContext _context;
        public UserRepository(ERPDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
