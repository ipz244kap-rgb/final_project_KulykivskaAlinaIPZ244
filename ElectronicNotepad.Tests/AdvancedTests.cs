using NUnit.Framework;
using ElectronicNotepad.Core.Interfaces;
using ElectronicNotepad.Core.Models;
using ElectronicNotepad.Core.Enums;
using ElectronicNotepad.Core.Services;
using ElectronicNotepad.Infrastructure.Services;
using System;
using System.Linq;

namespace ElectronicNotepad.Tests;

[TestFixture]
public class PropertyValidatorTests
{
    [Test]
    public void NoteTitleValidator_VariousInputs_ReturnsCorrectErrors()
    {
        var validator = new NoteTitleValidator();
        
        Assert.That(validator.Validate("").Count(), Is.AtLeast(1));
        Assert.That(validator.Validate("Ab").Count(), Is.AtLeast(1));
        Assert.That(validator.Validate(new string('A', 101)).Count(), Is.AtLeast(1));
        Assert.That(validator.Validate("Valid Title").Count(), Is.EqualTo(0));
    }

    [Test]
    public void ReminderTimeValidator_PastTime_ReturnsError()
    {
        var validator = new ReminderTimeValidator();
        Assert.That(validator.Validate(DateTime.Now.AddMinutes(-5)).Count(), Is.AtLeast(1));
        Assert.That(validator.Validate(DateTime.Now.AddDays(1)).Count(), Is.EqualTo(0));
    }

    [Test]
    public void ReminderMessageValidator_Empty_ReturnsError()
    {
        var validator = new ReminderMessageValidator();
        Assert.That(validator.Validate("").Count(), Is.AtLeast(1));
        Assert.That(validator.Validate("Don't forget").Count(), Is.EqualTo(0));
    }
}

[TestFixture]
public class AdvancedExportTests
{
    private ExportService _service;

    [SetUp]
    public void SetUp() => _service = new ExportService();

    [Test]
    public void ExportAsHtml_IncludesCssStyles()
    {
        var note = new Note { Title = "Styled", Content = "Content", Priority = PriorityLevel.High };
        var html = _service.ExportNote(note, ExportFormat.Html);
        Assert.That(html, Does.Contain("<style>"));
        Assert.That(html, Does.Contain("box-shadow"));
        Assert.That(html, Does.Contain("High"));
    }

    [Test]
    public void ExportAsMarkdown_IncludesCheckboxes()
    {
        var note = new Note { Title = "MD", Content = "Text" };
        note.Reminders.Add(new Reminder { Message = "Done", IsCompleted = true });
        note.Reminders.Add(new Reminder { Message = "Todo", IsCompleted = false });
        
        var md = _service.ExportNote(note, ExportFormat.Markdown);
        Assert.That(md, Does.Contain("[x]"));
        Assert.That(md, Does.Contain("[ ]"));
    }

    [Test]
    public void ExportAsText_HasBorderDecorations()
    {
        var note = new Note { Title = "Bordered", Content = "Text" };
        var txt = _service.ExportNote(note, ExportFormat.Text);
        Assert.That(txt, Does.Contain("========================================"));
    }

    [Test]
    public void ExportService_SupportsCustomExporter()
    {
        // Arrange
        var mockExporter = new MockExporter();
        var service = new ExportService(new[] { mockExporter });
        var note = new Note { Title = "Test" };

        // Act
        var result = service.ExportNote(note, (ExportFormat)999);

        // Assert
        Assert.That(result, Is.EqualTo("Custom Export: Test"));
    }

    private class MockExporter : INoteExporter
    {
        public ExportFormat Format => (ExportFormat)999;
        public string Export(Note note) => $"Custom Export: {note.Title}";
    }
}
