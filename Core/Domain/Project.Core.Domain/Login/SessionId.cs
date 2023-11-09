namespace Project.Core.Domain.Login
{
    public record SessionId(Guid Value)
    {
        public Guid Value { get; private set; } = Validate(Value);

        private static Guid Validate(Guid value)
        {
            return value;
        }
    }
}
