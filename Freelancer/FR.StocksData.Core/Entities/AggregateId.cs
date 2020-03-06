using System;
using FR.StocksData.Core.Exceptions;

namespace FR.StocksData.Core.Entities
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