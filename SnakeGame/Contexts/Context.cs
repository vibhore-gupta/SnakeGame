using System.Collections.Generic;

namespace SnakeGame.Source.Contexts
{
    public abstract class Context<T> : IContext<T> where T : class
    {
        protected Dictionary<int, T> Mappings;
        public abstract T Get(int level);
    }
}
