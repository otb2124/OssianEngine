using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class IOConfigManager
    {

        private static readonly Type[] ConfigList = new Type[]
        {
            typeof(IOItemsConfig)
        };


        public List<IOConfig> Configs;

        public IOConfigManager() 
        {
            Configs = new List<IOConfig>();
        }

        public void Init()
        {
            Configs.Clear();

            foreach (Type type in ConfigList)
            {
                if (Activator.CreateInstance(type) is IOConfig configInstance)
                {
                    Configs.Add(configInstance);
                }
            }
        }


        public void Load()
        {
            foreach (IOConfig config in Configs)
            {
                config.Load();
            }
        }


        public IOConfig GetConfig(Type type)
        {
            foreach (IOConfig config in Configs)
            {
                if(type.IsAssignableFrom(config.GetType()))
                {
                    return config;
                }
            }

            return null;
        }


        public void ApplyAll()
        {
            foreach (IOConfig config in Configs)
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
