namespace SnakeGame.Source.Contexts
{
    public interface IContext<T> where T: class
    {
        T Get(int level);
    }
}
