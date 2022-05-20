using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;

namespace Trackem.ERT.Web.Apis.Extensions;
public class AzureSqlAuthenticationProvider : SqlAuthenticationProvider
{
    private static readonly string[] _azureSqlScopes = new[]
    {
            "https://database.windows.net//.default"
        };

    private static readonly TokenCredential _credential = new DefaultAzureCredential();



    public override async Task<SqlAuthenticationToken> AcquireTokenAsync(SqlAuthenticationParameters parameters)
    {

        var tokenRequestContext = new TokenRequestContext(_azureSqlScopes);
        AccessToken tokenResult = await _credential.GetTokenAsync(tokenRequestContext, default);

        return new SqlAuthenticationToken(tokenResult.Token, tokenResult.ExpiresOn);
    }

    //public override bool IsSupported(SqlAuthenticationMethod authenticationMethod) => authenticationMethod.Equals(SqlAuthenticationMethod.SqlPassword);
    public override bool IsSupported(SqlAuthenticationMethod authenticationMethod) => authenticationMethod.Equals(SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow);
}
