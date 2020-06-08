using System;

namespace BankKata
{
    public class StatementFilter
    {
        private readonly Func<Transaction, bool> predicate;

        private StatementFilter(Func<Transaction, bool> predicate)
        {
            this.predicate = predicate;
        }

        public Func<Transaction, bool> Compile()
        {
            return this.predicate;
        }

        public static StatementFilter All
            => new StatementFilter((_) => true);

        public static StatementFilter DepositedMoreThan(Money amount)
            => new StatementFilter((transaction) => transaction.Amount > amount);

        public static StatementFilter WithdrawnMoreThan(Money amount)
            => new StatementFilter((transaction) => transaction.Amount < amount * new Money(-1));

        public static StatementFilter Before(DateTime timestamp)
         => new StatementFilter((transaction) => transaction.Timestamp < timestamp);
    }
}
