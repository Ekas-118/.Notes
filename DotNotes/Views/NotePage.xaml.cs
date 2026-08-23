using CommunityToolkit.Mvvm.Messaging;
using DotNotes.Bus;
using DotNotes.Bus.Models;
using DotNotes.Bus.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace DotNotes.Views
{
    public sealed partial class NotePage : Page
    {
        private NoteViewModel? _noteVm;

        public NotePage()
        {
            InitializeComponent();
        }

        public void RegisterForDeleteMessages()
        {
            WeakReferenceMessenger.Default.Register<NoteDeletedMessage>(this, (r, m) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _noteVm = App.Current.Services.GetService<NoteViewModel>();
            RegisterForDeleteMessages();

            if (e.Parameter is Note note && _noteVm is not null)
            {
                _noteVm.InitializeForExistingNote(note);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<NoteDeletedMessage>(this);
            base.OnNavigatedFrom(e);
        }
    }
}