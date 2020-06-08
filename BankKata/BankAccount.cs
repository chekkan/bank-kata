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
        private readonly Stack<Transaction> transactions = new Stack<Transaction>();
        private int balance = 0;

        public BankAccount(TextWriter printer, IClock clock)
        {
            this.printer = printer;
            this.clock = clock;
        }

        public void Deposit(int amount)
        {
            this.balance += amount;
            this.transactions.Push(new Transaction(this.clock.Now, amount, balance));
        }

        public void Withdraw(int amount)
        {
            this.balance -= amount;
            this.transactions.Push(new Transaction(this.clock.Now, amount * -1, balance));
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
