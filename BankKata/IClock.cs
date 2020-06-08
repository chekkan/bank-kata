using System;

namespace BankKata
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}