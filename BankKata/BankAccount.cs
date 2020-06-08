using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankKata
{
    public class BankAccount
    {
        private readonly TextWriter printer;
        private readonly IClock clock;
        private readonly List<Transaction> transactions = new List<Transaction>();
        private int balance = 0;

        public BankAccount(TextWriter printer, IClock clock)
        {
            this.printer = printer;
            this.clock = clock;
        }

        public void Deposit(int amount)
        {
            this.balance += amount;
            this.transactions.Add(new Transaction(this.clock.Now, amount, balance));
        }

        public void Withdraw(int amount)
        {
            this.balance -= amount;
            this.transactions.Add(new Transaction(this.clock.Now, amount * -1, balance));
        }

        public void Transfer(BankAccount account, int amount)
        {
            this.Withdraw(amount);
            account.Deposit(amount);
        }

        public void PrintStatement(StatementFilter filter)
        {
            var predicate = filter.Compile();
            this.printer.WriteLine("DATE         AMOUNT   BALANCE");

            foreach (var transaction in this.transactions.Where(predicate))
            {
                this.printer.WriteLine(transaction.ToString());
            }
            this.printer.Flush();
        }
    }

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

        public static StatementFilter DepositedMoreThan(int amount) 
            => new StatementFilter((transaction) => transaction.Amount > amount);

        public static StatementFilter WithdrawnMoreThan(int amount)
            => new StatementFilter((transaction) => transaction.Amount < amount * -1);

        public static StatementFilter Before(DateTime timestamp)
         => new StatementFilter((transaction) => transaction.Timestamp < timestamp);
    }
}
