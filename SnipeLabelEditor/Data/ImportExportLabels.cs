using System.Text.Json;
using Microsoft.AspNetCore.Components.Forms;
using SnipeLabelEditor.Models;

namespace SnipeLabelEditor.Data;

public static class ImportExportLabels
{
    public static async Task ImportLabels(IBrowserFile file, LabelsDBContext context)
    {
        var memoryStream = new MemoryStream();
        await file.OpenReadStream().CopyToAsync(memoryStream);
        
        memoryStream.Position = 0;
        var sr = new StreamReader(memoryStream);
        
        string json = await sr.ReadToEndAsync();

        List<Label> labels = JsonSerializer.Deserialize<List<Label>>(json);

        foreach (var label in labels)
        {
            label.Id = 0;
            context.Labels.Add(label);
        }
        
        await context.SaveChangesAsync();
    }
    
    public static string ExportLabels(List<Label> labels)
    {
        string jsonString = JsonSerializer.Serialize(labels);
        return jsonString;
    }
}