using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class IOIniConfigManager
    {

        private static readonly Type[] IniConfigList = new Type[]
        {
            typeof(IOGameIniConfig)
        };


        public List<IOIniConfig> IniConfigs;

        public IOIniConfigManager()
        {
            IniConfigs = new List<IOIniConfig>();
        }

        public void Init()
        {
            IniConfigs.Clear();

            foreach (Type type in IniConfigList)
            {
                if (Activator.CreateInstance(type) is IOIniConfig configInstance)
                {
                    IniConfigs.Add(configInstance);
                }
            }
        }


        public void Load()
        {
            foreach (IOIniConfig config in IniConfigs)
            {
                config.Load();
            }
        }


        public IOIniConfig GetConfig(Type type)
        {
            foreach (IOIniConfig config in IniConfigs)
            {
                if (type.IsAssignableFrom(config.GetType()))
                {
                    return config;
                }
            }

            return null;
        }


        public void ApplyAll()
        {
            foreach (IOIniConfig config in IniConfigs)
            {
                config.Apply();
            }
        }

        public void Apply(Type type)
        {
            GetConfig(type).Apply();
        }
    }
}
