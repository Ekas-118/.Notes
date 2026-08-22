using DotNotes.Bus.Services;
using System;
using System.Threading.Tasks;

namespace DotNotes.Bus.Models;

public class Note
{
    private IFileService fileService;
    public string Filename { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;

    public Note(IFileService fileService)
    {
        Filename = "notes" + DateTime.Now.ToBinary().ToString() + ".txt";
        this.fileService = fileService;
    }

    public async Task SaveAsync()
    {
        await fileService.CreateOrUpdateFileAsync(Filename, Text);
    }

    public async Task DeleteAsync()
    {
        await fileService.DeleteFileAsync(Filename);
    }

    public bool NoteFileExists()
    {
        return fileService.FileExists(Filename);
    }
}