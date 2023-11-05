namespace Project.Core.Domain.User
{
    public record UserId(int value)
    {
        public int Value { get; private set; } = Validate(value);

        private static int Validate(int value)
        {
            return value;
        }
    }
}
