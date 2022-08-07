namespace MeterGraphicsExample.Drawables;

public class MeterDrawable : BaseDrawable, IDrawable
{
    public Color MeterColor { get; set; } = Colors.Black;
    public Color FillableColor { get; set; } = Colors.Red;

    public int MaxValue { get; set; } = 100;
    public int Steps { get; set; } = 20;

    public int BaseTickSize { get; set; } = 10;

    public int RoundedRadius { get; set; } = 10;

    public bool HorizontalMode { get; set; } = false;

    public int FillValue { get; set; }

    public bool GradiantFill { get; set; } = false;

    public bool ShowSeries { get; set; } = false; 


    public override void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = MeterColor;
        canvas.StrokeSize = 4;
        //canvas.EnableDefaultShadow();

        // It doesn't seem to respect this, but putting it here anyways
        //  I just drew in the order I wanted to make sure the tick marks were on top of the fill area
        canvas.BlendMode = BlendMode.DestinationOver;


        var height = dirtyRect.Height; 
        if (HorizontalMode)
            height = height - (height / 10);
        
        //Just going to take up the whole GraphicsView, makes it easier to adjust
        // the size. Though, you can change the size of the meter here as well. 
        Rect meterRect = new(dirtyRect.X, dirtyRect.Y, dirtyRect.Width, height);

        if (FillValue > MaxValue)
            FillValue = MaxValue;

        if (FillValue < 0)
            FillValue = 0;

        FillMeter(canvas, FillValue, meterRect);

        AddTickMarks(canvas, Steps, meterRect);
        canvas.SetShadow(new SizeF(10, 10), 10, Colors.Grey); 
        DrawMeter(canvas, meterRect);

    }

    private Rect DrawMeter(ICanvas canvas, RectF dirtyRect)
    {

        var x = dirtyRect.X;
        var y = dirtyRect.Y;
        var width = dirtyRect.Width;
        var height = dirtyRect.Height;


        var meterRect = new Rect(x, y, width, height);
        canvas.DrawRoundedRectangle(meterRect, (double)RoundedRadius);
        return meterRect; 
        
    }

    private void AddTickMarks(ICanvas canvas, int steps, Rect meter)
    {
        for (int i = 0; i < steps; i++)
        {
            var stepScale = (double)i / steps;

            Point nextLine;
            if (HorizontalMode)
            {
                nextLine = new Point(meter.X + (meter.Width * stepScale), meter.Height); 
            } else
            {
                nextLine = new Point(meter.X, meter.Y + meter.Height * stepScale);
            }
            
            var tickSize = BaseTickSize;
            // don't draw the ticks for the first and last one
            if (i != 0)
            {
                if (i == (steps / 2))
                {
                    tickSize = BaseTickSize * 2;
                }
                else if (i % (steps / 4) == 0)
                {
                    tickSize = (int)(BaseTickSize * 1.5);
                }
                if (HorizontalMode)
                    canvas.DrawLine(nextLine, nextLine.Offset(0, -tickSize)); 
                else 
                    canvas.DrawLine(nextLine, nextLine.Offset(tickSize, 0));

                if (ShowSeries)
                {
                    canvas.Font = Microsoft.Maui.Graphics.Font.Default;
                    var scaleDir = HorizontalMode ? i : (steps - i);
                    var percentOfMax = (int)(((double)MaxValue / steps) * scaleDir);
                    PointF stringPoint;

                    if (HorizontalMode)
                        stringPoint = nextLine.Offset(0, 20);
                    else
                        stringPoint = nextLine.Offset(-10, 0);


                    canvas.DrawString(percentOfMax.ToString(), stringPoint.X, stringPoint.Y, HorizontalAlignment.Center); 


                    
                }
            }

        }
    }

    private void FillMeter(ICanvas canvas, int fillValue, Rect meter)
    {
        var percentFill = (double)fillValue / MaxValue;
        Rect fillRect; 


        if (HorizontalMode)
        {

            var fillWidth = meter.Width * percentFill;

            fillRect = new Rect(meter.X, meter.Y, fillWidth, meter.Height);

        }
        else
        {
            var fillHeight = meter.Height * percentFill;
            var fillHeightY = meter.Y + meter.Height - fillHeight;

            fillRect = new Rect(meter.X, fillHeightY, meter.Width, fillHeight);
        }

        if (GradiantFill)
        {
            LinearGradientPaint lgp = new LinearGradientPaint
            {
                StartColor = HorizontalMode ? Colors.Green : Colors.Red,
                EndColor = HorizontalMode ? Colors.Red : Colors.Green,
                StartPoint = new Point(0, 0),
                EndPoint = HorizontalMode ? new Point(1, 0) : new Point(0, 1)
            };
            canvas.SetFillPaint(lgp, meter); 
        }
        else 
            canvas.FillColor = FillableColor;

        canvas.FillRoundedRectangle(fillRect, (double)RoundedRadius); 
    }

}
