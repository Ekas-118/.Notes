using Microsoft.UI.Xaml.Controls;

namespace DotNotes
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WinUIEx.WindowEx
    {
        private const int WindowMinWidth = 300;
        private const int WindowMinHeight = 300;

        public MainWindow()
        {
            InitializeComponent();

            SetMinSize();
            SetCustomTitleBar();
        }

        private void SetMinSize()
        {
            MinWidth = WindowMinWidth;
            MinHeight = WindowMinHeight;
        }

        private void SetCustomTitleBar()
        {
            // Hide the default system title bar.
            ExtendsContentIntoTitleBar = true;
            // Replace system title bar with the WinUI TitleBar.
            SetTitleBar(AppTitleBar);
        }

        private void AppTitleBar_BackRequested(TitleBar sender, object args)
        {
            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }
    }
}
