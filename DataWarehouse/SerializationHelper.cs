using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataWarehouse
{
    using System.IO;
    using System.Runtime.Serialization.Json;

    public class SerializationHelper
    {
        /// <summary>
        /// Serialize the object into JSON
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="value">Instance of the object</param>
        /// <returns>JSON representation of the object</returns>
        public static string Serialize<T>(T value)
        {
            string serializedJsonForObject = null; 

            using (MemoryStream ms = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(ms, value);
                ms.Position = 0; 
                using (StreamReader sr = new StreamReader(ms))
                {
                    serializedJsonForObject = sr.ReadToEnd();
                }
            }

            return serializedJsonForObject;
        }


        /// <summary>
        /// Deserializes Json response to the type T
        /// </summary>
        /// <typeparam name="T">Type to deserialize to</typeparam>
        /// <param name="value">Json string value</param>
        /// <returns>Instance of type T</returns>
        public static T Deserialize<T>(string value)
        {
            T instance = default(T);

            var deserializer = new DataContractJsonSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(ms))
                {
                    writer.Write(value);
                    writer.Flush();
                    ms.Position = 0;
                    instance = (T)deserializer.ReadObject(ms);
                }
            }

            return instance;
        }
    }
}