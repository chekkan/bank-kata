using System;
using System.IO;

namespace BankKata
{
    public class BankAccount
    {
        private readonly TextWriter printer;
        private readonly IClock clock;
        private int amount;

        public BankAccount(TextWriter printer, IClock clock)
        {
            this.printer = printer;
            this.clock = clock;
        }

        public void Deposit(int amount)
        {
            this.amount = amount;
        }

        public void PrintStatement()
        {
            var formattedAmount = $"{amount}.00".PadLeft(7);
            this.printer.WriteLine("DATE         AMOUNT   BALANCE");
            this.printer.WriteLine($"{this.clock.Now.ToString("d")}   {formattedAmount} {formattedAmount}");
            this.printer.Flush();
        }
    }
}
