using System;

namespace API.Data.Interfaces
{
    public interface ITransaction : IDisposable
    {
        void Execute(Action action);
    }
}
