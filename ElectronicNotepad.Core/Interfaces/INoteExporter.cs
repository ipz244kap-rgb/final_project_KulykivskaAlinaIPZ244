using ElectronicNotepad.Core.Enums;
using ElectronicNotepad.Core.Models;

namespace ElectronicNotepad.Core.Interfaces;

public interface INoteExporter
{
    ExportFormat Format { get; }
    string Export(Note note);
}
