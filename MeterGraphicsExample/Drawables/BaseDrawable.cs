namespace MeterGraphicsExample.Drawables;
public abstract class BaseDrawable : IDrawable
{
    public abstract void Draw(ICanvas canvas, RectF dirtyRect); 
}
