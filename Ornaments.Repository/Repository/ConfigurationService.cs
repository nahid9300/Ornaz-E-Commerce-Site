using Ornaments.DatabaseContext.DatabaseContext;
using Ornaments.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.Repository.Repository
{
    public class ConfigurationService
    {
        OrnamentDbContext _dbContext = new OrnamentDbContext();
        #region Singleton
        public static ConfigurationService Instance
        {

            get
            {
                if (instance == null) instance = new ConfigurationService();

                return instance;
            }
        }
        private static ConfigurationService instance { get; set; }
        private ConfigurationService()
        {
        }
        #endregion

        public Config GetConfig(string Key)
        {
           
            
                return _dbContext.Configurations.Find(Key);
            
        }

        public int PageSize()
        {
                var pageSizeConfig = _dbContext.Configurations.Find("PageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 3;
            
        }

        public int ShopPageSize()
        {
            
        
            var pageSizeConfig = _dbContext.Configurations.Find("ShopPageSize");

           return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
          
        }
    }
}
