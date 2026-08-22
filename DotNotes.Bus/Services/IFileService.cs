using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace DotNotes.Bus.Services
{
    public interface IFileService
    {
        Task<IReadOnlyList<IStorageItem>> GetStorageItemsAsync();
        Task<IReadOnlyList<IStorageItem>> GetStorageItemsAsync(IStorageFolder storageFolder);
        Task<string> GetTextFromFileAsync(IStorageFile file);
        Task CreateOrUpdateFileAsync(string filename, string contents);
        Task DeleteFileAsync(string filename);
        bool FileExists(string filename);
        IStorageFolder GetLocalFolder();
    }
}