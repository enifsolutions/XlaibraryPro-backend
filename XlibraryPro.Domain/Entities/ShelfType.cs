using XlibraryPro.Domain.Common;

namespace XlibraryPro.Domain.Entities;

/// <summary>
/// Maps to: tblShelf_type (shelf_type_id PK, shelf_type Short Text)
/// </summary>
public class ShelfTypeEntity : BaseEntity
{
    public string ShelfType { get; private set; }

    private ShelfTypeEntity() : base() { ShelfType = string.Empty; }

    public ShelfTypeEntity(long id, string shelfType) : base(id)
    {
        if (string.IsNullOrWhiteSpace(shelfType))
            throw new ArgumentException("Shelf type cannot be empty.", nameof(shelfType));
        ShelfType = shelfType.Trim();
    }

    public override string ToString() => ShelfType;
}