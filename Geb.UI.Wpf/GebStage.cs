using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Geb.UI.Wpf
{
    using Geb.UI;
    using Geb.UI.Wpf.Drawing;

    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Geb.UI.Wpf"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Geb.UI.Wpf;assembly=Geb.UI.Wpf"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class GebStage : Control
    {
        private Stage _stage;
        private WpfGraphics _graphics;

        static GebStage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GebStage), new FrameworkPropertyMetadata(typeof(GebStage)));
        }

        public GebStage():base()
        {
            _stage = new Stage();
            _graphics = new WpfGraphics();
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            _stage.Width = arrangeBounds.Width;
            _stage.Height = arrangeBounds.Height;
            return base.ArrangeOverride(arrangeBounds);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            _graphics.Context = drawingContext;
            _stage.SetInvalidated(false);
            _stage.Draw(_graphics);
        }
    }
}
