using CGI.Asset.Inventory.Data;
using CGI.Asset.Inventory.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGI.Asset.Inventory.Service
{
    public class AuthenticationService
    {
        private readonly CGIAssetInventoryContext db;

        public AuthenticationService(CGIAssetInventoryContext context)
        {
            db = context;
        }

        public bool IsUserValid(string user)
        {
            var resultExists = db.Users.Any(a => a.UserName.Equals(user));
            return resultExists;
        }

        public bool isUserAuthorized(string user, int authorizationLevel)
        {
            var result = db.Users.Single(a => a.UserName.Equals(user));
            if (result.RoleId >= authorizationLevel)
            {
                return true;
            }
            else
                return false;
        }
    }
}
