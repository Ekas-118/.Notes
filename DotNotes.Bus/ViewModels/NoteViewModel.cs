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
    public partial class NoteViewModel(IFileService fileService) : ObservableObject
    {
        private Note _note = new(fileService);

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _text = string.Empty;

        [ObservableProperty]
        private DateTime _date = DateTime.Now;

        private bool NoteExists { get; set; }

        public string Filename => _note.Filename;

        public void InitializeForExistingNote(Note note)
        {
            NoteExists = true;
            _note = note;
            Text = note.Text;
            Date = note.Date;
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task Save()
        {
            _note.Text = Text;
            _note.Date = Date;
            await _note.SaveAsync();

            WeakReferenceMessenger.Default.Send(new NoteCloseMessage());
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Text);
        }

        [RelayCommand(CanExecute = nameof(NoteExists))]
        private async Task Delete()
        {
            await _note.DeleteAsync();

            WeakReferenceMessenger.Default.Send(new NoteCloseMessage());
        }
    }
}