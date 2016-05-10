namespace yesmarket.Linq.Expressions.Tests.Helpers
{
    public class Customer
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Customer;
            if (other == null) return false;
            return Name.Equals(other.Name) && Address.Equals(other.Address);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + Address.GetHashCode();
                return hash;
            }
        }
    }
}