using System.Text.Json;
using mvc_console_app.Interfaces;

namespace mvc_console_app.Repositories;

public class JsonRepository<TKey, TItem> : IRepository<TKey, TItem> where TKey : notnull
{
    
    DirectoryInfo RepoDirectory {get; set;}

    // any item that gets stored, save it as a file where the filename is the GUID of the item
    // item content is just the JSON serialisation of the item

    public IEnumerable<TItem> GetAll()
    {
        throw new NotImplementedException();
    }

    public bool RemoveItem(TKey key)
    {
        throw new NotImplementedException();
    }

    public bool StoreOrUpdateItem(TKey key, TItem item)
    {
        // get the filename
        var filePath = GetFilePathForKey(key);

        // check if file already exists
        bool fileExists = File.Exists(filePath);

        // serialise the item into JSON
        var json = GetJsonOfItem(item);

        // write the JSON string to the file
        File.WriteAllText(filePath, json);

        // return if the file already existed or not
        return fileExists;
    }

    public bool TryGetItem(TKey key, out TItem item)
    {
        // get the filename of the key

        // check if it exists, if it doesn't, return false

        // if it exists, deserialise the item (what if it doesn't work? - try/catch throw exception)

        // set the out var to the item

        // return whether or not the item exists
        return true;
    }

    public JsonRepository(string repoDirectoryPath)
    {
        RepoDirectory = new DirectoryInfo(repoDirectoryPath);
        if (!RepoDirectory.Exists)
        {
            throw new ArgumentException($"Directory '{repoDirectoryPath}' doesn't exist. Repo directory path must exist.");
        }
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
    /// Serialises a given item into a JSON string
    /// </summary>
    /// <param name="item"> The item to serialize </param>
    /// <returns> The string of the serialized JSON item </returns>
    private string GetJsonOfItem(TItem item)
    {
        return JsonSerializer.Serialize(item);
    }
}