using System.Text.Json;
using mvc_console_app.Interfaces;

namespace mvc_console_app.Repositories;

public class JsonRepository<TKey, TItem> : IRepository<TKey, TItem> 
    where TKey : notnull
{    
    DirectoryInfo RepoDirectory {get; set;}

    // any item that gets stored, save it as a file where the filename is the GUID of the item
    // item content is just the JSON serialisation of the item

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<TKey> GetAllKeys()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets all items stored in the repository
    /// </summary>
    /// <returns> An enumerable of all items in the repository </returns>
    public IEnumerable<TItem> GetAll()
    {
        // get all the files in the repo directory
        var files = GetAllFiles();

        // deserialise each file into an item and return them
        return DeserialiseFiles(files);
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

        // serialise the item into JSON
        var json = GetJsonOfItem(item);

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

        // check if it exists, if it doesn't, return false
        bool fileExists = File.Exists(filePath);

        if (!fileExists)
        {
            item = default!;
            return false;
        }

        // if it exists, deserialise the item
        return TryDeserialiseFile(filePath, out item);
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

    /// <summary>
    /// Gets all .json files in the repo directory
    /// </summary>
    /// <returns> An enumerable of FileInfo for each .json file </returns>
    private IEnumerable<FileInfo> GetAllFiles()
    {
        return RepoDirectory.GetFiles("*.json");
    }

    /// <summary>
    /// Deserialises all files in the given enumerable into items
    /// </summary>
    /// <param name="files"> The files to deserialise </param>
    /// <returns> An enumerable of deserialised items </returns>
    private IEnumerable<TItem> DeserialiseFiles(IEnumerable<FileInfo> files)
    {
        var items = new List<TItem>();
        
        // for each file in the collection
        foreach (var file in files)
        {
            // deserialise the file
            if (TryDeserialiseFile(file.FullName, out var item))
            {
                // add the file to the list to return
                items.Add(item);
            }            
        }

        // return the list
        return items;
    }

    /// <summary>
    /// Tries to read and deserialise a single file into an item
    /// </summary>
    /// <param name="filePath"> The path of the file to deserialise </param>
    /// <param name="item"> Set to the deserialised file if it was successful </param>
    /// <returns> true if the JSON was successfully deserialised, false if not </returns>
    private bool TryDeserialiseFile(string filePath, out TItem item)
    {
        // read the JSON string from the file - could this even be its own method?...
        var json = File.ReadAllText(filePath);

        item = TryDeserialiseJson(json, out var deserializedItemFromJson)
            ? deserializedItemFromJson ?? default!
            : default!;
        return deserializedItemFromJson is not null;
    }

    /// <summary>
    /// Deserialises a JSON string into an item
    /// </summary>
    /// <param name="json"> The JSON string to deserialise </param>
    /// <returns> The deserialised item </returns>
    private static bool TryDeserialiseJson(string json, out TItem? item)
    {
        // try to deserialise the json into this item type...
        // and if it returns null then throw an exception
        //return JsonSerializer.Deserialize<TItem>(json);//try/catch
        try
        {
            item = JsonSerializer.Deserialize<TItem>(json);
            return true;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Failed to deserialise json - Error message : {ex.Message}");
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