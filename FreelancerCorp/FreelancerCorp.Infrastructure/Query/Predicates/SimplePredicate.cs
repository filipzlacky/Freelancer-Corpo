using System;
using System.Collections.Generic;
using System.Linq;
using FreelancerCorp.Infrastructure.Query.Predicates.Operators;

namespace FreelancerCorp.Infrastructure.Query.Predicates
{
    public class SimplePredicate : IPredicate
    {
        public string TargetPropertyName { get; set; }
        public Object ComparedValue { get; set; }        
        public ValueComparingOperator ValueComparingOperator { get; }
        public SimplePredicate(string targetPropertyName, ValueComparingOperator valOp, Object value) {
            TargetPropertyName = targetPropertyName;
            ComparedValue = value;
            ValueComparingOperator = valOp;
        }


        protected bool Equals(SimplePredicate other) {
            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj.GetType() == this.GetType() && Equals((SimplePredicate)obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((TargetPropertyName != null ? TargetPropertyName.GetHashCode() : 0) * 397) ^ (int)ValueComparingOperator ^ ComparedValue.GetHashCode();
            }
        }
    }
}
