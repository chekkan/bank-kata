using System;

namespace BankKata.UnitTests
{
    internal class ManualClock : IClock
    {
        private DateTime now;

        public ManualClock(DateTime now)
        {
            this.now = now;
        }

        public DateTime Now => this.now;
    }
}