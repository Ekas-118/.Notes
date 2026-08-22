using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotNotes.Bus.Models;
using DotNotes.Bus.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DotNotes.Bus.ViewModels
{
    public partial class AllNotesViewModel : ObservableObject
    {
        private readonly AllNotes allNotes;

        [ObservableProperty]
        private ObservableCollection<Note> notes;

        public AllNotesViewModel(IFileService fileService)
        {
            allNotes = new AllNotes(fileService);
            notes = new ObservableCollection<Note>();
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            await allNotes.LoadNotes();
            Notes.Clear();
            foreach (var note in allNotes.Notes)
            {
                Notes.Add(note);
            }
        }
    }
}