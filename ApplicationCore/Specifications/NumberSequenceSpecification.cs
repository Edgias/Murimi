using NigTech.Murimi.ApplicationCore.Entities;

namespace NigTech.Murimi.ApplicationCore.Specifications
{
    public class NumberSequenceSpecification : BaseSpecification<NumberSequence>
    {
        public NumberSequenceSpecification()
            : base(criteria: null!)
        {
        }

        public NumberSequenceSpecification(string entity)
            : base(ns => ns.Entity.Equals(entity))
        {
        }

        public NumberSequenceSpecification(int skip, int take, string searchQuery) 
            : base(ns => string.IsNullOrEmpty(searchQuery) || 
            ns.Entity.Contains(searchQuery) || 
            ns.Prefix.Contains(searchQuery) || 
            ns.Seperator.Contains(searchQuery))
        {
            ApplyOrderBy(ns => ns.Entity);
            ApplyPaging(skip, take);
        }
    }
}
