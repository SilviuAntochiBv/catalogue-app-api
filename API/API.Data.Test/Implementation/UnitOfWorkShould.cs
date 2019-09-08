using System;
using Moq;
using API.Data.Implementation;
using API.Data.Interfaces;
using API.Data.Interfaces.Specific;
using Xunit;

namespace API.Data.Test.Implementation
{
    public class UnitOfWorkShould
    {
        private readonly Mock<APIDbContext> _mockedDbContext;
        private readonly Mock<IServiceProvider> _mockedServiceProvider;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkShould()
        {
            _mockedDbContext = new Mock<APIDbContext>();
            _mockedServiceProvider = new Mock<IServiceProvider>();
            _unitOfWork = new UnitOfWork(_mockedDbContext.Object, _mockedServiceProvider.Object);
        }

        [Fact]
        public void SetTheCanCommitToTrueWhenCallingConstructor()
        {
            // assert
            Assert.True(_unitOfWork.CanCommit);
        }

        [Fact]
        public void CallDbContextWhenSaveIsTriggered()
        {
            // act
            _unitOfWork.Save();

            // assert
            _mockedDbContext.Verify(dbContext => dbContext.SaveChanges(), Times.Once);
        }

        [Fact]
        public void CallServiceProviderWhenGettingRepository()
        {
            // act
            _unitOfWork.GetRepository<IExampleRepository>();

            // assert
            _mockedServiceProvider.Verify(sp => sp.GetService(typeof(IExampleRepository)), Times.Once);
        }

        [Fact]
        public void CreateNewTransactionObjectWhenCallingCreateTransactionMethod()
        {
            // act
            var transaction = _unitOfWork.CreateTransaction();

            // assert
            Assert.NotNull(transaction);
        }

        [Fact]
        public void SetTheCanCommitFlagToFalseWhenANewTransactionIsCreated()
        {
            // act
            _unitOfWork.CreateTransaction();

            // assert
            Assert.False(_unitOfWork.CanCommit);
        }


        [Fact]
        public void CallSaveMethodWhenCanCommitIsSetToTrue()
        {
            // arrange
            _unitOfWork.CanCommit = true;

            // act
            _unitOfWork.Save();

            // assert
            _mockedDbContext.Verify(dbContext => dbContext.SaveChanges(), Times.Once);
        }

        [Fact]
        public void NotCallSaveMethodWhenCanCommitIsSetToFalse()
        {
            // arrange
            _unitOfWork.CanCommit = false;

            // act
            _unitOfWork.Save();

            // assert
            _mockedDbContext.Verify(dbContext => dbContext.SaveChanges(), Times.Never);
        }
    }
}
