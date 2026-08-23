using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotNotes.Bus.Models;
using DotNotes.Bus.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DotNotes.Bus.ViewModels
{
    public partial class AllNotesViewModel(IFileService fileService) : ObservableObject
    {
        private readonly AllNotes _allNotes = new(fileService);

        [ObservableProperty]
        private ObservableCollection<Note> _notes = [];

        [RelayCommand]
        public async Task LoadAsync()
        {
            await _allNotes.LoadNotes();
            Notes.Clear();
            foreach (var note in _allNotes.Notes)
            {
                Notes.Add(note);
            }
        }
    }
}