namespace mvc_console_app.Interfaces;

/// <summary>
/// An interface to an object that provides a unique Guid
/// </summary>
public interface IGuidManager
{
    /// <summary>
    /// Returns a unique Guid
    /// </summary>
    /// <returns>the aforementioned Guid</returns>
    Guid GetNewGuid();
}