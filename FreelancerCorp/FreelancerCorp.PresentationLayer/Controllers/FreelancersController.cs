using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.Facades;
using FreelancerCorp.PresentationLayer.Controllers.Helpers;
using FreelancerCorp.PresentationLayer.Models.Freelancers;
using FreelancerCorp.PresentationLayer.Models.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FreelancerCorp.PresentationLayer.Controllers
{
    public class FreelancersController : Controller
    {
        public const int PageSize = 9;

        private const string FilterSessionKey = "filter";

        public UserFacade UserFacade { get; set; }
        public RatingFacade RatingFacade { get; set; }
        public OfferFacade OfferFacade { get; set; }

        // GET: FreelancerController
        public async Task<ActionResult> Index(int page = 1)
        {
            var allFreelancers = await UserFacade.GetFreelancersAsync();

            var model = InitializeFreelancerListViewModel(new QueryResultDTO<FreelancerDTO, FreelancerFilterDTO> { Items = allFreelancers, Filter = new FreelancerFilterDTO() });
            return View("FreelancerListView", model);
        }

        private List<FreelancerDTO> FilterFreelancersByRating(List<FreelancerDTO> freelancers, double wantedRating)
        {
            var filteredFreelancers = new List<FreelancerDTO>();

            foreach (var freelancer in freelancers)
            {
                var avgRating = RatingHelper.CountAverageRating(freelancer.RatingCount, freelancer.SumRating);
                if (avgRating.HasValue && avgRating >= wantedRating)
                {
                    filteredFreelancers.Add(freelancer);
                }
            }

            return filteredFreelancers;
        } 

        [HttpPost]
        public async Task<ActionResult> Index(FreelancerListViewModel model)
        {
            model.Filter.PageSize = PageSize;            
            Session[FilterSessionKey] = model.Filter;

            var result = await UserFacade.GetFreelancersAsync(model.Filter);

            if (model.Filter.SearchedAverage.HasValue)
            {
                result.Items = FilterFreelancersByRating(result.Items.ToList(), model.Filter.SearchedAverage.Value);
            }

            var newModel = InitializeFreelancerListViewModel(result);
            return View("FreelancerListView", newModel);
        }

        // GET: FreelancerController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await UserFacade.GetFreelancerAsync(id);
            var user = await UserFacade.GetUserAsync(id);

            var offers = await OfferFacade.ListOffersAsync(new OfferFilterDTO { SearchedAuthorsIds = new int[] { id } });            
            model.Offers = new List<OfferDTO>(offers.Items);

            var ratings = await RatingFacade.ListRatingsAsync(new RatingFilterDTO { SearchedRatedUsersId = new int[] { id } });
            model.Ratings = await RatingHelper.MergeRatingsCreators(UserFacade, ratings.Items.ToList());

            model.SumRating = RatingHelper.CountAverageRating(model.RatingCount, model.SumRating);

            return View("FreelancerDetailView", InitializeFreelancerDetailViewModel(model, user.UserName));           
        }      

        private FreelancerDTO ParseCollection(FormCollection collection, FreelancerDTO freelancer)
        {
            string name = "", lastName = "";

            foreach (string key in collection.AllKeys)
            {
                switch (key)
                {
                    case "Name":
                        name = collection[key];
                        break;
                    case "LastName":
                        lastName = collection[key];
                        break;
                    case "Email":
                        freelancer.Email = collection[key];
                        break;
                    case "PhoneNumber":
                        freelancer.PhoneNumber = collection[key];
                        break;
                    case "Info":
                        freelancer.Info = collection[key];
                        break;
                    case "Location":
                        freelancer.Location = collection[key];
                        break;
                    case "Sex":
                        if (!Enum.TryParse(collection[key], out Sex newSex))
                        {
                            // THROW ERROR
                        }
                        else
                        {
                            freelancer.Sex = newSex;
                        }
                        break;
                    case "DoB":
                        if (!DateTime.TryParse(collection[key], out DateTime newDate))
                        {
                            // THROW ERROR
                        }
                        else
                        {
                            freelancer.DoB = newDate;
                        }
                        break;
                }
            }

            freelancer.Name = string.IsNullOrEmpty(lastName) ? name : name + " " + lastName;

            return freelancer;
        }

        // GET: FreelancerController/Create
        public ActionResult Create()
        {
            return View("FreelancerCreateView");
        }

        // POST: FreelancerController/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                FreelancerDTO newFreelancer = ParseCollection(collection, new FreelancerDTO());

                int newId = await UserFacade.CreateFreelancerAsync(newFreelancer);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }

        // GET: FreelancerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await UserFacade.GetFreelancerAsync(id);
            return View("FreelancerEditView", model);
        }

        // POST: FreelancerController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                FreelancerDTO editedFreelancer = new FreelancerDTO();
                editedFreelancer.Id = id;

                editedFreelancer = ParseCollection(collection, editedFreelancer);

                bool success = await UserFacade.EditFreelancerAsync(editedFreelancer);
                if (!success)
                    // Throw ERROR
                    throw new NotImplementedException();
                return RedirectToAction("Details", id);
            }
            catch
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }

        // GET: FreelancerController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            bool success = await UserFacade.DeleteFreelancerAsync(id);

            if (!success)
                // THROW ERROR
                throw new NotImplementedException();

            return RedirectToAction("Index");
        }

        // POST: FreelancerController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                bool success = await UserFacade.DeleteFreelancerAsync(id);

                if (!success)
                    // THROW ERROR
                    throw new NotImplementedException();

                return RedirectToAction("Index");
            }
            catch
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }
        private FreelancerDetailViewModel InitializeFreelancerDetailViewModel(FreelancerDTO freelancer, string freelancerUserName)
        {
            return new FreelancerDetailViewModel
            {
                Freelancer = freelancer,
                UserName = freelancerUserName
            };
        }

        private FreelancerListViewModel InitializeFreelancerListViewModel(QueryResultDTO<FreelancerDTO, FreelancerFilterDTO> result)
        {
            foreach (FreelancerDTO freelancer in result.Items)
            {
                freelancer.SumRating = RatingHelper.CountAverageRating(freelancer.RatingCount, freelancer.SumRating);                
            }

            return new FreelancerListViewModel
            {
                Freelancers = new List<FreelancerDTO>(result.Items),
                Filter = result.Filter
            };
        }
    }
}
