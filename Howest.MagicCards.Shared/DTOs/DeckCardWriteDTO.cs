﻿using System.ComponentModel.DataAnnotations;

namespace Howest.MagicCards.Shared.DTOs;

public record DeckCardWriteDTO
{
    public long CardId { get; init; }
    [Required(ErrorMessage = "Pleas provide an amount for the deck card")]
    [Range(1, 60)]
    public int Amount { get; init; } = 1;
}
