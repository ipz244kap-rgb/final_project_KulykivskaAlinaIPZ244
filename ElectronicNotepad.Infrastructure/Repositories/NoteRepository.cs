using System;
using System.Collections.Generic;
using System.Linq;
using ElectronicNotepad.Core.Interfaces;
using ElectronicNotepad.Core.Models;
using ElectronicNotepad.Infrastructure.Data;
using ElectronicNotepad.Core.Data;

namespace ElectronicNotepad.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly FileStorageContext _context;
    private List<Note> _notes;
    private List<Category> _categories;
    private List<Tag> _tags;

    public NoteRepository(FileStorageContext context)
    {
        _context = context;
        _notes = _context.LoadNotes();
        _categories = DefaultCategories.All.ToList();
        _tags = new List<Tag>();
    }

    public IEnumerable<Note> GetAllNotes() => _notes;

    public Note GetNoteById(Guid id) => _notes.FirstOrDefault(n => n.Id == id)!;

    public void AddNote(Note note)
    {
        _notes.Add(note);
        _context.SaveNotes(_notes);
    }

    public void UpdateNote(Note note)
    {
        var existing = GetNoteById(note.Id);
        if (existing != null)
        {
            existing.Title = note.Title;
            existing.Content = note.Content;
            existing.Reminders = note.Reminders;
            existing.Tags = note.Tags;
            existing.Priority = note.Priority;
            existing.CategoryId = note.CategoryId;
            existing.IsPinned = note.IsPinned;
            existing.ColorHex = note.ColorHex;
            existing.UpdatedAt = DateTime.UtcNow;
            _context.SaveNotes(_notes);
        }
    }

    public void DeleteNote(Guid id)
    {
        var note = GetNoteById(id);
        if (note != null)
        {
            _notes.Remove(note);
            _context.SaveNotes(_notes);
        }
    }

    public IEnumerable<Category> GetAllCategories() => _categories;
    public void AddCategory(Category category) { _categories.Add(category); }
    public void DeleteCategory(Guid id) { _categories.RemoveAll(c => c.Id == id); }

    public IEnumerable<Tag> GetAllTags() => _tags;
    public void AddTag(Tag tag) { _tags.Add(tag); }
    public void DeleteTag(Guid id) { _tags.RemoveAll(t => t.Id == id); }
}
