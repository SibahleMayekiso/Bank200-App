namespace Bank200_BackendDeveloper_PracticalTest.Services
{
    public class WithdrawalAmountTooLargeException: Exception
    {
        public WithdrawalAmountTooLargeException() { }

        public WithdrawalAmountTooLargeException(string message) : base(message)
        {

        }

        public WithdrawalAmountTooLargeException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
