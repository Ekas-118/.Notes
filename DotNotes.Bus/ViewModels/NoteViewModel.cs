using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DotNotes.Bus.Messages;
using DotNotes.Bus.Models;
using DotNotes.Bus.Services;
using System;
using System.Threading.Tasks;

namespace DotNotes.Bus.ViewModels
{
    public partial class NoteViewModel : ObservableObject
    {
        private Note _note;
        private readonly IFileService _fileService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
        private string _filename = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _text = string.Empty;

        [ObservableProperty]
        private DateTime _date = DateTime.Now;

        public NoteViewModel(IFileService fileService)
        {
            _fileService = fileService;
            _note = new Note(fileService);
            Filename = _note.Filename;
        }

        public void InitializeForExistingNote(Note note)
        {
            _note = note;
            Filename = note.Filename;
            Text = note.Text;
            Date = note.Date;
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task Save()
        {
            _note.Filename = Filename;
            _note.Text = Text;
            _note.Date = Date;
            await _note.SaveAsync();

            // Check if the DeleteCommand can now execute
            // (it can if the file now exists)
            DeleteCommand.NotifyCanExecuteChanged();

            WeakReferenceMessenger.Default.Send(new NoteCloseMessage());
        }

        private bool CanSave()
        {
            return _note is not null
                && !string.IsNullOrWhiteSpace(Text)
                && !string.IsNullOrWhiteSpace(Filename);
        }

        [RelayCommand(CanExecute = nameof(CanDelete))]
        private async Task Delete()
        {
            await _note.DeleteAsync();
            _note = new Note(_fileService);
            // Send a message from some other module
            WeakReferenceMessenger.Default.Send(new NoteCloseMessage());
        }

        private bool CanDelete()
        {
            // Note: This is to illustrate how commands can be
            // enabled or disabled.
            // In a real application, you shouldn't perform
            // file operations in your CanExecute logic.
            return _note is not null
                && !string.IsNullOrWhiteSpace(Filename)
                && _note.NoteFileExists();
        }
    }
}