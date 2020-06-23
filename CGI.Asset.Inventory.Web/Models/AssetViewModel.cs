using CGI.Asset.Inventory.Domain;
using CGI.Asset.Inventory.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGI.Asset.Inventory.Web.Models
{
    public class AssetViewModel
    {
        public AssetViewModel(/*IEnumerable<AssetDTO> assetDTOs,*/ List<ClientSiteDTO> clientSiteDTOs,List<LocationDTO> locationDTOs, AssetDTO assetDTO, List<ProductDTO> productDTOs, List<ManufacturerDTO> manufacturerDTOs, List<ModelDTO> modelDTOs)
        {
            //AssetDTOs = assetDTOs;
            ClientSiteDTOs = clientSiteDTOs;
            LocationDTOs = locationDTOs;
            AssetDTO = assetDTO;
            ProductDTOs = productDTOs;
            ManufacturerDTOs = manufacturerDTOs;
            ModelDTOs = modelDTOs;
        }
        public AssetDTO AssetDTO { get; set; }
        public LocationDTO LocationDTO { get; set; }
        public List<LocationDTO> LocationDTOs { get; set; }
        public ClientSiteDTO ClintSiteDTO { get; set; }
        public List<ClientSiteDTO> ClientSiteDTOs { get; set; }
        //public IEnumerable<AssetDTO> AssetDTOs {get; set;}
        public List<ProductDTO> ProductDTOs { get; set; }
        public ProductDTO ProductDTO { get; set; }
        public List<ManufacturerDTO> ManufacturerDTOs { get; set; }
        public ManufacturerDTO ManufacturerDTO { get; set; }
        public List<ModelDTO> ModelDTOs { get; set; }
        public ModelDTO ModelDTO { get; set; }

    }
}
