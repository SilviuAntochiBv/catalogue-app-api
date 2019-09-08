namespace API.Data.Interfaces
{
    public interface IUnitOfWork
    {
        TRepo GetRepository<TRepo>()
            where TRepo : class;

        ITransaction CreateTransaction();

        void Save();

        bool CanCommit { get; set; }
    }
}
