namespace Hotelos.Application.Contracts.Subscriptions.Dtos
{
    public sealed record UpdateSubscriptionDto(int Id,
                                               string Title,
                                               decimal Price,
                                               int NumberOfMounths);
}
