using Bank200_BackendDeveloper_PracticalTest.DataContext.Interfaces;
using Bank200_BackendDeveloper_PracticalTest.Models;
using Bank200_BackendDeveloper_PracticalTest.Services;
using Bank200_BackendDeveloper_PracticalTest.Services.Exceptions;
using NSubstitute;

namespace Bank200_BackendDeveloper_PracticalTest.Tests
{
    [TestFixture]
    public class CurrentAccountTests
    {
        private CurrentAccount _currentAccount;
        private ISystemDB _systemDb;
        private Dictionary<long, Account> _mockSystemAccounts;

        [SetUp]
        public void Setup()
        {
            _systemDb = Substitute.For<ISystemDB>();
            _currentAccount = new CurrentAccount(_systemDb);

            _mockSystemAccounts = new Dictionary<long, Account>
            {
                { 1, new Savings { CustomerNumber = 1, Balance = 2000 } },
                { 2, new Savings { CustomerNumber = 2, Balance = 5000 } },
                { 3, new Current { CustomerNumber = 3, Balance = 1000, Overdraft = 10000 } },
                { 4, new Current { CustomerNumber = 4, Balance = -5000, Overdraft = 20000 } }
            };
        }

        [Test]
        public void Withdraw_WhenAccountExists_DecreaseAccountBalance()
        {
            //Arrange
            var customerNumber = 3;
            var amountToWithdraw = 100;
            var initialBalance = _mockSystemAccounts[customerNumber].Balance;

            _systemDb.Accounts.Returns(_mockSystemAccounts);

            //Act
            _currentAccount.Withdraw(customerNumber, amountToWithdraw);
            var currentBalance = _systemDb.Accounts[customerNumber].Balance;

            //Assert
            Assert.Less(currentBalance, initialBalance);
        }

        [Test]
        public void Withdraw_WhenAccountDoesNotExist_ThrowAccountNotFoundException()
        {
            //Arrange
            var customerNumber = 1000000000;
            var amountToWithdraw = 100;

            _systemDb.Accounts.Returns(_mockSystemAccounts);

            //Assert
            Assert.Throws<AccountNotFoundException>(() => _currentAccount.Withdraw(customerNumber, amountToWithdraw));
        }

        [TestCase(3)]
        [TestCase(4)]
        public void Withdraw_WhenWithdrawalAmountViolatesOverdraftLimit_ThrowWithdrawalAmountTooLargeException(int accountId)
        {
            //Arrange
            var customerNumber = _mockSystemAccounts[accountId].CustomerNumber;
            var amountToWithdraw = 16000;

            _systemDb.Accounts.Returns(_mockSystemAccounts);

            //Assert
            Assert.Throws<WithdrawalAmountTooLargeException>(() => _currentAccount.Withdraw(customerNumber, amountToWithdraw));
        }

        [Test]
        public void Deposit_WhenAccountExists_IncreaseAccountBalance()
        {
            //Arrange
            var customerNumber = 4;
            var amountToDeposit = 100;
            var initialBalance = _mockSystemAccounts[customerNumber].Balance;

            _systemDb.Accounts.Returns(_mockSystemAccounts);

            //Act
            _currentAccount.Deposit(customerNumber, amountToDeposit);
            var currentBalance = _systemDb.Accounts[customerNumber].Balance;

            //Assert
            Assert.Greater(currentBalance, initialBalance);
        }

        [Test]
        public void Deposit_WhenAccountDoesNotExist_ThrowAccountNotFoundException()
        {
            //Arrange
            var customerNumber = 1000000000;
            var amountToDeposit = 100;

            _systemDb.Accounts.Returns(_mockSystemAccounts);

            //Assert
            Assert.Throws<AccountNotFoundException>(() => _currentAccount.Deposit(customerNumber, amountToDeposit));
        }
    }
}