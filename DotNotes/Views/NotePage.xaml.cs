using CommunityToolkit.Mvvm.Messaging;
using DotNotes.Bus.Messages;
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

        public void RegisterForCloseMessages()
        {
            WeakReferenceMessenger.Default.Register<NoteCloseMessage>(this, (_, _) =>
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
            RegisterForCloseMessages();

            if (e.Parameter is Note note && _noteVm is not null)
            {
                _noteVm.InitializeForExistingNote(note);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<NoteCloseMessage>(this);
            base.OnNavigatedFrom(e);
        }
    }
}