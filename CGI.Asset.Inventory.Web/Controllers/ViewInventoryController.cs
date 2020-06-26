using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CGI.Asset.Inventory.DataTables;
using CGI.Asset.Inventory.Domain;
using CGI.Asset.Inventory.DTO;
using CGI.Asset.Inventory.Service;
using CGI.Asset.Inventory.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CGI.Asset.Inventory.Web.Controllers
{
    public class ViewInventoryController : Controller
    {
        private readonly AssetService _service;
        private readonly string _user;

        public ViewInventoryController(AssetService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _user = httpContextAccessor.HttpContext.User.Identity.Name.Remove(0, 4);
        }

        public ActionResult Index()
        {
            var user = _user;
            var clientSiteDTO = _service.CreateClientSiteDTOs();
            var locationDTO = _service.CreateLocationDTOs();
            var assetDTO = _service.CreateAssetDTO();
            var productDTO = _service.CreateProductDTOs();
            var manufacturerDTO = _service.CreateManufacturerDTOs();
            var modelDTO = _service.CreateModelDTOs();
            AssetViewModel model = new AssetViewModel(clientSiteDTO, locationDTO, assetDTO,productDTO,manufacturerDTO,modelDTO);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddItem(AssetDTO assetDTO)
        {
            var doesAssetAlreadyExist = _service.SearchInventory(assetDTO);
            if (doesAssetAlreadyExist)
            {
                return Json(new { success = false, responseText = "This Asset Already Exists. Please Try Again" });
            }
            else
            {
                _service.CreateAsset(assetDTO);
                return Json(new { success = true, responseText = "The Asset Was Added" });
            }
        }

        [HttpPost]
        public ActionResult MessageModal()
        {
            return RedirectToAction("Index");
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
            return PartialView("_ModifyAssetModal", model);
        }

        [HttpPost]
        public void ModifyAsset(AssetDTO assetDTO)
        {
            _service.ModifyAsset(assetDTO);
        }

        [HttpPost]
        public ActionResult SearchInventory(AssetDTO assetDTO)
        {
            var wasInventoryFound = _service.SearchInventory(assetDTO);
            if (wasInventoryFound.Equals(true))
            {
                return PartialView("_ItemFound");
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

        [HttpPost]
        public ActionResult SubmitAsset(AssetDTO assetDTO)
        {
            _service.SubmitAsset(assetDTO);
            return PartialView("_StartPage");
        }
    }
}