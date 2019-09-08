using System;
using API.Data.Interfaces;

namespace API.Data.Implementation
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;

        public void Dispose()
        {
            _unitOfWork.Save();
        }

        public Transaction(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Execute(Action action)
        {
            try
            {
                action();
                _unitOfWork.CanCommit = true;
            }
            catch (Exception)
            {
                _unitOfWork.CanCommit = false;
            }
        }
    }
}
