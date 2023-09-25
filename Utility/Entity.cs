using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Utility
{
    public abstract class Entity
    {
        public long Id { get; set; }

        public Entity() { }

        public Entity(long id)
        {
            Id = id;
        }

        public override string ToString()
        {
            string output = "";

            // Loop over the public properties
            foreach (var propInfo in this.GetType().GetProperties())
            {
                output += $"{propInfo.Name}: {propInfo.GetValue(this)}{Environment.NewLine}";
            }

            return output;
        }

        public void PopulateFromRecord(Dictionary<string, object> record)
        {
            foreach (var propInfo in this.GetType().GetProperties())
            {
                try
                {
                    // Try to get the custom name of the property assigned by ColumnAttribute.
                    var columnAttr = propInfo.GetCustomAttribute<ColumnAttribute>();
                    // If ColumnAttribute is not used, fall back on property's name.
                    string keyName = columnAttr?.Name ?? propInfo.Name.ToLower();

                    // The record recieves object values from the database. They may be DBNull!
                    if (record.ContainsKey(keyName) && record[keyName] is not DBNull)
                    {
                        propInfo.SetValue(this, record[keyName]);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write($"ERROR populating {propInfo.Name} property.");
                    Console.WriteLine($"Value is not of type {propInfo.PropertyType.Name}.");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}