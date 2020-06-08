using System;

namespace BankKata
{
    internal class Transaction
    {
        public Transaction(DateTime timestamp, int amount, int balance)
        {
            Timestamp = timestamp;
            Amount = amount;
            Balance = balance;
        }

        public DateTime Timestamp { get; }
        public int Amount { get; }
        public int Balance { get; }

        public override string ToString()
        {
            var formattedAmount = $"{this.Amount}.00".PadLeft(7);
            var formattedBalance = $"{this.Balance}.00".PadLeft(7);
            return $"{this.Timestamp.ToString("d")}  {formattedAmount} {formattedBalance}";
        }
    }
}