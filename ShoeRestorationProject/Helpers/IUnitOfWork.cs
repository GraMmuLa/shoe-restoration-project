namespace ShoeRestorationProject.Helpers
{
    public interface IUnitOfWork<T> where T : class
    {
        public void Execute(Action action);
        public Task ExecuteAsync(Func<Task> action);
        public T Execute(Func<T> func);
        public Task<T> ExecuteAsync(Func<Task<T>> func);
        public Task ExecuteAsync(Action func);
        public Task<T> ExecuteAsync(Func<T> func);
    }
}
