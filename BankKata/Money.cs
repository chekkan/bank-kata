namespace BankKata
{
    public class Money
    {
        private readonly int amount;
        public Money(int amount)
        {
            this.amount = amount;
        }

        public static Money operator +(Money a, Money b)
            => new Money(a.amount + b.amount);

        public static Money operator -(Money a, Money b)
            => new Money(a.amount - b.amount);

        public static Money operator *(Money a, Money b)
            => new Money(a.amount * b.amount);

        public static bool operator >(Money a, Money b)
            => a.amount > b.amount;

        public static bool operator <(Money a, Money b)
            => a.amount < b.amount;

        public override string ToString()
            => $"{this.amount}.00";
    }
}
