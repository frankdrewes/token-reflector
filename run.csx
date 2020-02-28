
    // Reference from https://github.com/azure-ad-b2c/samples/blob/master/policies/user_info/source-code/AADB2C.UserInfo/AADB2C.UserInfo/Controllers/user_infoController.cs
// SPecial thanks to Yoel Horvitz for the original 
// This implementation only depends on the .NetCore and AspNetCore
// Observe the project.json for dependencies 

#r "Newtonsoft.Json"
#r "Microsoft.AspNetCore"
#r "System.IdentityModel.Tokens.Jwt"
#r "Microsoft.AspNetCore.Authentication.JwtBearer"
#r "Microsoft.AspNetCore.Authentication"
#r "Microsoft.IdentityModel.Tokens"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Microsoft.AspNetCore;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string authorization = req.Headers["Authorization"];
    string token;
    token = authorization.Substring("Bearer ".Length).Trim();
    log.LogInformation(token);

    var tokenHandler = new JwtSecurityTokenHandler();
    JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);
    System.Collections.Generic.IEnumerable<System.Security.Claims.Claim> claimsFromJWT = jwtToken.Claims;
    Dictionary<string, string> returnClaims = new Dictionary<string, string>();
 
string firstName = String.Empty;
string lastName = String.Empty;
string telephoneNumber = String.Empty;
string email = String.Empty;

    
    foreach (var claim in claimsFromJWT)
                {
                    
                    if (claim.Type=="given_name") 
                        {firstName= claim.Value; }
                    if (claim.Type=="family_name") 
                        {lastName = claim.Value;  }
                    if (claim.Type=="email") 
                        {email = claim.Value ;           }  
                    if (claim.Type=="telephoneNumber") 
                        {telephoneNumber = claim.Value; }
               log.LogInformation(claim.Type + " = "+ claim.Value);
                }


            returnClaims.Add("firstName", firstName);
            returnClaims.Add("lastName", lastName);
            returnClaims.Add("email", email);
            returnClaims.Add("phoneNumber", telephoneNumber);
            returnClaims.Add("isFederated", "false");


    return returnClaims != null
        ? (ActionResult)new OkObjectResult(returnClaims)

        : new BadRequestObjectResult("Claims could not be constructed");
}
