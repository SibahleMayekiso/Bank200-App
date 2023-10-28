namespace Bank200_BackendDeveloper_PracticalTest.Services
{
    public class AccountNotFoundException: Exception
    {
        public AccountNotFoundException() { }

        public AccountNotFoundException(string message): base(message)
        {
            
        }

        public AccountNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
