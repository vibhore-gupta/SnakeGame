using SnakeGame.Source.ObstacleLayoutStrategies.Interfaces;

namespace SnakeGame.Source.ObstacleLayoutStrategies
{
    public class Level1Strategy : LayoutStrategyBase, ILayoutDrawer
    {
        public void Draw()
        {
            DrawBoundary();
        }        
    }
}
