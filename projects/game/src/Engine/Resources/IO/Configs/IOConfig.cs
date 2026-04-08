using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resources
{
    public abstract class IOConfig
    {
        [JsonIgnore]
        public string FilePath { get; protected set; }

        [JsonIgnore]
        public string RawJsonData { get; private set; } = string.Empty;

        protected IOConfig() { }

        /// <summary>
        /// Universal Load that handles both single objects and arrays (List<T> properties)
        /// </summary>
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

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true,
                    PropertyNameCaseInsensitive = true
                };

                // First attempt: Deserialize as normal object
                try
                {
                    JsonSerializer.Deserialize(json, this.GetType(), options);
                    Console.WriteLine($"Loaded config (object): {FilePath}");
                    return;
                }
                catch
                {
                    // Not a single object → probably an array. Continue below.
                }

                // Second attempt: Look for any property that contains "List" or "list" in its name
                var listProperties = this.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.PropertyType.IsGenericType &&
                                p.PropertyType.GetGenericTypeDefinition() == typeof(List<>) &&
                                (p.Name.Contains("List", StringComparison.OrdinalIgnoreCase) ||
                                 p.Name.Contains("Items", StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                if (listProperties.Count > 0)
                {
                    var listProperty = listProperties.First(); // Take the first matching property

                    var listType = listProperty.PropertyType;
                    var deserializedList = JsonSerializer.Deserialize(json, listType, options);

                    if (deserializedList != null)
                    {
                        listProperty.SetValue(this, deserializedList);
                        int count = ((dynamic)deserializedList).Count;
                        Console.WriteLine($"Loaded config (array): {FilePath} → {count} items into '{listProperty.Name}'");
                        return;
                    }
                }

                Console.WriteLine($"Warning: Could not deserialize {FilePath} as object or recognized list.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading {FilePath}: {ex.Message}");
                RawJsonData = string.Empty;
            }
        }

        public string GetRawJson()
        {
            return RawJsonData ?? string.Empty;
        }

        public virtual void Save()
        {
            if (string.IsNullOrEmpty(FilePath))
                return;

            string fullPath = GetFullPath();

            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(this, this.GetType(), options);
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