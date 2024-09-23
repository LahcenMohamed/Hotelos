namespace Hotelos.Application.Contracts.Subscriptions.Dtos
{
    public sealed record CreateSubscriptionDto(string Title,
                                               decimal Price,
                                               int NumberOfMounths);
}
