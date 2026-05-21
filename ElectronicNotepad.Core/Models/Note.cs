using System;
using System.Collections.Generic;
using ElectronicNotepad.Core.Enums;

namespace ElectronicNotepad.Core.Models;

public class Note
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    public Guid? CategoryId { get; set; }
    public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;
    public bool IsPinned { get; set; } = false;
    public string ColorHex { get; set; } = "#A8D5BA";
    
    public List<Reminder> Reminders { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();

    // Патерн Prototype: дозволяє об'єкту самому створювати свою копію
        public Note Clone()
        {
            return new Note
            {
                Id = this.Id,
                Title = this.Title,
                Content = this.Content,
                CategoryId = this.CategoryId,
                Priority = this.Priority,
                IsPinned = this.IsPinned,
                // Робимо глибоку копію списку, щоб не було проблем з посиланнями
                Reminders = this.Reminders != null ? new List<Reminder>(this.Reminders) : new List<Reminder>()
            };
        }
}
