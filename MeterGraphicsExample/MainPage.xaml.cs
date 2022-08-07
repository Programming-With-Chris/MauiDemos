using MeterGraphicsExample.Drawables;
using System.Timers;

namespace MeterGraphicsExample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        var timer = new System.Timers.Timer(50);
        timer.Elapsed += new ElapsedEventHandler(DrainMeter);

        // You can get the drawable and set settings in this constructor,
        // to manipulate before it's drawn
        // the first time
        var graphicsView = this.MeterDrawableView;
        var meterDrawable = (MeterDrawable)graphicsView.Drawable;
        meterDrawable.HorizontalMode = false;

        meterDrawable.GradiantFill = true;
        meterDrawable.Steps = 12; 
        meterDrawable.BaseTickSize = 10;
        meterDrawable.ShowSeries = true;
        timer.Start();

        var radialView = this.RadialGaugeView;
        var radialDrawable = (RadialGaugeDrawable)radialView.Drawable;
        radialDrawable.MaxValue = 100;
        radialDrawable.GradiantFill = true;
        radialDrawable.Steps = 12; 

    }

    private void DrainMeter(object sender, EventArgs e)
    {
        var graphicsView = this.MeterDrawableView;
        var meterDrawable = (MeterDrawable)graphicsView.Drawable;
        meterDrawable.HorizontalMode = false;


        if (meterDrawable.FillValue != 0)
        {
            meterDrawable.FillValue -= 1;
            graphicsView.Invalidate();
        }


        var radialView = this.RadialGaugeView;
        var radialDrawable = (RadialGaugeDrawable)radialView.Drawable;

        radialDrawable.GaugeThickness = 2;
        if (radialDrawable.FillValue != 0)
        {
            radialDrawable.FillValue -= 1;
            radialView.Invalidate(); 
        }
    }

    private void DontTouchTheButton(object sender, EventArgs e)
    {
        var graphicsView = this.MeterDrawableView;
        var meterDrawable = (MeterDrawable)graphicsView.Drawable;

        meterDrawable.FillValue += 10;

        graphicsView.Invalidate(); 
        
        
        var radialView = this.RadialGaugeView;
        var radialDrawable = (RadialGaugeDrawable)radialView.Drawable;

        radialDrawable.FillValue += 10;
        radialView.Invalidate(); 
    }
}
