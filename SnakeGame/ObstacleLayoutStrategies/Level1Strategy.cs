using SnakeGame.ObstacleLayoutStrategies.Interfaces;

namespace SnakeGame.ObstacleLayoutStrategies
{
    public class Level1Strategy : LayoutStrategyBase, ILayoutDrawer
    {
        public void Draw()
        {
            DrawBoundary();
        }        
    }
}
