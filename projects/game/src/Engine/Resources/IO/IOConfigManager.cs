using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class IOConfigManager
    {

        public static List<IOConfig> Configs = new List<IOConfig>();

        public IOConfigManager() 
        {
            
        }

        public void Init()
        {
            Configs.Add(new IOGameConfig());
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
    }
}
