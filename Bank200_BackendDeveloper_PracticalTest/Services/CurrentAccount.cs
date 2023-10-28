using Bank200_BackendDeveloper_PracticalTest.DataContext;
using Bank200_BackendDeveloper_PracticalTest.Models;

namespace Bank200_BackendDeveloper_PracticalTest.Services
{
    public class CurrentAccount: AccountService
    {
        private ISystemDB _systemDB;

        public CurrentAccount(ISystemDB systemDB)
        {
            _systemDB = systemDB;
        }

        public override void Deposit(long accountId, int amountToDeposit)
        {
            if (!_systemDB.Accounts.ContainsKey(accountId))
            {
                throw new AccountNotFoundException("Account could not be found. Ensure the account id you entered is correct.");
            }

            _systemDB.Accounts[accountId].Balance += amountToDeposit;
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
