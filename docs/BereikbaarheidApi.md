# Org.OpenAPITools.Api.BereikbaarheidApi

All URIs are relative to *https://apigw.idm.diginetwerk.net/api/brp/berichten/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**PingGet**](BereikbaarheidApi.md#pingget) | **GET** /berichten/ping | Ping operatie t.b.v. het toetsen van de bereikbaarheid van de API vanuit de aangesloten partij. |
| [**PingHead**](BereikbaarheidApi.md#pinghead) | **HEAD** /berichten/ping | Ping operatie t.b.v. het toetsen van de bereikbaarheid van de API vanuit de aangesloten partij. |

<a id="pingget"></a>
# **PingGet**
> void PingGet ()

Ping operatie t.b.v. het toetsen van de bereikbaarheid van de API vanuit de aangesloten partij.

Deze operatie kan aangeroepen worden door aangesloten partijen om te verifiëren dat er communicatie met de berichtendienst mogelijks is. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PingGetExample
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

            var apiInstance = new BereikbaarheidApi(config);

            try
            {
                // Ping operatie t.b.v. het toetsen van de bereikbaarheid van de API vanuit de aangesloten partij.
                apiInstance.PingGet();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BereikbaarheidApi.PingGet: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PingGetWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Ping operatie t.b.v. het toetsen van de bereikbaarheid van de API vanuit de aangesloten partij.
    apiInstance.PingGetWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BereikbaarheidApi.PingGetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[LapOAuth](../README.md#LapOAuth), [PrdOAuth](../README.md#PrdOAuth), [DemoOAuth](../README.md#DemoOAuth), [AccOAuth](../README.md#AccOAuth), [DemoBasicAuth](../README.md#DemoBasicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | Geeft aan dat de API bereikt kon worden. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="pinghead"></a>
# **PingHead**
> void PingHead ()

Ping operatie t.b.v. het toetsen van de bereikbaarheid van de API vanuit de aangesloten partij.

Deze operatie kan aangeroepen worden door aangesloten partijen om te verifiëren dat er communicatie met de berichtendienst mogelijks is. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PingHeadExample
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

            var apiInstance = new BereikbaarheidApi(config);

            try
            {
                // Ping operatie t.b.v. het toetsen van de bereikbaarheid van de API vanuit de aangesloten partij.
                apiInstance.PingHead();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BereikbaarheidApi.PingHead: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PingHeadWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Ping operatie t.b.v. het toetsen van de bereikbaarheid van de API vanuit de aangesloten partij.
    apiInstance.PingHeadWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BereikbaarheidApi.PingHeadWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[LapOAuth](../README.md#LapOAuth), [PrdOAuth](../README.md#PrdOAuth), [DemoOAuth](../README.md#DemoOAuth), [AccOAuth](../README.md#AccOAuth), [DemoBasicAuth](../README.md#DemoBasicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | Geeft aan dat de API bereikt kon worden. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

