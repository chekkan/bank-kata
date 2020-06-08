using System;
using System.IO;
using System.Text;
using Xunit;

namespace BankKata.UnitTests
{
    public class BankAccountTests
    {
        [Theory]
        [InlineData("10-04-2014", 500)]
        [InlineData("10-04-2014", 1000)]
        [InlineData("12-07-2020", 1401)]
        public void PrintStatementAfterDeposit(string date, int amount)
        {
            DateTime now = DateTime.Parse(date);
            var clock = new ManualClock(now);
            string amountStr = $"{amount}.00".PadLeft(7);
            var expected = new StringBuilder();
            expected.AppendLine("DATE         AMOUNT   BALANCE");
            expected.AppendLine($"{now.ToString("d")}   {amountStr} {amountStr}");
            var printer = new StringWriter();
            var sut = new BankAccount(printer, clock);
            sut.Deposit(amount);
            sut.PrintStatement();
            var actual = printer.GetStringBuilder();
            Assert.Equal(expected.ToString(), actual.ToString());
        }
    }
}
