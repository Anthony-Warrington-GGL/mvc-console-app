using System.Collections.Immutable;
using System.Text.Json;
using mvc_console_app.Interfaces;

namespace mvc_console_app.Repositories;

// TODO: finish updating the methods to read KVPs and return what matters

public class JsonRepository<TKey, TItem> : IRepository<TKey, TItem>
    where TKey : notnull
{
    // Properties

    /// <summary>
    /// Gets all items stored in the repository
    /// </summary>
    /// <returns> An enumerable of all items in the repository </returns>
    public IReadOnlyCollection<TItem> Items
    {
        get
        {
            var fileInfos = GetAllFiles();
            var keyItemKvps = GetKeyValuePairs(fileInfos);
            return GetItemsFromKeyValuePairs(keyItemKvps);
        }
    }

    /// <summary>
    /// Gets all keys stored in the repository
    /// </summary>
    /// <returns> An enumerable of all keys in the repository </returns>
    public IReadOnlySet<TKey> Keys
    {
        get
        {
            var fileInfos = GetAllFiles();
            var keyItemKvps = GetKeyValuePairs(fileInfos);
            return GetKeysFromKeyValuePairs(keyItemKvps);
        }
    }

    /// <summary>
    /// The directory in which repository item files are stored
    /// </summary>
    DirectoryInfo RepoDirectory { get; set; }

    // Public methods

    /// <summary>
    /// Creates a new instance of <see cref="JsonRepository{TKey, TItem}"/> backed by the given directory
    /// </summary>
    /// <param name="repoDirectoryPath"> The path of the directory to use as the repository store </param>
    /// <exception cref="ArgumentException"> Thrown if the given directory path does not exist </exception>
    public JsonRepository(string repoDirectoryPath)
    {
        RepoDirectory = new DirectoryInfo(repoDirectoryPath);
        if (!RepoDirectory.Exists)
        {
            throw new ArgumentException($"Directory '{repoDirectoryPath}' doesn't exist. Repo directory path must exist.");
        }
    }

    /// <summary>
    /// Removes an item from the repository by its key
    /// </summary>
    /// <param name="key"> The key of the item to remove </param>
    /// <returns> True if the item was removed, false if it didn't exist </returns>
    public bool RemoveItem(TKey key)
    {
        var filePath = GetFilePathForKey(key);
        bool fileExists = File.Exists(filePath);

        if (!fileExists)
        {
            return false;
        }

        DeleteFile(filePath);
        return true;
    }

    /// <summary>
    /// Stores a new item in the repository, or overwrites the existing item if the key already exists
    /// </summary>
    /// <param name="key"> The key to store the item under </param>
    /// <param name="item"> The item to store </param>
    /// <returns> True if an existing item was overwritten, false if the item was newly stored </returns>
    public bool StoreOrUpdateItem(TKey key, TItem item)
    {
        var filePath = GetFilePathForKey(key);
        bool fileExists = File.Exists(filePath);
        var json = GetJsonOfKeyItemPair(key, item);
        File.WriteAllText(filePath, json);
        return fileExists;
    }

    /// <summary>
    /// Tries to retrieve an item from the repository by its key
    /// </summary>
    /// <param name="key"> The key of the item to retrieve </param>
    /// <param name="item"> The retrieved item if found, otherwise the default value </param>
    /// <returns> True if the item was found and retrieved, false if it didn't exist </returns>
    public bool TryGetItem(TKey key, out TItem item)
    {
        var filePath = GetFilePathForKey(key);
        var fileInfo = new FileInfo(filePath);

        if (fileInfo.Exists && TryGetKeyValuePairFromFile(fileInfo, out var kvp))
        {
            item = kvp.Value;
            return true;
        }

        item = default!;
        return false;
    }

    // Private methods

    /// <summary>
    /// Deletes the file at the given path
    /// </summary>
    /// <param name="filePath"> The path of the file to delete </param>
    private void DeleteFile(string filePath)
    {
        File.Delete(filePath);
    }

    /// <summary>
    /// Gets all .json files in the repo directory
    /// </summary>
    /// <returns> An enumerable of FileInfo for each .json file </returns>
    private IEnumerable<FileInfo> GetAllFiles()
    {
        return RepoDirectory.GetFiles("*.json");
    }

    /// <summary>
    /// Gets the filename for a given key
    /// </summary>
    /// <param name="key"> The key to get the filename for </param>
    /// <returns> The filename for the given key </returns>
    private string GetFileNameForKey(TKey key)
    {
        return $"{key}.json";
    }

    /// <summary>
    /// Gets the full filepath for a given key
    /// </summary>
    /// <param name="key"> The key to get the filepath for </param>
    /// <returns> The full filepath for the given key </returns>
    private string GetFilePathForKey(TKey key)
    {
        var fileName = GetFileNameForKey(key);
        return Path.Combine(RepoDirectory.FullName, fileName);
    }

    /// <summary>
    /// Gets a list of items from a list of key-item key value pairs
    /// </summary>
    /// <param name="keyValuePairs"> The given list of key value pairs </param>
    /// <returns> The list of items </returns>
    private IReadOnlyCollection<TItem> GetItemsFromKeyValuePairs(IEnumerable<KeyValuePair<TKey, TItem>> keyValuePairs)
    {
        List<TItem> items = [];
        foreach (var kvp in keyValuePairs)
        {
            items.Add(kvp.Value);
        }
        return items;
    }

    /// <summary>
    /// Serialises a key and an item as a pair into a JSON string
    /// </summary>
    /// <param name="key"> The key of the pair to serialize </param>
    /// <param name="item"> The item of the pair to serialize </param>
    /// <returns> The string of the serialized JSON key item pair </returns>
    private string GetJsonOfKeyItemPair(TKey key, TItem item)
    {
        var kvp = new KeyValuePair<TKey, TItem>(key, item);
        return JsonSerializer.Serialize(kvp);
    }

    /// <summary>
    /// Gets the key and item key value pairs from a list of files
    /// </summary>
    /// <param name="files"> The list of given files </param>
    /// <returns> The key and item key value pairs </returns>
    private IEnumerable<KeyValuePair<TKey, TItem>> GetKeyValuePairs(IEnumerable<FileInfo> files)
    {
        List<KeyValuePair<TKey, TItem>> keyValuePairs = [];
        foreach (var file in files)
        {
            if (TryGetKeyValuePairFromFile(file, out var keyValuePair))
            {
                keyValuePairs.Add(keyValuePair);
            }
        }
        return keyValuePairs;
    }

    /// <summary>
    /// Gets a list of keys from a list of key-item key value pairs
    /// </summary>
    /// <param name="keyValuePairs"> The given list of key value pairs </param>
    /// <returns> The list of keys </returns>
    private IReadOnlySet<TKey> GetKeysFromKeyValuePairs(IEnumerable<KeyValuePair<TKey, TItem>> keyValuePairs)
    {
        HashSet<TKey> keys = [];
        foreach (var kvp in keyValuePairs)
        {
            keys.Add(kvp.Key);
        }
        return keys;
    }

    /// <summary>
    /// Tries to deserialise a JSON string into an instance of a specified type
    /// </summary>
    /// <param name="json"> The JSON string to deserialise </param>
    /// <param name="item"> The deserialised instance if successful, otherwise the default value </param>
    /// <typeparam name="T"> The type to deserialise the JSON to </typeparam>
    /// <returns> True if the JSON was successfully deserialised, false if not </returns>
    private static bool TryDeserialiseJson<T>(string json, out T? item)
    {
        try
        {
            item = JsonSerializer.Deserialize<T>(json);
            return true;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Failed to deserialise json to type {typeof(T).FullName} - Error message : {ex.Message}");
            item = default;
            return false;
        }
    }

    /// <summary>
    /// Tries to get a key value pair from a file's contents
    /// </summary>
    /// <param name="file"> The file to read the key value pair from </param>
    /// <param name="keyValuePair"> The deserialised key value pair if successful, otherwise the default value </param>
    /// <returns> True if the key value pair was successfully retrieved, false if not </returns>
    private bool TryGetKeyValuePairFromFile(FileInfo file, out KeyValuePair<TKey, TItem> keyValuePair)
    {
        var json = File.ReadAllText(file.FullName);
        return TryDeserialiseJson(json, out keyValuePair);
    }
}