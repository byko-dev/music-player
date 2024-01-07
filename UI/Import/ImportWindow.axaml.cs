using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using music_player.Services;

namespace music_player.UI.Import;

public partial class ImportWindow : Window
{
    private string FilePath { get; set; }

    public ImportWindow()
    {
        InitializeComponent();
    }

    public void ImportTable_ButtonEvent(object sender, RoutedEventArgs e)
    {
        try
        {
            var selectedItem = ImportComboBox.SelectedValue as ComboBoxItem;

            ImportService importService = new ImportService
            {
                FilePath = FilePath,
                TableName = selectedItem?.Content?.ToString() ?? ""
            };
            importService.Import();
            
            SetResponseMessage("Data was imported successful!");
        }
        catch (Exception exception)
        {
            SetResponseMessage(exception.Message, false);
        }
        
    }

    public async void OnFileSelectClick_ButtonEvent(object? sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog();
        var result = await dialog.ShowAsync(this);

        if (result != null && result.Any())
        {
            FilePathBox.Text = result.First();
            FilePath = result.First();
        }
    }

    public void ExportTable_ButtonEvent(object sender, RoutedEventArgs e)
    {
        var selectedItem = ExportComboBox.SelectedValue as ComboBoxItem;
        
        ExportService exportController = new ExportService
        {
            TableName = selectedItem?.Content?.ToString() ?? ""
        };
        exportController.ExportDataToCsv(this);
    }
    
    private void SetResponseMessage(string message, bool success = true)
    {
        ImportMessage.Content = message;
        ImportMessage.Foreground = new SolidColorBrush(success ? Colors.Green : Colors.Red);
    }
}