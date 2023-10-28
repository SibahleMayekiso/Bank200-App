using Bank200_BackendDeveloper_PracticalTest.DataContext;
using Bank200_BackendDeveloper_PracticalTest.Services.Interfaces;

namespace Bank200_BackendDeveloper_PracticalTest.Services
{
    public abstract class AccountService: IAccountService
    {
        //During Refactoring add default implementation here since Savings and Currnet accounts have the same Deposit logic
        public abstract void Deposit(long accountId, int amountToDeposit);

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
