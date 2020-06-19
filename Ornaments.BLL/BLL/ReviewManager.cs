using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ornaments.Model.Model;
using Ornaments.Repository.Repository;

namespace Ornaments.BLL.BLL
{
    public class ReviewManager
    {
        ReviewRepository _reviewRepository=new ReviewRepository();
        public bool SaveReview(Review review)
        {
            return _reviewRepository.SaveReview(review);
        }

        public List<Review> GetRelatedReviewByProduct(int productID)
        {
            return _reviewRepository.GetRelatedReviewByProduct(productID);
        }
    }
}
