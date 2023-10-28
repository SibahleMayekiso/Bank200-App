using Bank200_BackendDeveloper_PracticalTest.DataContext.Interfaces;
using Bank200_BackendDeveloper_PracticalTest.Services.Exceptions;

namespace Bank200_BackendDeveloper_PracticalTest.Services
{
    public class SavingsAccount : AccountService
    {
        private readonly ISystemDB _systemDB;

        public SavingsAccount(ISystemDB systemDB) : base(systemDB)
        {
            _systemDB = systemDB;
        }

        public override void Withdraw(long accountId, int amountToWithdraw)
        {
            if (!_systemDB.Accounts.ContainsKey(accountId))
            {
                throw new AccountNotFoundException("Account could not be found. Ensure the account id you entered is correct.");
            }

            if (_systemDB.Accounts[accountId].Balance < amountToWithdraw)
            {
                throw new WithdrawalAmountTooLargeException("Insuficient Funds. Please check your account ballance and try again.");
            }

            _systemDB.Accounts[accountId].Balance -= amountToWithdraw;
        }
    }
}
