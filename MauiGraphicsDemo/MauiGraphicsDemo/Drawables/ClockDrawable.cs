namespace MauiGraphicsDemo.Drawables;

public class ClockDrawable : IDrawable
{

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        DateTime curTime = DateTime.Now;
        var clockCenterPoint = new PointF(0, 0); 

        canvas.StrokeColor = Colors.Aqua;
        canvas.StrokeSize = 6;

        canvas.DrawCircle(clockCenterPoint, 100);
        canvas.DrawCircle(clockCenterPoint, 5);

        canvas.StrokeSize = 4; 
        var hourPoint = GetHourLine(curTime, 100); 
        canvas.DrawLine(clockCenterPoint, hourPoint);


        var minutePoint = GetMinuteLine(curTime, 100);
        canvas.DrawLine(clockCenterPoint, minutePoint);

        var secondPoint =  GetSecondHand(curTime, 100);
        canvas.DrawLine(clockCenterPoint, secondPoint);

    }

    public static PointF GetHourLine(DateTime curTime, int radius)
    {

        int currentHour = curTime.Hour;

        if (currentHour > 12)
            currentHour -= 12; 

        var angleDegrees = (currentHour * 360) / 12;
        var angle = (Math.PI / 180.0) * angleDegrees;

        var hourShorter = radius * .8; 
        PointF outerPoint = new((float)(hourShorter * Math.Sin(angle)), (float)(-hourShorter * Math.Cos(angle))); 

        return outerPoint; 
    }

    public static PointF GetMinuteLine(DateTime curTime, int radius)
    {

        int currentMin =  curTime.Minute;

        var angleDegrees = (currentMin * 360) / 60;
        var angle = (Math.PI / 180.0) * angleDegrees; 

        PointF outerPoint = new((float)(radius * Math.Sin(angle)), (float)(-radius * Math.Cos(angle))); 

        return outerPoint; 
    }
    public static PointF GetSecondHand(DateTime curTime, int radius)
    {

        int currentSecond = curTime.Second;

        var angleDegrees = (currentSecond * 360) / 60;
        var angle = (Math.PI / 180.0) * angleDegrees; 

        PointF outerPoint = new((float)(radius * Math.Sin(angle)), (float)(-radius * Math.Cos(angle))); 

        return outerPoint; 
    }
}
