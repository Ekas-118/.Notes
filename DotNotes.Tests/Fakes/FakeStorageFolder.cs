using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace DotNotes.Tests.Fakes
{
    internal partial class FakeStorageFolder(Dictionary<string, string> files) : IStorageFolder
    {
        private string? _name;
        private readonly Dictionary<string, string> _fileStorage = files;

        public Windows.Storage.FileAttributes Attributes => throw new NotImplementedException();

        public DateTimeOffset DateCreated => throw new NotImplementedException();

        public string? Name => _name;

        public string Path => throw new NotImplementedException();

        public IAsyncOperation<StorageFile> CreateFileAsync(string desiredName)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<StorageFile> CreateFileAsync(string desiredName, CreationCollisionOption options)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<StorageFolder> CreateFolderAsync(string desiredName)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<StorageFolder> CreateFolderAsync(string desiredName, CreationCollisionOption options)
        {
            throw new NotImplementedException();
        }

        public IAsyncAction DeleteAsync()
        {
            // For simplicity, do nothing.
            return AsyncInfo.Run(cancelToken =>
            {
                // No operation
                return Task.CompletedTask;
            });
        }

        public IAsyncAction DeleteAsync(StorageDeleteOption option)
        {
            // For simplicity, do nothing.
            return AsyncInfo.Run(cancelToken =>
            {
                // No operation
                return Task.CompletedTask;
            });
        }

        public IAsyncOperation<BasicProperties> GetBasicPropertiesAsync()
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<StorageFile> GetFileAsync(string name)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IReadOnlyList<StorageFile>> GetFilesAsync()
        {
            return AsyncInfo.Run(cancelToken =>
            {
                List<IStorageFile> files = [];
                foreach (var filename in _fileStorage.Keys)
                {
                    files.Add(new FakeStorageFile(filename));
                }
                return Task.FromResult((IReadOnlyList<StorageFile>)files);
            });
        }

        public IAsyncOperation<StorageFolder?> GetFolderAsync(string name)
        {
            // no folders to return. return null.
            return AsyncInfo.Run(cancelToken =>
            {
                return Task.FromResult<StorageFolder?>(null);
            });
        }

        public IAsyncOperation<IReadOnlyList<StorageFolder>> GetFoldersAsync()
        {
            // no folders to return. return empty list.
            return AsyncInfo.Run(cancelToken =>
            {
                List<IStorageFolder> folders = [];
                return Task.FromResult((IReadOnlyList<StorageFolder>)folders);
            });
        }

        public IAsyncOperation<IStorageItem?> GetItemAsync(string name)
        {
            // Check if the name exists in the file storage
            return AsyncInfo.Run(cancelToken =>
            {
                if (_fileStorage.ContainsKey(name))
                {
                    return Task.FromResult<IStorageItem?>(new FakeStorageFile(name));
                }
                else
                {
                    return Task.FromResult<IStorageItem?>(null);
                }
            });
        }

        public IAsyncOperation<IReadOnlyList<IStorageItem>> GetItemsAsync()
        {
            // Return all files as IStorageItem
            return AsyncInfo.Run(cancelToken =>
            {
                List<IStorageItem> items = [];
                foreach (var filename in _fileStorage.Keys)
                {
                    items.Add(new FakeStorageFile(filename));
                }
                return Task.FromResult((IReadOnlyList<IStorageItem>)items);
            });
        }

        public bool IsOfType(StorageItemTypes type)
        {
            if (type == StorageItemTypes.Folder)
            {
                return true;
            }

            return false;
        }

        public IAsyncAction RenameAsync(string desiredName)
        {
            // For simplicity, just change the name property.
            return AsyncInfo.Run(cancelToken =>
            {
                _name = desiredName;
                return Task.CompletedTask;
            });
        }

        public IAsyncAction RenameAsync(string desiredName, NameCollisionOption option)
        {
            // For simplicity, just change the name property.
            return AsyncInfo.Run(cancelToken =>
            {
                _name = desiredName;
                return Task.CompletedTask;
            });
        }
    }
}