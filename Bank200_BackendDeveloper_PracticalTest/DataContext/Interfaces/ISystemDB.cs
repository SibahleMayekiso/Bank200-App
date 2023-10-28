using Bank200_BackendDeveloper_PracticalTest.Models;

namespace Bank200_BackendDeveloper_PracticalTest.DataContext.Interfaces
{
    public interface ISystemDB
    {
        Dictionary<long, Account> Accounts { get; }
        bool CanWithdraw(long accountId, double amount);
    }
}
