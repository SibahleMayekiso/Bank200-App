using Bank200_BackendDeveloper_PracticalTest.DataContext.Interfaces;
using Bank200_BackendDeveloper_PracticalTest.Models;

namespace Bank200_BackendDeveloper_PracticalTest.DataContext
{
    public sealed class SystemDB: ISystemDB
    {
        private static SystemDB? _instance;
        private static readonly object _instanceLock = new object();

        public Dictionary<long, Account> Accounts { get; private set; }

        SystemDB()
        {
            Accounts = new Dictionary<long, Account>
            {
                { 1, new Savings { CustomerNumber = 1, Balance = 2000 } },
                { 2, new Savings { CustomerNumber = 2, Balance = 5000 } },
                { 3, new Current { CustomerNumber = 3, Balance = 1000, Overdraft = 10000 } },
                { 4, new Current { CustomerNumber = 4, Balance = -5000, Overdraft = 20000 } }
            };
        }

        public static SystemDB Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    _instance ??= new SystemDB();

                    return _instance;
                }
            }
        }

        public bool CanWithdraw(long accountId, double amount)
        {
            return Accounts.ContainsKey(accountId) && Accounts[accountId].Balance >= amount;
        }
    }
}
