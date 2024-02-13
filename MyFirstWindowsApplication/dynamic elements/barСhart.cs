using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MyFirstWindowsApplication.dynamic_elements
{
    public class BarChart
    {
        int Lenght = 0;
        Rectangle[] rectangles;
        public BarChart(int Lenght)
        {
            this.Lenght = Lenght;
            rectangles = new Rectangle[Lenght];
            for (int i = 0; i < Lenght; i++)
            {
                rectangles[i] = new Rectangle();
                rectangles[i].Fill = Brushes.Red;
                rectangles[i].Width = 10;
                rectangles[i].StrokeThickness = 0;
            }
        }
        public void SetValues(int[] arr)
        {
            for(int i = 0; i < arr.Length; i++)
            {
                rectangles[i].Height = arr[i];
            }
        }
        public void Draw(Canvas canvas)
        {
            int cumulativeWidth = 0;
            for (int i = 0; i < rectangles.Length; i++)
            {
                Canvas.SetLeft(rectangles[i], cumulativeWidth);
                Canvas.SetBottom(rectangles[i], 0);
                canvas.Children.Add(rectangles[i]);
                cumulativeWidth += 15;
            }
        }
        public void Clear(Canvas canvas)
        {
            canvas.Children.Clear();
        }
        public void ChangeColor(int i, byte a, byte r, byte g, byte b)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(a, r, g, b);
            rectangles[i].Fill = mySolidColorBrush;
        }
    }
}
