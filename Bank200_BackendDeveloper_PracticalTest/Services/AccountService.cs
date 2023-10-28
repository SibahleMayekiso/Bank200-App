using Bank200_BackendDeveloper_PracticalTest.DataContext.Interfaces;
using Bank200_BackendDeveloper_PracticalTest.Services.Exceptions;
using Bank200_BackendDeveloper_PracticalTest.Services.Interfaces;

namespace Bank200_BackendDeveloper_PracticalTest.Services
{
    public abstract class AccountService: IAccountService
    {
        private readonly ISystemDB _systemDB;

        protected AccountService(ISystemDB systemDB)
        {
            _systemDB = systemDB;
        }

        public virtual void Deposit(long accountId, int amountToDeposit)
        {
            if (!_systemDB.Accounts.ContainsKey(accountId))
            {
                throw new AccountNotFoundException("Account could not be found. Ensure the account id you entered is correct.");
            }

            _systemDB.Accounts[accountId].Balance += amountToDeposit;
        }

        //OpenCurrentAccount not implementeted becuase it is stated that we will only implement the “withdraw” and “deposit” functionality.
        public virtual void OpenCurrentAccount(long accountId)
        {
            throw new NotImplementedException();
        }

        //OpenSavingsAccount not implementeted becuase it is stated that we will only implement the “withdraw” and “deposit” functionality.
        public virtual void OpenSavingsAccount(long accountId, long amountToDeposit)
        {
            throw new NotImplementedException();
        }

        public abstract void Withdraw(long accountId, int amountToWithdraw);
    }
}
