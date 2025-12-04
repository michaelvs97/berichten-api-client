/*
 * BRP Berichten API
 *
 * Een REST API voor het a-synchroon uitwisselen van berichten welke in het Logisch Ontwerp gedefinieerd zijn.  # Wijzigingshistorie:  ## 0.7.6 November 2025 - Verwijzingen aangepast naar LO2025Q3 versie berichten schema's. Dit is puur een actualisatieslag, datamodel-technisch zijn er geen wijzigingen t.o.v. de vorige schema-versie. De wijzigingen beperken zich tot tekstuele wijzigen van de omschrijvingen.  ## 0.7.5 Oktober 2025 - \"Ontvanger\" toegevoegd aan apiGetBerichtKenmerken en apiListBerichtKenmerken. In principe weet de ontvanger tijdens het moment van ontvangen dat deze berichten voor hem bestemd zijn. Echter in andere situaties waarbij berichten (buiten de context van de berichten-api) verwerkt worden, kan het relevant zijn dat zowel de verzender als de ontvanger gedefinieerd zijn bij het bericht.  ## 0.7.4 Oktober 2025 - Aanpassing autorisatietabel elementen: De hoofdletter E is nu een kleine letter e, conform de elementen van andere berichtsoorten.  ## 0.7.3 September 2025 - Aangeduid dat request-body bij POST requests aanwezig moeten zijn. - Aangeduid dat `status` bij een Foutmelding verplicht is.  ## 0.7.2 September 2025 - HTTP response bij het versturen berichten aangepast van 201 naar 200. De implementatie van de berichten-api gaf al 200. - Beschrijving van het response bij het versturen van berichten aangepast explicieter wordt gemaakt dat de response gecontroleerd moet worden op succesvol en niet-succesvol verstuurde berichten.  ## 0.7.1 September 2025 - OAuth2 scopes beschreven zoals ze zijn in LAP en PRD.  ## 0.7.0 April 2025 - Aanpassingen in de structuur van de onderliggende JSON schema's, de inhoud is onveranderd gebleven.  ## 0.6.4 Maart 2025 - Details rondom MutualTLS/tweezijdig-TLS d.m.v. PKI-Overheid toegevoegd.   ## 0.6.3 Januari 2025 - Tekstuele verwijzing naar '/berichten/list' vervangen door '/berichten'.  ## 0.6.2 Januari 2025 - OAuth endpoint demo omgeving gecorrigeerd.  ## 0.6.1 Januari 2025 - Ping endpoints zijn niet langer bereikbaar zonder eerst te authenticeren.  ## 0.6.0 Januari 2025 - Servers toegevoegd.   - Hiervoor moesten de paden van de endpoints aangepast worden. Hierdoor is '/api/v1' komen te vervallen bij de endpoints. Dit is verhuisd naar de base-url's van de genoemde servers. - Authenticatie details toegevoegd. - Foutmeldingen bij het conversie endpoint bijgewerkt. - Alle namespace paden van de foutmeldingen nagelopen en gecorrigeerd daar waar nodig. Dit zodat ze consistent starten met: \"https://www.rvig.nl/brp/berichten-api/probleem/\".  ## 0.5.5 November 2024 - 'berichtVolgnummer' en 'afzender' required gemaakt in ListMessageKenmerken (en daardoor ook in GetMessageKenmerken). - De GET en DELETE foutsituaties zijn voorzien van discriminators ten behoeve van het onderscheiden van foutsituatie subtypes door de gegenereerde client.  ## 0.5.4 November 2024 - Properties van PagineringResultaat en PagineerbaarResultaat welke altijd aanwezig zullen zijn bij een list-request required gemaakt. - Required properties van put-message-antwoord gecorrigeerd. - Schema van het Null bericht gecorrigeerd.  ## 0.5.3 November 2024 - Het Sv11 bericht is qua schema aangepast aangezien deze onterecht de property plData bevatte. Deze property is verwijderd uit dit bericht. - Foutsituaties zijn voorzien van discriminators ten behoeve van het onderscheiden van foutsituatie subtypes door de gegenereerde client.  ## 0.5.2 Oktober 2024 - De request die volledig afgekeurd worden en een statuscode 4xx of 5xx retourneren, doen dit nu met de response header 'Content-Type: application/problem+json'. - Requests die een 2xx response in JSON formaat retourneren, doen dit met \"Content-Type: application/json\" ipv \"Content-Type: application/json: charset=utf-8\". Conform rfc8259 (https://www.rfc-editor.org/rfc/rfc8259) is JSON altijd in UTF-8 formaat en heeft dit type geen charset parameter.  ## 0.5.1 Oktober 2024  - De fout BBA-PUT-F002 is aangepast naar een algemene fout voor onjuiste velden in een PutMessage, via het veld \"invalidParams\" in de response word aangegeven welke velden onjuist zijn en waarom.  ## 0.5.0 September 2024  - Het veld berichtId en verwijzingBerichtId zijn omgezet van type 'integer' naar type 'string' om beter aan te sluiten op de bestaande voorziening.  - Het probleem-antwoord response object is overal vervangen met de algemene Foutmelding response welke zich conformeert aan RFC7807.  - De velden 'foutTitel', 'foutType', 'foutDetail' zijn aangepast naar 'title, 'type', 'detail' zodat zij zich conformeren aan de RFC7807.  - De afhankelijkheid op 'openapi-problem-detail-v1.yml' is komen te vervallen (https://github.com/rvig-brp/BRP-Berichten-API/issues/3).  ## 0.4.1 Juli 2024  - Het json-schema voor de autorisatieberichten Ct01, Cw01 en Cb01 is toegevoegd.  - De ontvanger is opgenomen in de response bij het verzenden van een bericht. Dit is met name relevant wanneer er een bericht naar een berichtgroep gestuurd wordt. In dat geval weet de verzender wie de uiteindelijke ontvangers zijn. Bij het versturen van een bericht naar de een regulier account zal dit nummer 1:1 overeenkomen met de ontvanger die bij het te verzenden bericht is opgegeven.  ## 0.4.0 Juni 2024 - De json-schema's van de berichtsoorten zijn opgenomen in de OpenAPI Specificatie. Voor elke berichtsoort die het LO beschrijft, is opgenomen hoe dit bericht gestructureerd is.   - Houdt er rekening mee dat het weergeven van de OpenAPI specificatie in de web-versie va SwaggerUI hierdoor trager geworden is. Het is aan te raden om de alternatieve (redocly) weergave te gebruiken:     - https://brp-berichten-api.dictua.ictu-sr.nl/openapi/berichten-api.html     - De JSON schema's zijn tevens te vinden op onze Github pagina. - De JSON-response van het conversie endpoint is iets aangepast zodat naast het geconverteerde bericht tevens validatiefouten opgenomen kunnen worden.  ## 0.3.0 - Mei 2024 - Conversie endpoints   - Introductie bericht-conversie (/berichten/conversie) endpoint. Houdt er rekening mee dat de conversie naar JSON opgenomen is, maar nog niet geïmplementeerd is in de demo omgeving. - Het limiet van het aantal berichten dat verwijderd kan worden is gelijkgesteld aan dat wat gelijktijdig opgehaald kan worden (100). - De API is hernoemd van \"BRP A-Synchrone berichten API\" naar \"BRP berichten API\".   - Nieuwe URL's demo omgeving:     - https://brp-berichten-api.dictua.ictu-sr.nl/api/v1/berichten     - https://brp-berichten-api.dictua.ictu-sr.nl/openapi/berichten-api.html     - https://brp-berichten-api.dictua.ictu-sr.nl/swagger-ui/index.html - Beschikbaarheid endpoint(s)   - Er is een tweede ping endpoint bijgekomen waardoor en nu een HEAD of een GET gedaan kan worden. De response blijft hetzelfde. U kunt zelf kiezen welke van deze twee u hanteert.   - De noodzaak voor authenticatie op het `ping` endpoint is komen te vervallen. U kunt dus zonder noodzaak van authenticatie vaststellen of de dienst beschikbaar is.  ## 0.2.2 - April 2024 - Ping operatie toegevoegd t.b.v. het verifiëren dat er communicatie met de berichtendienst mogelijks is. - De standaard sortering bij een LIST operatie is op dit moment:   1. `Datum + tijdstip van ontvangst` waarbij geldt dat het oudste bericht als eerste wordt weergegeven in de lijst met beschikbare berichten (rationale deze dient als eerste verwerkt worden door de ontvanger).   2. Indien `datum + tijdstip van ontvangst` gelijk zijn (wat kan voorkomen aangezien er meerdere berichten tegelijk ingestuurd kunnen worden), dan worden `afzender` en het `messageId` meegenomen in de sortering. De volgorde die de afzender toegekend heen via de messageId is op dat moment dus bepalend.  ## 0.2.1 - April 2024 - Mogelijkheden tot sortering bij een list operatie zijn verwijderd. De standaard sortering wordt nog bepaald. - Demo omgeving is toegevoegd aan de lijst met servers.   - API te benaderen via https://brp-berichten-api.dictua.ictu-sr.nl/api/v1/berichten   - Swagger UI via: https://brp-berichten-api.dictua.ictu-sr.nl/swagger-ui/index.html   - De OpenAPI specificatie via: https://brp-berichten-api.dictua.ictu-sr.nl/openapi.brp-berichten-api-v1.yaml - Wachtwoord wijzigingen optie is verwijderd. - Het `berichtFormaat` attribuut is komen te vervallen. Alle berichten zijn nu per definitie in JSON formaat. De eis om de berichtInhoud Base64 te   encoderen komt daarmee te vervallen. - Voorbeelddata verbeterd. - Tellingen endpoint toegevoegd welke invulling geeft aan de mailbox Summarize tegenhanger. - Delete endpoint gecorrigeerd. De collectie `succesvolVerwijderdeBerichten` was van het type string i.p.v. berichtTransportId. - Limieten zijn gewijzigd:   - Het aantal berichten dat via een PUT verstuurd kan worden is verhoogd naar 25. Uitgaande van een gemiddelde berichtgrootte van 40kb geeft dat een request van 1MB groot.   - Het aantal berichten dat via een LIST opgevraagd kan worden is vergroot naar 2000. Daarbij krijgt u de mogelijkheid om dit aantal te beperken.     - 2000 berichten in een LIST operatie komt neer op ongeveer 600KB response grootte.   - Het aantal berichten dat via een GET ontvangen kan worden is verhoogd naar 100. Dit heeft te maken met de gangbare (veilige) restricties van een URL qua lengte (2KB).     - Voor de URL worden 256 bytes gereserveerd.       - Voor de UUID blijven dan 1.792 bytes over.     - Een BerichtTransportId is 17 bytes groot (UUID + separatie-karakter ',')       - Uitgaande van 17 bytes, zou dit 105 keer herhaald kunnen worden. Om aan de veilige kan te zitten en om op een mooi rond getal uit te komen kiezen wij voor 100 als limiet.     - Uitgaande van een gemiddelde berichtgrootte van 40KB komt je met 100 berichten uit op 4MB qua response-grootte. - \"aantalKeerOpgehaald\" en \"dtLaatstOpgehaald\" zijn verwijderd uit response van LIST (ListMessageKenmerken schema). Wij zien hierin geen meerwaarde voor de aansluitende partijen. Wel kunt u blijven zien OF het bericht is opgehaald (boolean waarde).  ## 0.2.0 - April 2024 - \"List\" verzoek is verhuisd van \"/berichten/lijst\" - -> \"/berichten\" - \"GET\" van meerdere berichten wordt gedaan middels path-parameters i.p.v. query-parameters en is samengevoegd met het endpoint voor het ophalen van een enkel bericht.<br/>   (/berichten/?berichtTransportIds=[UUID],[UUID] - -> /berichten/[UUID],[UUID])   (/berichten/?berichtTransportIds=[UUID],[UUID] - -> /berichten/[UUID],[UUID]) - \"DELETE\" van meerdere berichten wordt gedaan middels path-parameters i.p.v. query-parameters en is samengevoegd met het endpoint voor het verwijderen van een enkel bericht.<br/>   (/berichten/?berichtTransportIds=[UUID],[UUID] - -> /berichten/[UUID],[UUID]) - Het \"berichtId\" wat correspondeert met het \"MessageId\" veld van de mailboxserver is qua type gewijzigd van String naar Integer. Maximale lengte 12.   - Beschrijving LO: MessageId, lengte: 12, Het unieke volgnummer dat aan het uitgaande bericht wordt toegekend. - Het veld \"verwijzingBerichtId\" wat correspondeert met het \"CrossReference\" veld van de mailboxserver:   - is qua type gewijzigd van String naar Integer. Maximale lengte 12.   - kan of weggelaten worden, of gevuld worden met 0 indien het bericht een eerste bericht in de cyclus betreft. - \"aantalKeerOpgehaald\" is toegevoegd aan de ListMessageKenmerken.  ## 0.1.0 - Maart 2024 Initiële versie.  # In ontwikkeling: - Bepalen of het een checksum op de berichtinhoud van meerwaarde kan zijn.  # Voorlopige limieten: | Waarde | Omschrijving | |- -- -- -- -|- -- -- -- -- -- -- -| | 1      | Aantal ontvangers per bericht. | | 25     | Maximum aantal berichten dat in één PUT request verstuurd mag worden. | | 100     | Maximum aantal berichten dat in één DELETE request verwijderd mag worden | | 2000   | Maximum aantal berichten dat in één LIST request getoond zal worden. Indien wenselijk kunt u dit aantal middels een query-parameter beperken. | | 100    | Maximum aantal berichten dat in één GET request ontvangen mag worden. | | 64kb   | Maximum grootte van één enkel bericht. Één request zal qua grootte dan uitkomen op ((maximale-grootte-enkel-bericht * maximaal-aantal-berichten) + overhead). Houdt er rekening mee dat dit een waarde is die in te toekomst kan gaan groeien. Beperk uw oplossing dus niet op deze waarde! | 
 *
 * The version of the OpenAPI document: 0.7.6
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Client.Auth;
using Org.OpenAPITools.Model;

namespace Org.OpenAPITools.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IBerichtenverkeerApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Het verwijderen van een of meerdere berichten (DELETE).
        /// </summary>
        /// <remarks>
        /// Verwijderen van een of meerdere berichten. De berichten kunnen na deze actie niet meer bij de berichten API opgehaald worden. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>DeleteMessageAntwoord</returns>
        DeleteMessageAntwoord DeleteMessages(List<Guid> berichtTransportIdsParam, int operationIndex = 0);

        /// <summary>
        /// Het verwijderen van een of meerdere berichten (DELETE).
        /// </summary>
        /// <remarks>
        /// Verwijderen van een of meerdere berichten. De berichten kunnen na deze actie niet meer bij de berichten API opgehaald worden. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of DeleteMessageAntwoord</returns>
        ApiResponse<DeleteMessageAntwoord> DeleteMessagesWithHttpInfo(List<Guid> berichtTransportIdsParam, int operationIndex = 0);
        /// <summary>
        /// Het ophalen van een of meerdere berichten (GET).
        /// </summary>
        /// <remarks>
        /// Dit endpoint gebruikt u om berichten op te halen. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetMessageAntwoord</returns>
        GetMessageAntwoord GetMessages(List<Guid> berichtTransportIdsParam, int operationIndex = 0);

        /// <summary>
        /// Het ophalen van een of meerdere berichten (GET).
        /// </summary>
        /// <remarks>
        /// Dit endpoint gebruikt u om berichten op te halen. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetMessageAntwoord</returns>
        ApiResponse<GetMessageAntwoord> GetMessagesWithHttpInfo(List<Guid> berichtTransportIdsParam, int operationIndex = 0);
        /// <summary>
        /// Het ophalen van een lijst met berichten die klaarstaan (LIST).
        /// </summary>
        /// <remarks>
        /// Dit endpoint gebruikt u om te achterhalen welke berichten er voor u beschikbaar zijn. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="status">Geeft aan welke berichten opgenomen moeten worden in het resultaat. Comma-gescheiden veld. Indien leeg worden alle niet opgehaalde berichten getoond (&#x60;nieuw&#x60; + &#x60;gezien-in-lijst&#x60;).    - &#x60;nieuw&#x60;: Geeft berichten welke nog nooit opgehaald zijn en tevens niet in de lijst operatie zijn getoond.   - &#x60;gezien-in-lijst&#x60;: Geeft berichten die nog niet opgehaald zijn, maar al wel gezien zijn in de lijst.   - &#x60;opgehaald&#x60;: Geeft berichten reeds opgehaald zijn.  (optional)</param>
        /// <param name="berichtType">Geeft aan welke type berichten opgenomen moeten worden in het resultaat. Indien leeg of niet aanwezig worden alle berichten getoond. Niet hoofdlettergevoelig. (optional)</param>
        /// <param name="vanafMoment">Het resultaat zal alleen berichten bevatten die vanaf dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond die nog beschikbaar zijn (waarvan de retentietijd nog niet verlopen is). (optional)</param>
        /// <param name="totMoment">Het resultaat zal alleen berichten bevatten die tot dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond t/m het heden. (optional)</param>
        /// <param name="pagina">Pagina nummer, startend bij 1 t/m N (niet bij 0). (optional, default to 1)</param>
        /// <param name="berichtenPerPagina">Het maximum aantal berichten dat geretourneerd mag worden. Indien deze waarde het ingesteld systeemlimiet overschrijdt, dan wordt het systeemlimiet gehanteerd. Indien er geen waarde wordt opgegeven, dan wordt tevens het systeemlimiet gehanteerd.  (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListMessageAntwoord</returns>
        ListMessageAntwoord ListMessages(List<string>? status = default, string? berichtType = default, DateTime? vanafMoment = default, DateTime? totMoment = default, int? pagina = default, int? berichtenPerPagina = default, int operationIndex = 0);

        /// <summary>
        /// Het ophalen van een lijst met berichten die klaarstaan (LIST).
        /// </summary>
        /// <remarks>
        /// Dit endpoint gebruikt u om te achterhalen welke berichten er voor u beschikbaar zijn. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="status">Geeft aan welke berichten opgenomen moeten worden in het resultaat. Comma-gescheiden veld. Indien leeg worden alle niet opgehaalde berichten getoond (&#x60;nieuw&#x60; + &#x60;gezien-in-lijst&#x60;).    - &#x60;nieuw&#x60;: Geeft berichten welke nog nooit opgehaald zijn en tevens niet in de lijst operatie zijn getoond.   - &#x60;gezien-in-lijst&#x60;: Geeft berichten die nog niet opgehaald zijn, maar al wel gezien zijn in de lijst.   - &#x60;opgehaald&#x60;: Geeft berichten reeds opgehaald zijn.  (optional)</param>
        /// <param name="berichtType">Geeft aan welke type berichten opgenomen moeten worden in het resultaat. Indien leeg of niet aanwezig worden alle berichten getoond. Niet hoofdlettergevoelig. (optional)</param>
        /// <param name="vanafMoment">Het resultaat zal alleen berichten bevatten die vanaf dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond die nog beschikbaar zijn (waarvan de retentietijd nog niet verlopen is). (optional)</param>
        /// <param name="totMoment">Het resultaat zal alleen berichten bevatten die tot dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond t/m het heden. (optional)</param>
        /// <param name="pagina">Pagina nummer, startend bij 1 t/m N (niet bij 0). (optional, default to 1)</param>
        /// <param name="berichtenPerPagina">Het maximum aantal berichten dat geretourneerd mag worden. Indien deze waarde het ingesteld systeemlimiet overschrijdt, dan wordt het systeemlimiet gehanteerd. Indien er geen waarde wordt opgegeven, dan wordt tevens het systeemlimiet gehanteerd.  (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListMessageAntwoord</returns>
        ApiResponse<ListMessageAntwoord> ListMessagesWithHttpInfo(List<string>? status = default, string? berichtType = default, DateTime? vanafMoment = default, DateTime? totMoment = default, int? pagina = default, int? berichtenPerPagina = default, int operationIndex = 0);
        /// <summary>
        /// Het versturen van een of meerdere berichten (PUT).
        /// </summary>
        /// <remarks>
        /// Dit endpoint gebruikt u om berichten zoals gespecificeerd in het Logisch Ontwerp te versturen. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="putMessageRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>PutMessageAntwoord</returns>
        PutMessageAntwoord PutMessages(PutMessageRequest putMessageRequest, int operationIndex = 0);

        /// <summary>
        /// Het versturen van een of meerdere berichten (PUT).
        /// </summary>
        /// <remarks>
        /// Dit endpoint gebruikt u om berichten zoals gespecificeerd in het Logisch Ontwerp te versturen. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="putMessageRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of PutMessageAntwoord</returns>
        ApiResponse<PutMessageAntwoord> PutMessagesWithHttpInfo(PutMessageRequest putMessageRequest, int operationIndex = 0);
        /// <summary>
        /// Voert verschillende soorten tellingen uit waarmee bepaald kan worden hoeveel berichten er verwerkt dienen te worden (SUMMARIZE).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="soort">Geeft aan welke telling er uitgevoerd moet worden.   - nieuw: Geeft het aantal nieuwe berichten terug dat nog niet gezien is via een LIST operatie en tevens nog niet opgehaald is middels een GET operatie.   - gezien-in-lijst-en-niet-opgehaald: Geeft het aantal nieuwe berichten terug dat gezien is via een LIST operatie, maar nog niet opgehaald is middels een GET operatie.   - niet-opgehaald: Geeft het aantal nieuwe berichten terug dat nog niet opgehaald is middels een GET operatie. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Summarize200Response</returns>
        Summarize200Response Summarize(string soort, int operationIndex = 0);

        /// <summary>
        /// Voert verschillende soorten tellingen uit waarmee bepaald kan worden hoeveel berichten er verwerkt dienen te worden (SUMMARIZE).
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="soort">Geeft aan welke telling er uitgevoerd moet worden.   - nieuw: Geeft het aantal nieuwe berichten terug dat nog niet gezien is via een LIST operatie en tevens nog niet opgehaald is middels een GET operatie.   - gezien-in-lijst-en-niet-opgehaald: Geeft het aantal nieuwe berichten terug dat gezien is via een LIST operatie, maar nog niet opgehaald is middels een GET operatie.   - niet-opgehaald: Geeft het aantal nieuwe berichten terug dat nog niet opgehaald is middels een GET operatie. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Summarize200Response</returns>
        ApiResponse<Summarize200Response> SummarizeWithHttpInfo(string soort, int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IBerichtenverkeerApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Het verwijderen van een of meerdere berichten (DELETE).
        /// </summary>
        /// <remarks>
        /// Verwijderen van een of meerdere berichten. De berichten kunnen na deze actie niet meer bij de berichten API opgehaald worden. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of DeleteMessageAntwoord</returns>
        System.Threading.Tasks.Task<DeleteMessageAntwoord> DeleteMessagesAsync(List<Guid> berichtTransportIdsParam, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Het verwijderen van een of meerdere berichten (DELETE).
        /// </summary>
        /// <remarks>
        /// Verwijderen van een of meerdere berichten. De berichten kunnen na deze actie niet meer bij de berichten API opgehaald worden. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (DeleteMessageAntwoord)</returns>
        System.Threading.Tasks.Task<ApiResponse<DeleteMessageAntwoord>> DeleteMessagesWithHttpInfoAsync(List<Guid> berichtTransportIdsParam, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// Het ophalen van een of meerdere berichten (GET).
        /// </summary>
        /// <remarks>
        /// Dit endpoint gebruikt u om berichten op te halen. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetMessageAntwoord</returns>
        System.Threading.Tasks.Task<GetMessageAntwoord> GetMessagesAsync(List<Guid> berichtTransportIdsParam, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Het ophalen van een of meerdere berichten (GET).
        /// </summary>
        /// <remarks>
        /// Dit endpoint gebruikt u om berichten op te halen. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetMessageAntwoord)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetMessageAntwoord>> GetMessagesWithHttpInfoAsync(List<Guid> berichtTransportIdsParam, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// Het ophalen van een lijst met berichten die klaarstaan (LIST).
        /// </summary>
        /// <remarks>
        /// Dit endpoint gebruikt u om te achterhalen welke berichten er voor u beschikbaar zijn. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="status">Geeft aan welke berichten opgenomen moeten worden in het resultaat. Comma-gescheiden veld. Indien leeg worden alle niet opgehaalde berichten getoond (&#x60;nieuw&#x60; + &#x60;gezien-in-lijst&#x60;).    - &#x60;nieuw&#x60;: Geeft berichten welke nog nooit opgehaald zijn en tevens niet in de lijst operatie zijn getoond.   - &#x60;gezien-in-lijst&#x60;: Geeft berichten die nog niet opgehaald zijn, maar al wel gezien zijn in de lijst.   - &#x60;opgehaald&#x60;: Geeft berichten reeds opgehaald zijn.  (optional)</param>
        /// <param name="berichtType">Geeft aan welke type berichten opgenomen moeten worden in het resultaat. Indien leeg of niet aanwezig worden alle berichten getoond. Niet hoofdlettergevoelig. (optional)</param>
        /// <param name="vanafMoment">Het resultaat zal alleen berichten bevatten die vanaf dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond die nog beschikbaar zijn (waarvan de retentietijd nog niet verlopen is). (optional)</param>
        /// <param name="totMoment">Het resultaat zal alleen berichten bevatten die tot dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond t/m het heden. (optional)</param>
        /// <param name="pagina">Pagina nummer, startend bij 1 t/m N (niet bij 0). (optional, default to 1)</param>
        /// <param name="berichtenPerPagina">Het maximum aantal berichten dat geretourneerd mag worden. Indien deze waarde het ingesteld systeemlimiet overschrijdt, dan wordt het systeemlimiet gehanteerd. Indien er geen waarde wordt opgegeven, dan wordt tevens het systeemlimiet gehanteerd.  (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListMessageAntwoord</returns>
        System.Threading.Tasks.Task<ListMessageAntwoord> ListMessagesAsync(List<string>? status = default, string? berichtType = default, DateTime? vanafMoment = default, DateTime? totMoment = default, int? pagina = default, int? berichtenPerPagina = default, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Het ophalen van een lijst met berichten die klaarstaan (LIST).
        /// </summary>
        /// <remarks>
        /// Dit endpoint gebruikt u om te achterhalen welke berichten er voor u beschikbaar zijn. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="status">Geeft aan welke berichten opgenomen moeten worden in het resultaat. Comma-gescheiden veld. Indien leeg worden alle niet opgehaalde berichten getoond (&#x60;nieuw&#x60; + &#x60;gezien-in-lijst&#x60;).    - &#x60;nieuw&#x60;: Geeft berichten welke nog nooit opgehaald zijn en tevens niet in de lijst operatie zijn getoond.   - &#x60;gezien-in-lijst&#x60;: Geeft berichten die nog niet opgehaald zijn, maar al wel gezien zijn in de lijst.   - &#x60;opgehaald&#x60;: Geeft berichten reeds opgehaald zijn.  (optional)</param>
        /// <param name="berichtType">Geeft aan welke type berichten opgenomen moeten worden in het resultaat. Indien leeg of niet aanwezig worden alle berichten getoond. Niet hoofdlettergevoelig. (optional)</param>
        /// <param name="vanafMoment">Het resultaat zal alleen berichten bevatten die vanaf dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond die nog beschikbaar zijn (waarvan de retentietijd nog niet verlopen is). (optional)</param>
        /// <param name="totMoment">Het resultaat zal alleen berichten bevatten die tot dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond t/m het heden. (optional)</param>
        /// <param name="pagina">Pagina nummer, startend bij 1 t/m N (niet bij 0). (optional, default to 1)</param>
        /// <param name="berichtenPerPagina">Het maximum aantal berichten dat geretourneerd mag worden. Indien deze waarde het ingesteld systeemlimiet overschrijdt, dan wordt het systeemlimiet gehanteerd. Indien er geen waarde wordt opgegeven, dan wordt tevens het systeemlimiet gehanteerd.  (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListMessageAntwoord)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListMessageAntwoord>> ListMessagesWithHttpInfoAsync(List<string>? status = default, string? berichtType = default, DateTime? vanafMoment = default, DateTime? totMoment = default, int? pagina = default, int? berichtenPerPagina = default, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// Het versturen van een of meerdere berichten (PUT).
        /// </summary>
        /// <remarks>
        /// Dit endpoint gebruikt u om berichten zoals gespecificeerd in het Logisch Ontwerp te versturen. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="putMessageRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PutMessageAntwoord</returns>
        System.Threading.Tasks.Task<PutMessageAntwoord> PutMessagesAsync(PutMessageRequest putMessageRequest, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Het versturen van een of meerdere berichten (PUT).
        /// </summary>
        /// <remarks>
        /// Dit endpoint gebruikt u om berichten zoals gespecificeerd in het Logisch Ontwerp te versturen. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="putMessageRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PutMessageAntwoord)</returns>
        System.Threading.Tasks.Task<ApiResponse<PutMessageAntwoord>> PutMessagesWithHttpInfoAsync(PutMessageRequest putMessageRequest, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default);
        /// <summary>
        /// Voert verschillende soorten tellingen uit waarmee bepaald kan worden hoeveel berichten er verwerkt dienen te worden (SUMMARIZE).
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="soort">Geeft aan welke telling er uitgevoerd moet worden.   - nieuw: Geeft het aantal nieuwe berichten terug dat nog niet gezien is via een LIST operatie en tevens nog niet opgehaald is middels een GET operatie.   - gezien-in-lijst-en-niet-opgehaald: Geeft het aantal nieuwe berichten terug dat gezien is via een LIST operatie, maar nog niet opgehaald is middels een GET operatie.   - niet-opgehaald: Geeft het aantal nieuwe berichten terug dat nog niet opgehaald is middels een GET operatie. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Summarize200Response</returns>
        System.Threading.Tasks.Task<Summarize200Response> SummarizeAsync(string soort, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default);

        /// <summary>
        /// Voert verschillende soorten tellingen uit waarmee bepaald kan worden hoeveel berichten er verwerkt dienen te worden (SUMMARIZE).
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="soort">Geeft aan welke telling er uitgevoerd moet worden.   - nieuw: Geeft het aantal nieuwe berichten terug dat nog niet gezien is via een LIST operatie en tevens nog niet opgehaald is middels een GET operatie.   - gezien-in-lijst-en-niet-opgehaald: Geeft het aantal nieuwe berichten terug dat gezien is via een LIST operatie, maar nog niet opgehaald is middels een GET operatie.   - niet-opgehaald: Geeft het aantal nieuwe berichten terug dat nog niet opgehaald is middels een GET operatie. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Summarize200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<Summarize200Response>> SummarizeWithHttpInfoAsync(string soort, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default);
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IBerichtenverkeerApi : IBerichtenverkeerApiSync, IBerichtenverkeerApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class BerichtenverkeerApi : IBerichtenverkeerApi
    {
        private Org.OpenAPITools.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BerichtenverkeerApi"/> class.
        /// </summary>
        /// <returns></returns>
        public BerichtenverkeerApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BerichtenverkeerApi"/> class.
        /// </summary>
        /// <returns></returns>
        public BerichtenverkeerApi(string basePath)
        {
            this.Configuration = Org.OpenAPITools.Client.Configuration.MergeConfigurations(
                Org.OpenAPITools.Client.GlobalConfiguration.Instance,
                new Org.OpenAPITools.Client.Configuration { BasePath = basePath }
            );
            this.Client = new Org.OpenAPITools.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new Org.OpenAPITools.Client.ApiClient(this.Configuration.BasePath);
            this.ExceptionFactory = Org.OpenAPITools.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BerichtenverkeerApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public BerichtenverkeerApi(Org.OpenAPITools.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = Org.OpenAPITools.Client.Configuration.MergeConfigurations(
                Org.OpenAPITools.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.Client = new Org.OpenAPITools.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new Org.OpenAPITools.Client.ApiClient(this.Configuration.BasePath);
            ExceptionFactory = Org.OpenAPITools.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BerichtenverkeerApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public BerichtenverkeerApi(Org.OpenAPITools.Client.ISynchronousClient client, Org.OpenAPITools.Client.IAsynchronousClient asyncClient, Org.OpenAPITools.Client.IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = Org.OpenAPITools.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public Org.OpenAPITools.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public Org.OpenAPITools.Client.ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public string GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public Org.OpenAPITools.Client.IReadableConfiguration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public Org.OpenAPITools.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Het verwijderen van een of meerdere berichten (DELETE). Verwijderen van een of meerdere berichten. De berichten kunnen na deze actie niet meer bij de berichten API opgehaald worden. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>DeleteMessageAntwoord</returns>
        public DeleteMessageAntwoord DeleteMessages(List<Guid> berichtTransportIdsParam, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<DeleteMessageAntwoord> localVarResponse = DeleteMessagesWithHttpInfo(berichtTransportIdsParam);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Het verwijderen van een of meerdere berichten (DELETE). Verwijderen van een of meerdere berichten. De berichten kunnen na deze actie niet meer bij de berichten API opgehaald worden. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of DeleteMessageAntwoord</returns>
        public Org.OpenAPITools.Client.ApiResponse<DeleteMessageAntwoord> DeleteMessagesWithHttpInfo(List<Guid> berichtTransportIdsParam, int operationIndex = 0)
        {
            // verify the required parameter 'berichtTransportIdsParam' is set
            if (berichtTransportIdsParam == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'berichtTransportIdsParam' when calling BerichtenverkeerApi->DeleteMessages");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json",
                "application/problem+json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            var localVarMultipartFormData = localVarContentType == "multipart/form-data";
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("berichtTransportIdsParam", Org.OpenAPITools.Client.ClientUtils.ParameterToString(berichtTransportIdsParam)); // path parameter

            localVarRequestOptions.Operation = "BerichtenverkeerApi.DeleteMessages";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (LapOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (PrdOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (AccOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoBasicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + Org.OpenAPITools.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<DeleteMessageAntwoord>("/berichten/{berichtTransportIdsParam}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteMessages", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Het verwijderen van een of meerdere berichten (DELETE). Verwijderen van een of meerdere berichten. De berichten kunnen na deze actie niet meer bij de berichten API opgehaald worden. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of DeleteMessageAntwoord</returns>
        public async System.Threading.Tasks.Task<DeleteMessageAntwoord> DeleteMessagesAsync(List<Guid> berichtTransportIdsParam, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default)
        {
            Org.OpenAPITools.Client.ApiResponse<DeleteMessageAntwoord> localVarResponse = await DeleteMessagesWithHttpInfoAsync(berichtTransportIdsParam, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Het verwijderen van een of meerdere berichten (DELETE). Verwijderen van een of meerdere berichten. De berichten kunnen na deze actie niet meer bij de berichten API opgehaald worden. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (DeleteMessageAntwoord)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<DeleteMessageAntwoord>> DeleteMessagesWithHttpInfoAsync(List<Guid> berichtTransportIdsParam, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default)
        {
            // verify the required parameter 'berichtTransportIdsParam' is set
            if (berichtTransportIdsParam == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'berichtTransportIdsParam' when calling BerichtenverkeerApi->DeleteMessages");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json",
                "application/problem+json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("berichtTransportIdsParam", Org.OpenAPITools.Client.ClientUtils.ParameterToString(berichtTransportIdsParam)); // path parameter

            localVarRequestOptions.Operation = "BerichtenverkeerApi.DeleteMessages";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (LapOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (PrdOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (AccOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoBasicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + Org.OpenAPITools.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<DeleteMessageAntwoord>("/berichten/{berichtTransportIdsParam}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteMessages", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Het ophalen van een of meerdere berichten (GET). Dit endpoint gebruikt u om berichten op te halen. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetMessageAntwoord</returns>
        public GetMessageAntwoord GetMessages(List<Guid> berichtTransportIdsParam, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetMessageAntwoord> localVarResponse = GetMessagesWithHttpInfo(berichtTransportIdsParam);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Het ophalen van een of meerdere berichten (GET). Dit endpoint gebruikt u om berichten op te halen. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetMessageAntwoord</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetMessageAntwoord> GetMessagesWithHttpInfo(List<Guid> berichtTransportIdsParam, int operationIndex = 0)
        {
            // verify the required parameter 'berichtTransportIdsParam' is set
            if (berichtTransportIdsParam == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'berichtTransportIdsParam' when calling BerichtenverkeerApi->GetMessages");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json",
                "application/problem+json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            var localVarMultipartFormData = localVarContentType == "multipart/form-data";
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("berichtTransportIdsParam", Org.OpenAPITools.Client.ClientUtils.ParameterToString(berichtTransportIdsParam)); // path parameter

            localVarRequestOptions.Operation = "BerichtenverkeerApi.GetMessages";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (LapOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (PrdOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (AccOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoBasicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + Org.OpenAPITools.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetMessageAntwoord>("/berichten/{berichtTransportIdsParam}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetMessages", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Het ophalen van een of meerdere berichten (GET). Dit endpoint gebruikt u om berichten op te halen. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetMessageAntwoord</returns>
        public async System.Threading.Tasks.Task<GetMessageAntwoord> GetMessagesAsync(List<Guid> berichtTransportIdsParam, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default)
        {
            Org.OpenAPITools.Client.ApiResponse<GetMessageAntwoord> localVarResponse = await GetMessagesWithHttpInfoAsync(berichtTransportIdsParam, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Het ophalen van een of meerdere berichten (GET). Dit endpoint gebruikt u om berichten op te halen. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="berichtTransportIdsParam">Een UUID of meerdere UUID&#39;s van het bericht(en) die opgehaald moet worden. Indien meerdere UUID&#39;s, dan scheiden met een komma. Deze query parameter verwijst naar het berichtenId zoals deze bij de \&quot;BRP berichten API\&quot; bekend is, niet te verwarren met het BerichtId dat door de verzender is opgegeven. U verkrijgt deze UUID&#39;s middels een request naar &#x60;/berichten&#x60;. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetMessageAntwoord)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetMessageAntwoord>> GetMessagesWithHttpInfoAsync(List<Guid> berichtTransportIdsParam, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default)
        {
            // verify the required parameter 'berichtTransportIdsParam' is set
            if (berichtTransportIdsParam == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'berichtTransportIdsParam' when calling BerichtenverkeerApi->GetMessages");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json",
                "application/problem+json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("berichtTransportIdsParam", Org.OpenAPITools.Client.ClientUtils.ParameterToString(berichtTransportIdsParam)); // path parameter

            localVarRequestOptions.Operation = "BerichtenverkeerApi.GetMessages";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (LapOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (PrdOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (AccOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoBasicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + Org.OpenAPITools.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetMessageAntwoord>("/berichten/{berichtTransportIdsParam}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetMessages", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Het ophalen van een lijst met berichten die klaarstaan (LIST). Dit endpoint gebruikt u om te achterhalen welke berichten er voor u beschikbaar zijn. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="status">Geeft aan welke berichten opgenomen moeten worden in het resultaat. Comma-gescheiden veld. Indien leeg worden alle niet opgehaalde berichten getoond (&#x60;nieuw&#x60; + &#x60;gezien-in-lijst&#x60;).    - &#x60;nieuw&#x60;: Geeft berichten welke nog nooit opgehaald zijn en tevens niet in de lijst operatie zijn getoond.   - &#x60;gezien-in-lijst&#x60;: Geeft berichten die nog niet opgehaald zijn, maar al wel gezien zijn in de lijst.   - &#x60;opgehaald&#x60;: Geeft berichten reeds opgehaald zijn.  (optional)</param>
        /// <param name="berichtType">Geeft aan welke type berichten opgenomen moeten worden in het resultaat. Indien leeg of niet aanwezig worden alle berichten getoond. Niet hoofdlettergevoelig. (optional)</param>
        /// <param name="vanafMoment">Het resultaat zal alleen berichten bevatten die vanaf dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond die nog beschikbaar zijn (waarvan de retentietijd nog niet verlopen is). (optional)</param>
        /// <param name="totMoment">Het resultaat zal alleen berichten bevatten die tot dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond t/m het heden. (optional)</param>
        /// <param name="pagina">Pagina nummer, startend bij 1 t/m N (niet bij 0). (optional, default to 1)</param>
        /// <param name="berichtenPerPagina">Het maximum aantal berichten dat geretourneerd mag worden. Indien deze waarde het ingesteld systeemlimiet overschrijdt, dan wordt het systeemlimiet gehanteerd. Indien er geen waarde wordt opgegeven, dan wordt tevens het systeemlimiet gehanteerd.  (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListMessageAntwoord</returns>
        public ListMessageAntwoord ListMessages(List<string>? status = default, string? berichtType = default, DateTime? vanafMoment = default, DateTime? totMoment = default, int? pagina = default, int? berichtenPerPagina = default, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListMessageAntwoord> localVarResponse = ListMessagesWithHttpInfo(status, berichtType, vanafMoment, totMoment, pagina, berichtenPerPagina);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Het ophalen van een lijst met berichten die klaarstaan (LIST). Dit endpoint gebruikt u om te achterhalen welke berichten er voor u beschikbaar zijn. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="status">Geeft aan welke berichten opgenomen moeten worden in het resultaat. Comma-gescheiden veld. Indien leeg worden alle niet opgehaalde berichten getoond (&#x60;nieuw&#x60; + &#x60;gezien-in-lijst&#x60;).    - &#x60;nieuw&#x60;: Geeft berichten welke nog nooit opgehaald zijn en tevens niet in de lijst operatie zijn getoond.   - &#x60;gezien-in-lijst&#x60;: Geeft berichten die nog niet opgehaald zijn, maar al wel gezien zijn in de lijst.   - &#x60;opgehaald&#x60;: Geeft berichten reeds opgehaald zijn.  (optional)</param>
        /// <param name="berichtType">Geeft aan welke type berichten opgenomen moeten worden in het resultaat. Indien leeg of niet aanwezig worden alle berichten getoond. Niet hoofdlettergevoelig. (optional)</param>
        /// <param name="vanafMoment">Het resultaat zal alleen berichten bevatten die vanaf dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond die nog beschikbaar zijn (waarvan de retentietijd nog niet verlopen is). (optional)</param>
        /// <param name="totMoment">Het resultaat zal alleen berichten bevatten die tot dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond t/m het heden. (optional)</param>
        /// <param name="pagina">Pagina nummer, startend bij 1 t/m N (niet bij 0). (optional, default to 1)</param>
        /// <param name="berichtenPerPagina">Het maximum aantal berichten dat geretourneerd mag worden. Indien deze waarde het ingesteld systeemlimiet overschrijdt, dan wordt het systeemlimiet gehanteerd. Indien er geen waarde wordt opgegeven, dan wordt tevens het systeemlimiet gehanteerd.  (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListMessageAntwoord</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListMessageAntwoord> ListMessagesWithHttpInfo(List<string>? status = default, string? berichtType = default, DateTime? vanafMoment = default, DateTime? totMoment = default, int? pagina = default, int? berichtenPerPagina = default, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json",
                "application/problem+json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            var localVarMultipartFormData = localVarContentType == "multipart/form-data";
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            if (status != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("csv", "status", status));
            }
            if (berichtType != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "berichtType", berichtType));
            }
            if (vanafMoment != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "vanafMoment", vanafMoment));
            }
            if (totMoment != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "totMoment", totMoment));
            }
            if (pagina != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "pagina", pagina));
            }
            if (berichtenPerPagina != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "berichtenPerPagina", berichtenPerPagina));
            }

            localVarRequestOptions.Operation = "BerichtenverkeerApi.ListMessages";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (LapOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (PrdOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (AccOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoBasicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + Org.OpenAPITools.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListMessageAntwoord>("/berichten", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListMessages", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Het ophalen van een lijst met berichten die klaarstaan (LIST). Dit endpoint gebruikt u om te achterhalen welke berichten er voor u beschikbaar zijn. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="status">Geeft aan welke berichten opgenomen moeten worden in het resultaat. Comma-gescheiden veld. Indien leeg worden alle niet opgehaalde berichten getoond (&#x60;nieuw&#x60; + &#x60;gezien-in-lijst&#x60;).    - &#x60;nieuw&#x60;: Geeft berichten welke nog nooit opgehaald zijn en tevens niet in de lijst operatie zijn getoond.   - &#x60;gezien-in-lijst&#x60;: Geeft berichten die nog niet opgehaald zijn, maar al wel gezien zijn in de lijst.   - &#x60;opgehaald&#x60;: Geeft berichten reeds opgehaald zijn.  (optional)</param>
        /// <param name="berichtType">Geeft aan welke type berichten opgenomen moeten worden in het resultaat. Indien leeg of niet aanwezig worden alle berichten getoond. Niet hoofdlettergevoelig. (optional)</param>
        /// <param name="vanafMoment">Het resultaat zal alleen berichten bevatten die vanaf dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond die nog beschikbaar zijn (waarvan de retentietijd nog niet verlopen is). (optional)</param>
        /// <param name="totMoment">Het resultaat zal alleen berichten bevatten die tot dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond t/m het heden. (optional)</param>
        /// <param name="pagina">Pagina nummer, startend bij 1 t/m N (niet bij 0). (optional, default to 1)</param>
        /// <param name="berichtenPerPagina">Het maximum aantal berichten dat geretourneerd mag worden. Indien deze waarde het ingesteld systeemlimiet overschrijdt, dan wordt het systeemlimiet gehanteerd. Indien er geen waarde wordt opgegeven, dan wordt tevens het systeemlimiet gehanteerd.  (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListMessageAntwoord</returns>
        public async System.Threading.Tasks.Task<ListMessageAntwoord> ListMessagesAsync(List<string>? status = default, string? berichtType = default, DateTime? vanafMoment = default, DateTime? totMoment = default, int? pagina = default, int? berichtenPerPagina = default, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default)
        {
            Org.OpenAPITools.Client.ApiResponse<ListMessageAntwoord> localVarResponse = await ListMessagesWithHttpInfoAsync(status, berichtType, vanafMoment, totMoment, pagina, berichtenPerPagina, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Het ophalen van een lijst met berichten die klaarstaan (LIST). Dit endpoint gebruikt u om te achterhalen welke berichten er voor u beschikbaar zijn. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="status">Geeft aan welke berichten opgenomen moeten worden in het resultaat. Comma-gescheiden veld. Indien leeg worden alle niet opgehaalde berichten getoond (&#x60;nieuw&#x60; + &#x60;gezien-in-lijst&#x60;).    - &#x60;nieuw&#x60;: Geeft berichten welke nog nooit opgehaald zijn en tevens niet in de lijst operatie zijn getoond.   - &#x60;gezien-in-lijst&#x60;: Geeft berichten die nog niet opgehaald zijn, maar al wel gezien zijn in de lijst.   - &#x60;opgehaald&#x60;: Geeft berichten reeds opgehaald zijn.  (optional)</param>
        /// <param name="berichtType">Geeft aan welke type berichten opgenomen moeten worden in het resultaat. Indien leeg of niet aanwezig worden alle berichten getoond. Niet hoofdlettergevoelig. (optional)</param>
        /// <param name="vanafMoment">Het resultaat zal alleen berichten bevatten die vanaf dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond die nog beschikbaar zijn (waarvan de retentietijd nog niet verlopen is). (optional)</param>
        /// <param name="totMoment">Het resultaat zal alleen berichten bevatten die tot dit moment ontvangen zijn. Wanneer deze waarde niet opgegeven is, worden alle berichten getoond t/m het heden. (optional)</param>
        /// <param name="pagina">Pagina nummer, startend bij 1 t/m N (niet bij 0). (optional, default to 1)</param>
        /// <param name="berichtenPerPagina">Het maximum aantal berichten dat geretourneerd mag worden. Indien deze waarde het ingesteld systeemlimiet overschrijdt, dan wordt het systeemlimiet gehanteerd. Indien er geen waarde wordt opgegeven, dan wordt tevens het systeemlimiet gehanteerd.  (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListMessageAntwoord)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListMessageAntwoord>> ListMessagesWithHttpInfoAsync(List<string>? status = default, string? berichtType = default, DateTime? vanafMoment = default, DateTime? totMoment = default, int? pagina = default, int? berichtenPerPagina = default, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default)
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json",
                "application/problem+json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            if (status != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("csv", "status", status));
            }
            if (berichtType != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "berichtType", berichtType));
            }
            if (vanafMoment != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "vanafMoment", vanafMoment));
            }
            if (totMoment != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "totMoment", totMoment));
            }
            if (pagina != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "pagina", pagina));
            }
            if (berichtenPerPagina != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "berichtenPerPagina", berichtenPerPagina));
            }

            localVarRequestOptions.Operation = "BerichtenverkeerApi.ListMessages";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (LapOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (PrdOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (AccOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoBasicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + Org.OpenAPITools.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListMessageAntwoord>("/berichten", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListMessages", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Het versturen van een of meerdere berichten (PUT). Dit endpoint gebruikt u om berichten zoals gespecificeerd in het Logisch Ontwerp te versturen. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="putMessageRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>PutMessageAntwoord</returns>
        public PutMessageAntwoord PutMessages(PutMessageRequest putMessageRequest, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<PutMessageAntwoord> localVarResponse = PutMessagesWithHttpInfo(putMessageRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Het versturen van een of meerdere berichten (PUT). Dit endpoint gebruikt u om berichten zoals gespecificeerd in het Logisch Ontwerp te versturen. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="putMessageRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of PutMessageAntwoord</returns>
        public Org.OpenAPITools.Client.ApiResponse<PutMessageAntwoord> PutMessagesWithHttpInfo(PutMessageRequest putMessageRequest, int operationIndex = 0)
        {
            // verify the required parameter 'putMessageRequest' is set
            if (putMessageRequest == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'putMessageRequest' when calling BerichtenverkeerApi->PutMessages");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json",
                "application/problem+json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            var localVarMultipartFormData = localVarContentType == "multipart/form-data";
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = putMessageRequest;

            localVarRequestOptions.Operation = "BerichtenverkeerApi.PutMessages";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (LapOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (PrdOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (AccOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoBasicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + Org.OpenAPITools.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<PutMessageAntwoord>("/berichten", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PutMessages", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Het versturen van een of meerdere berichten (PUT). Dit endpoint gebruikt u om berichten zoals gespecificeerd in het Logisch Ontwerp te versturen. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="putMessageRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PutMessageAntwoord</returns>
        public async System.Threading.Tasks.Task<PutMessageAntwoord> PutMessagesAsync(PutMessageRequest putMessageRequest, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default)
        {
            Org.OpenAPITools.Client.ApiResponse<PutMessageAntwoord> localVarResponse = await PutMessagesWithHttpInfoAsync(putMessageRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Het versturen van een of meerdere berichten (PUT). Dit endpoint gebruikt u om berichten zoals gespecificeerd in het Logisch Ontwerp te versturen. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="putMessageRequest"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PutMessageAntwoord)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<PutMessageAntwoord>> PutMessagesWithHttpInfoAsync(PutMessageRequest putMessageRequest, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default)
        {
            // verify the required parameter 'putMessageRequest' is set
            if (putMessageRequest == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'putMessageRequest' when calling BerichtenverkeerApi->PutMessages");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json",
                "application/problem+json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = putMessageRequest;

            localVarRequestOptions.Operation = "BerichtenverkeerApi.PutMessages";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (LapOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (PrdOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (AccOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoBasicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + Org.OpenAPITools.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<PutMessageAntwoord>("/berichten", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PutMessages", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Voert verschillende soorten tellingen uit waarmee bepaald kan worden hoeveel berichten er verwerkt dienen te worden (SUMMARIZE). 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="soort">Geeft aan welke telling er uitgevoerd moet worden.   - nieuw: Geeft het aantal nieuwe berichten terug dat nog niet gezien is via een LIST operatie en tevens nog niet opgehaald is middels een GET operatie.   - gezien-in-lijst-en-niet-opgehaald: Geeft het aantal nieuwe berichten terug dat gezien is via een LIST operatie, maar nog niet opgehaald is middels een GET operatie.   - niet-opgehaald: Geeft het aantal nieuwe berichten terug dat nog niet opgehaald is middels een GET operatie. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Summarize200Response</returns>
        public Summarize200Response Summarize(string soort, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<Summarize200Response> localVarResponse = SummarizeWithHttpInfo(soort);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Voert verschillende soorten tellingen uit waarmee bepaald kan worden hoeveel berichten er verwerkt dienen te worden (SUMMARIZE). 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="soort">Geeft aan welke telling er uitgevoerd moet worden.   - nieuw: Geeft het aantal nieuwe berichten terug dat nog niet gezien is via een LIST operatie en tevens nog niet opgehaald is middels een GET operatie.   - gezien-in-lijst-en-niet-opgehaald: Geeft het aantal nieuwe berichten terug dat gezien is via een LIST operatie, maar nog niet opgehaald is middels een GET operatie.   - niet-opgehaald: Geeft het aantal nieuwe berichten terug dat nog niet opgehaald is middels een GET operatie. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Summarize200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<Summarize200Response> SummarizeWithHttpInfo(string soort, int operationIndex = 0)
        {
            // verify the required parameter 'soort' is set
            if (soort == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'soort' when calling BerichtenverkeerApi->Summarize");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json",
                "application/problem+json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            var localVarMultipartFormData = localVarContentType == "multipart/form-data";
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "soort", soort));

            localVarRequestOptions.Operation = "BerichtenverkeerApi.Summarize";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (LapOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (PrdOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (AccOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoBasicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + Org.OpenAPITools.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<Summarize200Response>("/berichten/telling", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("Summarize", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Voert verschillende soorten tellingen uit waarmee bepaald kan worden hoeveel berichten er verwerkt dienen te worden (SUMMARIZE). 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="soort">Geeft aan welke telling er uitgevoerd moet worden.   - nieuw: Geeft het aantal nieuwe berichten terug dat nog niet gezien is via een LIST operatie en tevens nog niet opgehaald is middels een GET operatie.   - gezien-in-lijst-en-niet-opgehaald: Geeft het aantal nieuwe berichten terug dat gezien is via een LIST operatie, maar nog niet opgehaald is middels een GET operatie.   - niet-opgehaald: Geeft het aantal nieuwe berichten terug dat nog niet opgehaald is middels een GET operatie. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Summarize200Response</returns>
        public async System.Threading.Tasks.Task<Summarize200Response> SummarizeAsync(string soort, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default)
        {
            Org.OpenAPITools.Client.ApiResponse<Summarize200Response> localVarResponse = await SummarizeWithHttpInfoAsync(soort, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Voert verschillende soorten tellingen uit waarmee bepaald kan worden hoeveel berichten er verwerkt dienen te worden (SUMMARIZE). 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="soort">Geeft aan welke telling er uitgevoerd moet worden.   - nieuw: Geeft het aantal nieuwe berichten terug dat nog niet gezien is via een LIST operatie en tevens nog niet opgehaald is middels een GET operatie.   - gezien-in-lijst-en-niet-opgehaald: Geeft het aantal nieuwe berichten terug dat gezien is via een LIST operatie, maar nog niet opgehaald is middels een GET operatie.   - niet-opgehaald: Geeft het aantal nieuwe berichten terug dat nog niet opgehaald is middels een GET operatie. </param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Summarize200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Summarize200Response>> SummarizeWithHttpInfoAsync(string soort, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default)
        {
            // verify the required parameter 'soort' is set
            if (soort == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'soort' when calling BerichtenverkeerApi->Summarize");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json",
                "application/problem+json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "soort", soort));

            localVarRequestOptions.Operation = "BerichtenverkeerApi.Summarize";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (LapOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (PrdOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (AccOAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (DemoBasicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + Org.OpenAPITools.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<Summarize200Response>("/berichten/telling", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("Summarize", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
