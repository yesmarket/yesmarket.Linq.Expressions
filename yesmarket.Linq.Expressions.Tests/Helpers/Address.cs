namespace yesmarket.Linq.Expressions.Tests.Helpers
{
    public class Address
    {
        public string Suburb { get; set; }
        public int Postcode { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Address;
            if (other == null) return false;
            return Suburb.Equals(other.Suburb) && Postcode.Equals(other.Postcode);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + Suburb.GetHashCode();
                hash = hash * 23 + Postcode.GetHashCode();
                return hash;
            }
        }
    }
}