using System;
using System.Collections.Generic;
using System.IO;

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

        public void PrintStatement()
        {
            this.printer.WriteLine("DATE         AMOUNT   BALANCE");

            foreach (var transaction in this.transactions)
            {
                this.printer.WriteLine(transaction.ToString());
            }
            this.printer.Flush();
        }
    }
}
