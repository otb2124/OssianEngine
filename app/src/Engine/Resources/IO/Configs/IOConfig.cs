using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Resources
{
    public abstract class IOConfig
    {
        [JsonIgnore]
        public string FilePath { get; protected set; }

        [JsonIgnore]
        public string RawJsonData { get; private set; } = string.Empty;

        protected IOConfig() { }

        public virtual void Load()
        {
            if (string.IsNullOrEmpty(FilePath))
                throw new InvalidOperationException("FilePath must be set in derived class.");

            string fullPath = GetFullPath();

            string directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"Config not found: {FilePath}. Creating default...");
                Save();
                RawJsonData = string.Empty;
                return;
            }

            try
            {
                string json = File.ReadAllText(fullPath);
                RawJsonData = json;

                // Try as single object first
                try
                {
                    JsonConvert.PopulateObject(json, this);
                    Console.WriteLine($"{FilePath}.json loaded successfully.");
                    return;
                }
                catch (Exception ex1)
                {
                }

                // Try as array into List properties
                var listProperties = this.GetType()
                    .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                    .Where(p => p.CanWrite &&
                                p.PropertyType.IsGenericType &&
                                p.PropertyType.GetGenericTypeDefinition() == typeof(List<>) &&
                                (p.Name.Contains("List", StringComparison.OrdinalIgnoreCase) ||
                                 p.Name.Contains("Items", StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                if (listProperties.Count > 0)
                {
                    var targetProperty = listProperties.First();
                    var listType = targetProperty.PropertyType;

                    try
                    {
                        var deserializedList = JsonConvert.DeserializeObject(json, listType);

                        if (deserializedList != null)
                        {
                            targetProperty.SetValue(this, deserializedList);
                            int count = ((dynamic)deserializedList).Count;
                            Console.WriteLine($"{FilePath}.json with {count} objects loaded successfully.");
                            return;
                        }
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine($"Array deserialization failed: {ex2.Message}");
                    }
                }

                Console.WriteLine($"Warning: Could not deserialize {FilePath}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Critical error loading {FilePath}: {ex.Message}");
                RawJsonData = string.Empty;
            }
        }

        public string GetRawJson() => RawJsonData ?? string.Empty;

        public virtual void Save()
        {
            if (string.IsNullOrEmpty(FilePath)) return;

            string fullPath = GetFullPath();

            try
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(fullPath, json);
                RawJsonData = json;
                Console.WriteLine($"Saved config: {FilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save {FilePath}: {ex.Message}");
            }
        }

        private string GetFullPath()
        {
            return Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                ResourceLoader.GLOBAL_RES_PATH,
                "config",
                FilePath + ".json");
        }

        public virtual void Apply() { }
    }
}