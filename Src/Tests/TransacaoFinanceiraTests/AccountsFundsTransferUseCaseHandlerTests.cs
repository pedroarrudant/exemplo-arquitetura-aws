using Application.Shared.Repositories.Interfaces;
using Application.UseCases.AccountsFundsTransfer.Constants;
using Application.UseCases.AccountsFundsTransfer.Repository.Interfaces;
using Application.UseCases.AccountsFundsTransfer.UseCase;
using Moq;
using TransacaoFinanceiraTests.Static;

namespace TransacaoFinanceiraTests
{
    public class AccountsFundsTransferUseCaseHandlerTests
    {
        private readonly Mock<IAccountsFundsTransferRepository> _transferRepositoyMock;
        private readonly Mock<IBalanceRepository> _balanceRepositoyMock;

        public AccountsFundsTransferUseCaseHandlerTests()
        {
            _transferRepositoyMock = new Mock<IAccountsFundsTransferRepository> ();
            _balanceRepositoyMock = new Mock<IBalanceRepository> ();
        }

        [Fact]
        public async void Should_Create_Transaction_Validating_Balance()
        {
            //Arrange
            var originAccount = AccountBalanceStatic.GetAOriginAccountBalance;
            var targetAccount = AccountBalanceStatic.GetATargetAccountBalance;
            var transaction = TransactionStatic.GetASingleTransaction;

            _transferRepositoyMock
                .Setup(m => m.CreateTransaction(It.IsAny<int>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<decimal>()))
                .ReturnsAsync(transaction);

            _balanceRepositoyMock.Setup(m => m.GetAccountBalance(1)).ReturnsAsync(originAccount);
            _balanceRepositoyMock.Setup(m => m.GetAccountBalance(2)).ReturnsAsync(targetAccount);

            var handler = new AccountsFundsTransferUseCaseHandler(_transferRepositoyMock.Object, _balanceRepositoyMock.Object);

            //Act
            var result = await handler.CreateTransaction(1, 1, 2, transaction.Valor);

            //Assert
            Assert.Equal(nameof(AccountsFundsTransferMessages.SuccessOperation), result?.Code);
            _balanceRepositoyMock.Verify(repo => repo.UpdateBalance(originAccount.Conta, 0), Times.Once);
            _balanceRepositoyMock.Verify(repo => repo.UpdateBalance(targetAccount.Conta, 2000m), Times.Once);
        }

        [Fact]
        public async void Should_Not_Create_Transaction_No_Funds_Error()
        {
            //Arrange
            _transferRepositoyMock
                .Setup(m => m.CreateTransaction(It.IsAny<int>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<decimal>()))
                .ReturnsAsync(TransactionStatic.GetASingleTransaction);

            _balanceRepositoyMock.Setup(m => m.GetAccountBalance(1)).ReturnsAsync(AccountBalanceStatic.GetAOriginAccountBalance);
            _balanceRepositoyMock.Setup(m => m.GetAccountBalance(2)).ReturnsAsync(AccountBalanceStatic.GetATargetAccountBalance);

            var handler = new AccountsFundsTransferUseCaseHandler(_transferRepositoyMock.Object, _balanceRepositoyMock.Object);

            //Act
            var result = await handler.CreateTransaction(1, 1, 2, 2000);

            //Assert
            Assert.Equal(nameof(AccountsFundsTransferMessages.NoFundsError), result?.Code);
        }
    }
}