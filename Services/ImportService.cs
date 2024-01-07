using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using music_player.Models;
using music_player.Models.Export;

namespace music_player.Services;

public class ImportService
{
    public string FilePath { get; set; }
    public string TableName { get; set; }
    
    public void Import()
    {
        using (var reader = new StreamReader(FilePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            ImportToBase(csv);
        }
    }

    private void ImportToBase(CsvReader csv)
    {
        switch (TableName)
        {
            case "Users":
                foreach (User record in FormatDataToList<User>(csv))
                {
                    (new UserService()).Import(record);
                }
                break;
            case "Playlists":
                foreach (PlaylistRaw record in FormatDataToList<PlaylistRaw>(csv))
                {
                    (new PlaylistService()).Import(record);
                }
                break;
            case "Sounds":
                foreach (SoundRaw record in FormatDataToList<SoundRaw>(csv))
                {
                    (new SoundService()).Import(record);
                }
                break;
        }
    }
    
    private List<T> FormatDataToList<T>(CsvReader csv)
    {
        try
        {
            return csv.GetRecords<T>().ToList();
        }
        catch (CsvHelperException ex)
        {
            throw new Exception("Wrong import file format!");
        }
    }
}