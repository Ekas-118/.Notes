using DotNotes.Bus.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace DotNotes.Views
{
    public sealed partial class AllNotesPage : Page
    {
        private readonly AllNotesViewModel? _viewModel;

        public AllNotesPage()
        {
            this.InitializeComponent();
            _viewModel = App.Current.Services.GetService<AllNotesViewModel>();
        }

        private void NewNoteButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NotePage));
        }

        private void ItemsView_ItemInvoked(ItemsView sender, ItemsViewItemInvokedEventArgs args)
        {
            Frame.Navigate(typeof(NotePage), args.InvokedItem);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (_viewModel is not null)
            {
                await _viewModel.LoadAsync();
            }
        }
    }
}