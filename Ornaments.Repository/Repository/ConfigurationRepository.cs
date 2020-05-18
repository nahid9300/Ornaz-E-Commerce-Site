using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.Data.Entity;
using Ornaments.DatabaseContext.DatabaseContext;
using Ornaments.Model.Model;

namespace Ornaments.Repository.Repository
{
    public class ConfigurationRepository
    {
        OrnamentDbContext _dbContext=new OrnamentDbContext();

        public bool Add(Config config)
        {
            _dbContext.Configurations.Add(config);
            return _dbContext.SaveChanges() > 0;
        }

        public Config GetConfig(string Key)
        {

            return _dbContext.Configurations.FirstOrDefault((c => c.Key == Key));
        }

        public bool Update(Config config)
        {
            Config aConfig = _dbContext.Configurations.FirstOrDefault(c => c.Key == config.Key);
            if (aConfig != null)
            {
                aConfig.Value = config.Value;
                
            }

            return _dbContext.SaveChanges() > 0;
        }

        public List<Config> GetAll()
        {

            return _dbContext.Configurations.ToList();
        }

        public bool Delete(string Key)
        {
            Config aConfig = _dbContext.Configurations.FirstOrDefault(c => c.Key == Key);
            _dbContext.Configurations.Remove(aConfig);
            return _dbContext.SaveChanges() > 0;
        }

        public Config GetByKey(string Key)
        {

            return _dbContext.Configurations.FirstOrDefault((c => c.Key == Key));
        }



    }
}
