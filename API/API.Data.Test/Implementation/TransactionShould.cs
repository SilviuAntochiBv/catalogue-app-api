using System;
using Moq;
using API.Data.Implementation;
using API.Data.Interfaces;
using Xunit;

namespace API.Data.Test.Implementation
{
    public class TransactionShould
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly ITransaction _transaction;

        public TransactionShould()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _transaction = new Transaction(_unitOfWork.Object);
        }

        [Fact]
        public void CallUnitOfWorkSaveWhenDisposing()
        {
            // act
            _transaction.Dispose();

            // assert
            _unitOfWork.Verify(uow => uow.Save(), Times.Once);
        }

        [Fact]
        public void ExecutePassedAction()
        {
            // arrange
            var mockedAction = new Mock<Action>();

            // act
            _transaction.Execute(mockedAction.Object);

            // assert
            mockedAction.Verify(function => function(), Times.Once);
        }

        [Fact]
        public void SetTheCanCommitFlagToTrueWhenPassedActionIsCompletedSuccessfully()
        {
            // arrange
            var mockedAction = new Mock<Action>();

            // act
            _transaction.Execute(mockedAction.Object);

            // assert
            _unitOfWork.VerifySet(uow => { uow.CanCommit = true; });
        }

        [Fact]
        public void SetTheCanCommitFlagToFalseWhenPassedActionIsFailing()
        {
            // arrange
            var mockedAction = new Mock<Action>();
            mockedAction.Setup(act => act()).Throws<Exception>();

            // act
            _transaction.Execute(mockedAction.Object);

            // assert
            _unitOfWork.VerifySet(uow => { uow.CanCommit = false; });
        }
    }
}
