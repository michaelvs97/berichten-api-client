/*
 * BRP Berichten API
 *
 * Een REST API voor het a-synchroon uitwisselen van berichten welke in het Logisch Ontwerp gedefinieerd zijn.  # Wijzigingshistorie:  ## 0.7.6 November 2025 - Verwijzingen aangepast naar LO2025Q3 versie berichten schema's. Dit is puur een actualisatieslag, datamodel-technisch zijn er geen wijzigingen t.o.v. de vorige schema-versie. De wijzigingen beperken zich tot tekstuele wijzigen van de omschrijvingen.  ## 0.7.5 Oktober 2025 - \"Ontvanger\" toegevoegd aan apiGetBerichtKenmerken en apiListBerichtKenmerken. In principe weet de ontvanger tijdens het moment van ontvangen dat deze berichten voor hem bestemd zijn. Echter in andere situaties waarbij berichten (buiten de context van de berichten-api) verwerkt worden, kan het relevant zijn dat zowel de verzender als de ontvanger gedefinieerd zijn bij het bericht.  ## 0.7.4 Oktober 2025 - Aanpassing autorisatietabel elementen: De hoofdletter E is nu een kleine letter e, conform de elementen van andere berichtsoorten.  ## 0.7.3 September 2025 - Aangeduid dat request-body bij POST requests aanwezig moeten zijn. - Aangeduid dat `status` bij een Foutmelding verplicht is.  ## 0.7.2 September 2025 - HTTP response bij het versturen berichten aangepast van 201 naar 200. De implementatie van de berichten-api gaf al 200. - Beschrijving van het response bij het versturen van berichten aangepast explicieter wordt gemaakt dat de response gecontroleerd moet worden op succesvol en niet-succesvol verstuurde berichten.  ## 0.7.1 September 2025 - OAuth2 scopes beschreven zoals ze zijn in LAP en PRD.  ## 0.7.0 April 2025 - Aanpassingen in de structuur van de onderliggende JSON schema's, de inhoud is onveranderd gebleven.  ## 0.6.4 Maart 2025 - Details rondom MutualTLS/tweezijdig-TLS d.m.v. PKI-Overheid toegevoegd.   ## 0.6.3 Januari 2025 - Tekstuele verwijzing naar '/berichten/list' vervangen door '/berichten'.  ## 0.6.2 Januari 2025 - OAuth endpoint demo omgeving gecorrigeerd.  ## 0.6.1 Januari 2025 - Ping endpoints zijn niet langer bereikbaar zonder eerst te authenticeren.  ## 0.6.0 Januari 2025 - Servers toegevoegd.   - Hiervoor moesten de paden van de endpoints aangepast worden. Hierdoor is '/api/v1' komen te vervallen bij de endpoints. Dit is verhuisd naar de base-url's van de genoemde servers. - Authenticatie details toegevoegd. - Foutmeldingen bij het conversie endpoint bijgewerkt. - Alle namespace paden van de foutmeldingen nagelopen en gecorrigeerd daar waar nodig. Dit zodat ze consistent starten met: \"https://www.rvig.nl/brp/berichten-api/probleem/\".  ## 0.5.5 November 2024 - 'berichtVolgnummer' en 'afzender' required gemaakt in ListMessageKenmerken (en daardoor ook in GetMessageKenmerken). - De GET en DELETE foutsituaties zijn voorzien van discriminators ten behoeve van het onderscheiden van foutsituatie subtypes door de gegenereerde client.  ## 0.5.4 November 2024 - Properties van PagineringResultaat en PagineerbaarResultaat welke altijd aanwezig zullen zijn bij een list-request required gemaakt. - Required properties van put-message-antwoord gecorrigeerd. - Schema van het Null bericht gecorrigeerd.  ## 0.5.3 November 2024 - Het Sv11 bericht is qua schema aangepast aangezien deze onterecht de property plData bevatte. Deze property is verwijderd uit dit bericht. - Foutsituaties zijn voorzien van discriminators ten behoeve van het onderscheiden van foutsituatie subtypes door de gegenereerde client.  ## 0.5.2 Oktober 2024 - De request die volledig afgekeurd worden en een statuscode 4xx of 5xx retourneren, doen dit nu met de response header 'Content-Type: application/problem+json'. - Requests die een 2xx response in JSON formaat retourneren, doen dit met \"Content-Type: application/json\" ipv \"Content-Type: application/json: charset=utf-8\". Conform rfc8259 (https://www.rfc-editor.org/rfc/rfc8259) is JSON altijd in UTF-8 formaat en heeft dit type geen charset parameter.  ## 0.5.1 Oktober 2024  - De fout BBA-PUT-F002 is aangepast naar een algemene fout voor onjuiste velden in een PutMessage, via het veld \"invalidParams\" in de response word aangegeven welke velden onjuist zijn en waarom.  ## 0.5.0 September 2024  - Het veld berichtId en verwijzingBerichtId zijn omgezet van type 'integer' naar type 'string' om beter aan te sluiten op de bestaande voorziening.  - Het probleem-antwoord response object is overal vervangen met de algemene Foutmelding response welke zich conformeert aan RFC7807.  - De velden 'foutTitel', 'foutType', 'foutDetail' zijn aangepast naar 'title, 'type', 'detail' zodat zij zich conformeren aan de RFC7807.  - De afhankelijkheid op 'openapi-problem-detail-v1.yml' is komen te vervallen (https://github.com/rvig-brp/BRP-Berichten-API/issues/3).  ## 0.4.1 Juli 2024  - Het json-schema voor de autorisatieberichten Ct01, Cw01 en Cb01 is toegevoegd.  - De ontvanger is opgenomen in de response bij het verzenden van een bericht. Dit is met name relevant wanneer er een bericht naar een berichtgroep gestuurd wordt. In dat geval weet de verzender wie de uiteindelijke ontvangers zijn. Bij het versturen van een bericht naar de een regulier account zal dit nummer 1:1 overeenkomen met de ontvanger die bij het te verzenden bericht is opgegeven.  ## 0.4.0 Juni 2024 - De json-schema's van de berichtsoorten zijn opgenomen in de OpenAPI Specificatie. Voor elke berichtsoort die het LO beschrijft, is opgenomen hoe dit bericht gestructureerd is.   - Houdt er rekening mee dat het weergeven van de OpenAPI specificatie in de web-versie va SwaggerUI hierdoor trager geworden is. Het is aan te raden om de alternatieve (redocly) weergave te gebruiken:     - https://brp-berichten-api.dictua.ictu-sr.nl/openapi/berichten-api.html     - De JSON schema's zijn tevens te vinden op onze Github pagina. - De JSON-response van het conversie endpoint is iets aangepast zodat naast het geconverteerde bericht tevens validatiefouten opgenomen kunnen worden.  ## 0.3.0 - Mei 2024 - Conversie endpoints   - Introductie bericht-conversie (/berichten/conversie) endpoint. Houdt er rekening mee dat de conversie naar JSON opgenomen is, maar nog niet geïmplementeerd is in de demo omgeving. - Het limiet van het aantal berichten dat verwijderd kan worden is gelijkgesteld aan dat wat gelijktijdig opgehaald kan worden (100). - De API is hernoemd van \"BRP A-Synchrone berichten API\" naar \"BRP berichten API\".   - Nieuwe URL's demo omgeving:     - https://brp-berichten-api.dictua.ictu-sr.nl/api/v1/berichten     - https://brp-berichten-api.dictua.ictu-sr.nl/openapi/berichten-api.html     - https://brp-berichten-api.dictua.ictu-sr.nl/swagger-ui/index.html - Beschikbaarheid endpoint(s)   - Er is een tweede ping endpoint bijgekomen waardoor en nu een HEAD of een GET gedaan kan worden. De response blijft hetzelfde. U kunt zelf kiezen welke van deze twee u hanteert.   - De noodzaak voor authenticatie op het `ping` endpoint is komen te vervallen. U kunt dus zonder noodzaak van authenticatie vaststellen of de dienst beschikbaar is.  ## 0.2.2 - April 2024 - Ping operatie toegevoegd t.b.v. het verifiëren dat er communicatie met de berichtendienst mogelijks is. - De standaard sortering bij een LIST operatie is op dit moment:   1. `Datum + tijdstip van ontvangst` waarbij geldt dat het oudste bericht als eerste wordt weergegeven in de lijst met beschikbare berichten (rationale deze dient als eerste verwerkt worden door de ontvanger).   2. Indien `datum + tijdstip van ontvangst` gelijk zijn (wat kan voorkomen aangezien er meerdere berichten tegelijk ingestuurd kunnen worden), dan worden `afzender` en het `messageId` meegenomen in de sortering. De volgorde die de afzender toegekend heen via de messageId is op dat moment dus bepalend.  ## 0.2.1 - April 2024 - Mogelijkheden tot sortering bij een list operatie zijn verwijderd. De standaard sortering wordt nog bepaald. - Demo omgeving is toegevoegd aan de lijst met servers.   - API te benaderen via https://brp-berichten-api.dictua.ictu-sr.nl/api/v1/berichten   - Swagger UI via: https://brp-berichten-api.dictua.ictu-sr.nl/swagger-ui/index.html   - De OpenAPI specificatie via: https://brp-berichten-api.dictua.ictu-sr.nl/openapi.brp-berichten-api-v1.yaml - Wachtwoord wijzigingen optie is verwijderd. - Het `berichtFormaat` attribuut is komen te vervallen. Alle berichten zijn nu per definitie in JSON formaat. De eis om de berichtInhoud Base64 te   encoderen komt daarmee te vervallen. - Voorbeelddata verbeterd. - Tellingen endpoint toegevoegd welke invulling geeft aan de mailbox Summarize tegenhanger. - Delete endpoint gecorrigeerd. De collectie `succesvolVerwijderdeBerichten` was van het type string i.p.v. berichtTransportId. - Limieten zijn gewijzigd:   - Het aantal berichten dat via een PUT verstuurd kan worden is verhoogd naar 25. Uitgaande van een gemiddelde berichtgrootte van 40kb geeft dat een request van 1MB groot.   - Het aantal berichten dat via een LIST opgevraagd kan worden is vergroot naar 2000. Daarbij krijgt u de mogelijkheid om dit aantal te beperken.     - 2000 berichten in een LIST operatie komt neer op ongeveer 600KB response grootte.   - Het aantal berichten dat via een GET ontvangen kan worden is verhoogd naar 100. Dit heeft te maken met de gangbare (veilige) restricties van een URL qua lengte (2KB).     - Voor de URL worden 256 bytes gereserveerd.       - Voor de UUID blijven dan 1.792 bytes over.     - Een BerichtTransportId is 17 bytes groot (UUID + separatie-karakter ',')       - Uitgaande van 17 bytes, zou dit 105 keer herhaald kunnen worden. Om aan de veilige kan te zitten en om op een mooi rond getal uit te komen kiezen wij voor 100 als limiet.     - Uitgaande van een gemiddelde berichtgrootte van 40KB komt je met 100 berichten uit op 4MB qua response-grootte. - \"aantalKeerOpgehaald\" en \"dtLaatstOpgehaald\" zijn verwijderd uit response van LIST (ListMessageKenmerken schema). Wij zien hierin geen meerwaarde voor de aansluitende partijen. Wel kunt u blijven zien OF het bericht is opgehaald (boolean waarde).  ## 0.2.0 - April 2024 - \"List\" verzoek is verhuisd van \"/berichten/lijst\" - -> \"/berichten\" - \"GET\" van meerdere berichten wordt gedaan middels path-parameters i.p.v. query-parameters en is samengevoegd met het endpoint voor het ophalen van een enkel bericht.<br/>   (/berichten/?berichtTransportIds=[UUID],[UUID] - -> /berichten/[UUID],[UUID])   (/berichten/?berichtTransportIds=[UUID],[UUID] - -> /berichten/[UUID],[UUID]) - \"DELETE\" van meerdere berichten wordt gedaan middels path-parameters i.p.v. query-parameters en is samengevoegd met het endpoint voor het verwijderen van een enkel bericht.<br/>   (/berichten/?berichtTransportIds=[UUID],[UUID] - -> /berichten/[UUID],[UUID]) - Het \"berichtId\" wat correspondeert met het \"MessageId\" veld van de mailboxserver is qua type gewijzigd van String naar Integer. Maximale lengte 12.   - Beschrijving LO: MessageId, lengte: 12, Het unieke volgnummer dat aan het uitgaande bericht wordt toegekend. - Het veld \"verwijzingBerichtId\" wat correspondeert met het \"CrossReference\" veld van de mailboxserver:   - is qua type gewijzigd van String naar Integer. Maximale lengte 12.   - kan of weggelaten worden, of gevuld worden met 0 indien het bericht een eerste bericht in de cyclus betreft. - \"aantalKeerOpgehaald\" is toegevoegd aan de ListMessageKenmerken.  ## 0.1.0 - Maart 2024 Initiële versie.  # In ontwikkeling: - Bepalen of het een checksum op de berichtinhoud van meerwaarde kan zijn.  # Voorlopige limieten: | Waarde | Omschrijving | |- -- -- -- -|- -- -- -- -- -- -- -| | 1      | Aantal ontvangers per bericht. | | 25     | Maximum aantal berichten dat in één PUT request verstuurd mag worden. | | 100     | Maximum aantal berichten dat in één DELETE request verwijderd mag worden | | 2000   | Maximum aantal berichten dat in één LIST request getoond zal worden. Indien wenselijk kunt u dit aantal middels een query-parameter beperken. | | 100    | Maximum aantal berichten dat in één GET request ontvangen mag worden. | | 64kb   | Maximum grootte van één enkel bericht. Één request zal qua grootte dan uitkomen op ((maximale-grootte-enkel-bericht * maximaal-aantal-berichten) + overhead). Houdt er rekening mee dat dit een waarde is die in te toekomst kan gaan groeien. Beperk uw oplossing dus niet op deze waarde! | 
 *
 * The version of the OpenAPI document: 0.7.6
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using JsonSubTypes;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = Org.OpenAPITools.Client.OpenAPIDateConverter;
using System.Reflection;

namespace Org.OpenAPITools.Model
{
    /// <summary>
    /// LoBericht
    /// </summary>
    [JsonConverter(typeof(LoBerichtJsonConverter))]
    [DataContract(Name = "LoBericht")]
    public partial class LoBericht : AbstractOpenAPISchema, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class.
        /// </summary>
        public LoBericht()
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Af01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Af01.</param>
        public LoBericht(Af01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Af11" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Af11.</param>
        public LoBericht(Af11 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Ag01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Ag01.</param>
        public LoBericht(Ag01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Ag11" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Ag11.</param>
        public LoBericht(Ag11 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Ag21" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Ag21.</param>
        public LoBericht(Ag21 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Ag31" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Ag31.</param>
        public LoBericht(Ag31 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Ap01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Ap01.</param>
        public LoBericht(Ap01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Av01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Av01.</param>
        public LoBericht(Av01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Cb01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Cb01.</param>
        public LoBericht(Cb01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Ct01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Ct01.</param>
        public LoBericht(Ct01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Cw01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Cw01.</param>
        public LoBericht(Cw01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Dt01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Dt01.</param>
        public LoBericht(Dt01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Dw01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Dw01.</param>
        public LoBericht(Dw01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Gv01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Gv01.</param>
        public LoBericht(Gv01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Gv02" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Gv02.</param>
        public LoBericht(Gv02 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Ha01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Ha01.</param>
        public LoBericht(Ha01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Hf01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Hf01.</param>
        public LoBericht(Hf01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Hq01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Hq01.</param>
        public LoBericht(Hq01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Ib01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Ib01.</param>
        public LoBericht(Ib01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="If01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of If01.</param>
        public LoBericht(If01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="If21" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of If21.</param>
        public LoBericht(If21 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="If31" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of If31.</param>
        public LoBericht(If31 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="If41" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of If41.</param>
        public LoBericht(If41 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Ii01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Ii01.</param>
        public LoBericht(Ii01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Iv01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Iv01.</param>
        public LoBericht(Iv01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Iv11" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Iv11.</param>
        public LoBericht(Iv11 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Iv21" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Iv21.</param>
        public LoBericht(Iv21 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Jb01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Jb01.</param>
        public LoBericht(Jb01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Jf01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Jf01.</param>
        public LoBericht(Jf01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Jf21" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Jf21.</param>
        public LoBericht(Jf21 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Jf31" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Jf31.</param>
        public LoBericht(Jf31 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Ji01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Ji01.</param>
        public LoBericht(Ji01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Jv01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Jv01.</param>
        public LoBericht(Jv01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="La01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of La01.</param>
        public LoBericht(La01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Lf01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Lf01.</param>
        public LoBericht(Lf01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Lg01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Lg01.</param>
        public LoBericht(Lg01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Lq01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Lq01.</param>
        public LoBericht(Lq01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Ng01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Ng01.</param>
        public LoBericht(Ng01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Of11" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Of11.</param>
        public LoBericht(Of11 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Og11" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Og11.</param>
        public LoBericht(Og11 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Pf01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Pf01.</param>
        public LoBericht(Pf01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Pf02" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Pf02.</param>
        public LoBericht(Pf02 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Pf03" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Pf03.</param>
        public LoBericht(Pf03 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Rb01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Rb01.</param>
        public LoBericht(Rb01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Rf01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Rf01.</param>
        public LoBericht(Rf01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Rf31" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Rf31.</param>
        public LoBericht(Rf31 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Rv01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Rv01.</param>
        public LoBericht(Rv01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Sv01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Sv01.</param>
        public LoBericht(Sv01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Sv11" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Sv11.</param>
        public LoBericht(Sv11 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Tb01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Tb01.</param>
        public LoBericht(Tb01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Tb02" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Tb02.</param>
        public LoBericht(Tb02 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Tf01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Tf01.</param>
        public LoBericht(Tf01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Tf11" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Tf11.</param>
        public LoBericht(Tf11 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Tf21" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Tf21.</param>
        public LoBericht(Tf21 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Tv01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Tv01.</param>
        public LoBericht(Tv01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Vb01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Vb01.</param>
        public LoBericht(Vb01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Vb02" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Vb02.</param>
        public LoBericht(Vb02 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Wa01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Wa01.</param>
        public LoBericht(Wa01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Wa11" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Wa11.</param>
        public LoBericht(Wa11 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Wf01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Wf01.</param>
        public LoBericht(Wf01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Xa01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Xa01.</param>
        public LoBericht(Xa01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Xf01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Xf01.</param>
        public LoBericht(Xf01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoBericht" /> class
        /// with the <see cref="Xq01" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of Xq01.</param>
        public LoBericht(Xq01 actualInstance)
        {
            this.IsNullable = true;
            this.SchemaType= "oneOf";
            this.ActualInstance = actualInstance;
        }


        private Object _actualInstance;

        /// <summary>
        /// Gets or Sets ActualInstance
        /// </summary>
        public override Object ActualInstance
        {
            get
            {
                return _actualInstance;
            }
            set
            {
                if (value.GetType() == typeof(Af01) || value is Af01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Af11) || value is Af11)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Ag01) || value is Ag01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Ag11) || value is Ag11)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Ag21) || value is Ag21)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Ag31) || value is Ag31)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Ap01) || value is Ap01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Av01) || value is Av01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Cb01) || value is Cb01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Ct01) || value is Ct01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Cw01) || value is Cw01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Dt01) || value is Dt01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Dw01) || value is Dw01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Gv01) || value is Gv01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Gv02) || value is Gv02)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Ha01) || value is Ha01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Hf01) || value is Hf01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Hq01) || value is Hq01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Ib01) || value is Ib01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(If01) || value is If01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(If21) || value is If21)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(If31) || value is If31)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(If41) || value is If41)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Ii01) || value is Ii01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Iv01) || value is Iv01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Iv11) || value is Iv11)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Iv21) || value is Iv21)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Jb01) || value is Jb01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Jf01) || value is Jf01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Jf21) || value is Jf21)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Jf31) || value is Jf31)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Ji01) || value is Ji01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Jv01) || value is Jv01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(La01) || value is La01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Lf01) || value is Lf01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Lg01) || value is Lg01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Lq01) || value is Lq01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Ng01) || value is Ng01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Of11) || value is Of11)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Og11) || value is Og11)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Pf01) || value is Pf01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Pf02) || value is Pf02)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Pf03) || value is Pf03)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Rb01) || value is Rb01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Rf01) || value is Rf01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Rf31) || value is Rf31)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Rv01) || value is Rv01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Sv01) || value is Sv01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Sv11) || value is Sv11)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Tb01) || value is Tb01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Tb02) || value is Tb02)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Tf01) || value is Tf01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Tf11) || value is Tf11)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Tf21) || value is Tf21)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Tv01) || value is Tv01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Vb01) || value is Vb01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Vb02) || value is Vb02)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Wa01) || value is Wa01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Wa11) || value is Wa11)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Wf01) || value is Wf01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Xa01) || value is Xa01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Xf01) || value is Xf01)
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(Xq01) || value is Xq01)
                {
                    this._actualInstance = value;
                }
                else
                {
                    throw new ArgumentException("Invalid instance found. Must be the following types: Af01, Af11, Ag01, Ag11, Ag21, Ag31, Ap01, Av01, Cb01, Ct01, Cw01, Dt01, Dw01, Gv01, Gv02, Ha01, Hf01, Hq01, Ib01, If01, If21, If31, If41, Ii01, Iv01, Iv11, Iv21, Jb01, Jf01, Jf21, Jf31, Ji01, Jv01, La01, Lf01, Lg01, Lq01, Ng01, Of11, Og11, Pf01, Pf02, Pf03, Rb01, Rf01, Rf31, Rv01, Sv01, Sv11, Tb01, Tb02, Tf01, Tf11, Tf21, Tv01, Vb01, Vb02, Wa01, Wa11, Wf01, Xa01, Xf01, Xq01");
                }
            }
        }

        /// <summary>
        /// Get the actual instance of `Af01`. If the actual instance is not `Af01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Af01</returns>
        public Af01 GetAf01()
        {
            return (Af01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Af11`. If the actual instance is not `Af11`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Af11</returns>
        public Af11 GetAf11()
        {
            return (Af11)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Ag01`. If the actual instance is not `Ag01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Ag01</returns>
        public Ag01 GetAg01()
        {
            return (Ag01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Ag11`. If the actual instance is not `Ag11`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Ag11</returns>
        public Ag11 GetAg11()
        {
            return (Ag11)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Ag21`. If the actual instance is not `Ag21`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Ag21</returns>
        public Ag21 GetAg21()
        {
            return (Ag21)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Ag31`. If the actual instance is not `Ag31`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Ag31</returns>
        public Ag31 GetAg31()
        {
            return (Ag31)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Ap01`. If the actual instance is not `Ap01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Ap01</returns>
        public Ap01 GetAp01()
        {
            return (Ap01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Av01`. If the actual instance is not `Av01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Av01</returns>
        public Av01 GetAv01()
        {
            return (Av01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Cb01`. If the actual instance is not `Cb01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Cb01</returns>
        public Cb01 GetCb01()
        {
            return (Cb01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Ct01`. If the actual instance is not `Ct01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Ct01</returns>
        public Ct01 GetCt01()
        {
            return (Ct01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Cw01`. If the actual instance is not `Cw01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Cw01</returns>
        public Cw01 GetCw01()
        {
            return (Cw01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Dt01`. If the actual instance is not `Dt01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Dt01</returns>
        public Dt01 GetDt01()
        {
            return (Dt01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Dw01`. If the actual instance is not `Dw01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Dw01</returns>
        public Dw01 GetDw01()
        {
            return (Dw01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Gv01`. If the actual instance is not `Gv01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Gv01</returns>
        public Gv01 GetGv01()
        {
            return (Gv01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Gv02`. If the actual instance is not `Gv02`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Gv02</returns>
        public Gv02 GetGv02()
        {
            return (Gv02)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Ha01`. If the actual instance is not `Ha01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Ha01</returns>
        public Ha01 GetHa01()
        {
            return (Ha01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Hf01`. If the actual instance is not `Hf01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Hf01</returns>
        public Hf01 GetHf01()
        {
            return (Hf01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Hq01`. If the actual instance is not `Hq01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Hq01</returns>
        public Hq01 GetHq01()
        {
            return (Hq01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Ib01`. If the actual instance is not `Ib01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Ib01</returns>
        public Ib01 GetIb01()
        {
            return (Ib01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `If01`. If the actual instance is not `If01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of If01</returns>
        public If01 GetIf01()
        {
            return (If01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `If21`. If the actual instance is not `If21`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of If21</returns>
        public If21 GetIf21()
        {
            return (If21)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `If31`. If the actual instance is not `If31`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of If31</returns>
        public If31 GetIf31()
        {
            return (If31)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `If41`. If the actual instance is not `If41`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of If41</returns>
        public If41 GetIf41()
        {
            return (If41)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Ii01`. If the actual instance is not `Ii01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Ii01</returns>
        public Ii01 GetIi01()
        {
            return (Ii01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Iv01`. If the actual instance is not `Iv01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Iv01</returns>
        public Iv01 GetIv01()
        {
            return (Iv01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Iv11`. If the actual instance is not `Iv11`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Iv11</returns>
        public Iv11 GetIv11()
        {
            return (Iv11)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Iv21`. If the actual instance is not `Iv21`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Iv21</returns>
        public Iv21 GetIv21()
        {
            return (Iv21)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Jb01`. If the actual instance is not `Jb01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Jb01</returns>
        public Jb01 GetJb01()
        {
            return (Jb01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Jf01`. If the actual instance is not `Jf01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Jf01</returns>
        public Jf01 GetJf01()
        {
            return (Jf01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Jf21`. If the actual instance is not `Jf21`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Jf21</returns>
        public Jf21 GetJf21()
        {
            return (Jf21)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Jf31`. If the actual instance is not `Jf31`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Jf31</returns>
        public Jf31 GetJf31()
        {
            return (Jf31)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Ji01`. If the actual instance is not `Ji01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Ji01</returns>
        public Ji01 GetJi01()
        {
            return (Ji01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Jv01`. If the actual instance is not `Jv01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Jv01</returns>
        public Jv01 GetJv01()
        {
            return (Jv01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `La01`. If the actual instance is not `La01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of La01</returns>
        public La01 GetLa01()
        {
            return (La01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Lf01`. If the actual instance is not `Lf01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Lf01</returns>
        public Lf01 GetLf01()
        {
            return (Lf01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Lg01`. If the actual instance is not `Lg01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Lg01</returns>
        public Lg01 GetLg01()
        {
            return (Lg01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Lq01`. If the actual instance is not `Lq01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Lq01</returns>
        public Lq01 GetLq01()
        {
            return (Lq01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Ng01`. If the actual instance is not `Ng01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Ng01</returns>
        public Ng01 GetNg01()
        {
            return (Ng01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Of11`. If the actual instance is not `Of11`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Of11</returns>
        public Of11 GetOf11()
        {
            return (Of11)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Og11`. If the actual instance is not `Og11`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Og11</returns>
        public Og11 GetOg11()
        {
            return (Og11)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Pf01`. If the actual instance is not `Pf01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Pf01</returns>
        public Pf01 GetPf01()
        {
            return (Pf01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Pf02`. If the actual instance is not `Pf02`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Pf02</returns>
        public Pf02 GetPf02()
        {
            return (Pf02)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Pf03`. If the actual instance is not `Pf03`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Pf03</returns>
        public Pf03 GetPf03()
        {
            return (Pf03)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Rb01`. If the actual instance is not `Rb01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Rb01</returns>
        public Rb01 GetRb01()
        {
            return (Rb01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Rf01`. If the actual instance is not `Rf01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Rf01</returns>
        public Rf01 GetRf01()
        {
            return (Rf01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Rf31`. If the actual instance is not `Rf31`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Rf31</returns>
        public Rf31 GetRf31()
        {
            return (Rf31)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Rv01`. If the actual instance is not `Rv01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Rv01</returns>
        public Rv01 GetRv01()
        {
            return (Rv01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Sv01`. If the actual instance is not `Sv01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Sv01</returns>
        public Sv01 GetSv01()
        {
            return (Sv01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Sv11`. If the actual instance is not `Sv11`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Sv11</returns>
        public Sv11 GetSv11()
        {
            return (Sv11)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Tb01`. If the actual instance is not `Tb01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Tb01</returns>
        public Tb01 GetTb01()
        {
            return (Tb01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Tb02`. If the actual instance is not `Tb02`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Tb02</returns>
        public Tb02 GetTb02()
        {
            return (Tb02)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Tf01`. If the actual instance is not `Tf01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Tf01</returns>
        public Tf01 GetTf01()
        {
            return (Tf01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Tf11`. If the actual instance is not `Tf11`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Tf11</returns>
        public Tf11 GetTf11()
        {
            return (Tf11)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Tf21`. If the actual instance is not `Tf21`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Tf21</returns>
        public Tf21 GetTf21()
        {
            return (Tf21)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Tv01`. If the actual instance is not `Tv01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Tv01</returns>
        public Tv01 GetTv01()
        {
            return (Tv01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Vb01`. If the actual instance is not `Vb01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Vb01</returns>
        public Vb01 GetVb01()
        {
            return (Vb01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Vb02`. If the actual instance is not `Vb02`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Vb02</returns>
        public Vb02 GetVb02()
        {
            return (Vb02)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Wa01`. If the actual instance is not `Wa01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Wa01</returns>
        public Wa01 GetWa01()
        {
            return (Wa01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Wa11`. If the actual instance is not `Wa11`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Wa11</returns>
        public Wa11 GetWa11()
        {
            return (Wa11)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Wf01`. If the actual instance is not `Wf01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Wf01</returns>
        public Wf01 GetWf01()
        {
            return (Wf01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Xa01`. If the actual instance is not `Xa01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Xa01</returns>
        public Xa01 GetXa01()
        {
            return (Xa01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Xf01`. If the actual instance is not `Xf01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Xf01</returns>
        public Xf01 GetXf01()
        {
            return (Xf01)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `Xq01`. If the actual instance is not `Xq01`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of Xq01</returns>
        public Xq01 GetXq01()
        {
            return (Xq01)this.ActualInstance;
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class LoBericht {\n");
            sb.Append("  ActualInstance: ").Append(this.ActualInstance).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this.ActualInstance, LoBericht.SerializerSettings);
        }

        /// <summary>
        /// Converts the JSON string into an instance of LoBericht
        /// </summary>
        /// <param name="jsonString">JSON string</param>
        /// <returns>An instance of LoBericht</returns>
        public static LoBericht FromJson(string jsonString)
        {
            LoBericht newLoBericht = null;

            if (string.IsNullOrEmpty(jsonString))
            {
                return newLoBericht;
            }
            int match = 0;
            List<string> matchedTypes = new List<string>();

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Af01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Af01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Af01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Af01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Af01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Af11).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Af11>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Af11>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Af11");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Af11: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Ag01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ag01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ag01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Ag01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Ag01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Ag11).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ag11>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ag11>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Ag11");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Ag11: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Ag21).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ag21>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ag21>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Ag21");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Ag21: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Ag31).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ag31>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ag31>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Ag31");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Ag31: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Ap01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ap01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ap01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Ap01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Ap01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Av01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Av01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Av01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Av01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Av01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Cb01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Cb01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Cb01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Cb01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Cb01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Ct01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ct01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ct01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Ct01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Ct01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Cw01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Cw01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Cw01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Cw01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Cw01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Dt01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Dt01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Dt01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Dt01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Dt01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Dw01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Dw01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Dw01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Dw01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Dw01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Gv01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Gv01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Gv01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Gv01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Gv01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Gv02).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Gv02>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Gv02>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Gv02");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Gv02: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Ha01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ha01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ha01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Ha01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Ha01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Hf01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Hf01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Hf01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Hf01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Hf01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Hq01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Hq01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Hq01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Hq01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Hq01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Ib01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ib01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ib01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Ib01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Ib01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(If01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<If01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<If01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("If01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into If01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(If21).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<If21>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<If21>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("If21");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into If21: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(If31).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<If31>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<If31>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("If31");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into If31: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(If41).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<If41>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<If41>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("If41");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into If41: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Ii01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ii01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ii01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Ii01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Ii01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Iv01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Iv01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Iv01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Iv01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Iv01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Iv11).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Iv11>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Iv11>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Iv11");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Iv11: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Iv21).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Iv21>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Iv21>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Iv21");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Iv21: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Jb01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Jb01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Jb01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Jb01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Jb01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Jf01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Jf01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Jf01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Jf01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Jf01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Jf21).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Jf21>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Jf21>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Jf21");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Jf21: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Jf31).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Jf31>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Jf31>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Jf31");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Jf31: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Ji01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ji01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ji01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Ji01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Ji01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Jv01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Jv01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Jv01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Jv01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Jv01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(La01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<La01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<La01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("La01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into La01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Lf01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Lf01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Lf01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Lf01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Lf01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Lg01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Lg01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Lg01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Lg01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Lg01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Lq01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Lq01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Lq01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Lq01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Lq01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Ng01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ng01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Ng01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Ng01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Ng01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Of11).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Of11>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Of11>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Of11");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Of11: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Og11).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Og11>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Og11>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Og11");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Og11: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Pf01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Pf01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Pf01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Pf01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Pf01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Pf02).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Pf02>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Pf02>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Pf02");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Pf02: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Pf03).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Pf03>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Pf03>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Pf03");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Pf03: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Rb01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Rb01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Rb01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Rb01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Rb01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Rf01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Rf01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Rf01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Rf01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Rf01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Rf31).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Rf31>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Rf31>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Rf31");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Rf31: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Rv01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Rv01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Rv01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Rv01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Rv01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Sv01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Sv01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Sv01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Sv01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Sv01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Sv11).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Sv11>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Sv11>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Sv11");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Sv11: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Tb01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Tb01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Tb01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Tb01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Tb01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Tb02).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Tb02>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Tb02>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Tb02");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Tb02: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Tf01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Tf01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Tf01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Tf01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Tf01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Tf11).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Tf11>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Tf11>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Tf11");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Tf11: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Tf21).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Tf21>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Tf21>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Tf21");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Tf21: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Tv01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Tv01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Tv01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Tv01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Tv01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Vb01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Vb01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Vb01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Vb01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Vb01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Vb02).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Vb02>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Vb02>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Vb02");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Vb02: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Wa01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Wa01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Wa01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Wa01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Wa01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Wa11).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Wa11>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Wa11>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Wa11");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Wa11: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Wf01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Wf01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Wf01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Wf01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Wf01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Xa01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Xa01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Xa01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Xa01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Xa01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Xf01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Xf01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Xf01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Xf01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Xf01: {1}", jsonString, exception.ToString()));
            }

            try
            {
                // if it does not contains "AdditionalProperties", use SerializerSettings to deserialize
                if (typeof(Xq01).GetProperty("AdditionalProperties") == null)
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Xq01>(jsonString, LoBericht.SerializerSettings));
                }
                else
                {
                    newLoBericht = new LoBericht(JsonConvert.DeserializeObject<Xq01>(jsonString, LoBericht.AdditionalPropertiesSerializerSettings));
                }
                matchedTypes.Add("Xq01");
                match++;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into Xq01: {1}", jsonString, exception.ToString()));
            }

            if (match == 0)
            {
                throw new InvalidDataException("The JSON string `" + jsonString + "` cannot be deserialized into any schema defined.");
            }
            else if (match > 1)
            {
                throw new InvalidDataException("The JSON string `" + jsonString + "` incorrectly matches more than one schema (should be exactly one match): " + String.Join(",", matchedTypes));
            }

            // deserialization is considered successful at this point if no exception has been thrown.
            return newLoBericht;
        }


        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

    /// <summary>
    /// Custom JSON converter for LoBericht
    /// </summary>
    public class LoBerichtJsonConverter : JsonConverter
    {
        /// <summary>
        /// To write the JSON string
        /// </summary>
        /// <param name="writer">JSON writer</param>
        /// <param name="value">Object to be converted into a JSON string</param>
        /// <param name="serializer">JSON Serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue((string)(typeof(LoBericht).GetMethod("ToJson").Invoke(value, null)));
        }

        /// <summary>
        /// To convert a JSON string into an object
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="objectType">Object type</param>
        /// <param name="existingValue">Existing value</param>
        /// <param name="serializer">JSON Serializer</param>
        /// <returns>The object converted from the JSON string</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch(reader.TokenType) 
            {
                case JsonToken.StartObject:
                    return LoBericht.FromJson(JObject.Load(reader).ToString(Formatting.None));
                case JsonToken.StartArray:
                    return LoBericht.FromJson(JArray.Load(reader).ToString(Formatting.None));
                default:
                    return null;
            }
        }

        /// <summary>
        /// Check if the object can be converted
        /// </summary>
        /// <param name="objectType">Object type</param>
        /// <returns>True if the object can be converted</returns>
        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }

}
