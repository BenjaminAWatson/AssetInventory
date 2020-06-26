using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CGI.Asset.Inventory.DataTables;
using CGI.Asset.Inventory.DTO;
using CGI.Asset.Inventory.Service;
using CGI.Asset.Inventory.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CGI.Asset.Inventory.Web.Controllers
{
    public class ViewInventoryDetailsController : Controller
    {
        private readonly AssetService _service;
        private readonly AuthenticationService _authService;
        private readonly string _user;

        public ViewInventoryDetailsController(AssetService service, AuthenticationService authService, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _authService = authService;
            _user = httpContextAccessor.HttpContext.User.Identity.Name.Remove(0, 4);
        }

        // GET: ViewInventoryDetails
        public ActionResult Index()
        {
            var user = _user;
            var isUserValid = _authService.IsUserValid(user);
            var authroizationLevelRequired = 1;
            if (isUserValid)
            {
                var isUserAuthorized = _authService.isUserAuthorized(user, authroizationLevelRequired);
                if (isUserAuthorized)
                {
                    var clientSiteDTO = _service.CreateClientSiteDTOs();
                    var locationDTO = _service.CreateLocationDTOs();
                    var assetDTO = _service.CreateAssetDTO();
                    var productDTO = _service.CreateProductDTOs();
                    var manufacturerDTO = _service.CreateManufacturerDTOs();
                    var modelDTO = _service.CreateModelDTOs();
                    AssetViewModel model = new AssetViewModel(clientSiteDTO, locationDTO, assetDTO, productDTO, manufacturerDTO, modelDTO);
                    return View(model);
                }
                else
                    return RedirectToAction("Index", "UnauthorizedAccess");
            }
            else
                return RedirectToAction("Index", "UnauthorizedAccess");

        }

        [HttpPost]
        public ActionResult GetModalData(int id)
        {
            var clientSiteDTO = _service.CreateClientSiteDTOs();
            var locationDTO = _service.CreateLocationDTOs();
            var asset = _service.GetAssetDTOsByAssetTag(id).Single();
            var productDTO = _service.CreateProductDTOs();
            var manufacturerDTO = _service.CreateManufacturerDTOs();
            var modelDTO = _service.CreateModelDTOs();
            AssetViewModel model = new AssetViewModel(clientSiteDTO, locationDTO, asset, productDTO, manufacturerDTO, modelDTO);
            return PartialView("_ModifyAssetModal",model);
        }

        [HttpPost]
        public ActionResult ModifyAsset(AssetDTO assetDTO)
        {

            _service.ModifyAsset(assetDTO);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult SearchInventoryDetails(AssetDTO assetDTO)
        {
            var wasInventoryFound = _service.SearchInventory(assetDTO);
            if (wasInventoryFound.Equals(true))
            {
                return PartialView("_SearchResults");
            }
            else
            {
                return PartialView("_ItemNotFound");
            }
        }

        [HttpPost]
        public JsonResult GetResultData(ResultsDataTableSent sent)

        {
            var model = _service.GetAssetsPaginated(sent);
            return Json(model);
        }
    }
}