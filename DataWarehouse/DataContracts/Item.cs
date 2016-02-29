namespace DataWarehouse
{
    using System.Runtime.Serialization;
    using System.Threading;

    [DataContract]
    public class Item
    {
        // TODO: Add validations for the members
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Description { get; private set; }

        [DataMember]
        public int Price { get; private set; }

        public Item(string name, string description, int price, int initialCount)
        {
            _count = initialCount;
            Name = name;
            Description = description;
            Price = price;
        }

        internal int DecrementCount()
        {
            return Interlocked.Decrement(ref _count);
        }

        internal int GetCount()
        {
            return _count;
        }

        private int _count;

        private string _id;
    }
}