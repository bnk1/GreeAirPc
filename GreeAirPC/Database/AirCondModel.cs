namespace GreeAirPC.Database
{
    public class AirCondModel
    {
        public AirCondModel(string iD, string name, string privateKey, string address)
        {
            ID = iD;
            Name = name;
            PrivateKey = privateKey;
            Address = address;
        }

        public AirCondModel()
        {
        }

        public string ID { get; set; }

        public string Name { get; set; }

        public string PrivateKey { get; set; }

        public string Address { get; set; }

        public override string ToString()
        {
            return $"AirConditionerModel(ID={ID}, Name={Name}, PrivateKey={PrivateKey}, Address={Address})";
        }

        public override bool Equals(object obj)
        {
            var o = obj as AirCondModel;

            if (o == null)
            {
                return false;
            }

            return this.ID == o.ID &&
                this.Name == o.Name &&
                this.PrivateKey == o.PrivateKey &&
                this.Address == o.Address;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode()
                ^ this.Name.GetHashCode()
                ^ this.PrivateKey.GetHashCode()
                ^ this.Address.GetHashCode();
        }
    }
}
