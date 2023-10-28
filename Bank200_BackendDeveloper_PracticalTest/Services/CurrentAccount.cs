using Bank200_BackendDeveloper_PracticalTest.DataContext.Interfaces;
using Bank200_BackendDeveloper_PracticalTest.Models;
using Bank200_BackendDeveloper_PracticalTest.Services.Exceptions;

namespace Bank200_BackendDeveloper_PracticalTest.Services
{
    public class CurrentAccount: AccountService
    {
        private readonly ISystemDB _systemDB;

        public CurrentAccount(ISystemDB systemDB) : base(systemDB)
        {
            _systemDB = systemDB;
        }

        public override void Withdraw(long accountId, int amountToWithdraw)
        {
            if (!_systemDB.Accounts.ContainsKey(accountId))
            {
                throw new AccountNotFoundException("Account could not be found. Ensure the account id you entered is correct.");
            }

            if (_systemDB.Accounts[accountId] is Current current && amountToWithdraw > current.Balance + current.Overdraft)
            {
                throw new WithdrawalAmountTooLargeException("Insuficient Funds. Please check your account ballance and try again.");
            }

            _systemDB.Accounts[accountId].Balance -= amountToWithdraw;
        }
    }
}
