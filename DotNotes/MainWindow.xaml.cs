using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinRT;

namespace DotNotes
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private const int MinWidth = 300;
        private const int MinHeight = 300;

        public MainWindow()
        {
            InitializeComponent();

            SetMinSize();

            // Hide the default system title bar.
            ExtendsContentIntoTitleBar = true;
            // Replace system title bar with the WinUI TitleBar.
            SetTitleBar(AppTitleBar);
        }

        private void SetMinSize()
        {
            var presenter = AppWindow.Presenter.As<OverlappedPresenter>();

            presenter.PreferredMinimumWidth = MinWidth;
            presenter.PreferredMinimumHeight = MinHeight;
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
