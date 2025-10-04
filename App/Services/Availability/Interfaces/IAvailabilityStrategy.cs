using App.Models;

namespace App.Services.Availability.Interfaces;

public interface IAvailabilityStrategy
{
    AvailabilityStatus CalculateAvailability(Product product, int currentStock);
}

/// <summary>
/// Statut de disponibilité d'un produit
/// </summary>
public enum AvailabilityStatus
{
    Available,
    Unavailable,
    PreOrder,
    Blocked
}
