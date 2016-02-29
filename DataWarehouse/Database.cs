namespace DataWarehouse
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
 
    public static class Database
    {
        /// <summary>
        /// Static Initializer for the database to populate the elements. 
        /// The static constructor for a class executes at most once in a given application domain. Hence, this should also be thread safe. 
        /// </summary>
        static Database()
        {
            PopulateInMemoryDatabase();
        }

        /// <summary>
        /// Return all the items from the repository which are available.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, Item>>  GetAllItems()
        {
            // Return all items that are currently available. 
            // Its possible that by the time the user places the order, some of them get sold out.
            return _allItems.Where(item => item.Value.GetCount() > 0);
        }

        /// <summary>
        /// A wrapper around the concurrent dictionary's TryGetValue. 
        /// Instead of exposing the dictionary itself and all its methods, we can closely control what functionality of the dictionary is exposed to client. 
        /// </summary>
        /// <param name="id">Id of the element</param>
        /// <param name="item">Return the Item requested</param>
        /// <returns>true if the operation succeeded, false otherwise</returns>
        public static bool TryGetValue(string id, out Item item)
        {
            return _allItems.TryGetValue(id, out item);
        }

        public static bool TryRemove(string id, out Item item)
        {
            return _allItems.TryRemove(id, out item);
        }

        /// <summary>
        /// This method is defined for the tests.  This allows the tests to add their own items and check if the values are properly getting retrieved or not. 
        /// </summary>
        /// <param name="itemName">Name of the item</param>
        /// <param name="itemDescription">Description of the item</param>
        /// <param name="price">Price of the item</param>
        /// <param name="count">Initial count of the item in the repository</param>
        static internal KeyValuePair<string, Item> AddItem(string itemName, string itemDescription, int price, int count)
        {
            if (count == 0)
            {
                throw new ApplicationException("Cannot add an item with 0 initial count");
            }

            Item newItem = new Item(itemName, itemDescription, price, count);
            string itemId = Guid.NewGuid().ToString(); 

            // should never throw exception because key can never be null
            if (!_allItems.TryAdd(itemId, newItem))
            {
                throw new ApplicationException("failed to add item");
            }

            return new KeyValuePair<string, Item>(itemId, newItem);
        } 


        /// <summary>
        /// Initialized the database and fills it with items
        /// </summary>
        private static void PopulateInMemoryDatabase()
        {
            _allItems = new ConcurrentDictionary<string, Item>();

            try
            {
                AddXItems(6);
            }
            catch (OverflowException oe)
            {
                throw new ApplicationException(
                    string.Format("Currently the database allows only {0} values to be stored", Int32.MaxValue));
            }
        }

        /// <summary>
        /// Adds x number of items to the database
        /// </summary>
        /// <param name="x">Number of items to be added</param>
        private static void AddXItems(int x)
        {
            for (int index = 0; index < x; index++)
            {
                AddItem(string.Concat("Name", index.ToString()), string.Concat("Description", index.ToString()), index, 100);
            }
        }

        // In memory database list of all items. 
        private static ConcurrentDictionary<String, Item> _allItems;
    }
}