﻿using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using FreelancerCorp.BusinessLayer.Facades;
using FreelancerCorp.PresentationLayer.Models.Corporations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FreelancerCorp.PresentationLayer.Controllers
{
    public class CorporationsController : Controller
    {
        public const int PageSize = 9;

        private const string FilterSessionKey = "filter";

        public UserFacade UserFacade { get; set; }

        // GET: Corporations
        public async Task<ActionResult> Index(int page = 1)
        {            
            var allCorporations = await UserFacade.GetCorporationsAsync();            

            var model = InitializeCorporationListViewModel(new QueryResultDTO<CorporationDTO, CorporationFilterDTO> { Items = allCorporations });
            return View("CorporationListView", model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(CorporationListViewModel model)
        {
            model.Filter.PageSize = PageSize;
            Session[FilterSessionKey] = model.Filter;

            var result = await UserFacade.GetCorporationsAsync(model.Filter);

            var newModel = InitializeCorporationListViewModel(result);
            return View("CorporationListView", newModel);
        }

        // GET: Corporations/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await UserFacade.GetCorporationAsync(id);
            return View("CorporationDetailView", model);
        }

        // GET: Corporations/Create
        public ActionResult Create()
        {
            return View("CorporationCreateView");
        }

        // POST: Corporations/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                CorporationDTO newCorporation = new CorporationDTO();

                foreach (string key in collection.AllKeys)
                {
                    switch (key)
                    {
                        case "Name":
                            newCorporation.Name = collection[key];
                            break;                        
                        case "Email":
                            newCorporation.Email = collection[key];
                            break;
                        case "PhoneNumber":
                            newCorporation.PhoneNumber = collection[key];
                            break;
                        case "Info":
                            newCorporation.Info = collection[key];
                            break;
                        case "Address":
                            newCorporation.Address = collection[key];
                            break;                        
                    }
                }

                int newId = await UserFacade.CreateCorporationAsync(newCorporation);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }

        // GET: Corporations/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var corporation = await UserFacade.GetCorporationAsync(id);
            return View("CorporationEditView", corporation);
        }

        // POST: Corporations/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                CorporationDTO newCorporation = new CorporationDTO();

                foreach (string key in collection.AllKeys)
                {
                    switch (key)
                    {
                        case "Name":
                            newCorporation.Name = collection[key];
                            break;                        
                        case "Email":
                            newCorporation.Email = collection[key];
                            break;
                        case "PhoneNumber":
                            newCorporation.PhoneNumber = collection[key];
                            break;
                        case "Info":
                            newCorporation.Info = collection[key];
                            break;
                        case "Address":
                            newCorporation.Address = collection[key];
                            break;
                    }
                }

                bool success = await UserFacade.EditCorporationAsync(newCorporation);

                if (!success)
                {
                    // THROW ERROR
                    throw new NotImplementedException();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }

        // GET: Corporations/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            bool success = await UserFacade.DeleteCorporationAsync(id);

            if (!success)
                // THROW ERROR
                throw new NotImplementedException();

            return RedirectToAction("Index");
        }

        // POST: Corporations/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                bool success = await UserFacade.DeleteCorporationAsync(id);

                if (!success)
                {
                    // THROW ERROR
                    throw new NotImplementedException();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        private CorporationListViewModel InitializeCorporationListViewModel(QueryResultDTO<CorporationDTO, CorporationFilterDTO> result)
        {
            return new CorporationListViewModel
            {
                Corporations = new List<CorporationDTO>(result.Items),
                Filter = result.Filter
            };
        }
    }
}
