using System;
using API.Data.Interfaces;

namespace API.Data.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly APIDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(APIDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            CanCommit = true;
        }

        public bool CanCommit { get; set; }

        public TRepo GetRepository<TRepo>()
            where TRepo : class
        {
            return _serviceProvider.GetService(typeof(TRepo)) as TRepo;
        }

        public ITransaction CreateTransaction()
        {
            CanCommit = false;
            return new Transaction(this);
        }

        public void Save()
        {
            if (CanCommit)
            {
                _context.SaveChanges();
            }
        }
    }
}
