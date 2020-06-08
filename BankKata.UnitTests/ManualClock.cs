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

        public void Advance(int days)
        {
            this.now = this.now.AddDays(days);
        }

        internal void Change(DateTime now)
        {
            this.now = now;
        }
    }
}