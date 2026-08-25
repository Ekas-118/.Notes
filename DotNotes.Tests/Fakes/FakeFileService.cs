using DotNotes.Bus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace DotNotes.Tests.Fakes
{
    internal class FakeFileService : IFileService
    {
        private readonly Dictionary<string, string> _fileStorage = [];

        public async Task CreateOrUpdateFileAsync(string filename, string contents)
        {
            if (!_fileStorage.TryAdd(filename, contents))
            {
                _fileStorage[filename] = contents;
            }

            await Task.Delay(10); // Simulate some async work
        }

        public async Task DeleteFileAsync(string filename)
        {
            _fileStorage.Remove(filename);

            await Task.Delay(10); // Simulate some async work
        }

        public bool FileExists(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("Filename cannot be null or empty", nameof(filename));
            }

            if (_fileStorage.ContainsKey(filename))
            {
                return true;
            }

            return false;
        }

        public IStorageFolder GetLocalFolder()
        {
            return new FakeStorageFolder(_fileStorage);
        }

        public async Task<IReadOnlyList<IStorageItem>> GetStorageItemsAsync()
        {
            await Task.Delay(10);
            return GetStorageItemsInternal();
        }

        public async Task<IReadOnlyList<IStorageItem>> GetStorageItemsAsync(IStorageFolder storageFolder)
        {
            await Task.Delay(10);
            return GetStorageItemsInternal();
        }

        private IReadOnlyList<IStorageItem> GetStorageItemsInternal()
        {
            return [.. _fileStorage.Keys.Select(filename => CreateFakeStorageItem(filename))];
        }

        private static FakeStorageFile CreateFakeStorageItem(string filename)
        {
            return new FakeStorageFile(filename);
        }

        public async Task<string> GetTextFromFileAsync(IStorageFile file)
        {
            await Task.Delay(10);

            if (_fileStorage.TryGetValue(file.Name, out string? value))
            {
                return value;
            }

            return string.Empty;
        }
    }
}