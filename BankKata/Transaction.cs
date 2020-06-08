using System;

namespace BankKata
{
    public class Transaction
    {
        public Transaction(DateTime timestamp, Money amount, Money balance)
        {
            Timestamp = timestamp;
            Amount = amount;
            this.balance = balance;
        }

        public DateTime Timestamp { get; }

        public Money Amount { get; }
        private Money balance { get; }

        public override string ToString()
        {
            var formattedAmount = $"{this.Amount}".PadLeft(7);
            var formattedBalance = $"{this.balance}".PadLeft(9);
            return $"{this.Timestamp.ToString("d")}  {formattedAmount} {formattedBalance}";
        }
    }
}