namespace Runtime.Interfaces
{
    public interface ICommand
    {
        public void Execute(int param)
        {
        }

        public void Undo()
        {
        }
    }
}