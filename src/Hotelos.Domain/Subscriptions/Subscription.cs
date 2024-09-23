using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Hotelos.Domain.Subscriptions
{
    public sealed class Subscription : FullAuditedAggregateRoot<int>
    {
        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public int NumberOfMonths { get; private set; }


        private Subscription()
        {
        }
        public static Subscription Create(string title,
                                          decimal price,
                                          int numberOfMonths,
                                          Guid userId)
        {
            var subscription = new Subscription()
            {
                Title = title,
                Price = price,
                NumberOfMonths = numberOfMonths,
                CreatorId = userId,
                CreationTime = DateTime.Now,
            };

            return subscription;
        }

        public void Update(string title,
                           decimal price,
                           int numberOfMonths,
                           Guid userId)
        {
            Title = title;
            Price = price;
            NumberOfMonths = numberOfMonths;
            LastModifierId = userId;
            LastModificationTime = DateTime.Now;
        }
    }
}
