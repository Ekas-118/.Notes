using DotNotes.Bus.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;

namespace DotNotes.Bus.Models;

public class AllNotes(IFileService fileService)
{
    private readonly IFileService _fileService = fileService;
    public ObservableCollection<Note> Notes { get; set; } = [];

    public async Task LoadNotes()
    {
        Notes.Clear();
        await GetFilesInFolderAsync(_fileService.GetLocalFolder());
    }

    private async Task GetFilesInFolderAsync(IStorageFolder folder)
    {
        // Each StorageItem can be either a folder or a file.
        IReadOnlyList<IStorageItem> storageItems =
                                    await _fileService.GetStorageItemsAsync(folder);
        foreach (IStorageItem item in storageItems)
        {
            if (item.IsOfType(StorageItemTypes.Folder))
            {
                // Recursively get items from subfolders.
                await GetFilesInFolderAsync((IStorageFolder)item);
            }
            else if (item.IsOfType(StorageItemTypes.File))
            {
                IStorageFile file = (IStorageFile)item;
                Note note = new(_fileService)
                {
                    Filename = file.Name,
                    Text = await _fileService.GetTextFromFileAsync(file),
                    Date = file.DateCreated.DateTime
                };
                Notes.Add(note);
            }
        }
    }
}