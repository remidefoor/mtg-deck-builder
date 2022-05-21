using System.ComponentModel.DataAnnotations;

namespace Howest.MagicCards.Shared.DTOs;

public record DeckWriteDTO
{
    [Required(ErrorMessage = "Please provide a name for the deck")]
    [MinLength(1)]
    [MaxLength(255)]
    public string? Name { get; set; }
}
