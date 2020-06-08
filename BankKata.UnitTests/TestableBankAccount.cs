using System.IO;

namespace BankKata.UnitTests
{
    internal class TestableBankAccount : BankAccount
    {
        internal ManualClock Clock;
        public StringWriter Printer;

        public TestableBankAccount(StringWriter printer, ManualClock clock) 
            : base(printer, clock)
        {
            Printer = printer;
            Clock = clock;
        }
    }
}
