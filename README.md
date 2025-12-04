# Org.OpenAPITools - the C# library for the BRP Berichten API

Een REST API voor het a-synchroon uitwisselen van berichten welke in het Logisch Ontwerp gedefinieerd zijn.

# Wijzigingshistorie:

## 0.7.6 November 2025
- Verwijzingen aangepast naar LO2025Q3 versie berichten schema's. Dit is puur een actualisatieslag, datamodel-technisch zijn er geen wijzigingen t.o.v. de vorige schema-versie. De wijzigingen beperken zich tot tekstuele wijzigen van de omschrijvingen.

## 0.7.5 Oktober 2025
- \"Ontvanger\" toegevoegd aan apiGetBerichtKenmerken en apiListBerichtKenmerken. In principe weet de ontvanger tijdens het moment van ontvangen dat deze berichten voor hem bestemd zijn. Echter in andere situaties waarbij berichten (buiten de context van de berichten-api) verwerkt worden, kan het relevant zijn dat zowel de verzender als de ontvanger gedefinieerd zijn bij het bericht.

## 0.7.4 Oktober 2025
- Aanpassing autorisatietabel elementen: De hoofdletter E is nu een kleine letter e, conform de elementen van andere berichtsoorten.

## 0.7.3 September 2025
- Aangeduid dat request-body bij POST requests aanwezig moeten zijn.
- Aangeduid dat `status` bij een Foutmelding verplicht is.

## 0.7.2 September 2025
- HTTP response bij het versturen berichten aangepast van 201 naar 200. De implementatie van de berichten-api gaf al 200.
- Beschrijving van het response bij het versturen van berichten aangepast explicieter wordt gemaakt dat de response gecontroleerd moet worden op succesvol en niet-succesvol verstuurde berichten.

## 0.7.1 September 2025
- OAuth2 scopes beschreven zoals ze zijn in LAP en PRD.

## 0.7.0 April 2025
- Aanpassingen in de structuur van de onderliggende JSON schema's, de inhoud is onveranderd gebleven.

## 0.6.4 Maart 2025
- Details rondom MutualTLS/tweezijdig-TLS d.m.v. PKI-Overheid toegevoegd.


## 0.6.3 Januari 2025
- Tekstuele verwijzing naar '/berichten/list' vervangen door '/berichten'.

## 0.6.2 Januari 2025
- OAuth endpoint demo omgeving gecorrigeerd.

## 0.6.1 Januari 2025
- Ping endpoints zijn niet langer bereikbaar zonder eerst te authenticeren.

## 0.6.0 Januari 2025
- Servers toegevoegd.
  - Hiervoor moesten de paden van de endpoints aangepast worden. Hierdoor is '/api/v1' komen te vervallen bij de endpoints. Dit is verhuisd naar de base-url's van de genoemde servers.
- Authenticatie details toegevoegd.
- Foutmeldingen bij het conversie endpoint bijgewerkt.
- Alle namespace paden van de foutmeldingen nagelopen en gecorrigeerd daar waar nodig. Dit zodat ze consistent starten met: \"https://www.rvig.nl/brp/berichten-api/probleem/\".

## 0.5.5 November 2024
- 'berichtVolgnummer' en 'afzender' required gemaakt in ListMessageKenmerken (en daardoor ook in GetMessageKenmerken).
- De GET en DELETE foutsituaties zijn voorzien van discriminators ten behoeve van het onderscheiden van foutsituatie subtypes door de gegenereerde client.

## 0.5.4 November 2024
- Properties van PagineringResultaat en PagineerbaarResultaat welke altijd aanwezig zullen zijn bij een list-request required gemaakt.
- Required properties van put-message-antwoord gecorrigeerd.
- Schema van het Null bericht gecorrigeerd.

## 0.5.3 November 2024
- Het Sv11 bericht is qua schema aangepast aangezien deze onterecht de property plData bevatte. Deze property is verwijderd uit dit bericht.
- Foutsituaties zijn voorzien van discriminators ten behoeve van het onderscheiden van foutsituatie subtypes door de gegenereerde client.

## 0.5.2 Oktober 2024
- De request die volledig afgekeurd worden en een statuscode 4xx of 5xx retourneren, doen dit nu met de response header 'Content-Type: application/problem+json'.
- Requests die een 2xx response in JSON formaat retourneren, doen dit met \"Content-Type: application/json\" ipv \"Content-Type: application/json: charset=utf-8\". Conform rfc8259 (https://www.rfc-editor.org/rfc/rfc8259) is JSON altijd in UTF-8 formaat en heeft dit type geen charset parameter.

## 0.5.1 Oktober 2024
 - De fout BBA-PUT-F002 is aangepast naar een algemene fout voor onjuiste velden in een PutMessage, via het veld \"invalidParams\" in de response word aangegeven welke velden onjuist zijn en waarom.

## 0.5.0 September 2024
 - Het veld berichtId en verwijzingBerichtId zijn omgezet van type 'integer' naar type 'string' om beter aan te sluiten op de bestaande voorziening.
 - Het probleem-antwoord response object is overal vervangen met de algemene Foutmelding response welke zich conformeert aan RFC7807.
 - De velden 'foutTitel', 'foutType', 'foutDetail' zijn aangepast naar 'title, 'type', 'detail' zodat zij zich conformeren aan de RFC7807.
 - De afhankelijkheid op 'openapi-problem-detail-v1.yml' is komen te vervallen (https://github.com/rvig-brp/BRP-Berichten-API/issues/3).

## 0.4.1 Juli 2024
 - Het json-schema voor de autorisatieberichten Ct01, Cw01 en Cb01 is toegevoegd.
 - De ontvanger is opgenomen in de response bij het verzenden van een bericht. Dit is met name relevant wanneer er een bericht naar een berichtgroep gestuurd wordt. In dat geval weet de verzender wie de uiteindelijke ontvangers zijn. Bij het versturen van een bericht naar de een regulier account zal dit nummer 1:1 overeenkomen met de ontvanger die bij het te verzenden bericht is opgegeven.

## 0.4.0 Juni 2024
- De json-schema's van de berichtsoorten zijn opgenomen in de OpenAPI Specificatie. Voor elke berichtsoort die het LO beschrijft, is opgenomen hoe dit bericht gestructureerd is.
  - Houdt er rekening mee dat het weergeven van de OpenAPI specificatie in de web-versie va SwaggerUI hierdoor trager geworden is. Het is aan te raden om de alternatieve (redocly) weergave te gebruiken:
    - https://brp-berichten-api.dictua.ictu-sr.nl/openapi/berichten-api.html
    - De JSON schema's zijn tevens te vinden op onze Github pagina.
- De JSON-response van het conversie endpoint is iets aangepast zodat naast het geconverteerde bericht tevens validatiefouten opgenomen kunnen worden.

## 0.3.0 - Mei 2024
- Conversie endpoints
  - Introductie bericht-conversie (/berichten/conversie) endpoint. Houdt er rekening mee dat de conversie naar JSON opgenomen is, maar nog niet geïmplementeerd is in de demo omgeving.
- Het limiet van het aantal berichten dat verwijderd kan worden is gelijkgesteld aan dat wat gelijktijdig opgehaald kan worden (100).
- De API is hernoemd van \"BRP A-Synchrone berichten API\" naar \"BRP berichten API\".
  - Nieuwe URL's demo omgeving:
    - https://brp-berichten-api.dictua.ictu-sr.nl/api/v1/berichten
    - https://brp-berichten-api.dictua.ictu-sr.nl/openapi/berichten-api.html
    - https://brp-berichten-api.dictua.ictu-sr.nl/swagger-ui/index.html
- Beschikbaarheid endpoint(s)
  - Er is een tweede ping endpoint bijgekomen waardoor en nu een HEAD of een GET gedaan kan worden. De response blijft hetzelfde. U kunt zelf kiezen welke van deze twee u hanteert.
  - De noodzaak voor authenticatie op het `ping` endpoint is komen te vervallen. U kunt dus zonder noodzaak van authenticatie vaststellen of de dienst beschikbaar is.

## 0.2.2 - April 2024
- Ping operatie toegevoegd t.b.v. het verifiëren dat er communicatie met de berichtendienst mogelijks is.
- De standaard sortering bij een LIST operatie is op dit moment:
  1. `Datum + tijdstip van ontvangst` waarbij geldt dat het oudste bericht als eerste wordt weergegeven in de lijst met beschikbare berichten (rationale deze dient als eerste verwerkt worden door de ontvanger).
  2. Indien `datum + tijdstip van ontvangst` gelijk zijn (wat kan voorkomen aangezien er meerdere berichten tegelijk ingestuurd kunnen worden), dan worden `afzender` en het `messageId` meegenomen in de sortering. De volgorde die de afzender toegekend heen via de messageId is op dat moment dus bepalend.

## 0.2.1 - April 2024
- Mogelijkheden tot sortering bij een list operatie zijn verwijderd. De standaard sortering wordt nog bepaald.
- Demo omgeving is toegevoegd aan de lijst met servers.
  - API te benaderen via https://brp-berichten-api.dictua.ictu-sr.nl/api/v1/berichten
  - Swagger UI via: https://brp-berichten-api.dictua.ictu-sr.nl/swagger-ui/index.html
  - De OpenAPI specificatie via: https://brp-berichten-api.dictua.ictu-sr.nl/openapi.brp-berichten-api-v1.yaml
- Wachtwoord wijzigingen optie is verwijderd.
- Het `berichtFormaat` attribuut is komen te vervallen. Alle berichten zijn nu per definitie in JSON formaat. De eis om de berichtInhoud Base64 te
  encoderen komt daarmee te vervallen.
- Voorbeelddata verbeterd.
- Tellingen endpoint toegevoegd welke invulling geeft aan de mailbox Summarize tegenhanger.
- Delete endpoint gecorrigeerd. De collectie `succesvolVerwijderdeBerichten` was van het type string i.p.v. berichtTransportId.
- Limieten zijn gewijzigd:
  - Het aantal berichten dat via een PUT verstuurd kan worden is verhoogd naar 25. Uitgaande van een gemiddelde berichtgrootte van 40kb geeft dat een request van 1MB groot.
  - Het aantal berichten dat via een LIST opgevraagd kan worden is vergroot naar 2000. Daarbij krijgt u de mogelijkheid om dit aantal te beperken.
    - 2000 berichten in een LIST operatie komt neer op ongeveer 600KB response grootte.
  - Het aantal berichten dat via een GET ontvangen kan worden is verhoogd naar 100. Dit heeft te maken met de gangbare (veilige) restricties van een URL qua lengte (2KB).
    - Voor de URL worden 256 bytes gereserveerd.
      - Voor de UUID blijven dan 1.792 bytes over.
    - Een BerichtTransportId is 17 bytes groot (UUID + separatie-karakter ',')
      - Uitgaande van 17 bytes, zou dit 105 keer herhaald kunnen worden. Om aan de veilige kan te zitten en om op een mooi rond getal uit te komen kiezen wij voor 100 als limiet.
    - Uitgaande van een gemiddelde berichtgrootte van 40KB komt je met 100 berichten uit op 4MB qua response-grootte.
- \"aantalKeerOpgehaald\" en \"dtLaatstOpgehaald\" zijn verwijderd uit response van LIST (ListMessageKenmerken schema). Wij zien hierin geen meerwaarde voor de aansluitende partijen. Wel kunt u blijven zien OF het bericht is opgehaald (boolean waarde).

## 0.2.0 - April 2024
- \"List\" verzoek is verhuisd van \"/berichten/lijst\" - -> \"/berichten\"
- \"GET\" van meerdere berichten wordt gedaan middels path-parameters i.p.v. query-parameters en is samengevoegd met het endpoint voor het ophalen van een enkel bericht.<br/>
  (/berichten/?berichtTransportIds=[UUID],[UUID] - -> /berichten/[UUID],[UUID])
  (/berichten/?berichtTransportIds=[UUID],[UUID] - -> /berichten/[UUID],[UUID])
- \"DELETE\" van meerdere berichten wordt gedaan middels path-parameters i.p.v. query-parameters en is samengevoegd met het endpoint voor het verwijderen van een enkel bericht.<br/>
  (/berichten/?berichtTransportIds=[UUID],[UUID] - -> /berichten/[UUID],[UUID])
- Het \"berichtId\" wat correspondeert met het \"MessageId\" veld van de mailboxserver is qua type gewijzigd van String naar Integer. Maximale lengte 12.
  - Beschrijving LO: MessageId, lengte: 12, Het unieke volgnummer dat aan het uitgaande bericht wordt toegekend.
- Het veld \"verwijzingBerichtId\" wat correspondeert met het \"CrossReference\" veld van de mailboxserver:
  - is qua type gewijzigd van String naar Integer. Maximale lengte 12.
  - kan of weggelaten worden, of gevuld worden met 0 indien het bericht een eerste bericht in de cyclus betreft.
- \"aantalKeerOpgehaald\" is toegevoegd aan de ListMessageKenmerken.

## 0.1.0 - Maart 2024
Initiële versie.

# In ontwikkeling:
- Bepalen of het een checksum op de berichtinhoud van meerwaarde kan zijn.

# Voorlopige limieten:
| Waarde | Omschrijving |
|- -- -- -- -|- -- -- -- -- -- -- -|
| 1      | Aantal ontvangers per bericht. |
| 25     | Maximum aantal berichten dat in één PUT request verstuurd mag worden. |
| 100     | Maximum aantal berichten dat in één DELETE request verwijderd mag worden |
| 2000   | Maximum aantal berichten dat in één LIST request getoond zal worden. Indien wenselijk kunt u dit aantal middels een query-parameter beperken. |
| 100    | Maximum aantal berichten dat in één GET request ontvangen mag worden. |
| 64kb   | Maximum grootte van één enkel bericht. Één request zal qua grootte dan uitkomen op ((maximale-grootte-enkel-bericht * maximaal-aantal-berichten) + overhead). Houdt er rekening mee dat dit een waarde is die in te toekomst kan gaan groeien. Beperk uw oplossing dus niet op deze waarde! |


This C# SDK is automatically generated by the [OpenAPI Generator](https://openapi-generator.tech) project:

- API version: 0.7.6
- SDK version: 1.0.0
- Generator version: 7.17.0
- Build package: org.openapitools.codegen.languages.CSharpClientCodegen

<a id="frameworks-supported"></a>
## Frameworks supported

<a id="dependencies"></a>
## Dependencies

- [RestSharp](https://www.nuget.org/packages/RestSharp) - 112.0.0 or later
- [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json/) - 13.0.2 or later
- [JsonSubTypes](https://www.nuget.org/packages/JsonSubTypes/) - 1.8.0 or later
- [System.ComponentModel.Annotations](https://www.nuget.org/packages/System.ComponentModel.Annotations) - 5.0.0 or later

The DLLs included in the package may not be the latest version. We recommend using [NuGet](https://docs.nuget.org/consume/installing-nuget) to obtain the latest version of the packages:
```
Install-Package RestSharp
Install-Package Newtonsoft.Json
Install-Package JsonSubTypes
Install-Package System.ComponentModel.Annotations
```

NOTE: RestSharp versions greater than 105.1.0 have a bug which causes file uploads to fail. See [RestSharp#742](https://github.com/restsharp/RestSharp/issues/742).
NOTE: RestSharp for .Net Core creates a new socket for each api call, which can lead to a socket exhaustion problem. See [RestSharp#1406](https://github.com/restsharp/RestSharp/issues/1406).

<a id="installation"></a>
## Installation
Run the following command to generate the DLL
- [Mac/Linux] `/bin/sh build.sh`
- [Windows] `build.bat`

Then include the DLL (under the `bin` folder) in the C# project, and use the namespaces:
```csharp
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;
```
<a id="packaging"></a>
## Packaging

A `.nuspec` is included with the project. You can follow the Nuget quickstart to [create](https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package#create-the-package) and [publish](https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package#publish-the-package) packages.

This `.nuspec` uses placeholders from the `.csproj`, so build the `.csproj` directly:

```
nuget pack -Build -OutputDirectory out Org.OpenAPITools.csproj
```

Then, publish to a [local feed](https://docs.microsoft.com/en-us/nuget/hosting-packages/local-feeds) or [other host](https://docs.microsoft.com/en-us/nuget/hosting-packages/overview) and consume the new package via Nuget as usual.

<a id="usage"></a>
## Usage

To use the API client with a HTTP proxy, setup a `System.Net.WebProxy`
```csharp
Configuration c = new Configuration();
System.Net.WebProxy webProxy = new System.Net.WebProxy("http://myProxyUrl:80/");
webProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
c.Proxy = webProxy;
```

<a id="getting-started"></a>
## Getting Started

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class Example
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
            catch (ApiException e)
            {
                Debug.Print("Exception when calling BereikbaarheidApi.PingGet: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }

        }
    }
}
```

<a id="documentation-for-api-endpoints"></a>
## Documentation for API Endpoints

All URIs are relative to *https://apigw.idm.diginetwerk.net/api/brp/berichten/v1*

Class | Method | HTTP request | Description
------------ | ------------- | ------------- | -------------
*BereikbaarheidApi* | [**PingGet**](docs/BereikbaarheidApi.md#pingget) | **GET** /berichten/ping | Ping operatie t.b.v. het toetsen van de bereikbaarheid van de API vanuit de aangesloten partij.
*BereikbaarheidApi* | [**PingHead**](docs/BereikbaarheidApi.md#pinghead) | **HEAD** /berichten/ping | Ping operatie t.b.v. het toetsen van de bereikbaarheid van de API vanuit de aangesloten partij.
*BerichtconversieApi* | [**Converteer**](docs/BerichtconversieApi.md#converteer) | **POST** /berichten/conversie | Dit endpoint faciliteert bij de conversie van berichten tussen de verschillende soorten berichtformaten.
*BerichtenverkeerApi* | [**DeleteMessages**](docs/BerichtenverkeerApi.md#deletemessages) | **DELETE** /berichten/{berichtTransportIdsParam} | Het verwijderen van een of meerdere berichten (DELETE).
*BerichtenverkeerApi* | [**GetMessages**](docs/BerichtenverkeerApi.md#getmessages) | **GET** /berichten/{berichtTransportIdsParam} | Het ophalen van een of meerdere berichten (GET).
*BerichtenverkeerApi* | [**ListMessages**](docs/BerichtenverkeerApi.md#listmessages) | **GET** /berichten | Het ophalen van een lijst met berichten die klaarstaan (LIST).
*BerichtenverkeerApi* | [**PutMessages**](docs/BerichtenverkeerApi.md#putmessages) | **POST** /berichten | Het versturen van een of meerdere berichten (PUT).
*BerichtenverkeerApi* | [**Summarize**](docs/BerichtenverkeerApi.md#summarize) | **GET** /berichten/telling | Voert verschillende soorten tellingen uit waarmee bepaald kan worden hoeveel berichten er verwerkt dienen te worden (SUMMARIZE).


<a id="documentation-for-models"></a>
## Documentation for Models

 - [Model.Af01](docs/Af01.md)
 - [Model.Af01PlData](docs/Af01PlData.md)
 - [Model.Af01PlDataC01Inner](docs/Af01PlDataC01Inner.md)
 - [Model.Af01PlDataC01InnerAllOfHistorieInner](docs/Af01PlDataC01InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC02Inner](docs/Af01PlDataC02Inner.md)
 - [Model.Af01PlDataC02InnerAllOfHistorieInner](docs/Af01PlDataC02InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC04Inner](docs/Af01PlDataC04Inner.md)
 - [Model.Af01PlDataC04InnerAllOfHistorieInner](docs/Af01PlDataC04InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC05Inner](docs/Af01PlDataC05Inner.md)
 - [Model.Af01PlDataC05InnerAllOfHistorieInner](docs/Af01PlDataC05InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC06Inner](docs/Af01PlDataC06Inner.md)
 - [Model.Af01PlDataC06InnerAllOfHistorieInner](docs/Af01PlDataC06InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC07Inner](docs/Af01PlDataC07Inner.md)
 - [Model.Af01PlDataC07InnerAllOfHistorieInner](docs/Af01PlDataC07InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC08Inner](docs/Af01PlDataC08Inner.md)
 - [Model.Af01PlDataC08InnerAllOfHistorieInner](docs/Af01PlDataC08InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC09Inner](docs/Af01PlDataC09Inner.md)
 - [Model.Af01PlDataC09InnerAllOfHistorieInner](docs/Af01PlDataC09InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC10Inner](docs/Af01PlDataC10Inner.md)
 - [Model.Af01PlDataC10InnerAllOfHistorieInner](docs/Af01PlDataC10InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC11Inner](docs/Af01PlDataC11Inner.md)
 - [Model.Af01PlDataC11InnerAllOfHistorieInner](docs/Af01PlDataC11InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC12Inner](docs/Af01PlDataC12Inner.md)
 - [Model.Af01PlDataC12InnerAllOfHistorieInner](docs/Af01PlDataC12InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC13Inner](docs/Af01PlDataC13Inner.md)
 - [Model.Af01PlDataC13InnerAllOfHistorieInner](docs/Af01PlDataC13InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC14Inner](docs/Af01PlDataC14Inner.md)
 - [Model.Af01PlDataC14InnerAllOfHistorieInner](docs/Af01PlDataC14InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC15Inner](docs/Af01PlDataC15Inner.md)
 - [Model.Af01PlDataC15InnerAllOfHistorieInner](docs/Af01PlDataC15InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC16Inner](docs/Af01PlDataC16Inner.md)
 - [Model.Af01PlDataC16InnerAllOfHistorieInner](docs/Af01PlDataC16InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC17Inner](docs/Af01PlDataC17Inner.md)
 - [Model.Af01PlDataC17InnerAllOfHistorieInner](docs/Af01PlDataC17InnerAllOfHistorieInner.md)
 - [Model.Af01PlDataC21Inner](docs/Af01PlDataC21Inner.md)
 - [Model.Af01PlDataC21InnerAllOfHistorieInner](docs/Af01PlDataC21InnerAllOfHistorieInner.md)
 - [Model.Af11](docs/Af11.md)
 - [Model.Ag01](docs/Ag01.md)
 - [Model.Ag11](docs/Ag11.md)
 - [Model.Ag21](docs/Ag21.md)
 - [Model.Ag31](docs/Ag31.md)
 - [Model.Ap01](docs/Ap01.md)
 - [Model.Autorisatietabelregel](docs/Autorisatietabelregel.md)
 - [Model.Av01](docs/Av01.md)
 - [Model.BBAAUTHF001](docs/BBAAUTHF001.md)
 - [Model.BBAAUTHF002](docs/BBAAUTHF002.md)
 - [Model.BBACONVF001](docs/BBACONVF001.md)
 - [Model.BBACONVF002](docs/BBACONVF002.md)
 - [Model.BBACONVF003](docs/BBACONVF003.md)
 - [Model.BBACONVF004](docs/BBACONVF004.md)
 - [Model.BBACONVF005](docs/BBACONVF005.md)
 - [Model.BBACONVF006](docs/BBACONVF006.md)
 - [Model.BBADELETEF001](docs/BBADELETEF001.md)
 - [Model.BBADELETEF002](docs/BBADELETEF002.md)
 - [Model.BBADELETEF003](docs/BBADELETEF003.md)
 - [Model.BBADELETEF004](docs/BBADELETEF004.md)
 - [Model.BBAF999](docs/BBAF999.md)
 - [Model.BBAGETF001](docs/BBAGETF001.md)
 - [Model.BBAGETF002](docs/BBAGETF002.md)
 - [Model.BBAGETF003](docs/BBAGETF003.md)
 - [Model.BBAGETF004](docs/BBAGETF004.md)
 - [Model.BBALISTF001](docs/BBALISTF001.md)
 - [Model.BBAPUTF001](docs/BBAPUTF001.md)
 - [Model.BBAPUTF002](docs/BBAPUTF002.md)
 - [Model.BBAPUTF003](docs/BBAPUTF003.md)
 - [Model.BBAPUTF004](docs/BBAPUTF004.md)
 - [Model.BBASUMMARIZEF001](docs/BBASUMMARIZEF001.md)
 - [Model.BerichtKenmerken](docs/BerichtKenmerken.md)
 - [Model.Cb01](docs/Cb01.md)
 - [Model.Converteer200Response](docs/Converteer200Response.md)
 - [Model.Converteer200ResponseValidatieFoutenInner](docs/Converteer200ResponseValidatieFoutenInner.md)
 - [Model.Converteer400Response](docs/Converteer400Response.md)
 - [Model.Converteer406Response](docs/Converteer406Response.md)
 - [Model.Converteer415Response](docs/Converteer415Response.md)
 - [Model.Ct01](docs/Ct01.md)
 - [Model.Cw01](docs/Cw01.md)
 - [Model.DeleteMessageAntwoord](docs/DeleteMessageAntwoord.md)
 - [Model.DeleteMessageAntwoordAllOfFoutmeldingen](docs/DeleteMessageAntwoordAllOfFoutmeldingen.md)
 - [Model.DeleteMessageAntwoordAllOfNietSuccesvolVerwijderdeBerichten](docs/DeleteMessageAntwoordAllOfNietSuccesvolVerwijderdeBerichten.md)
 - [Model.Dt01](docs/Dt01.md)
 - [Model.Dw01](docs/Dw01.md)
 - [Model.Foutmelding](docs/Foutmelding.md)
 - [Model.GeneriekAntwoord](docs/GeneriekAntwoord.md)
 - [Model.GetMessageAntwoord](docs/GetMessageAntwoord.md)
 - [Model.GetMessageAntwoordAllOfFoutmeldingen](docs/GetMessageAntwoordAllOfFoutmeldingen.md)
 - [Model.GetMessageAntwoordAllOfNietOpgehaaldeBerichten](docs/GetMessageAntwoordAllOfNietOpgehaaldeBerichten.md)
 - [Model.GetMessageAntwoordAllOfOpgehaaldeBerichten](docs/GetMessageAntwoordAllOfOpgehaaldeBerichten.md)
 - [Model.GetMessageKenmerken](docs/GetMessageKenmerken.md)
 - [Model.Gv01](docs/Gv01.md)
 - [Model.Gv02](docs/Gv02.md)
 - [Model.Ha01](docs/Ha01.md)
 - [Model.Hf01](docs/Hf01.md)
 - [Model.Hq01](docs/Hq01.md)
 - [Model.Ib01](docs/Ib01.md)
 - [Model.If01](docs/If01.md)
 - [Model.If21](docs/If21.md)
 - [Model.If31](docs/If31.md)
 - [Model.If41](docs/If41.md)
 - [Model.Ii01](docs/Ii01.md)
 - [Model.InvalidParametersInner](docs/InvalidParametersInner.md)
 - [Model.Iv01](docs/Iv01.md)
 - [Model.Iv11](docs/Iv11.md)
 - [Model.Iv21](docs/Iv21.md)
 - [Model.Jb01](docs/Jb01.md)
 - [Model.Jf01](docs/Jf01.md)
 - [Model.Jf21](docs/Jf21.md)
 - [Model.Jf31](docs/Jf31.md)
 - [Model.Ji01](docs/Ji01.md)
 - [Model.Jv01](docs/Jv01.md)
 - [Model.La01](docs/La01.md)
 - [Model.Lf01](docs/Lf01.md)
 - [Model.Lg01](docs/Lg01.md)
 - [Model.Lg01PlData](docs/Lg01PlData.md)
 - [Model.ListMessageAntwoord](docs/ListMessageAntwoord.md)
 - [Model.ListMessageKenmerken](docs/ListMessageKenmerken.md)
 - [Model.ListMessages401Response](docs/ListMessages401Response.md)
 - [Model.LoBericht](docs/LoBericht.md)
 - [Model.Lq01](docs/Lq01.md)
 - [Model.Ng01](docs/Ng01.md)
 - [Model.Null](docs/Null.md)
 - [Model.Of11](docs/Of11.md)
 - [Model.Og11](docs/Og11.md)
 - [Model.PagineerbaarResultaat](docs/PagineerbaarResultaat.md)
 - [Model.PagineringResultaat](docs/PagineringResultaat.md)
 - [Model.PagineringVerzoek](docs/PagineringVerzoek.md)
 - [Model.Pf01](docs/Pf01.md)
 - [Model.Pf02](docs/Pf02.md)
 - [Model.Pf03](docs/Pf03.md)
 - [Model.PutMessage](docs/PutMessage.md)
 - [Model.PutMessageAntwoord](docs/PutMessageAntwoord.md)
 - [Model.PutMessageAntwoordAllOfFoutmeldingen](docs/PutMessageAntwoordAllOfFoutmeldingen.md)
 - [Model.PutMessageAntwoordAllOfNietVerwerkteBerichten](docs/PutMessageAntwoordAllOfNietVerwerkteBerichten.md)
 - [Model.PutMessageAntwoordAllOfVerwerkteBerichten](docs/PutMessageAntwoordAllOfVerwerkteBerichten.md)
 - [Model.PutMessageKenmerken](docs/PutMessageKenmerken.md)
 - [Model.PutMessageRequest](docs/PutMessageRequest.md)
 - [Model.Rb01](docs/Rb01.md)
 - [Model.Rf01](docs/Rf01.md)
 - [Model.Rf31](docs/Rf31.md)
 - [Model.Rv01](docs/Rv01.md)
 - [Model.Summarize200Response](docs/Summarize200Response.md)
 - [Model.Sv01](docs/Sv01.md)
 - [Model.Sv11](docs/Sv11.md)
 - [Model.Tb01](docs/Tb01.md)
 - [Model.Tb02](docs/Tb02.md)
 - [Model.Tf01](docs/Tf01.md)
 - [Model.Tf11](docs/Tf11.md)
 - [Model.Tf21](docs/Tf21.md)
 - [Model.Tv01](docs/Tv01.md)
 - [Model.Vb01](docs/Vb01.md)
 - [Model.Vb02](docs/Vb02.md)
 - [Model.Wa01](docs/Wa01.md)
 - [Model.Wa11](docs/Wa11.md)
 - [Model.Wf01](docs/Wf01.md)
 - [Model.Xa01](docs/Xa01.md)
 - [Model.Xf01](docs/Xf01.md)
 - [Model.Xq01](docs/Xq01.md)


<a id="documentation-for-authorization"></a>
## Documentation for Authorization


Authentication schemes defined for the API:
<a id="DemoBasicAuth"></a>
### DemoBasicAuth

- **Type**: HTTP basic authentication

<a id="DemoOAuth"></a>
### DemoOAuth

- **Type**: OAuth
- **Flow**: application
- **Authorization URL**: 
- **Scopes**: 
  - demo-bba-mailboxid: &#39;mailboxid&#39; dient vervangen te worden met het daadwerkelijke nummer van de mailbox.

<a id="AccOAuth"></a>
### AccOAuth

- **Type**: OAuth
- **Flow**: application
- **Authorization URL**: 
- **Scopes**: 
  - acc-bba-mailboxid: &#39;mailboxid&#39; dient vervangen te worden met het daadwerkelijke nummer van de mailbox.

<a id="LapOAuth"></a>
### LapOAuth

- **Type**: OAuth
- **Flow**: application
- **Authorization URL**: 
- **Scopes**: 
  - afnemercode-oin: &#39;afnemercode&#39; dient vervangen te worden met het daadwerkelijke afnemerId van de deelnemer (zie tabel35). &#39;oin&#39; Dient vervangen te worden met het organisatie-identificatie-nummer van de deelnemer (zie https://oinregister.logius.nl/oin-register) en dient tevens overeen te komen met het OIN dat in het client-certificaat is opgenomen dat gebruik wordt voor mTLS.

<a id="PrdOAuth"></a>
### PrdOAuth

- **Type**: OAuth
- **Flow**: application
- **Authorization URL**: 
- **Scopes**: 
  - afnemercode-oin: &#39;afnemercode&#39; dient vervangen te worden met het daadwerkelijke afnemerId van de deelnemer (zie tabel35). &#39;oin&#39; dient vervangen te worden met het organisatie-identificatie-nummer van de deelnemer (zie https://oinregister.logius.nl/oin-register) en dient tevens overeen te komen met het OIN dat in het client-certificaat is opgenomen dat gebruik wordt voor mTLS.

