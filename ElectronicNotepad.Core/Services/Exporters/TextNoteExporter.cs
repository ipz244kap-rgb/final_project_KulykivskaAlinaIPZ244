using System.Linq;
using System.Text;
using ElectronicNotepad.Core.Enums;
using ElectronicNotepad.Core.Interfaces;
using ElectronicNotepad.Core.Models;

namespace ElectronicNotepad.Core.Services.Exporters;

public class TextNoteExporter : INoteExporter
{
    public ExportFormat Format => ExportFormat.Text;

    public string Export(Note note)
    {
        var sb = new StringBuilder();
        sb.AppendLine("========================================");
        sb.AppendLine($"  {note.Title.ToUpper()}");
        sb.AppendLine("========================================");
        sb.AppendLine($"Дата створення: {note.CreatedAt:dd.MM.yyyy HH:mm}");
        sb.AppendLine($"Пріоритет:      {note.Priority}");
        sb.AppendLine("----------------------------------------");
        sb.AppendLine();
        sb.AppendLine(note.Content);
        sb.AppendLine();
        
        if (note.Reminders.Any())
        {
            sb.AppendLine("----------------------------------------");
            sb.AppendLine("НАГАДУВАННЯ:");
            foreach (var r in note.Reminders)
            {
                string status = r.IsCompleted ? "[ВИКОНАНО]" : "[АКТИВНЕ]";
                sb.AppendLine($"- {r.ReminderTime:dd.MM.yyyy HH:mm} {status}: {r.Message}");
            }
        }
        sb.AppendLine("========================================");
        return sb.ToString();
    }
}
