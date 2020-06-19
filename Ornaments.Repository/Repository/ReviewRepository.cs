using Ornaments.DatabaseContext.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ornaments.Model.Model;

namespace Ornaments.Repository.Repository
{
    public class ReviewRepository
    {
        OrnamentDbContext _dbContext = new OrnamentDbContext();

        
        public bool SaveReview(Review review)
        {
            _dbContext.Reviews.Add(review);
            return _dbContext.SaveChanges() > 0;
        }

        public List<Review> GetRelatedReviewByProduct(int productID)
        {

            return _dbContext.Reviews.Where(x => x.Product.Id == productID).OrderByDescending(x => x.Id).Take(4).ToList();
        }
    }
}
