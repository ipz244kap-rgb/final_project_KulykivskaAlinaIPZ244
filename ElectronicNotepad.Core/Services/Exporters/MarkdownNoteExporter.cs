using System.Linq;
using System.Text;
using ElectronicNotepad.Core.Enums;
using ElectronicNotepad.Core.Interfaces;
using ElectronicNotepad.Core.Models;

namespace ElectronicNotepad.Core.Services.Exporters;

public class MarkdownNoteExporter : INoteExporter
{
    public ExportFormat Format => ExportFormat.Markdown;

    public string Export(Note note)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"# {note.Title}");
        sb.AppendLine($"> **Створено:** {note.CreatedAt:dd.MM.yyyy HH:mm}  ");
        sb.AppendLine($"> **Пріоритет:** `{note.Priority}`");
        sb.AppendLine();
        sb.AppendLine("---");
        sb.AppendLine();
        sb.AppendLine(note.Content);
        sb.AppendLine();
        
        if (note.Reminders.Any())
        {
            sb.AppendLine("---");
            sb.AppendLine("### 🔔 Нагадування");
            foreach (var r in note.Reminders)
            {
                string checkbox = r.IsCompleted ? "[x]" : "[ ]";
                sb.AppendLine($"{checkbox} **{r.ReminderTime:dd.MM.yyyy HH:mm}**: {r.Message}");
            }
        }
        return sb.ToString();
    }
}
