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
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = Org.OpenAPITools.Client.OpenAPIDateConverter;

namespace Org.OpenAPITools.Model
{
    /// <summary>
    /// Een tabelregel voor Tabel 35 - autorisatietabel die de opsomming van de door de verantwoordelijk Minister geautoriseerde en aangesloten instanties binnen het BRP-stelsel bevat.
    /// </summary>
    [DataContract(Name = "autorisatietabelregel")]
    public partial class Autorisatietabelregel : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Autorisatietabelregel" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Autorisatietabelregel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Autorisatietabelregel" /> class.
        /// </summary>
        /// <param name="e9510">e9510 (required).</param>
        /// <param name="e9511">Dit veld komt alleen in oude tabelregels voor (N-maal) (required).</param>
        /// <param name="e9512">Numeriek: 0 &#x3D; geheimhouding niet van toepassing, 1 &#x3D; geheimhouding van toepassing (required).</param>
        /// <param name="e9513">Numeriek: 0 &#x3D; geen beperking, 1 &#x3D; gevoelig, 2 &#x3D; geheim (required).</param>
        /// <param name="e9514">Numeriek: 0 &#x3D; niet verstrekken, 1 &#x3D; verstrekken (required).</param>
        /// <param name="e9520">e9520 (required).</param>
        /// <param name="e9540">Numeriek (N maal) (required).</param>
        /// <param name="e9541">e9541 (required).</param>
        /// <param name="e9542">Numeriek (N maal) (required).</param>
        /// <param name="e9543">0 &#x3D; plaatsen afnemersindicatie, 1 &#x3D; conditionele gegevensverstrekking (required).</param>
        /// <param name="e9544">N &#x3D; Berichtendienst, A &#x3D; alternatief medium (required).</param>
        /// <param name="e9550">Numeriek (N maal) (required).</param>
        /// <param name="e9551">e9551 (required).</param>
        /// <param name="e9552">Numeriek: 0 &#x3D; niet plaatsen, 1 &#x3D; plaatsen, 2 &#x3D; logisch verwijderen, 3 &#x3D; voorwaardelijk fysiek verwijderen, 4 &#x3D; onvoorwaardelijk fysiek verwijderen (required).</param>
        /// <param name="e9553">Numeriek: 0 &#x3D; niet verstrekken, 1 &#x3D; verstrekken (required).</param>
        /// <param name="e9554">Numeriek: jjjjmmdd (required).</param>
        /// <param name="e9555">e9555 (required).</param>
        /// <param name="e9556">Alfanumeriek: N &#x3D; Berichtendienst, webservice of API; A &#x3D; alternatief medium (required).</param>
        /// <param name="e9560">Numeriek (N maal) (required).</param>
        /// <param name="e9561">e9561 (required).</param>
        /// <param name="e9562">Numeriek: 0 &#x3D; niet bevoegd, 1 &#x3D; bevoegd (required).</param>
        /// <param name="e9563">Numeriek (N maal) (required).</param>
        /// <param name="e9566">Numeriek: 0 &#x3D; niet bevoegd, 1 &#x3D; bevoegd (required).</param>
        /// <param name="e9567">Alfanumeriek: N &#x3D; Berichtendienst, webservice of API; A &#x3D; alternatief medium (required).</param>
        /// <param name="e9570">Numeriek: dit veld komt alleen in oude tabelregels voor; N maal (required).</param>
        /// <param name="e9571">Alfamumeriek: dit veld komt alleen in oude tabelregels voor (required).</param>
        /// <param name="e9573">Alfamumeriek: N &#x3D; Berichtendienst, A &#x3D; alternatief medium; dit veld komt alleen in oude tabelregels voor (required).</param>
        /// <param name="e9998">Numeriek: jjjjmmdd (required).</param>
        /// <param name="e9999">Numeriek: jjjjmmdd (required).</param>
        public Autorisatietabelregel(string e9510 = default, List<string> e9511 = default, string e9512 = default, string e9513 = default, string e9514 = default, string e9520 = default, List<string> e9540 = default, string e9541 = default, List<string> e9542 = default, string e9543 = default, string e9544 = default, List<string> e9550 = default, string e9551 = default, string e9552 = default, string e9553 = default, string e9554 = default, string e9555 = default, string e9556 = default, List<string> e9560 = default, string e9561 = default, string e9562 = default, List<string> e9563 = default, string e9566 = default, string e9567 = default, List<string> e9570 = default, string e9571 = default, string e9573 = default, string e9998 = default, string e9999 = default)
        {
            // to ensure "e9510" is required (not null)
            if (e9510 == null)
            {
                throw new ArgumentNullException("e9510 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9510 = e9510;
            // to ensure "e9511" is required (not null)
            if (e9511 == null)
            {
                throw new ArgumentNullException("e9511 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9511 = e9511;
            // to ensure "e9512" is required (not null)
            if (e9512 == null)
            {
                throw new ArgumentNullException("e9512 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9512 = e9512;
            // to ensure "e9513" is required (not null)
            if (e9513 == null)
            {
                throw new ArgumentNullException("e9513 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9513 = e9513;
            // to ensure "e9514" is required (not null)
            if (e9514 == null)
            {
                throw new ArgumentNullException("e9514 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9514 = e9514;
            // to ensure "e9520" is required (not null)
            if (e9520 == null)
            {
                throw new ArgumentNullException("e9520 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9520 = e9520;
            // to ensure "e9540" is required (not null)
            if (e9540 == null)
            {
                throw new ArgumentNullException("e9540 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9540 = e9540;
            // to ensure "e9541" is required (not null)
            if (e9541 == null)
            {
                throw new ArgumentNullException("e9541 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9541 = e9541;
            // to ensure "e9542" is required (not null)
            if (e9542 == null)
            {
                throw new ArgumentNullException("e9542 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9542 = e9542;
            // to ensure "e9543" is required (not null)
            if (e9543 == null)
            {
                throw new ArgumentNullException("e9543 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9543 = e9543;
            // to ensure "e9544" is required (not null)
            if (e9544 == null)
            {
                throw new ArgumentNullException("e9544 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9544 = e9544;
            // to ensure "e9550" is required (not null)
            if (e9550 == null)
            {
                throw new ArgumentNullException("e9550 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9550 = e9550;
            // to ensure "e9551" is required (not null)
            if (e9551 == null)
            {
                throw new ArgumentNullException("e9551 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9551 = e9551;
            // to ensure "e9552" is required (not null)
            if (e9552 == null)
            {
                throw new ArgumentNullException("e9552 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9552 = e9552;
            // to ensure "e9553" is required (not null)
            if (e9553 == null)
            {
                throw new ArgumentNullException("e9553 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9553 = e9553;
            // to ensure "e9554" is required (not null)
            if (e9554 == null)
            {
                throw new ArgumentNullException("e9554 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9554 = e9554;
            // to ensure "e9555" is required (not null)
            if (e9555 == null)
            {
                throw new ArgumentNullException("e9555 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9555 = e9555;
            // to ensure "e9556" is required (not null)
            if (e9556 == null)
            {
                throw new ArgumentNullException("e9556 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9556 = e9556;
            // to ensure "e9560" is required (not null)
            if (e9560 == null)
            {
                throw new ArgumentNullException("e9560 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9560 = e9560;
            // to ensure "e9561" is required (not null)
            if (e9561 == null)
            {
                throw new ArgumentNullException("e9561 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9561 = e9561;
            // to ensure "e9562" is required (not null)
            if (e9562 == null)
            {
                throw new ArgumentNullException("e9562 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9562 = e9562;
            // to ensure "e9563" is required (not null)
            if (e9563 == null)
            {
                throw new ArgumentNullException("e9563 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9563 = e9563;
            // to ensure "e9566" is required (not null)
            if (e9566 == null)
            {
                throw new ArgumentNullException("e9566 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9566 = e9566;
            // to ensure "e9567" is required (not null)
            if (e9567 == null)
            {
                throw new ArgumentNullException("e9567 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9567 = e9567;
            // to ensure "e9570" is required (not null)
            if (e9570 == null)
            {
                throw new ArgumentNullException("e9570 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9570 = e9570;
            // to ensure "e9571" is required (not null)
            if (e9571 == null)
            {
                throw new ArgumentNullException("e9571 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9571 = e9571;
            // to ensure "e9573" is required (not null)
            if (e9573 == null)
            {
                throw new ArgumentNullException("e9573 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9573 = e9573;
            // to ensure "e9998" is required (not null)
            if (e9998 == null)
            {
                throw new ArgumentNullException("e9998 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9998 = e9998;
            // to ensure "e9999" is required (not null)
            if (e9999 == null)
            {
                throw new ArgumentNullException("e9999 is a required property for Autorisatietabelregel and cannot be null");
            }
            this.E9999 = e9999;
        }

        /// <summary>
        /// Gets or Sets E9510
        /// </summary>
        [DataMember(Name = "e9510", IsRequired = true, EmitDefaultValue = true)]
        public string E9510 { get; set; }

        /// <summary>
        /// Dit veld komt alleen in oude tabelregels voor (N-maal)
        /// </summary>
        /// <value>Dit veld komt alleen in oude tabelregels voor (N-maal)</value>
        [DataMember(Name = "e9511", IsRequired = true, EmitDefaultValue = true)]
        public List<string> E9511 { get; set; }

        /// <summary>
        /// Numeriek: 0 &#x3D; geheimhouding niet van toepassing, 1 &#x3D; geheimhouding van toepassing
        /// </summary>
        /// <value>Numeriek: 0 &#x3D; geheimhouding niet van toepassing, 1 &#x3D; geheimhouding van toepassing</value>
        [DataMember(Name = "e9512", IsRequired = true, EmitDefaultValue = true)]
        public string E9512 { get; set; }

        /// <summary>
        /// Numeriek: 0 &#x3D; geen beperking, 1 &#x3D; gevoelig, 2 &#x3D; geheim
        /// </summary>
        /// <value>Numeriek: 0 &#x3D; geen beperking, 1 &#x3D; gevoelig, 2 &#x3D; geheim</value>
        [DataMember(Name = "e9513", IsRequired = true, EmitDefaultValue = true)]
        public string E9513 { get; set; }

        /// <summary>
        /// Numeriek: 0 &#x3D; niet verstrekken, 1 &#x3D; verstrekken
        /// </summary>
        /// <value>Numeriek: 0 &#x3D; niet verstrekken, 1 &#x3D; verstrekken</value>
        [DataMember(Name = "e9514", IsRequired = true, EmitDefaultValue = true)]
        public string E9514 { get; set; }

        /// <summary>
        /// Gets or Sets E9520
        /// </summary>
        [DataMember(Name = "e9520", IsRequired = true, EmitDefaultValue = true)]
        public string E9520 { get; set; }

        /// <summary>
        /// Numeriek (N maal)
        /// </summary>
        /// <value>Numeriek (N maal)</value>
        [DataMember(Name = "e9540", IsRequired = true, EmitDefaultValue = true)]
        public List<string> E9540 { get; set; }

        /// <summary>
        /// Gets or Sets E9541
        /// </summary>
        [DataMember(Name = "e9541", IsRequired = true, EmitDefaultValue = true)]
        public string E9541 { get; set; }

        /// <summary>
        /// Numeriek (N maal)
        /// </summary>
        /// <value>Numeriek (N maal)</value>
        [DataMember(Name = "e9542", IsRequired = true, EmitDefaultValue = true)]
        public List<string> E9542 { get; set; }

        /// <summary>
        /// 0 &#x3D; plaatsen afnemersindicatie, 1 &#x3D; conditionele gegevensverstrekking
        /// </summary>
        /// <value>0 &#x3D; plaatsen afnemersindicatie, 1 &#x3D; conditionele gegevensverstrekking</value>
        [DataMember(Name = "e9543", IsRequired = true, EmitDefaultValue = true)]
        public string E9543 { get; set; }

        /// <summary>
        /// N &#x3D; Berichtendienst, A &#x3D; alternatief medium
        /// </summary>
        /// <value>N &#x3D; Berichtendienst, A &#x3D; alternatief medium</value>
        [DataMember(Name = "e9544", IsRequired = true, EmitDefaultValue = true)]
        public string E9544 { get; set; }

        /// <summary>
        /// Numeriek (N maal)
        /// </summary>
        /// <value>Numeriek (N maal)</value>
        [DataMember(Name = "e9550", IsRequired = true, EmitDefaultValue = true)]
        public List<string> E9550 { get; set; }

        /// <summary>
        /// Gets or Sets E9551
        /// </summary>
        [DataMember(Name = "e9551", IsRequired = true, EmitDefaultValue = true)]
        public string E9551 { get; set; }

        /// <summary>
        /// Numeriek: 0 &#x3D; niet plaatsen, 1 &#x3D; plaatsen, 2 &#x3D; logisch verwijderen, 3 &#x3D; voorwaardelijk fysiek verwijderen, 4 &#x3D; onvoorwaardelijk fysiek verwijderen
        /// </summary>
        /// <value>Numeriek: 0 &#x3D; niet plaatsen, 1 &#x3D; plaatsen, 2 &#x3D; logisch verwijderen, 3 &#x3D; voorwaardelijk fysiek verwijderen, 4 &#x3D; onvoorwaardelijk fysiek verwijderen</value>
        [DataMember(Name = "e9552", IsRequired = true, EmitDefaultValue = true)]
        public string E9552 { get; set; }

        /// <summary>
        /// Numeriek: 0 &#x3D; niet verstrekken, 1 &#x3D; verstrekken
        /// </summary>
        /// <value>Numeriek: 0 &#x3D; niet verstrekken, 1 &#x3D; verstrekken</value>
        [DataMember(Name = "e9553", IsRequired = true, EmitDefaultValue = true)]
        public string E9553 { get; set; }

        /// <summary>
        /// Numeriek: jjjjmmdd
        /// </summary>
        /// <value>Numeriek: jjjjmmdd</value>
        [DataMember(Name = "e9554", IsRequired = true, EmitDefaultValue = true)]
        public string E9554 { get; set; }

        /// <summary>
        /// Gets or Sets E9555
        /// </summary>
        [DataMember(Name = "e9555", IsRequired = true, EmitDefaultValue = true)]
        public string E9555 { get; set; }

        /// <summary>
        /// Alfanumeriek: N &#x3D; Berichtendienst, webservice of API; A &#x3D; alternatief medium
        /// </summary>
        /// <value>Alfanumeriek: N &#x3D; Berichtendienst, webservice of API; A &#x3D; alternatief medium</value>
        [DataMember(Name = "e9556", IsRequired = true, EmitDefaultValue = true)]
        public string E9556 { get; set; }

        /// <summary>
        /// Numeriek (N maal)
        /// </summary>
        /// <value>Numeriek (N maal)</value>
        [DataMember(Name = "e9560", IsRequired = true, EmitDefaultValue = true)]
        public List<string> E9560 { get; set; }

        /// <summary>
        /// Gets or Sets E9561
        /// </summary>
        [DataMember(Name = "e9561", IsRequired = true, EmitDefaultValue = true)]
        public string E9561 { get; set; }

        /// <summary>
        /// Numeriek: 0 &#x3D; niet bevoegd, 1 &#x3D; bevoegd
        /// </summary>
        /// <value>Numeriek: 0 &#x3D; niet bevoegd, 1 &#x3D; bevoegd</value>
        [DataMember(Name = "e9562", IsRequired = true, EmitDefaultValue = true)]
        public string E9562 { get; set; }

        /// <summary>
        /// Numeriek (N maal)
        /// </summary>
        /// <value>Numeriek (N maal)</value>
        [DataMember(Name = "e9563", IsRequired = true, EmitDefaultValue = true)]
        public List<string> E9563 { get; set; }

        /// <summary>
        /// Numeriek: 0 &#x3D; niet bevoegd, 1 &#x3D; bevoegd
        /// </summary>
        /// <value>Numeriek: 0 &#x3D; niet bevoegd, 1 &#x3D; bevoegd</value>
        [DataMember(Name = "e9566", IsRequired = true, EmitDefaultValue = true)]
        public string E9566 { get; set; }

        /// <summary>
        /// Alfanumeriek: N &#x3D; Berichtendienst, webservice of API; A &#x3D; alternatief medium
        /// </summary>
        /// <value>Alfanumeriek: N &#x3D; Berichtendienst, webservice of API; A &#x3D; alternatief medium</value>
        [DataMember(Name = "e9567", IsRequired = true, EmitDefaultValue = true)]
        public string E9567 { get; set; }

        /// <summary>
        /// Numeriek: dit veld komt alleen in oude tabelregels voor; N maal
        /// </summary>
        /// <value>Numeriek: dit veld komt alleen in oude tabelregels voor; N maal</value>
        [DataMember(Name = "e9570", IsRequired = true, EmitDefaultValue = true)]
        public List<string> E9570 { get; set; }

        /// <summary>
        /// Alfamumeriek: dit veld komt alleen in oude tabelregels voor
        /// </summary>
        /// <value>Alfamumeriek: dit veld komt alleen in oude tabelregels voor</value>
        [DataMember(Name = "e9571", IsRequired = true, EmitDefaultValue = true)]
        public string E9571 { get; set; }

        /// <summary>
        /// Alfamumeriek: N &#x3D; Berichtendienst, A &#x3D; alternatief medium; dit veld komt alleen in oude tabelregels voor
        /// </summary>
        /// <value>Alfamumeriek: N &#x3D; Berichtendienst, A &#x3D; alternatief medium; dit veld komt alleen in oude tabelregels voor</value>
        [DataMember(Name = "e9573", IsRequired = true, EmitDefaultValue = true)]
        public string E9573 { get; set; }

        /// <summary>
        /// Numeriek: jjjjmmdd
        /// </summary>
        /// <value>Numeriek: jjjjmmdd</value>
        [DataMember(Name = "e9998", IsRequired = true, EmitDefaultValue = true)]
        public string E9998 { get; set; }

        /// <summary>
        /// Numeriek: jjjjmmdd
        /// </summary>
        /// <value>Numeriek: jjjjmmdd</value>
        [DataMember(Name = "e9999", IsRequired = true, EmitDefaultValue = true)]
        public string E9999 { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Autorisatietabelregel {\n");
            sb.Append("  E9510: ").Append(E9510).Append("\n");
            sb.Append("  E9511: ").Append(E9511).Append("\n");
            sb.Append("  E9512: ").Append(E9512).Append("\n");
            sb.Append("  E9513: ").Append(E9513).Append("\n");
            sb.Append("  E9514: ").Append(E9514).Append("\n");
            sb.Append("  E9520: ").Append(E9520).Append("\n");
            sb.Append("  E9540: ").Append(E9540).Append("\n");
            sb.Append("  E9541: ").Append(E9541).Append("\n");
            sb.Append("  E9542: ").Append(E9542).Append("\n");
            sb.Append("  E9543: ").Append(E9543).Append("\n");
            sb.Append("  E9544: ").Append(E9544).Append("\n");
            sb.Append("  E9550: ").Append(E9550).Append("\n");
            sb.Append("  E9551: ").Append(E9551).Append("\n");
            sb.Append("  E9552: ").Append(E9552).Append("\n");
            sb.Append("  E9553: ").Append(E9553).Append("\n");
            sb.Append("  E9554: ").Append(E9554).Append("\n");
            sb.Append("  E9555: ").Append(E9555).Append("\n");
            sb.Append("  E9556: ").Append(E9556).Append("\n");
            sb.Append("  E9560: ").Append(E9560).Append("\n");
            sb.Append("  E9561: ").Append(E9561).Append("\n");
            sb.Append("  E9562: ").Append(E9562).Append("\n");
            sb.Append("  E9563: ").Append(E9563).Append("\n");
            sb.Append("  E9566: ").Append(E9566).Append("\n");
            sb.Append("  E9567: ").Append(E9567).Append("\n");
            sb.Append("  E9570: ").Append(E9570).Append("\n");
            sb.Append("  E9571: ").Append(E9571).Append("\n");
            sb.Append("  E9573: ").Append(E9573).Append("\n");
            sb.Append("  E9998: ").Append(E9998).Append("\n");
            sb.Append("  E9999: ").Append(E9999).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            // E9510 (string) maxLength
            if (this.E9510 != null && this.E9510.Length > 6)
            {
                yield return new ValidationResult("Invalid value for E9510, length must be less than 6.", new [] { "E9510" });
            }

            // E9510 (string) minLength
            if (this.E9510 != null && this.E9510.Length < 6)
            {
                yield return new ValidationResult("Invalid value for E9510, length must be greater than 6.", new [] { "E9510" });
            }

            // E9512 (string) maxLength
            if (this.E9512 != null && this.E9512.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E9512, length must be less than 1.", new [] { "E9512" });
            }

            // E9512 (string) minLength
            if (this.E9512 != null && this.E9512.Length < 1)
            {
                yield return new ValidationResult("Invalid value for E9512, length must be greater than 1.", new [] { "E9512" });
            }

            // E9513 (string) maxLength
            if (this.E9513 != null && this.E9513.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E9513, length must be less than 1.", new [] { "E9513" });
            }

            // E9513 (string) minLength
            if (this.E9513 != null && this.E9513.Length < 1)
            {
                yield return new ValidationResult("Invalid value for E9513, length must be greater than 1.", new [] { "E9513" });
            }

            // E9514 (string) maxLength
            if (this.E9514 != null && this.E9514.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E9514, length must be less than 1.", new [] { "E9514" });
            }

            // E9514 (string) minLength
            if (this.E9514 != null && this.E9514.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9514, length must be greater than 0.", new [] { "E9514" });
            }

            // E9520 (string) maxLength
            if (this.E9520 != null && this.E9520.Length > 80)
            {
                yield return new ValidationResult("Invalid value for E9520, length must be less than 80.", new [] { "E9520" });
            }

            // E9520 (string) minLength
            if (this.E9520 != null && this.E9520.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9520, length must be greater than 0.", new [] { "E9520" });
            }

            // E9541 (string) maxLength
            if (this.E9541 != null && this.E9541.Length > 4096)
            {
                yield return new ValidationResult("Invalid value for E9541, length must be less than 4096.", new [] { "E9541" });
            }

            // E9541 (string) minLength
            if (this.E9541 != null && this.E9541.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9541, length must be greater than 0.", new [] { "E9541" });
            }

            // E9543 (string) maxLength
            if (this.E9543 != null && this.E9543.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E9543, length must be less than 1.", new [] { "E9543" });
            }

            // E9543 (string) minLength
            if (this.E9543 != null && this.E9543.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9543, length must be greater than 0.", new [] { "E9543" });
            }

            // E9544 (string) maxLength
            if (this.E9544 != null && this.E9544.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E9544, length must be less than 1.", new [] { "E9544" });
            }

            // E9544 (string) minLength
            if (this.E9544 != null && this.E9544.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9544, length must be greater than 0.", new [] { "E9544" });
            }

            // E9551 (string) maxLength
            if (this.E9551 != null && this.E9551.Length > 4096)
            {
                yield return new ValidationResult("Invalid value for E9551, length must be less than 4096.", new [] { "E9551" });
            }

            // E9551 (string) minLength
            if (this.E9551 != null && this.E9551.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9551, length must be greater than 0.", new [] { "E9551" });
            }

            // E9552 (string) maxLength
            if (this.E9552 != null && this.E9552.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E9552, length must be less than 1.", new [] { "E9552" });
            }

            // E9552 (string) minLength
            if (this.E9552 != null && this.E9552.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9552, length must be greater than 0.", new [] { "E9552" });
            }

            // E9553 (string) maxLength
            if (this.E9553 != null && this.E9553.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E9553, length must be less than 1.", new [] { "E9553" });
            }

            // E9553 (string) minLength
            if (this.E9553 != null && this.E9553.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9553, length must be greater than 0.", new [] { "E9553" });
            }

            // E9554 (string) maxLength
            if (this.E9554 != null && this.E9554.Length > 8)
            {
                yield return new ValidationResult("Invalid value for E9554, length must be less than 8.", new [] { "E9554" });
            }

            // E9554 (string) minLength
            if (this.E9554 != null && this.E9554.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9554, length must be greater than 0.", new [] { "E9554" });
            }

            // E9555 (string) maxLength
            if (this.E9555 != null && this.E9555.Length > 2)
            {
                yield return new ValidationResult("Invalid value for E9555, length must be less than 2.", new [] { "E9555" });
            }

            // E9555 (string) minLength
            if (this.E9555 != null && this.E9555.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9555, length must be greater than 0.", new [] { "E9555" });
            }

            // E9556 (string) maxLength
            if (this.E9556 != null && this.E9556.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E9556, length must be less than 1.", new [] { "E9556" });
            }

            // E9556 (string) minLength
            if (this.E9556 != null && this.E9556.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9556, length must be greater than 0.", new [] { "E9556" });
            }

            // E9561 (string) maxLength
            if (this.E9561 != null && this.E9561.Length > 4096)
            {
                yield return new ValidationResult("Invalid value for E9561, length must be less than 4096.", new [] { "E9561" });
            }

            // E9561 (string) minLength
            if (this.E9561 != null && this.E9561.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9561, length must be greater than 0.", new [] { "E9561" });
            }

            // E9562 (string) maxLength
            if (this.E9562 != null && this.E9562.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E9562, length must be less than 1.", new [] { "E9562" });
            }

            // E9562 (string) minLength
            if (this.E9562 != null && this.E9562.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9562, length must be greater than 0.", new [] { "E9562" });
            }

            // E9566 (string) maxLength
            if (this.E9566 != null && this.E9566.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E9566, length must be less than 1.", new [] { "E9566" });
            }

            // E9566 (string) minLength
            if (this.E9566 != null && this.E9566.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9566, length must be greater than 0.", new [] { "E9566" });
            }

            // E9567 (string) maxLength
            if (this.E9567 != null && this.E9567.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E9567, length must be less than 1.", new [] { "E9567" });
            }

            // E9567 (string) minLength
            if (this.E9567 != null && this.E9567.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9567, length must be greater than 0.", new [] { "E9567" });
            }

            // E9571 (string) maxLength
            if (this.E9571 != null && this.E9571.Length > 4096)
            {
                yield return new ValidationResult("Invalid value for E9571, length must be less than 4096.", new [] { "E9571" });
            }

            // E9571 (string) minLength
            if (this.E9571 != null && this.E9571.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9571, length must be greater than 0.", new [] { "E9571" });
            }

            // E9573 (string) maxLength
            if (this.E9573 != null && this.E9573.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E9573, length must be less than 1.", new [] { "E9573" });
            }

            // E9573 (string) minLength
            if (this.E9573 != null && this.E9573.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9573, length must be greater than 0.", new [] { "E9573" });
            }

            // E9998 (string) maxLength
            if (this.E9998 != null && this.E9998.Length > 8)
            {
                yield return new ValidationResult("Invalid value for E9998, length must be less than 8.", new [] { "E9998" });
            }

            // E9998 (string) minLength
            if (this.E9998 != null && this.E9998.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9998, length must be greater than 0.", new [] { "E9998" });
            }

            // E9999 (string) maxLength
            if (this.E9999 != null && this.E9999.Length > 8)
            {
                yield return new ValidationResult("Invalid value for E9999, length must be less than 8.", new [] { "E9999" });
            }

            // E9999 (string) minLength
            if (this.E9999 != null && this.E9999.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E9999, length must be greater than 0.", new [] { "E9999" });
            }

            yield break;
        }
    }

}
