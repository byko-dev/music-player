using System;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using music_player.Models.Export;

namespace music_player.Services;

public class ExportService
{
    public string TableName { get; set; }
    
    public async Task ExportDataToCsv(Window parentWindow)
    {
        Validator();
        
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.InitialFileName = TableName;
        saveFileDialog.Filters.Add(new FileDialogFilter() { Name = "CSV", Extensions = { "csv" } });
        saveFileDialog.DefaultExtension = "csv";

        var filePath = await saveFileDialog.ShowAsync(parentWindow);
        
        if (filePath == null)
            throw new Exception("Invalid path to save export file!");
            
        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            switch (TableName)
            {
                case "Sounds":
                    csv.WriteRecords((new SoundService()).GetAllSounds().Cast<SoundRaw>().ToList());
                    break;
                case "Users":
                    csv.WriteRecords((new UserService()).GetAllUsers().Cast<UserRaw>().ToList());
                    break;
                case "Playlists":
                    csv.WriteRecords((new PlaylistService()).GetAllPlaylists().Cast<PlaylistRaw>().ToList());
                    break;
            }
        }
    }
    
    private void Validator()
    {
        if (string.IsNullOrEmpty(TableName))
            throw new Exception("Table filed cannot be empty!");
    }
}