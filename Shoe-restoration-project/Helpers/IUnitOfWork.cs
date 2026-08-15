namespace Shoe_restoration_project.Helpers
{
    public interface IUnitOfWork
    {
        public void Execute(Action action);
    }
}
