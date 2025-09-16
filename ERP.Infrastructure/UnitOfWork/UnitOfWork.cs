using ERP.Infrastructure.Data;
using ERP.Infrastructure.Interfaces;
using ERP.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ERP.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ERPDbContext _context;
        public IUserRepository Users { get; }
        public UnitOfWork(ERPDbContext context, IUserRepository userRepository)
        {
            _context = context;
            Users = userRepository;
            
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
