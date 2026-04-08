using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Resources
{
    public abstract class IOIniConfig
    {
        public List<IOIniLine> Lines { get; protected set; } = new List<IOIniLine>();

        protected abstract string FileName { get; }

        private string FullPath => Path.Combine(
            ResourceLoader.GLOBAL_RES_PATH,
            "config",
            FileName);

        protected IOIniConfig()
        {
            InitializeDefaults();
        }

        protected abstract void InitializeDefaults();

        public virtual void Load()
        {
            string directory = Path.GetDirectoryName(FullPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(FullPath))
            {
                Console.WriteLine($"{FileName} not found. Creating default...");
                Save();
                return;
            }

            try
            {
                Lines.Clear();
                var fileLines = File.ReadAllLines(FullPath);

                foreach (var rawLine in fileLines)
                {
                    string line = rawLine.Trim();
                    if (string.IsNullOrEmpty(line) || line.StartsWith(";") || line.StartsWith("#"))
                        continue;

                    var parts = line.Split('=', 2);
                    if (parts.Length != 2) continue;

                    string key = parts[0].Trim();
                    string valuePart = parts[1].Trim();

                    string comment = "";
                    int commentIndex = valuePart.IndexOf(';');
                    if (commentIndex >= 0)
                    {
                        comment = valuePart.Substring(commentIndex + 1).Trim();
                        valuePart = valuePart.Substring(0, commentIndex).Trim();
                    }

                    Lines.Add(new IOIniLine(key, valuePart, comment));
                }

                Console.WriteLine($"{FileName} loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading {FileName}: {ex.Message}");
            }
        }

        public virtual void Save()
        {
            try
            {
                var content = $"[{FileName.Replace(".ini", "")}]\n";
                foreach (var line in Lines)
                {
                    content += line.ToString() + "\n";
                }

                File.WriteAllText(FullPath, content);
                Console.WriteLine($"{FileName} saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save {FileName}: {ex.Message}");
            }
        }

        public virtual void Apply()
        {
            //
        }

        // Helper methods (kept for convenience)
        public string GetValue(string key, string defaultValue = "")
        {
            var line = Lines.FirstOrDefault(l => l.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
            return line?.Value ?? defaultValue;
        }

        public void SetValue(string key, string value)
        {
            var line = Lines.FirstOrDefault(l => l.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (line != null)
                line.Value = value;
            else
                Lines.Add(new IOIniLine(key, value));
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            if (int.TryParse(GetValue(key), out int result))
                return result;
            return defaultValue;
        }

        public bool GetBool(string key, bool defaultValue = false)
        {
            string val = GetValue(key).ToLowerInvariant();
            return val == "true" || val == "1" || val == "yes";
        }

        public Point GetPoint(string key, Point defaultValue = default)
        {
            string val = GetValue(key);
            if (string.IsNullOrWhiteSpace(val))
                return defaultValue;

            var parts = val.Split(',');
            if (parts.Length == 2 &&
                int.TryParse(parts[0].Trim(), out int x) &&
                int.TryParse(parts[1].Trim(), out int y))
            {
                return new Point(x, y);
            }

            return defaultValue;
        }

        public void SetPoint(string key, Point value)
        {
            SetValue(key, $"{value.X},{value.Y}");
        }

        public float GetFloat(string key, float defaultValue = 0f)
        {
            string valueStr = GetValue(key);

            if (string.IsNullOrWhiteSpace(valueStr))
                return defaultValue;

            if (float.TryParse(valueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                return result;

            return defaultValue;
        }
    }
}