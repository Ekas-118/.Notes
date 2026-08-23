using DotNotes.Bus.Services;
using System;
using System.Threading.Tasks;

namespace DotNotes.Bus.Models;

public class Note(IFileService fileService)
{
    private readonly IFileService _fileService = fileService;
    public string Filename { get; set; } = "notes" + DateTime.Now.ToBinary().ToString() + ".txt";
    public string Text { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;

    public async Task SaveAsync()
    {
        await _fileService.CreateOrUpdateFileAsync(Filename, Text);
    }

    public async Task DeleteAsync()
    {
        await _fileService.DeleteFileAsync(Filename);
    }

    public bool NoteFileExists()
    {
        return _fileService.FileExists(Filename);
    }
}