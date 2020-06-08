using System;
using System.IO;
using System.Text;
using Xunit;

namespace BankKata.UnitTests
{
    public class BankAccountTests
    {
        private readonly TestableBankAccount sut;
        public BankAccountTests()
        {
            sut = new TestableBankAccount(new StringWriter(), new ManualClock(DateTime.Parse("2019-11-24")));
        }
        
        [Theory]
        [InlineData("2014-04-10", 500)]
        [InlineData("2014-04-10", 1000)]
        [InlineData("2020-07-12", 1401)]
        public void PrintStatementAfterDeposit(string date, int amount)
        {
            DateTime now = DateTime.Parse(date);
            sut.Clock.Change(now);
            string amountStr = $"{amount}.00".PadLeft(7);
            string depositStr = $"{amount}.00".PadLeft(9);
            var expected = new StringBuilder();
            expected.AppendLine("DATE         AMOUNT   BALANCE");
            expected.AppendLine($"{now:d}  {amountStr} {depositStr}");
            sut.Deposit(amount);
            sut.PrintStatement();
            var actual = sut.Printer.GetStringBuilder();
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        [Fact]
        public void MultipleDeposits()
        {
            var now = DateTime.Parse("2019-12-01");
            var later = DateTime.Parse("2019-12-21");
            var expected = new StringBuilder();
            expected.AppendLine("DATE         AMOUNT   BALANCE");
            expected.AppendLine($"{now:d}   100.00    100.00");
            expected.AppendLine($"{later:d}  1100.00   1200.00");

            sut.Clock.Change(now);
            sut.Deposit(100);
            sut.Clock.Change(later);
            sut.Deposit(1100);
            sut.PrintStatement();
            var actual = sut.Printer.GetStringBuilder();
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        [Fact]
        public void WithdrawThenPrintStatement()
        {
            var expected = new StringBuilder();
            expected.AppendLine("DATE         AMOUNT   BALANCE");
            expected.AppendLine("24/11/2019   100.00    100.00");
            expected.AppendLine("25/11/2019   -90.00     10.00");
            sut.Deposit(100);
            sut.Clock.Advance(1);
            sut.Withdraw(90);
            sut.PrintStatement();
            var actual = sut.Printer.GetStringBuilder();
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        [Fact]
        public void TransferToAnotherAccount()
        {
            var expected = new StringBuilder();
            expected.AppendLine("DATE         AMOUNT   BALANCE");
            expected.AppendLine("24/11/2019   200.00    200.00");
            expected.AppendLine("24/11/2019  -100.00    100.00");
            expected.AppendLine("DATE         AMOUNT   BALANCE");
            expected.AppendLine("24/11/2019   100.00    100.00");
            var anotherAccount = new BankAccount(sut.Printer, sut.Clock);
            
            sut.Deposit(200);
            sut.Transfer(anotherAccount, 100);
            sut.PrintStatement();
            anotherAccount.PrintStatement();
            
            var actual = sut.Printer.GetStringBuilder();
            Assert.Equal(expected.ToString(), actual.ToString());
            System.Console.Write(actual.ToString());
        }
    }
}
