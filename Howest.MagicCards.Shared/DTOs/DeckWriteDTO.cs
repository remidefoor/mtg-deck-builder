using System.ComponentModel.DataAnnotations;

namespace Howest.MagicCards.Shared.DTOs;

public record DeckWriteDTO
{
    [Required(ErrorMessage = "Please provide a name for the deck")]
    public string? Name { get; set; }
}
