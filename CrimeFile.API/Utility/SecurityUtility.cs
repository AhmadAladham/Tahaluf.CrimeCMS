using CrimeFile.Core.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CrimeFile.API.Utility
{
    public static class SecurityUtility
    {
        public static bool CheckPermissions(IEnumerable<Claim> claims, string permissions)
        {
            bool hasClaim = false;

            if (string.IsNullOrEmpty(permissions))
            {
                hasClaim = true;
            }
            else
            {
                List<string> userPermissions = GetPermissions(claims);
                if (HasMultiplePermissions(permissions) && userPermissions != null)
                {
                    List<string> actionPersmissions = permissions.Trim().ToLower()
                    .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                    if (userPermissions != null && userPermissions.Count > 0 && actionPersmissions != null && actionPersmissions.Count > 0)
                    {
                        IEnumerable<string> intersection = userPermissions.Intersect(actionPersmissions);

                        if (intersection != null && intersection.Count() > 0)
                        {
                            hasClaim = true;
                        }
                    }
                }
                else
                {
                    hasClaim = userPermissions.Any(c => c.Trim().ToLower() == permissions.Trim().ToLower());
                    // hasClaim .

                }
            }

            return hasClaim;
        }

        private static List<string> GetPermissions(IEnumerable<Claim> claims)
        {
            List<string> permissions;
            // get permissions from token
            permissions = claims.Where(c => c.Type == CustomClaimTypes.Permission)
            .Select(claim => claim.Value.Trim().ToLower())
            .ToList();
            var p = permissions[0].Split(',').Select(x => x).ToList();

            return p;
        }

        private static bool HasMultiplePermissions(string permission)
        {
            bool hasMultiple = false;

            if (!string.IsNullOrEmpty(permission) && permission.Trim().Contains(","))
            {
                hasMultiple = true;
            }

            return hasMultiple;
        }

        public static JwtSecurityToken DecodeToken(string authorization)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenString = authorization.ToString().Replace("Bearer ", string.Empty);
            var token = handler.ReadJwtToken(tokenString);
            return token;
        }
    }
}
