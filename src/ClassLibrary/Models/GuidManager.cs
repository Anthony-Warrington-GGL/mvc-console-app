using mvc_console_app.Interfaces;

namespace mvc_console_app.Models;

public class GuidManager : IGuidManager
{
    private HashSet<Guid> guids = [];

    public Guid GetNewGuid()
    {        
        Guid guid;
        
        do
        {
            guid = Guid.NewGuid();
        }
        while (guids.Contains(guid));

        guids.Add(guid);

        return guid;
    }
}