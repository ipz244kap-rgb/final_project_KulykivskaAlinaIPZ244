using System;
using System.Collections.Generic;
using ElectronicNotepad.Core.Models;

namespace ElectronicNotepad.Core.Data;

public static class DefaultCategories
{
    public static IReadOnlyList<Category> All { get; } = new List<Category>
    {
        new Category
        {
            Id   = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Name = "Робота"
        },
        new Category
        {
            Id   = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Name = "Особисте"
        },
        new Category
        {
            Id   = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Name = "Навчання"
        },
    };
}
