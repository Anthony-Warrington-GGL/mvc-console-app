using System.Text.Json;
using mvc_console_app.Interfaces;

namespace mvc_console_app.Repositories;

// TODO: finish updating the methods to read KVPs and return what matters

public class JsonRepository<TKey, TItem> : IRepository<TKey, TItem>
    where TKey : notnull
{
    DirectoryInfo RepoDirectory {get; set;}

    // any item that gets stored, save it as a file where the filename is the GUID of the item
    // item content is just the JSON serialisation of the item

    /// <summary>
    /// TODO:
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<TKey> Keys 
    {
        get
        {
            // Get all the filepaths
            var fileInfos = GetAllFiles();

            // get key value pairs
            var keyItemKvps = GetKeyValuePairs(fileInfos);

            // return a list of just the items
            return GetKeysFromKeyValuePairs(keyItemKvps);
        }
    }

    /// <summary>
    /// Gets all items stored in the repository
    /// </summary>
    /// <returns> An enumerable of all items in the repository </returns>
    public IEnumerable<TItem> Items
    {
        get
        {
            // get all the files in the repo directory
            var fileInfos = GetAllFiles();

            // get key value pairs
            var keyItemKvps = GetKeyValuePairs(fileInfos);

            // return a list of just the items
            return GetItemsFromKeyValuePairs(keyItemKvps);
        }
    }

    /// <summary>
    /// Removes an item from the repository by its key
    /// </summary>
    /// <param name="key"> The key of the item to remove </param>
    /// <returns> True if the item was removed, false if it didn't exist </returns>
    public bool RemoveItem(TKey key)
    {
        // get the file path of the item
        var filePath = GetFilePathForKey(key);

        // check if the file exists, if not return false
        bool fileExists = File.Exists(filePath);
        
        if (!fileExists)
        {
            return false;
        }

        // delete the file
        DeleteFile(filePath);

        // return true to indicate successful removal
        return true;
    }

    // TODO: DOCUMENT
    public bool StoreOrUpdateItem(TKey key, TItem item)
    {
        // get the file path
        var filePath = GetFilePathForKey(key);

        // check if file already exists
        bool fileExists = File.Exists(filePath);

        // serialise the item and the key into JSON
        var json = GetJsonOfKeyItemPair(key, item);

        // write the JSON string to the file
        File.WriteAllText(filePath, json);

        // return if the file already existed or not
        return fileExists;
    }

    // TODO: DOCUMENT
    public bool TryGetItem(TKey key, out TItem item)
    {
        // get the file path of the item
        var filePath = GetFilePathForKey(key);

        // get the fileInfo
        var fileInfo = new FileInfo(filePath);

        // if it exists, deserialise the item
        if (fileInfo.Exists && TryGetKeyValuePairFromFile(fileInfo, out var kvp))
        {
            item = kvp.Value;
            return true;
        }
        
        item = default!;
        return false;
    }

    // TODO: DOCUMENT
    public JsonRepository(string repoDirectoryPath)
    {
        RepoDirectory = new DirectoryInfo(repoDirectoryPath);
        if (!RepoDirectory.Exists)
        {
            throw new ArgumentException($"Directory '{repoDirectoryPath}' doesn't exist. Repo directory path must exist.");
        }
    }

    /// <summary>
    /// Gets a list of items from a list of key-item key value pairs
    /// </summary>
    /// <param name="keyValuePairs"> The given list of key value pairs </param>
    /// <returns> The list of items </returns>
    private IEnumerable<TItem> GetItemsFromKeyValuePairs(IEnumerable<KeyValuePair<TKey, TItem>> keyValuePairs)
    {
        List<TItem> items = [];

        foreach(var kvp in keyValuePairs)
        {
            items.Add(kvp.Value);
        }

        return items;
    }

    /// <summary>
    /// Gets a list of keys from a list of key-item key value pairs
    /// </summary>
    /// <param name="keyValuePairs"> The given list of key value pairs </param>
    /// <returns> The list of keys </returns>
    private IEnumerable<TKey> GetKeysFromKeyValuePairs(IEnumerable<KeyValuePair<TKey, TItem>> keyValuePairs)
    {
        List<TKey> keys = [];

        foreach(var kvp in keyValuePairs)
        {
            keys.Add(kvp.Key);
        }

        return keys;
    }

    /// <summary>
    /// Gets the full filepath for a given key
    /// </summary>
    /// <param name="key"> The key to get the filepath for </param>
    /// <returns> The full filepath for the given key </returns>
    private string GetFilePathForKey(TKey key)
    {
        // turn the key into a string for the filename
        var fileName = GetFileNameForKey(key);

        // combine the directory and the key filepath string
        return Path.Combine(RepoDirectory.FullName, fileName);
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
    /// Gets all .json files in the repo directory
    /// </summary>
    /// <returns> An enumerable of FileInfo for each .json file </returns>
    private IEnumerable<FileInfo> GetAllFiles()
    {
        return RepoDirectory.GetFiles("*.json");
    }

    /// <summary>
    /// Gets the key and item key value pairs from a list of files
    /// </summary>
    /// <param name="files"> The list of given files </param>
    /// <returns> The key and item key value pairs </returns>
    private IEnumerable<KeyValuePair<TKey, TItem>> GetKeyValuePairs (IEnumerable<FileInfo> files)
    {
        // make a list to hold the kvps
        List<KeyValuePair<TKey, TItem>> keyValuePairs = [];

        // for each file in the list
        foreach (var file in files)
        {
            // try to get the kvp from the file
            if (TryGetKeyValuePairFromFile(file, out var keyValuePair))
            {                
                // if successful, add it to the list
                keyValuePairs.Add(keyValuePair);
            }
        }

        return keyValuePairs;
    }

    /// <summary>
    /// Tries to get a key value pair from a file's contents
    /// </summary>
    /// <param name="file"> The given file </param>
    /// <param name="keyValuePair"> If successful the key value pair to return </param>
    /// <returns> True if the key value pair was successfully retrieved, false if not </returns>
    private bool TryGetKeyValuePairFromFile(FileInfo file, out KeyValuePair<TKey, TItem> keyValuePair)
    {
        // read the file content
        var json = File.ReadAllText(file.FullName);

        return TryDeserialiseJson(json, out keyValuePair);
    }

    /// <summary>
    /// Tries to deserialise a JSON string into an instance of a specified type
    /// </summary>
    /// <param name="json"> The JSON string to deserialise </param>
    /// <param name="item">  </param>
    /// <typeparam name="T"> the type to deserialise the json to </typeparam>
    /// <returns> true if the json was successfully deserialised, false if not </returns>
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
    /// Deletes the file at the given path
    /// </summary>
    /// <param name="filePath"> The path of the file to delete </param>
    private void DeleteFile(string filePath)
    {
        File.Delete(filePath);
    }
}