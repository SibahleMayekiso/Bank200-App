using Bank200_BackendDeveloper_PracticalTest.DataContext.Interfaces;
using Bank200_BackendDeveloper_PracticalTest.Models;
using Bank200_BackendDeveloper_PracticalTest.Services;
using Bank200_BackendDeveloper_PracticalTest.Services.Exceptions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Bank200_BackendDeveloper_PracticalTest.Tests
{
    [TestFixture]
    public class SavingsAccountTests
    {
        private SavingsAccount _savingsAccount;
        private ISystemDB _systemDb;
        private Dictionary<long, Account> _mockSystemAccounts;

        [SetUp]
        public void Setup()
        {
            _systemDb = Substitute.For<ISystemDB>();
            _savingsAccount = new SavingsAccount(_systemDb);

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
            var customerNumber = 1;
            var amountToWithdraw = 100;
            var initialBalance = _mockSystemAccounts[customerNumber].Balance;

            _systemDb.Accounts.Returns(_mockSystemAccounts);

            //Act
            _savingsAccount.Withdraw(customerNumber, amountToWithdraw);
            var currentBalance = _systemDb.Accounts[customerNumber].Balance;

            //Assert
            Assert.Less(currentBalance, initialBalance);
        }

        [Test]
        public void Withdraw_WhenAccountExists_AccountBalanceIsGreaterThan1000()
        {
            //Arrange
            var customerNumber = 1;
            var amountToWithdraw = 1000;
            var minimumAccountBalance = 1000;

            _systemDb.Accounts.Returns(_mockSystemAccounts);

            //Act
            _savingsAccount.Withdraw(customerNumber, amountToWithdraw);
            var currentBalance = _systemDb.Accounts[customerNumber].Balance;

            //Assert
            Assert.GreaterOrEqual(currentBalance, minimumAccountBalance);
        }

        [Test]
        public void Withdraw_WhenAccountDoesNotExist_ThrowAccountNotFoundException()
        { 
            //Arrange
            var customerNumber = 1000000000;
            var amountToWithdraw = 100;

            _systemDb.Accounts.Returns(_mockSystemAccounts);

            //Assert
            Assert.Throws<AccountNotFoundException>(() => _savingsAccount.Withdraw(customerNumber, amountToWithdraw));
        }

        [Test]
        public void Withdraw_WhenWithdrawalAmountViolatesMinimumBalance_ThrowWithdrawalAmountTooLargeException()
        {
            //Arrange
            var customerNumber = 1;
            var amountToWithdraw = 2100;

            _systemDb.Accounts.Returns(_mockSystemAccounts);

            //Assert
            Assert.Throws<WithdrawalAmountTooLargeException>(() => _savingsAccount.Withdraw(customerNumber, amountToWithdraw));
        }

        [Test]
        public void Deposit_WhenAccountExists_IncreaseAccountBalance()
        {
            //Arrange
            var customerNumber = 1;
            var amountToDeposit = 100;
            var initialBalance = _mockSystemAccounts[customerNumber].Balance;

            _systemDb.Accounts.Returns(_mockSystemAccounts);

            //Act
            _savingsAccount.Deposit(customerNumber, amountToDeposit);
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
            Assert.Throws<AccountNotFoundException>(() => _savingsAccount.Deposit(customerNumber, amountToDeposit));
        }
    }
}