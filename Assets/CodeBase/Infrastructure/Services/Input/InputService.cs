namespace CodeBase.Infrastructure.Service.InputService
{
    public abstract class InputService : IInputService
    {
        protected const string HORIZONTAL = "Horizontal";

        public abstract float X { get; }
        public abstract bool IsAttacking();
    }
}