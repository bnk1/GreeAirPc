namespace GreeAirPC.Database
{
    using System.Collections.Generic;

    internal class AirConditionerModelEqualityComparer : IEqualityComparer<AirCondModel>
    {
        public bool Equals(AirCondModel x, AirCondModel y)
        {
            if (x == null || y == null)
            {
                return x == y;
            }

            return x.Equals(y);
        }

        public int GetHashCode(AirCondModel obj)
        {
            if (obj == null)
            {
                return 0;
            }

            return obj.GetHashCode();
        }
    }
}