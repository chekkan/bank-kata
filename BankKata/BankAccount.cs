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
        private Money balance = new Money(0);

        public BankAccount(TextWriter printer, IClock clock)
        {
            this.printer = printer;
            this.clock = clock;
        }

        public void Deposit(Money amount)
        {
            this.balance += amount;
            Transaction item = new Transaction(this.clock.Now, amount, balance);
            this.transactions.Add(item);
        }

        public void Withdraw(Money amount)
        {
            this.balance -= amount;
            Transaction item = new Transaction(this.clock.Now, amount * new Money(-1), balance);
            this.transactions.Add(item);
        }

        public void Transfer(BankAccount account, Money amount)
        {
            this.Withdraw(amount);
            account.Deposit(amount);
        }

        public void PrintStatement()
        {
            PrintStatement(StatementFilter.All);
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
}
