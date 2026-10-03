namespace ShoeRestorationProject.Helpers
{
    public interface IUnitOfWork
    {
        public void Execute(Action action);
        public Task ExecuteAsync(Func<Task> action);
        public T Execute<T>(Func<T> func);
        public Task<T> ExecuteAsync<T>(Func<Task<T>> func);
        public Task ExecuteAsync(Action func);
        public Task<T> ExecuteAsync<T>(Func<T> func);
    }
}
