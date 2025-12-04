# Org.OpenAPITools.Api.BerichtenverkeerApi

All URIs are relative to *https://apigw.idm.diginetwerk.net/api/brp/berichten/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DeleteMessages**](BerichtenverkeerApi.md#deletemessages) | **DELETE** /berichten/{berichtTransportIdsParam} | Het verwijderen van een of meerdere berichten (DELETE). |
| [**GetMessages**](BerichtenverkeerApi.md#getmessages) | **GET** /berichten/{berichtTransportIdsParam} | Het ophalen van een of meerdere berichten (GET). |
| [**ListMessages**](BerichtenverkeerApi.md#listmessages) | **GET** /berichten | Het ophalen van een lijst met berichten die klaarstaan (LIST). |
| [**PutMessages**](BerichtenverkeerApi.md#putmessages) | **POST** /berichten | Het versturen van een of meerdere berichten (PUT). |
| [**Summarize**](BerichtenverkeerApi.md#summarize) | **GET** /berichten/telling | Voert verschillende soorten tellingen uit waarmee bepaald kan worden hoeveel berichten er verwerkt dienen te worden (SUMMARIZE). |

<a id="deletemessages"></a>
# **DeleteMessages**
> DeleteMessageAntwoord DeleteMessages (List<Guid> berichtTransportIdsParam)

Het verwijderen van een of meerdere berichten (DELETE).

Verwijderen van een of meerdere berichten. De berichten kunnen na deze actie niet meer bij de berichten API opgehaald worden. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteMessagesExample
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

            var apiInstance = new BerichtenverkeerApi(config);
            var berichtTransportIdsParam = new List<Guid>(); // List<Guid> | Een UUID of meerdere UUID's van het bericht(en) die opgehaald moet worden. Indien meerdere UUID's, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \"BRP berichten API\" bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID's middels een request naar `/berichten`. 

            try
            {
                // Het verwijderen van een of meerdere berichten (DELETE).
                DeleteMessageAntwoord result = apiInstance.DeleteMessages(berichtTransportIdsParam);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BerichtenverkeerApi.DeleteMessages: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteMessagesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Het verwijderen van een of meerdere berichten (DELETE).
    ApiResponse<DeleteMessageAntwoord> response = apiInstance.DeleteMessagesWithHttpInfo(berichtTransportIdsParam);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BerichtenverkeerApi.DeleteMessagesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **berichtTransportIdsParam** | [**List&lt;Guid&gt;**](Guid.md) | Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;.  |  |

### Return type

[**DeleteMessageAntwoord**](DeleteMessageAntwoord.md)

### Authorization

[LapOAuth](../README.md#LapOAuth), [PrdOAuth](../README.md#PrdOAuth), [DemoOAuth](../README.md#DemoOAuth), [AccOAuth](../README.md#AccOAuth), [DemoBasicAuth](../README.md#DemoBasicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Deze 200 OK response wordt vrijwel alle situaties geretourneerd, mits er geen technische fouten opgetreden zijn. Indien er bij het verwijderen van het bericht iets mis ging, dan vindt u dat per bericht terug in de response. |  -  |
| **400** | Onjuist verzoek |  -  |
| **401** | Onjuiste of ontbrekende authenticatie |  -  |
| **500** | Onbekende/technische fout |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getmessages"></a>
# **GetMessages**
> GetMessageAntwoord GetMessages (List<Guid> berichtTransportIdsParam)

Het ophalen van een of meerdere berichten (GET).

Dit endpoint gebruikt u om berichten op te halen. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetMessagesExample
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

            var apiInstance = new BerichtenverkeerApi(config);
            var berichtTransportIdsParam = new List<Guid>(); // List<Guid> | Een UUID of meerdere UUID's van het bericht(en) die opgehaald moet worden. Indien meerdere UUID's, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \"BRP berichten API\" bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID's middels een request naar `/berichten`. 

            try
            {
                // Het ophalen van een of meerdere berichten (GET).
                GetMessageAntwoord result = apiInstance.GetMessages(berichtTransportIdsParam);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BerichtenverkeerApi.GetMessages: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetMessagesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Het ophalen van een of meerdere berichten (GET).
    ApiResponse<GetMessageAntwoord> response = apiInstance.GetMessagesWithHttpInfo(berichtTransportIdsParam);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BerichtenverkeerApi.GetMessagesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **berichtTransportIdsParam** | [**List&lt;Guid&gt;**](Guid.md) | Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;.  |  |

### Return type

[**GetMessageAntwoord**](GetMessageAntwoord.md)

### Authorization

[LapOAuth](../README.md#LapOAuth), [PrdOAuth](../README.md#PrdOAuth), [DemoOAuth](../README.md#DemoOAuth), [AccOAuth](../README.md#AccOAuth), [DemoBasicAuth](../README.md#DemoBasicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Deze 200 OK response wordt vrijwel alle situaties geretourneerd, mits er geen technische fouten opgetreden zijn. Indien er bij het ophalen van het bericht iets mis ging, dan vindt u dat per bericht terug in de response. |  -  |
| **400** | Onjuist verzoek |  -  |
| **401** | Onjuiste of ontbrekende authenticatie |  -  |
| **500** | Onbekende/technische fout |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listmessages"></a>
# **ListMessages**
> ListMessageAntwoord ListMessages (List<string>? status = null, string? berichtType = null, DateTime? vanafMoment = null, DateTime? totMoment = null, int? pagina = null, int? berichtenPerPagina = null)

Het ophalen van een lijst met berichten die klaarstaan (LIST).

Dit endpoint gebruikt u om te achterhalen welke berichten er voor u beschikbaar zijn. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListMessagesExample
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

            var apiInstance = new BerichtenverkeerApi(config);
            var status = new List<string>?(); // List<string>? | Geeft aan welke berichten opgenomen moeten worden in het resultaat. Comma-gescheiden veld. Indien leeg worden alle niet opgehaalde berichten getoond (`nieuw` + `gezien-in-lijst`).    - `nieuw`: Geeft berichten welke nog nooit opgehaald zijn en tevens niet in de lijst operatie zijn getoond.   - `gezien-in-lijst`: Geeft berichten die nog niet opgehaald zijn, maar al wel gezien zijn in de lijst.   - `opgehaald`: Geeft berichten reeds opgehaald zijn.  (optional) 
            var berichtType = "berichtType_example";  // string? | Geeft aan welke type berichten opgenomen moeten worden in het resultaat. Indien leeg of niet aanwezig worden alle berichten getoond. Niet hoofdlettergevoelig. (optional) 
            var vanafMoment = DateTime.Parse("2013-10-20T19:20:30+01:00");  // DateTime? | Het resultaat zal alleen berichten bevatten die vanaf dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond die nog beschikbaar zijn (waarvan de retentietijd nog niet verlopen is). (optional) 
            var totMoment = DateTime.Parse("2013-10-20T19:20:30+01:00");  // DateTime? | Het resultaat zal alleen berichten bevatten die tot dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond t/m het heden. (optional) 
            var pagina = 1;  // int? | Pagina nummer, startend bij 1 t/m N (niet bij 0). (optional)  (default to 1)
            var berichtenPerPagina = 56;  // int? | Het maximum aantal berichten dat geretourneerd mag worden. Indien deze waarde het ingesteld systeemlimiet overschrijdt, dan wordt het systeemlimiet gehanteerd. Indien er geen waarde wordt opgegeven, dan wordt tevens het systeemlimiet gehanteerd.  (optional) 

            try
            {
                // Het ophalen van een lijst met berichten die klaarstaan (LIST).
                ListMessageAntwoord result = apiInstance.ListMessages(status, berichtType, vanafMoment, totMoment, pagina, berichtenPerPagina);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BerichtenverkeerApi.ListMessages: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListMessagesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Het ophalen van een lijst met berichten die klaarstaan (LIST).
    ApiResponse<ListMessageAntwoord> response = apiInstance.ListMessagesWithHttpInfo(status, berichtType, vanafMoment, totMoment, pagina, berichtenPerPagina);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BerichtenverkeerApi.ListMessagesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **status** | [**List&lt;string&gt;?**](string.md) | Geeft aan welke berichten opgenomen moeten worden in het resultaat. Comma-gescheiden veld. Indien leeg worden alle niet opgehaalde berichten getoond (&#x60;nieuw&#x60; + &#x60;gezien-in-lijst&#x60;).    - &#x60;nieuw&#x60;: Geeft berichten welke nog nooit opgehaald zijn en tevens niet in de lijst operatie zijn getoond.   - &#x60;gezien-in-lijst&#x60;: Geeft berichten die nog niet opgehaald zijn, maar al wel gezien zijn in de lijst.   - &#x60;opgehaald&#x60;: Geeft berichten reeds opgehaald zijn.  | [optional]  |
| **berichtType** | **string?** | Geeft aan welke type berichten opgenomen moeten worden in het resultaat. Indien leeg of niet aanwezig worden alle berichten getoond. Niet hoofdlettergevoelig. | [optional]  |
| **vanafMoment** | **DateTime?** | Het resultaat zal alleen berichten bevatten die vanaf dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond die nog beschikbaar zijn (waarvan de retentietijd nog niet verlopen is). | [optional]  |
| **totMoment** | **DateTime?** | Het resultaat zal alleen berichten bevatten die tot dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond t/m het heden. | [optional]  |
| **pagina** | **int?** | Pagina nummer, startend bij 1 t/m N (niet bij 0). | [optional] [default to 1] |
| **berichtenPerPagina** | **int?** | Het maximum aantal berichten dat geretourneerd mag worden. Indien deze waarde het ingesteld systeemlimiet overschrijdt, dan wordt het systeemlimiet gehanteerd. Indien er geen waarde wordt opgegeven, dan wordt tevens het systeemlimiet gehanteerd.  | [optional]  |

### Return type

[**ListMessageAntwoord**](ListMessageAntwoord.md)

### Authorization

[LapOAuth](../README.md#LapOAuth), [PrdOAuth](../README.md#PrdOAuth), [DemoOAuth](../README.md#DemoOAuth), [AccOAuth](../README.md#AccOAuth), [DemoBasicAuth](../README.md#DemoBasicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Een opsomming van de berichten die beschikbaar zijn voor de mailbox die gekoppeld is aan het geauthenticeerde account. |  -  |
| **400** | Onjuist verzoek |  -  |
| **401** | Onjuiste of ontbrekende authenticatie |  -  |
| **500** | Interne technische fout |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="putmessages"></a>
# **PutMessages**
> PutMessageAntwoord PutMessages (PutMessageRequest putMessageRequest)

Het versturen van een of meerdere berichten (PUT).

Dit endpoint gebruikt u om berichten zoals gespecificeerd in het Logisch Ontwerp te versturen. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PutMessagesExample
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

            var apiInstance = new BerichtenverkeerApi(config);
            var putMessageRequest = new PutMessageRequest(); // PutMessageRequest | 

            try
            {
                // Het versturen van een of meerdere berichten (PUT).
                PutMessageAntwoord result = apiInstance.PutMessages(putMessageRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BerichtenverkeerApi.PutMessages: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PutMessagesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Het versturen van een of meerdere berichten (PUT).
    ApiResponse<PutMessageAntwoord> response = apiInstance.PutMessagesWithHttpInfo(putMessageRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BerichtenverkeerApi.PutMessagesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **putMessageRequest** | [**PutMessageRequest**](PutMessageRequest.md) |  |  |

### Return type

[**PutMessageAntwoord**](PutMessageAntwoord.md)

### Authorization

[LapOAuth](../README.md#LapOAuth), [PrdOAuth](../README.md#PrdOAuth), [DemoOAuth](../README.md#DemoOAuth), [AccOAuth](../README.md#AccOAuth), [DemoBasicAuth](../README.md#DemoBasicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Uw berichten zijn succesvol aangeboden ter verwerking. In de response wordt aangeven welke berichten wel en niet succesvol verwerkt konden worden. Een 200 OK betekend dus nog niet zonder meer dat alle berichten succesvol verwerkt zijn. Controlleer dus altijd de response! |  -  |
| **400** | Het verzoek is onjuist en is daarom niet verwerkt. |  -  |
| **401** | Onjuiste of ontbrekende authenticatie |  -  |
| **500** | Onbekende/technische fout |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="summarize"></a>
# **Summarize**
> Summarize200Response Summarize (string soort)

Voert verschillende soorten tellingen uit waarmee bepaald kan worden hoeveel berichten er verwerkt dienen te worden (SUMMARIZE).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class SummarizeExample
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

            var apiInstance = new BerichtenverkeerApi(config);
            var soort = "nieuw";  // string | Geeft aan welke telling er uitgevoerd moet worden.   - nieuw: Geeft het aantal nieuwe berichten terug dat nog niet gezien is via een LIST operatie en tevens nog niet opgehaald is middels een GET operatie.   - gezien-in-lijst-en-niet-opgehaald: Geeft het aantal nieuwe berichten terug dat gezien is via een LIST operatie, maar nog niet opgehaald is middels een GET operatie.   - niet-opgehaald: Geeft het aantal nieuwe berichten terug dat nog niet opgehaald is middels een GET operatie. 

            try
            {
                // Voert verschillende soorten tellingen uit waarmee bepaald kan worden hoeveel berichten er verwerkt dienen te worden (SUMMARIZE).
                Summarize200Response result = apiInstance.Summarize(soort);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BerichtenverkeerApi.Summarize: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SummarizeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Voert verschillende soorten tellingen uit waarmee bepaald kan worden hoeveel berichten er verwerkt dienen te worden (SUMMARIZE).
    ApiResponse<Summarize200Response> response = apiInstance.SummarizeWithHttpInfo(soort);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BerichtenverkeerApi.SummarizeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **soort** | **string** | Geeft aan welke telling er uitgevoerd moet worden.   - nieuw: Geeft het aantal nieuwe berichten terug dat nog niet gezien is via een LIST operatie en tevens nog niet opgehaald is middels een GET operatie.   - gezien-in-lijst-en-niet-opgehaald: Geeft het aantal nieuwe berichten terug dat gezien is via een LIST operatie, maar nog niet opgehaald is middels een GET operatie.   - niet-opgehaald: Geeft het aantal nieuwe berichten terug dat nog niet opgehaald is middels een GET operatie.  |  |

### Return type

[**Summarize200Response**](Summarize200Response.md)

### Authorization

[LapOAuth](../README.md#LapOAuth), [PrdOAuth](../README.md#PrdOAuth), [DemoOAuth](../README.md#DemoOAuth), [AccOAuth](../README.md#AccOAuth), [DemoBasicAuth](../README.md#DemoBasicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Het resultaat van de telling. |  -  |
| **400** | Indien er een telling-soort gevraagd werdt die niet bestaat, of wanneer er geen telling-soort is opgegeven. |  -  |
| **401** | Onjuiste of ontbrekende authenticatie |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

