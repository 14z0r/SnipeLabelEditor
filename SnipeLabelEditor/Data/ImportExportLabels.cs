using System.Text.Json;
using SnipeLabelEditor.Models;

namespace SnipeLabelEditor.Data;

public static class ImportExportLabels
{
    public static string ExportLabels(List<Label> labels)
    {
        string jsonString = JsonSerializer.Serialize(labels);
        return jsonString;
    }
}