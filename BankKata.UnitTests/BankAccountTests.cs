using System;
using System.IO;
using System.Text;
using Xunit;

namespace BankKata.UnitTests
{
    public class BankAccountTests
    {
        [Theory]
        [InlineData("2014-04-10", 500)]
        [InlineData("2014-04-10", 1000)]
        [InlineData("2020-07-12", 1401)]
        public void PrintStatementAfterDeposit(string date, int amount)
        {
            DateTime now = DateTime.Parse(date);
            var clock = new ManualClock(now);
            string amountStr = $"{amount}.00".PadLeft(7);
            string depositStr = $"{amount}.00".PadLeft(9);
            var expected = new StringBuilder();
            expected.AppendLine("DATE         AMOUNT   BALANCE");
            expected.AppendLine($"{now:d}  {amountStr} {depositStr}");
            var printer = new StringWriter();
            var sut = new BankAccount(printer, clock);
            sut.Deposit(amount);
            sut.PrintStatement();
            var actual = printer.GetStringBuilder();
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

            var printer = new StringWriter();
            var clock = new ManualClock(now);
            var sut = new BankAccount(printer, clock);
            sut.Deposit(100);
            clock.Advance(20);
            sut.Deposit(1100);
            sut.PrintStatement();
            var actual = printer.GetStringBuilder();
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        [Fact]
        public void WithdrawThenPrintStatement()
        {
            var now = DateTime.Parse("2019-11-23");
            var expected = new StringBuilder();
            expected.AppendLine("DATE         AMOUNT   BALANCE");
            expected.AppendLine("23/11/2019   100.00    100.00");
            expected.AppendLine("24/11/2019   -90.00     10.00");
            var clock = new ManualClock(now);
            var printer = new StringWriter();
            var sut = new BankAccount(printer, clock);
            sut.Deposit(100);
            clock.Advance(1);
            sut.Withdraw(90);
            sut.PrintStatement();
            var actual = printer.GetStringBuilder();
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
            var clock = new ManualClock(DateTime.Parse("2019-11-24"));
            var printer = new StringWriter();
            var sut = new BankAccount(printer, clock);
            var anotherAccount = new BankAccount(printer, clock);
            
            sut.Deposit(200);
            sut.Transfer(anotherAccount, 100);
            sut.PrintStatement();
            anotherAccount.PrintStatement();
            
            var actual = printer.GetStringBuilder();
            Assert.Equal(expected.ToString(), actual.ToString());
            System.Console.Write(actual.ToString());
        }
    }
}
