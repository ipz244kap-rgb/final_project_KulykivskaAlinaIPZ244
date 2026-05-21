using System;
using System.Collections.Generic;
using System.Linq;
using ElectronicNotepad.Core.Enums;
using ElectronicNotepad.Core.Interfaces;
using ElectronicNotepad.Core.Models;
using ElectronicNotepad.Core.Services.Exporters;

namespace ElectronicNotepad.Core.Services;

public class ExportService : IExportService
{
    private readonly IEnumerable<INoteExporter> _exporters;

    public ExportService() : this(new List<INoteExporter>
    {
        new TextNoteExporter(),
        new MarkdownNoteExporter(),
        new HtmlNoteExporter()
    })
    {
    }

    public ExportService(IEnumerable<INoteExporter> exporters)
    {
        _exporters = exporters;
    }

    public string ExportNote(Note note, ExportFormat format)
    {
        var exporter = _exporters.FirstOrDefault(e => e.Format == format);
        
        if (exporter == null)
        {
            throw new NotSupportedException($"Export format {format} is not supported.");
        }

        return exporter.Export(note);
    }
}
