using System.Runtime.InteropServices;

namespace Bank200_BackendDeveloper_PracticalTest.Services.Interfaces
{
    public interface IAccountService
    {
        public void OpenSavingsAccount(long accountId, long amountToDeposit);
        public void OpenCurrentAccount(long accountId);
        public void Withdraw(long accountId, int amountToWithdraw);
        public void Deposit(long accountId, int amountToDeposit);
    }
}
