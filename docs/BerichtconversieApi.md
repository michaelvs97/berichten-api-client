# Org.OpenAPITools.Api.BerichtconversieApi

All URIs are relative to *https://apigw.idm.diginetwerk.net/api/brp/berichten/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**Converteer**](BerichtconversieApi.md#converteer) | **POST** /berichten/conversie | Dit endpoint faciliteert bij de conversie van berichten tussen de verschillende soorten berichtformaten. |

<a id="converteer"></a>
# **Converteer**
> Converteer200Response Converteer (string body)

Dit endpoint faciliteert bij de conversie van berichten tussen de verschillende soorten berichtformaten.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConverteerExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://apigw.idm.diginetwerk.net/api/brp/berichten/v1";
            // Configure OAuth2 access token for authorization: LapOAuth
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure OAuth2 access token for authorization: PrdOAuth
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure OAuth2 access token for authorization: DemoOAuth
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure OAuth2 access token for authorization: AccOAuth
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure HTTP basic authorization: DemoBasicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new BerichtconversieApi(config);
            var body = "body_example";  // string | Er zijn verschillende waarden die voor de headers `Content-Type` en `Accept` gebruikt kunnen worden. Elk van deze waarden stelt een van de bekende formaten voor:   - \"text/plain; charset=teletex\": Het klassieke berichtenformaat waarbij de teletex charset wordt gehanteerd.   - \"text/plain+base64; charset=teletex\": Het klassieke berichtenformaat waarbij de teletex charset wordt gehanteerd. De body is in dit geval Base64 geëncodeerd.   - \"text/plain; charset=utf-8\": Het klassieke berichtenformaat waarbij de UTF-8 charset wordt gehanteerd.   - \"text/plain+base64; charset=utf-8\": Het klassieke berichtenformaat waarbij de UTF-8 charset wordt gehanteerd. De body is in dit geval Base64 geëncodeerd.   - \"application/json\": Het meest recente berichtenformaat gebaseerd op JSON en de UTF-8 charset.  Middels deze waarden kunt u via de Content-Type en Accept headers aanduiden welk formaat u instuurt en welk formaat u terug wenst te ontvangen. 

            try
            {
                // Dit endpoint faciliteert bij de conversie van berichten tussen de verschillende soorten berichtformaten.
                Converteer200Response result = apiInstance.Converteer(body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BerichtconversieApi.Converteer: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConverteerWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Dit endpoint faciliteert bij de conversie van berichten tussen de verschillende soorten berichtformaten.
    ApiResponse<Converteer200Response> response = apiInstance.ConverteerWithHttpInfo(body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BerichtconversieApi.ConverteerWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | **string** | Er zijn verschillende waarden die voor de headers &#x60;Content-Type&#x60; en &#x60;Accept&#x60; gebruikt kunnen worden. Elk van deze waarden stelt een van de bekende formaten voor:   - \&quot;text/plain; charset&#x3D;teletex\&quot;: Het klassieke berichtenformaat waarbij de teletex charset wordt gehanteerd.   - \&quot;text/plain+base64; charset&#x3D;teletex\&quot;: Het klassieke berichtenformaat waarbij de teletex charset wordt gehanteerd. De body is in dit geval Base64 geëncodeerd.   - \&quot;text/plain; charset&#x3D;utf-8\&quot;: Het klassieke berichtenformaat waarbij de UTF-8 charset wordt gehanteerd.   - \&quot;text/plain+base64; charset&#x3D;utf-8\&quot;: Het klassieke berichtenformaat waarbij de UTF-8 charset wordt gehanteerd. De body is in dit geval Base64 geëncodeerd.   - \&quot;application/json\&quot;: Het meest recente berichtenformaat gebaseerd op JSON en de UTF-8 charset.  Middels deze waarden kunt u via de Content-Type en Accept headers aanduiden welk formaat u instuurt en welk formaat u terug wenst te ontvangen.  |  |

### Return type

[**Converteer200Response**](Converteer200Response.md)

### Authorization

[LapOAuth](../README.md#LapOAuth), [PrdOAuth](../README.md#PrdOAuth), [DemoOAuth](../README.md#DemoOAuth), [AccOAuth](../README.md#AccOAuth), [DemoBasicAuth](../README.md#DemoBasicAuth)

### HTTP request headers

 - **Content-Type**: text/plain; charset=utf-8, text/plain+base64; charset=utf-8, text/plain; charset=teletex, text/plain+base64; charset=teletex, application/json
 - **Accept**: application/json, text/plain; charset=utf-8, text/plain+base64; charset=utf-8, text/plain; charset=teletex, text/plain+base64; charset=teletex, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Een naar JSON geconverteerd bericht |  -  |
| **400** | Wanneer het aangeleverde bericht niet voldeed en daardoor niet vertaald kon worden. |  -  |
| **401** | Onjuiste of ontbrekende authenticatie |  -  |
| **406** | Wanneer een conversie werd aangevraagd voor een doelformaat dat niet ondersteund wordt. |  -  |
| **415** | Wanneer een conversie werd aangevraagd voor een bronformaat dat niet ondersteund wordt. |  -  |
| **500** | Onbekende/technische fout |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

