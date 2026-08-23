using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
            this._fileService = fileService;
            this._note = new Note(fileService);
            this.Filename = _note.Filename;
        }

        public void InitializeForExistingNote(Note note)
        {
            this._note = note;
            this.Filename = note.Filename;
            this.Text = note.Text;
            this.Date = note.Date;
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task Save()
        {
            _note.Filename = this.Filename;
            _note.Text = this.Text;
            _note.Date = this.Date;
            await _note.SaveAsync();

            // Check if the DeleteCommand can now execute
            // (it can if the file now exists)
            DeleteCommand.NotifyCanExecuteChanged();
        }

        private bool CanSave()
        {
            return _note is not null
                && !string.IsNullOrWhiteSpace(this.Text)
                && !string.IsNullOrWhiteSpace(this.Filename);
        }

        [RelayCommand(CanExecute = nameof(CanDelete))]
        private async Task Delete()
        {
            await _note.DeleteAsync();
            _note = new Note(_fileService);
            // Send a message from some other module
            WeakReferenceMessenger.Default.Send(new NoteDeletedMessage(_note));
        }

        private bool CanDelete()
        {
            // Note: This is to illustrate how commands can be
            // enabled or disabled.
            // In a real application, you shouldn't perform
            // file operations in your CanExecute logic.
            return _note is not null
                && !string.IsNullOrWhiteSpace(this.Filename)
                && this._note.NoteFileExists();
        }
    }
}