using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ViewInventoryDetailsController(AssetService service)
        {
            _service = service;
        }

        // GET: ViewInventoryDetails
        public ActionResult Index()
        {
            //var assetDTOs = _service.GetAssetDTOs();
            var clientSiteDTO = _service.CreateClientSiteDTOs();
            var locationDTO = _service.CreateLocationDTOs();
            var assetDTO = _service.CreateAssetDTO();
            var productDTO = _service.CreateProductDTOs();
            var manufacturerDTO = _service.CreateManufacturerDTOs();
            var modelDTO = _service.CreateModelDTOs();
            AssetViewModel model = new AssetViewModel(/*assetDTOs,*/ clientSiteDTO, locationDTO, assetDTO, productDTO, manufacturerDTO, modelDTO);
            return View(model);
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
            AssetViewModel model = new AssetViewModel(/*assetDTOs,*/ clientSiteDTO, locationDTO, asset, productDTO, manufacturerDTO, modelDTO);
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
    }
}