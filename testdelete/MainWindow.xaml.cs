using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace testdelete
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point LastMousePosition;
        public MainWindow()
        {
            InitializeComponent();
        }
		
		const double ScaleRate = 1.1;
		private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
		{
            if(e.Delta>0)
            {
                st.ScaleX *= ScaleRate;
                st.ScaleY *= ScaleRate;
            }
            else
            {
                st.ScaleX /= ScaleRate;
                st.ScaleY /= ScaleRate;
            }
            
		}

        public void DrawCircle(int count)
        {
            int n = count;
            double area = 250;
            double ds = (double)25 / area;
            double maxNum = 360 / (2 * (Math.Asin(ds) * 180 / Math.PI));

            int i = DrawC(Convert.ToInt32(maxNum), area, count);
            
            while (i < count - 1)
            {
                area += 100;
                ds = (double)25 / area;
                maxNum = 360 / (2 * (Math.Asin(ds) * 180 / Math.PI));
                i = i + DrawC(Convert.ToInt32(maxNum), area, count - i);
            }

        }

        private int DrawC(int maxNum, double area, int maxCount)
        {
            int i = 0;
            for (; i < maxNum && i < maxCount; i++)
            {
                drp d = new drp();
                double bajanarar;
                if (maxNum < maxCount)
                {
                    bajanarar = maxNum;
                }
                else
                {
                    bajanarar = maxCount;
                }
                double angle = 360 * i / bajanarar;
                double x1 = 675 + area * Math.Cos(angle * Math.PI / 180);
                double y1 = 525 + area * Math.Sin(angle * Math.PI / 180);

                board.Children.Add(d);
                d.Margin = new Thickness(x1, y1, 0, 0);
  
            }
            return i;
        }

        public void DrawBigCircle(string count = "")
        {
            //ShapeContainer shapeContainer1 = new ShapeContainer();
            //OvalShape ovalShape1 = new OvalShape();
            //shapeContainer1.Shapes.AddRange(new Shape[] { ovalShape1 });
            //ovalShape1.Location = new Point(200, 100);
            /*ovalShape1.Name = "ovalShape1";
            ovalShape1.Size = new Size(500, 500);
            TextBox textb1 = new TextBox();
            textb1.Name = "textBox1";
            textb1.Location = new Point(839, 52);
            textb1.Text = count.ToString();
            Button bt = new Button();
            bt.Name = "button1";
            bt.Click += new EventHandler((s, a) => { button1_Click(s, a); });
            bt.Location = new Point(867, 78);
            this.Controls.Add(shapeContainer1);
            this.Controls.Add(textb1);
            this.Controls.Add(bt);*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
		
			// play
            
            

			if (Convert.ToInt32(textBox1.Text) > 1)
            {
                if (board.Children.Count > 0)
                {
                    board.Children.Clear();
                }
                DrawBigCircle(textBox1.Text);
                DrawCircle(Convert.ToInt32(textBox1.Text));
            }
            player.MediaEnded += new RoutedEventHandler(m_MediaEnded);

            player.Play();
        }

        private void m_MediaEnded(object sender, RoutedEventArgs e)
        {
            player.Position = TimeSpan.FromSeconds(0);
            player.Play();
        }


        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                st.ScaleX *= ScaleRate;
                st.ScaleY *= ScaleRate;
            }
            else
            {
                st.ScaleX /= ScaleRate;
                st.ScaleY /= ScaleRate;
            }
            //var x = Math.Pow(2, e.Delta / 3.0 / Mouse.MouseWheelDeltaForOneLine);
            //st.ScaleX *= x;

            // Adjust the offset to make the point under the mouse stay still.
            //var position = (Vector)e.GetPosition(board);
            //st.Offset = (System.Drawing.Point)((Vector)
                //(st.Offset + position) * x - position);

            //e.Handled = true;
        }
        
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            var position = e.GetPosition(board);
            if (e.LeftButton == MouseButtonState.Pressed && !(e.OriginalSource is Thumb)) // Don't block the scrollbars.
            {
                //CaptureMouse();
                //board. -= position - LastMousePosition;
                //e.Handled = true;
            }
            else
            {
                ReleaseMouseCapture();
            }
            LastMousePosition = position;
        }
    }
}
