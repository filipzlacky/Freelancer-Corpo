using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.Facades;
using FreelancerCorp.PresentationLayer.Models.Freelancers;
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

        // GET: FreelancerController
        public async Task<ActionResult> Index(int page = 1)
        {
            //var filter = Session[FilterSessionKey] as FreelancerFilterDTO ?? new FreelancerFilterDTO { PageSize = PageSize };
            //filter.RequestedPageNumber = page;
            
            //var allFreelancers = await UserFacade.GetFreelancersAsync(new FreelancerFilterDTO());
            //var result = await UserFacade.GetFreelancersAsync(filter);
           
            //var model = await InitializeFreelancerListViewModel(result, (int)allFreelancers.TotalItemsCount);
            return View("FreelancerListView"/*, model*/);
        }

        [HttpPost]
        public async Task<ActionResult> Index(FreelancerListViewModel model)
        {
            model.Filter.PageSize = PageSize;            
            Session[FilterSessionKey] = model.Filter;

            var allFreelancers = await UserFacade.GetFreelancersAsync(new FreelancerFilterDTO());
            var result = await UserFacade.GetFreelancersAsync(model.Filter);
            var newModel = await InitializeFreelancerListViewModel(result, (int)allFreelancers.TotalItemsCount);
            return View("FreelancerListView", newModel);
        }

        // GET: FreelancerController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await UserFacade.GetFreelancerAsync(id);
            //return View("FreelancerDetailView", model);
            return View();
        }

        // GET: FreelancerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FreelancerController/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FreelancerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FreelancerController/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FreelancerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FreelancerController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private async Task<FreelancerListViewModel> InitializeFreelancerListViewModel(QueryResultDTO<FreelancerDTO, FreelancerFilterDTO> result, int totalItemsCount)
        {
            return new FreelancerListViewModel
            {
                Freelancers = new List<FreelancerDTO>(result.Items),
                Filter = result.Filter
            };
        }
    }
}
