using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ornaments.Model.Model;
using Ornaments.Repository.Repository;

namespace Ornaments.BLL.BLL
{
    public class ConfigurationManager
    {
        ConfigurationRepository _configurationRepository=new ConfigurationRepository();

        public bool Add(Config config)
        {
            return _configurationRepository.Add(config);

        }

        public Config GetConfig(string Key)
        {
            return _configurationRepository.GetConfig(Key);
        }

        public bool Update(Config config)
        {
            return _configurationRepository.Update(config);
        }

        public List<Config> GetAll()
        {
            return _configurationRepository.GetAll();
        }

        public bool Delete(string Key)
        {
            return _configurationRepository.Delete(Key);
        }

        public Config GetByKey(string Key)
        {
            return _configurationRepository.GetByKey(Key);
        }
    }
}
