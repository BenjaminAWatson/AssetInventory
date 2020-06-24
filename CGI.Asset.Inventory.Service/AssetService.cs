using CGI.Asset.Inventory.Data;
using CGI.Asset.Inventory.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CGI.Asset.Inventory.DTO;
using System.Collections;
using CGI.Asset.Inventory.DataTables;

namespace CGI.Asset.Inventory.Service
{
    public class AssetService
    {
        private readonly CGIAssetInventoryContext db;

        public AssetService(CGIAssetInventoryContext context)
        {
            db = context;
        }
        public AssetDTO CreateAssetDTO()
        {
            AssetDTO assetDTO = new AssetDTO()
            {
                LocationKey = 1,
                ClientSiteKey = 1,
            };
            return assetDTO;
        }

        public void CreateAsset(AssetDTO assetDTO)
        {
            Domain.Asset asset = new Domain.Asset()
            {
                AssetKey = assetDTO.AssetTag,
                ProductKey = assetDTO.ProductKey,
                ManufacturerKey = assetDTO.ManufacturerKey,
                ModelKey = assetDTO.ModelKey,
                LocationKey = assetDTO.LocationKey,
                ClientSiteKey = assetDTO.ClientSiteKey,
                SerialNumber = assetDTO.SerialNumber,
                ItemName = assetDTO.ItemName,
                InventoryDate = assetDTO.InventoryDate,
                PurchaseDate = assetDTO.PurchaseDate,
                InventoryOwner = assetDTO.InventoryOwner,
                InventoriedBy = assetDTO.InventoriedBy,
                IsDisposed = assetDTO.isDisposed
            };
            db.Asset.Add(asset);
            db.SaveChanges();
        }

        public void SubmitAsset(AssetDTO assetDTO)
        {
            var asset = db.Asset.Single(x => x.AssetKey == assetDTO.AssetTag);

            asset.InventoryDate = DateTime.Now;
            db.SaveChanges();
        }

        public void ModifyAsset(AssetDTO assetDTO)
        {
            var asset = db.Asset.Single(x => x.AssetKey == assetDTO.AssetTag);

            asset.AssetKey = assetDTO.AssetTag;
            asset.ProductKey = assetDTO.ProductKey;
            asset.ManufacturerKey = assetDTO.ManufacturerKey;
            asset.ModelKey = assetDTO.ModelKey;
            asset.LocationKey = assetDTO.LocationKey;
            asset.ClientSiteKey = assetDTO.ClientSiteKey;
            asset.SerialNumber = assetDTO.SerialNumber;
            asset.ItemName = assetDTO.ItemName;
            asset.PurchaseDate = assetDTO.PurchaseDate;
            asset.InventoryOwner = assetDTO.InventoryOwner;
            asset.InventoriedBy = assetDTO.InventoriedBy;
            asset.InventoryDate = assetDTO.InventoryDate;
            asset.IsDisposed = assetDTO.isDisposed;

            db.SaveChanges();

        }

        public IEnumerable<AssetDTO> GetAssetDTOs(ResultsDataTableSent sent)
        {
            if (string.IsNullOrEmpty(sent.KeywordSearch))
            {
                var result = from a in db.Asset
                         .Where(a => a.IsDisposed == false && (a.AssetKey.ToString().Contains(sent.AssetTag.ToString()) || a.InventoryOwner.Contains(sent.InventoryOwner)) && (a.ClientSiteKey.Equals(sent.ClientSiteKey) || a.LocationKey.Equals(sent.LocationKey)))
                             orderby a.AssetKey ascending
                             select new AssetDTO()
                             {
                                 AssetTag = a.AssetKey,
                                 Product = a.ProductKeyNavigation.ProductName,
                                 Manufacturer = a.ManufacturerKeyNavigation.ManufacturerName,
                                 Model = a.ModelKeyNavigation.ModelName,
                                 SerialNumber = a.SerialNumber,
                                 ItemName = a.ItemName,
                                 Location = a.LocationKeyNavigation.LocationName,
                                 ClientSite = a.ClientSiteKeyNavigation.ClientSiteName,
                                 PurchaseDate = a.PurchaseDate,
                                 InventoryOwner = a.InventoryOwner,
                                 InventoriedBy = a.InventoriedBy,
                                 isDisposed = a.IsDisposed
                             };
                return result;
            }
            else
            {
                var result = from a in db.Asset
                         .Where(a => a.IsDisposed == false &&
                         a.AssetKey.ToString().Contains(sent.KeywordSearch.ToString()) ||
                         a.InventoriedBy.Contains(sent.KeywordSearch) ||
                         a.InventoryDate.ToString().Contains(sent.KeywordSearch) ||
                         a.InventoryOwner.Contains(sent.KeywordSearch) ||
                         a.ItemName.Contains(sent.KeywordSearch) ||
                         a.LocationKeyNavigation.LocationName.Contains(sent.KeywordSearch) ||
                         a.ManufacturerKeyNavigation.ManufacturerName.Contains(sent.KeywordSearch) ||
                         a.ModelKeyNavigation.ModelName.Contains(sent.KeywordSearch) ||
                         a.ProductKeyNavigation.ProductName.Contains(sent.KeywordSearch) ||
                         a.PurchaseDate.ToString().Contains(sent.KeywordSearch) ||
                         a.SerialNumber.ToString().Contains(sent.KeywordSearch) ||
                         a.AssetKey.ToString().Contains(sent.AssetTag.ToString()) ||
                         a.ClientSiteKey.Equals(sent.ClientSiteKey) ||
                         a.LocationKey.Equals(sent.LocationKey))
                             orderby a.AssetKey ascending
                             select new AssetDTO()
                             {
                                 AssetTag = a.AssetKey,
                                 Product = a.ProductKeyNavigation.ProductName,
                                 Manufacturer = a.ManufacturerKeyNavigation.ManufacturerName,
                                 Model = a.ModelKeyNavigation.ModelName,
                                 SerialNumber = a.SerialNumber,
                                 ItemName = a.ItemName,
                                 Location = a.LocationKeyNavigation.LocationName,
                                 ClientSite = a.ClientSiteKeyNavigation.ClientSiteName,
                                 PurchaseDate = a.PurchaseDate,
                                 InventoryOwner = a.InventoryOwner,
                                 InventoriedBy = a.InventoriedBy,
                                 isDisposed = a.IsDisposed
                             };
                return result;
            }

                
        }

        public IEnumerable<AssetDTO> GetAssetDTOsByAssetTag(int assetTag)
        {
            var result = from a in db.Asset
                         .Where(a => a.AssetKey.Equals(assetTag))
                         select new AssetDTO()
                         {
                             AssetTag = a.AssetKey,
                             Product = a.ProductKeyNavigation.ProductName,
                             ProductKey = a.ProductKey,
                             Manufacturer = a.ManufacturerKeyNavigation.ManufacturerName,
                             Model = a.ModelKeyNavigation.ModelName,
                             SerialNumber = a.SerialNumber,
                             ItemName = a.ItemName,
                             Location = a.LocationKeyNavigation.LocationName,
                             ClientSite = a.ClientSiteKeyNavigation.ClientSiteName,
                             PurchaseDate = a.PurchaseDate,
                             InventoryOwner = a.InventoryOwner,
                             InventoriedBy = a.InventoriedBy,
                             InventoryDate = a.InventoryDate,
                             isDisposed = a.IsDisposed
                         };
            return result;
        }

        public IEnumerable GetClientSites()
        {
            var clientSites = db.ClientSite;
                return clientSites;
        }

        public List<ClientSiteDTO> CreateClientSiteDTOs()
        {
            List<ClientSiteDTO> clientSiteDTOList = new List<ClientSiteDTO>();
            var clientSites = GetClientSites();
            foreach(ClientSite clientSite in clientSites)
            {
                ClientSiteDTO clientSiteDTO = new ClientSiteDTO
                {
                    ClientSiteKey = clientSite.ClientSiteKey,
                    ClientSiteName = clientSite.ClientSiteName
                };
                clientSiteDTOList.Add(clientSiteDTO);
            }
            return clientSiteDTOList;
        }

        public IEnumerable GetLocations()
        {
            var locations = db.Location;
                return locations;
        }

        public List<LocationDTO> CreateLocationDTOs()
        {
            List<LocationDTO> locationDTOList = new List<LocationDTO>();
            var locations = GetLocations();
            foreach(Location location in locations)
            {
                LocationDTO locationDTO = new LocationDTO
                {
                    LocationKey = location.LocationKey,
                    LocationName = location.LocationName
                };
                locationDTOList.Add(locationDTO);
            }
            return locationDTOList;
        }

        public IEnumerable GetModels()
        {
            var models = db.Model;
            return models;
        }

        public List<ModelDTO> CreateModelDTOs()
        {
            List<ModelDTO> modelDTOList = new List<ModelDTO>();
            var models = GetModels();
            foreach(Model model in models)
            {
                ModelDTO modelDTO = new ModelDTO
                {
                    ModelKey = model.ModelKey,
                    ModelName = model.ModelName
                };
                modelDTOList.Add(modelDTO);
            }
            return modelDTOList;
        }

        public IEnumerable GetManufacturers()
        {
            var manufacturers = db.Manufacturer;
            return manufacturers;
        }

        public List<ManufacturerDTO> CreateManufacturerDTOs()
        {
            List<ManufacturerDTO> manufacturerDTOList = new List<ManufacturerDTO>();
            var manufacturers = GetManufacturers();
            foreach (Manufacturer manufacturer in manufacturers)
            {
                ManufacturerDTO manufacturerDTO = new ManufacturerDTO
                {
                    ManufacturerKey = manufacturer.ManufacturerKey,
                    ManufacturerName = manufacturer.ManufacturerName
                };
                manufacturerDTOList.Add(manufacturerDTO);
            }
            return manufacturerDTOList;
        }

        public IEnumerable GetProducts()
        {
            var products = db.Product;
            return products;
        }

        public List<ProductDTO> CreateProductDTOs()
        {
            List<ProductDTO> productDTOList = new List<ProductDTO>();
            var products = GetProducts();
            foreach (Product product in products)
            {
                ProductDTO productDTO = new ProductDTO
                {
                    ProductKey = product.ProductKey,
                    ProductName = product.ProductName
                };
                productDTOList.Add(productDTO);
            }
            return productDTOList;
        }

        public bool SearchInventory(AssetDTO assetDTO)
        {
            var result = from a in db.Asset
                         .Where(a =>
                         a.IsDisposed == false 
                         && (a.AssetKey.ToString().Contains(assetDTO.AssetTag.ToString()) || a.InventoryOwner.Contains(assetDTO.InventoryOwner)) && (a.ClientSiteKey.Equals(assetDTO.ClientSiteKey) || a.LocationKey.Equals(assetDTO.LocationKey)))
                         orderby a.AssetKey descending
                         select new AssetDTO()
                         {
                             AssetTag = a.AssetKey,
                             Product = a.ProductKeyNavigation.ProductName,
                             Manufacturer = a.ManufacturerKeyNavigation.ManufacturerName,
                             Model = a.ModelKeyNavigation.ModelName,
                             SerialNumber = a.SerialNumber,
                             ItemName = a.ItemName,
                             Location = a.LocationKeyNavigation.LocationName,
                             ClientSite = a.ClientSiteKeyNavigation.ClientSiteName,
                             PurchaseDate = a.PurchaseDate,
                             InventoryOwner = a.InventoryOwner,
                             InventoriedBy = a.InventoriedBy,
                             isDisposed = a.IsDisposed
                         };

            if (result.Any())
            {
                return true;
            }
            else
                return false;
        }

        public bool SearchInventoryDetails(AssetDTO assetDTO)
        {
            var result = from a in db.Asset
                         .Where(a => a.IsDisposed == false && 
                         a.AssetKey.ToString().Contains(assetDTO.KeywordSearch.ToString()) || 
                         a.InventoriedBy.Contains(assetDTO.KeywordSearch) || 
                         a.InventoryDate.ToString().Contains(assetDTO.KeywordSearch) ||
                         a.InventoryOwner.Contains(assetDTO.KeywordSearch) ||
                         a.ItemName.Contains(assetDTO.KeywordSearch) ||
                         a.LocationKeyNavigation.LocationName.Contains(assetDTO.KeywordSearch) ||
                         a.ManufacturerKeyNavigation.ManufacturerName.Contains(assetDTO.KeywordSearch) ||
                         a.ModelKeyNavigation.ModelName.Contains(assetDTO.KeywordSearch) ||
                         a.ProductKeyNavigation.ProductName.Contains(assetDTO.KeywordSearch) ||
                         a.PurchaseDate.ToString().Contains(assetDTO.KeywordSearch) ||
                         a.SerialNumber.ToString().Contains(assetDTO.KeywordSearch) ||
                         a.AssetKey.ToString().Contains(assetDTO.AssetTag.ToString()) ||
                         a.ClientSiteKey.Equals(assetDTO.ClientSiteKey) || 
                         a.LocationKey.Equals(assetDTO.LocationKey))
                         orderby a.AssetKey descending
                         select new AssetDTO()
                         {
                             AssetTag = a.AssetKey,
                             Product = a.ProductKeyNavigation.ProductName,
                             Manufacturer = a.ManufacturerKeyNavigation.ManufacturerName,
                             Model = a.ModelKeyNavigation.ModelName,
                             SerialNumber = a.SerialNumber,
                             ItemName = a.ItemName,
                             Location = a.LocationKeyNavigation.LocationName,
                             ClientSite = a.ClientSiteKeyNavigation.ClientSiteName,
                             PurchaseDate = a.PurchaseDate,
                             InventoryOwner = a.InventoryOwner,
                             InventoriedBy = a.InventoriedBy,
                             isDisposed = a.IsDisposed
                         };

            if (result.Any())
            {
                return true;
            }
            else
                return false;
        }

        public DataTableReturned GetAssetsPaginated(ResultsDataTableSent sent)
        {
            var searchResults = GetAssetDTOs(sent);
            var count = GetNumOfAssets(searchResults);
            var ordered = GetAssetsOrdered(searchResults, sent);
            //var data = DataTableHelpers.GetPagedWithoutOrder(ordered, sent);
            var data = searchResults;
            var model = new DataTableReturned(sent)
            {
                recordsTotal = count,
                recordsFiltered = count,
                data = data
            };
            return model;
        }

        public int GetNumOfAssets(IEnumerable<AssetDTO> assetDTOs)
        {
            int count = 0;
            foreach(AssetDTO assetDTO in assetDTOs)
            {
                count++;
            }
            return count;
        }

        public IOrderedEnumerable<AssetDTO> GetAssetsOrdered(IEnumerable<AssetDTO> assetDTOs, DataTableSent sent)
        {
            var isASC = DataTableHelpers.isASC(sent);
            try 
            {
                switch (sent.order.FirstOrDefault().column)
                {
                    case 0:
                        if (isASC)
                            return assetDTOs.OrderBy(c => c.AssetTag);
                        else
                            return assetDTOs.OrderBy(c => c.AssetTag);
                    case 1:
                        if (isASC)
                            return assetDTOs.OrderBy(c => c.Product);
                        else
                            return assetDTOs.OrderBy(c => c.Product);
                    case 2:
                        if (isASC)
                            return assetDTOs.OrderBy(c => c.Manufacturer);
                        else
                            return assetDTOs.OrderBy(c => c.Manufacturer);
                    case 3:
                        if (isASC)
                            return assetDTOs.OrderBy(c => c.Model);
                        else
                            return assetDTOs.OrderBy(c => c.Model);
                    case 4:
                        if (isASC)
                            return assetDTOs.OrderBy(c => c.SerialNumber);
                        else
                            return assetDTOs.OrderBy(c => c.SerialNumber);
                    case 5:
                        if (isASC)
                            return assetDTOs.OrderBy(c => c.ItemName);
                        else
                            return assetDTOs.OrderBy(c => c.ItemName);
                    default:
                        return assetDTOs.OrderBy(c => c.AssetTag);
                }
            }
            catch (Exception)
            {
                return assetDTOs.OrderBy(c => c.AssetTag);
            }
        }
    }
}
