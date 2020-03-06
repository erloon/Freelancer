using System;
using FR.IdentityServer.Core.Exceptions;

namespace FR.IdentityServer.Core.Entities
{
    public class AggregateId
    {
        public Guid Value { get; }

        public AggregateId()
        {
            Value = Guid.NewGuid();
        }

        public AggregateId(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidAggregateIdException();
        }
    }
}