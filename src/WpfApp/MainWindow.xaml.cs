// This code is public domain
#define ANGLE_ES3

using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using OpenTK;
using OpenTK.Graphics;

#if ANGLE_ES3
using ClearBufferMask = OpenTK.Graphics.ES30.ClearBufferMask;
using GL = OpenTK.Graphics.ES30.GL;
#else
using ClearBufferMask = OpenTK.Graphics.ES20.ClearBufferMask;
using GL = OpenTK.Graphics.ES20.GL;
#endif

namespace WpfAngle.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GLControl glControl;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowsFormsHost_Initialized(object sender, EventArgs e)
        {
            var flags = GraphicsContextFlags.Embedded | GraphicsContextFlags.Angle;
            int major = 2;
#if ANGLE_ES3
            major = 3;
#endif
            glControl = new GLControl(new GraphicsMode(32, 24), major, 0, flags);
            glControl.MakeCurrent();
            glControl.Paint += GLControl_Paint;
            glControl.Dock = DockStyle.Fill;
            (sender as WindowsFormsHost).Child = glControl;

        }

        private void GLControl_Paint(object sender, PaintEventArgs e)
        {
            GL.ClearColor(
                (float)Red.Value,
                (float)Green.Value,
                (float)Blue.Value,
                1);
            GL.Clear(
                ClearBufferMask.ColorBufferBit |
                ClearBufferMask.DepthBufferBit |
                ClearBufferMask.StencilBufferBit);

            glControl.SwapBuffers();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            glControl.Invalidate();
        }
    }
}
