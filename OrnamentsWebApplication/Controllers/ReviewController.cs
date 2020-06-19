using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Ornaments.BLL.BLL;
using Ornaments.Model.Model;
using OrnamentsWebApplication.ViewModel;

namespace OrnamentsWebApplication.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Review
        ReviewManager _reviewManager = new ReviewManager();
       

       
        [HttpPost]
        public JsonResult GetReview(ReviewViewModel reviewViewModel)
        {
            JsonResult result = new JsonResult();
                Review review = new Review();
                review.productId = reviewViewModel.ProductID;
                review.name = User.Identity.GetUserName();
                review.reviewDetails = reviewViewModel.reviewDetails;
                var res = _reviewManager.SaveReview(review);
                result.Data = new { Success = res };

                return result;
        }
    }
}