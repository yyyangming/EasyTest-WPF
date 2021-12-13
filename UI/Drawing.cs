using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Test.UI
{
    internal class DrawingTrajectory
    {

        public void DrawArrow() 
        {
            Line x_axis = new Line();//x轴
            Line y_axis = new Line();//y轴
            x_axis.Stroke = System.Windows.Media.Brushes.White;
            y_axis.Stroke = System.Windows.Media.Brushes.White;
            x_axis.StrokeThickness = 3;
            y_axis.StrokeThickness = 3;
            x_axis.X1 = 40;
            x_axis.Y1 = 320;
            x_axis.X2 = 600;
            x_axis.Y2 = 320;
            y_axis.X1 = 40;
            y_axis.Y1 = 320;
            y_axis.X2 = 40;
            y_axis.Y2 = 30;
        }
    }
}
