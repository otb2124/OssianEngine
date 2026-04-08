namespace Resources
{
    public class IOIniLine
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; } = "";

        public IOIniLine() { }

        public IOIniLine(string key, string value, string comment = "")
        {
            Key = key;
            Value = value;
            Comment = comment;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Comment))
                return $"{Key}={Value}";
            else
                return $"{Key}={Value}    ; {Comment}";
        }
    }
}