using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resources
{
    public abstract class IOConfig
    {
        [JsonIgnore]
        public string FilePath { get; protected set; }

        // This will store the raw JSON string after loading
        [JsonIgnore]
        public string RawJsonData { get; private set; } = string.Empty;

        protected IOConfig() { }

        /// <summary>
        /// Loads the config from JSON file and stores the raw JSON string.
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
                Save(); // Save default values
                RawJsonData = string.Empty;
                return;
            }

            try
            {
                string json = File.ReadAllText(fullPath);
                RawJsonData = json; // ← Store raw JSON here

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true
                };

                // Deserialize into current instance
                JsonSerializer.Deserialize(json, this.GetType(), options);

                Console.WriteLine($"Loaded config: {FilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading {FilePath}: {ex.Message}");
                Console.WriteLine("Using default values.");
                RawJsonData = string.Empty;
            }
        }

        /// <summary>
        /// Returns the raw JSON string that was loaded from file.
        /// Returns empty string if no file was loaded or loading failed.
        /// </summary>
        public string GetRawJson()
        {
            return RawJsonData ?? string.Empty;
        }

        /// <summary>
        /// Saves current config to its FilePath
        /// </summary>
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
                RawJsonData = json; // Update raw data after saving

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
    }
}