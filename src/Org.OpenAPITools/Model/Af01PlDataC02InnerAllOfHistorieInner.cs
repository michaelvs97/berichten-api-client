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
    /// Af01PlDataC02InnerAllOfHistorieInner
    /// </summary>
    [DataContract(Name = "Af01_plData_c02_inner_allOf_historie_inner")]
    public partial class Af01PlDataC02InnerAllOfHistorieInner : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Af01PlDataC02InnerAllOfHistorieInner" /> class.
        /// </summary>
        /// <param name="e0110">Het administratienummer, bedoeld in artikel 4.9 van de Wet BRP. ⦿ Groep: Identificatienummers (01) ⦿ Element: A-nummer (01.10).</param>
        /// <param name="e0120">Het burgerservicenummer, bedoeld in artikel 1.1 van de Wet algemene bepalingen burgerservicenummer. ⦿ Groep: Identificatienummers (01) ⦿ Element: Burgerservicenummer (01.20).</param>
        /// <param name="e0210">De verzameling namen die, gescheiden door spaties, aan de geslachtsnaam voorafgaat. Indien aanwezig, wordt het predicaat (tabel 38) afgesplitst. ⦿ Groep: Naam (02) ⦿ Element: Voornamen (02.10).</param>
        /// <param name="e0220">Een code, voorkomend in Tabel 38, Tabel Adellijke titel/predicaat, die aangeeft welke titel of welk predicaat behoort tot de naam (bij adellijke titel geslachtsnaam, bij predicaat voornaam). ⦿ Groep: Naam (02) ⦿ Element: Adellijke titel/predicaat (02.20).</param>
        /// <param name="e0230">Dat deel van de geslachtsnaam dat voorkomt in Tabel 36, Voorvoegseltabel en, gescheiden door een spatie, voorafgaat aan de rest van de geslachtsnaam. ⦿ Groep: Naam (02) ⦿ Element: Voorvoegsel geslachtsnaam (02.30).</param>
        /// <param name="e0240">De (geslachts)naam waarvan de eventueel aanwezige voorvoegsels (tabel 36) en adellijke titel/predicaat (tabel 38) zijn afgesplitst. ⦿ Groep: Naam (02) ⦿ Element: Geslachtsnaam (02.40).</param>
        /// <param name="e0310">De datum waarop de persoon is geboren. ⦿ Groep: Geboorte (03) ⦿ Element: Geboortedatum (03.10).</param>
        /// <param name="e0320">Een code, opgenomen in Tabel 33, Gemeententabel of een buitenlandse plaats of een plaatsbepaling, die aangeeft waar de persoon is geboren. ⦿ Groep: Geboorte (03) ⦿ Element: Geboorteplaats (03.20).</param>
        /// <param name="e0330">Een code, opgenomen in Tabel 34, Landentabel, die het land aangeeft waar de persoon is geboren. ⦿ Groep: Geboorte (03) ⦿ Element: Geboorteland (03.30).</param>
        /// <param name="e0410">Een aanduiding die aangeeft dat de ingeschrevene een man of een vrouw is, of dat het geslacht (nog) onbekend is. ⦿ Groep: Geslacht (04) ⦿ Element: Geslachtsaanduiding (04.10).</param>
        /// <param name="e6210">De datum waarop de familierechtelijke betrekking is ontstaan. ⦿ Groep: Familierechtelijke betrekking (62) ⦿ Element: Datum ingang familierechtelijke betrekking (62.10).</param>
        /// <param name="e8110">Een code, opgenomen in Tabel 33, Gemeententabel, die aangeeft in welke gemeente de akte in de registers van de burgerlijke stand in Nederland is opgenomen. ⦿ Groep: Akte (81) ⦿ Element: Registergemeente akte (81.10).</param>
        /// <param name="e8120">Een aanduiding van de akte die is opgenomen in de registers van de burgerlijke stand in Nederland.  De eerste drie posities van het aktenummer dienen conform Tabel 39, Tabel Akteaanduiding te zijn. De laatste 4 posities bevatten een volgnummer van de akte. ⦿ Groep: Akte (81) ⦿ Element: Aktenummer (81.20).</param>
        /// <param name="e8210">Een code, opgenomen in Tabel 33, Gemeententabel, die aangeeft in welke gemeente de ontlening aan of de afleiding uit het document heeft plaatsgevonden. ⦿ Groep: Document (82) ⦿ Element: Gemeente document (82.10).</param>
        /// <param name="e8220">De datum waarop de ontlening aan of de afleiding uit het document heeft plaatsgevonden. ⦿ Groep: Document (82) ⦿ Element: Datum document (82.20).</param>
        /// <param name="e8230">Beschrijving van het document waaraan de gegevens zijn ontleend of waaruit de gegevens zijn afgeleid. ⦿ Groep: Document (82) ⦿ Element: Beschrijving document (82.30).</param>
        /// <param name="e8310">Een aanduiding dat in een categorie één of meer gegevens met betrekking tot de onjuistheid of de strijdigheid met de openbare orde zijn of worden onderzocht. In categorie 08 Verblijfplaats kan de aanduiding tevens aangeven dat er is vastgesteld dat betrokkene niet op het geregistreerde adres woont. ⦿ Groep: Procedure (83) ⦿ Element: Aanduiding gegevens in onderzoek (83.10).</param>
        /// <param name="e8320">De datum waarop een onderzoek inzake de onjuistheid of de strijdigheid met de openbare orde is gestart. ⦿ Groep: Procedure (83) ⦿ Element: Datum ingang onderzoek (83.20).</param>
        /// <param name="e8330">De datum waarop een onderzoek inzake de onjuistheid of de strijdigheid met de openbare orde is beéindigd. ⦿ Groep: Procedure (83) ⦿ Element: Datum einde onderzoek (83.30).</param>
        /// <param name="e8410">Een aanduiding dat één of meer gegevens onjuist of strijdig zijn met de openbare orde. ⦿ Groep: Onjuist (84) ⦿ Element: Indicatie onjuist, dan wel strijdigheid met de openbare orde (84.10).</param>
        /// <param name="e8510">De datum waarop het geheel van gegevens geldig is geworden. ⦿ Groep: Geldigheid (85) ⦿ Element: Ingangsdatum geldigheid (85.10).</param>
        /// <param name="e8610">De datum waarop het geheel van gegevens daadwerkelijk in de BRP is opgenomen. ⦿ Groep: Opneming (86) ⦿ Element: Datum van opneming (86.10).</param>
        public Af01PlDataC02InnerAllOfHistorieInner(string e0110 = default, string e0120 = default, string e0210 = default, string e0220 = default, string e0230 = default, string e0240 = default, string e0310 = default, string e0320 = default, string e0330 = default, string e0410 = default, string e6210 = default, string e8110 = default, string e8120 = default, string e8210 = default, string e8220 = default, string e8230 = default, string e8310 = default, string e8320 = default, string e8330 = default, string e8410 = default, string e8510 = default, string e8610 = default)
        {
            this.E0110 = e0110;
            this.E0120 = e0120;
            this.E0210 = e0210;
            this.E0220 = e0220;
            this.E0230 = e0230;
            this.E0240 = e0240;
            this.E0310 = e0310;
            this.E0320 = e0320;
            this.E0330 = e0330;
            this.E0410 = e0410;
            this.E6210 = e6210;
            this.E8110 = e8110;
            this.E8120 = e8120;
            this.E8210 = e8210;
            this.E8220 = e8220;
            this.E8230 = e8230;
            this.E8310 = e8310;
            this.E8320 = e8320;
            this.E8330 = e8330;
            this.E8410 = e8410;
            this.E8510 = e8510;
            this.E8610 = e8610;
        }

        /// <summary>
        /// Het administratienummer, bedoeld in artikel 4.9 van de Wet BRP. ⦿ Groep: Identificatienummers (01) ⦿ Element: A-nummer (01.10)
        /// </summary>
        /// <value>Het administratienummer, bedoeld in artikel 4.9 van de Wet BRP. ⦿ Groep: Identificatienummers (01) ⦿ Element: A-nummer (01.10)</value>
        [DataMember(Name = "e0110", EmitDefaultValue = false)]
        public string E0110 { get; set; }

        /// <summary>
        /// Het burgerservicenummer, bedoeld in artikel 1.1 van de Wet algemene bepalingen burgerservicenummer. ⦿ Groep: Identificatienummers (01) ⦿ Element: Burgerservicenummer (01.20)
        /// </summary>
        /// <value>Het burgerservicenummer, bedoeld in artikel 1.1 van de Wet algemene bepalingen burgerservicenummer. ⦿ Groep: Identificatienummers (01) ⦿ Element: Burgerservicenummer (01.20)</value>
        [DataMember(Name = "e0120", EmitDefaultValue = false)]
        public string E0120 { get; set; }

        /// <summary>
        /// De verzameling namen die, gescheiden door spaties, aan de geslachtsnaam voorafgaat. Indien aanwezig, wordt het predicaat (tabel 38) afgesplitst. ⦿ Groep: Naam (02) ⦿ Element: Voornamen (02.10)
        /// </summary>
        /// <value>De verzameling namen die, gescheiden door spaties, aan de geslachtsnaam voorafgaat. Indien aanwezig, wordt het predicaat (tabel 38) afgesplitst. ⦿ Groep: Naam (02) ⦿ Element: Voornamen (02.10)</value>
        [DataMember(Name = "e0210", EmitDefaultValue = false)]
        public string E0210 { get; set; }

        /// <summary>
        /// Een code, voorkomend in Tabel 38, Tabel Adellijke titel/predicaat, die aangeeft welke titel of welk predicaat behoort tot de naam (bij adellijke titel geslachtsnaam, bij predicaat voornaam). ⦿ Groep: Naam (02) ⦿ Element: Adellijke titel/predicaat (02.20)
        /// </summary>
        /// <value>Een code, voorkomend in Tabel 38, Tabel Adellijke titel/predicaat, die aangeeft welke titel of welk predicaat behoort tot de naam (bij adellijke titel geslachtsnaam, bij predicaat voornaam). ⦿ Groep: Naam (02) ⦿ Element: Adellijke titel/predicaat (02.20)</value>
        [DataMember(Name = "e0220", EmitDefaultValue = false)]
        public string E0220 { get; set; }

        /// <summary>
        /// Dat deel van de geslachtsnaam dat voorkomt in Tabel 36, Voorvoegseltabel en, gescheiden door een spatie, voorafgaat aan de rest van de geslachtsnaam. ⦿ Groep: Naam (02) ⦿ Element: Voorvoegsel geslachtsnaam (02.30)
        /// </summary>
        /// <value>Dat deel van de geslachtsnaam dat voorkomt in Tabel 36, Voorvoegseltabel en, gescheiden door een spatie, voorafgaat aan de rest van de geslachtsnaam. ⦿ Groep: Naam (02) ⦿ Element: Voorvoegsel geslachtsnaam (02.30)</value>
        [DataMember(Name = "e0230", EmitDefaultValue = false)]
        public string E0230 { get; set; }

        /// <summary>
        /// De (geslachts)naam waarvan de eventueel aanwezige voorvoegsels (tabel 36) en adellijke titel/predicaat (tabel 38) zijn afgesplitst. ⦿ Groep: Naam (02) ⦿ Element: Geslachtsnaam (02.40)
        /// </summary>
        /// <value>De (geslachts)naam waarvan de eventueel aanwezige voorvoegsels (tabel 36) en adellijke titel/predicaat (tabel 38) zijn afgesplitst. ⦿ Groep: Naam (02) ⦿ Element: Geslachtsnaam (02.40)</value>
        [DataMember(Name = "e0240", EmitDefaultValue = false)]
        public string E0240 { get; set; }

        /// <summary>
        /// De datum waarop de persoon is geboren. ⦿ Groep: Geboorte (03) ⦿ Element: Geboortedatum (03.10)
        /// </summary>
        /// <value>De datum waarop de persoon is geboren. ⦿ Groep: Geboorte (03) ⦿ Element: Geboortedatum (03.10)</value>
        [DataMember(Name = "e0310", EmitDefaultValue = false)]
        public string E0310 { get; set; }

        /// <summary>
        /// Een code, opgenomen in Tabel 33, Gemeententabel of een buitenlandse plaats of een plaatsbepaling, die aangeeft waar de persoon is geboren. ⦿ Groep: Geboorte (03) ⦿ Element: Geboorteplaats (03.20)
        /// </summary>
        /// <value>Een code, opgenomen in Tabel 33, Gemeententabel of een buitenlandse plaats of een plaatsbepaling, die aangeeft waar de persoon is geboren. ⦿ Groep: Geboorte (03) ⦿ Element: Geboorteplaats (03.20)</value>
        [DataMember(Name = "e0320", EmitDefaultValue = false)]
        public string E0320 { get; set; }

        /// <summary>
        /// Een code, opgenomen in Tabel 34, Landentabel, die het land aangeeft waar de persoon is geboren. ⦿ Groep: Geboorte (03) ⦿ Element: Geboorteland (03.30)
        /// </summary>
        /// <value>Een code, opgenomen in Tabel 34, Landentabel, die het land aangeeft waar de persoon is geboren. ⦿ Groep: Geboorte (03) ⦿ Element: Geboorteland (03.30)</value>
        [DataMember(Name = "e0330", EmitDefaultValue = false)]
        public string E0330 { get; set; }

        /// <summary>
        /// Een aanduiding die aangeeft dat de ingeschrevene een man of een vrouw is, of dat het geslacht (nog) onbekend is. ⦿ Groep: Geslacht (04) ⦿ Element: Geslachtsaanduiding (04.10)
        /// </summary>
        /// <value>Een aanduiding die aangeeft dat de ingeschrevene een man of een vrouw is, of dat het geslacht (nog) onbekend is. ⦿ Groep: Geslacht (04) ⦿ Element: Geslachtsaanduiding (04.10)</value>
        [DataMember(Name = "e0410", EmitDefaultValue = false)]
        public string E0410 { get; set; }

        /// <summary>
        /// De datum waarop de familierechtelijke betrekking is ontstaan. ⦿ Groep: Familierechtelijke betrekking (62) ⦿ Element: Datum ingang familierechtelijke betrekking (62.10)
        /// </summary>
        /// <value>De datum waarop de familierechtelijke betrekking is ontstaan. ⦿ Groep: Familierechtelijke betrekking (62) ⦿ Element: Datum ingang familierechtelijke betrekking (62.10)</value>
        [DataMember(Name = "e6210", EmitDefaultValue = false)]
        public string E6210 { get; set; }

        /// <summary>
        /// Een code, opgenomen in Tabel 33, Gemeententabel, die aangeeft in welke gemeente de akte in de registers van de burgerlijke stand in Nederland is opgenomen. ⦿ Groep: Akte (81) ⦿ Element: Registergemeente akte (81.10)
        /// </summary>
        /// <value>Een code, opgenomen in Tabel 33, Gemeententabel, die aangeeft in welke gemeente de akte in de registers van de burgerlijke stand in Nederland is opgenomen. ⦿ Groep: Akte (81) ⦿ Element: Registergemeente akte (81.10)</value>
        [DataMember(Name = "e8110", EmitDefaultValue = false)]
        public string E8110 { get; set; }

        /// <summary>
        /// Een aanduiding van de akte die is opgenomen in de registers van de burgerlijke stand in Nederland.  De eerste drie posities van het aktenummer dienen conform Tabel 39, Tabel Akteaanduiding te zijn. De laatste 4 posities bevatten een volgnummer van de akte. ⦿ Groep: Akte (81) ⦿ Element: Aktenummer (81.20)
        /// </summary>
        /// <value>Een aanduiding van de akte die is opgenomen in de registers van de burgerlijke stand in Nederland.  De eerste drie posities van het aktenummer dienen conform Tabel 39, Tabel Akteaanduiding te zijn. De laatste 4 posities bevatten een volgnummer van de akte. ⦿ Groep: Akte (81) ⦿ Element: Aktenummer (81.20)</value>
        [DataMember(Name = "e8120", EmitDefaultValue = false)]
        public string E8120 { get; set; }

        /// <summary>
        /// Een code, opgenomen in Tabel 33, Gemeententabel, die aangeeft in welke gemeente de ontlening aan of de afleiding uit het document heeft plaatsgevonden. ⦿ Groep: Document (82) ⦿ Element: Gemeente document (82.10)
        /// </summary>
        /// <value>Een code, opgenomen in Tabel 33, Gemeententabel, die aangeeft in welke gemeente de ontlening aan of de afleiding uit het document heeft plaatsgevonden. ⦿ Groep: Document (82) ⦿ Element: Gemeente document (82.10)</value>
        [DataMember(Name = "e8210", EmitDefaultValue = false)]
        public string E8210 { get; set; }

        /// <summary>
        /// De datum waarop de ontlening aan of de afleiding uit het document heeft plaatsgevonden. ⦿ Groep: Document (82) ⦿ Element: Datum document (82.20)
        /// </summary>
        /// <value>De datum waarop de ontlening aan of de afleiding uit het document heeft plaatsgevonden. ⦿ Groep: Document (82) ⦿ Element: Datum document (82.20)</value>
        [DataMember(Name = "e8220", EmitDefaultValue = false)]
        public string E8220 { get; set; }

        /// <summary>
        /// Beschrijving van het document waaraan de gegevens zijn ontleend of waaruit de gegevens zijn afgeleid. ⦿ Groep: Document (82) ⦿ Element: Beschrijving document (82.30)
        /// </summary>
        /// <value>Beschrijving van het document waaraan de gegevens zijn ontleend of waaruit de gegevens zijn afgeleid. ⦿ Groep: Document (82) ⦿ Element: Beschrijving document (82.30)</value>
        [DataMember(Name = "e8230", EmitDefaultValue = false)]
        public string E8230 { get; set; }

        /// <summary>
        /// Een aanduiding dat in een categorie één of meer gegevens met betrekking tot de onjuistheid of de strijdigheid met de openbare orde zijn of worden onderzocht. In categorie 08 Verblijfplaats kan de aanduiding tevens aangeven dat er is vastgesteld dat betrokkene niet op het geregistreerde adres woont. ⦿ Groep: Procedure (83) ⦿ Element: Aanduiding gegevens in onderzoek (83.10)
        /// </summary>
        /// <value>Een aanduiding dat in een categorie één of meer gegevens met betrekking tot de onjuistheid of de strijdigheid met de openbare orde zijn of worden onderzocht. In categorie 08 Verblijfplaats kan de aanduiding tevens aangeven dat er is vastgesteld dat betrokkene niet op het geregistreerde adres woont. ⦿ Groep: Procedure (83) ⦿ Element: Aanduiding gegevens in onderzoek (83.10)</value>
        [DataMember(Name = "e8310", EmitDefaultValue = false)]
        public string E8310 { get; set; }

        /// <summary>
        /// De datum waarop een onderzoek inzake de onjuistheid of de strijdigheid met de openbare orde is gestart. ⦿ Groep: Procedure (83) ⦿ Element: Datum ingang onderzoek (83.20)
        /// </summary>
        /// <value>De datum waarop een onderzoek inzake de onjuistheid of de strijdigheid met de openbare orde is gestart. ⦿ Groep: Procedure (83) ⦿ Element: Datum ingang onderzoek (83.20)</value>
        [DataMember(Name = "e8320", EmitDefaultValue = false)]
        public string E8320 { get; set; }

        /// <summary>
        /// De datum waarop een onderzoek inzake de onjuistheid of de strijdigheid met de openbare orde is beéindigd. ⦿ Groep: Procedure (83) ⦿ Element: Datum einde onderzoek (83.30)
        /// </summary>
        /// <value>De datum waarop een onderzoek inzake de onjuistheid of de strijdigheid met de openbare orde is beéindigd. ⦿ Groep: Procedure (83) ⦿ Element: Datum einde onderzoek (83.30)</value>
        [DataMember(Name = "e8330", EmitDefaultValue = false)]
        public string E8330 { get; set; }

        /// <summary>
        /// Een aanduiding dat één of meer gegevens onjuist of strijdig zijn met de openbare orde. ⦿ Groep: Onjuist (84) ⦿ Element: Indicatie onjuist, dan wel strijdigheid met de openbare orde (84.10)
        /// </summary>
        /// <value>Een aanduiding dat één of meer gegevens onjuist of strijdig zijn met de openbare orde. ⦿ Groep: Onjuist (84) ⦿ Element: Indicatie onjuist, dan wel strijdigheid met de openbare orde (84.10)</value>
        [DataMember(Name = "e8410", EmitDefaultValue = false)]
        public string E8410 { get; set; }

        /// <summary>
        /// De datum waarop het geheel van gegevens geldig is geworden. ⦿ Groep: Geldigheid (85) ⦿ Element: Ingangsdatum geldigheid (85.10)
        /// </summary>
        /// <value>De datum waarop het geheel van gegevens geldig is geworden. ⦿ Groep: Geldigheid (85) ⦿ Element: Ingangsdatum geldigheid (85.10)</value>
        [DataMember(Name = "e8510", EmitDefaultValue = false)]
        public string E8510 { get; set; }

        /// <summary>
        /// De datum waarop het geheel van gegevens daadwerkelijk in de BRP is opgenomen. ⦿ Groep: Opneming (86) ⦿ Element: Datum van opneming (86.10)
        /// </summary>
        /// <value>De datum waarop het geheel van gegevens daadwerkelijk in de BRP is opgenomen. ⦿ Groep: Opneming (86) ⦿ Element: Datum van opneming (86.10)</value>
        [DataMember(Name = "e8610", EmitDefaultValue = false)]
        public string E8610 { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Af01PlDataC02InnerAllOfHistorieInner {\n");
            sb.Append("  E0110: ").Append(E0110).Append("\n");
            sb.Append("  E0120: ").Append(E0120).Append("\n");
            sb.Append("  E0210: ").Append(E0210).Append("\n");
            sb.Append("  E0220: ").Append(E0220).Append("\n");
            sb.Append("  E0230: ").Append(E0230).Append("\n");
            sb.Append("  E0240: ").Append(E0240).Append("\n");
            sb.Append("  E0310: ").Append(E0310).Append("\n");
            sb.Append("  E0320: ").Append(E0320).Append("\n");
            sb.Append("  E0330: ").Append(E0330).Append("\n");
            sb.Append("  E0410: ").Append(E0410).Append("\n");
            sb.Append("  E6210: ").Append(E6210).Append("\n");
            sb.Append("  E8110: ").Append(E8110).Append("\n");
            sb.Append("  E8120: ").Append(E8120).Append("\n");
            sb.Append("  E8210: ").Append(E8210).Append("\n");
            sb.Append("  E8220: ").Append(E8220).Append("\n");
            sb.Append("  E8230: ").Append(E8230).Append("\n");
            sb.Append("  E8310: ").Append(E8310).Append("\n");
            sb.Append("  E8320: ").Append(E8320).Append("\n");
            sb.Append("  E8330: ").Append(E8330).Append("\n");
            sb.Append("  E8410: ").Append(E8410).Append("\n");
            sb.Append("  E8510: ").Append(E8510).Append("\n");
            sb.Append("  E8610: ").Append(E8610).Append("\n");
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
            if (this.E0110 != null) {
                // E0110 (string) pattern
                Regex regexE0110 = new Regex(@"^(|[0-9]{10})$", RegexOptions.CultureInvariant);
                if (!regexE0110.Match(this.E0110).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E0110, must match a pattern of " + regexE0110, new [] { "E0110" });
                }
            }

            if (this.E0120 != null) {
                // E0120 (string) pattern
                Regex regexE0120 = new Regex(@"^(|[0-9]{9})$", RegexOptions.CultureInvariant);
                if (!regexE0120.Match(this.E0120).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E0120, must match a pattern of " + regexE0120, new [] { "E0120" });
                }
            }

            // E0210 (string) maxLength
            if (this.E0210 != null && this.E0210.Length > 200)
            {
                yield return new ValidationResult("Invalid value for E0210, length must be less than 200.", new [] { "E0210" });
            }

            // E0210 (string) minLength
            if (this.E0210 != null && this.E0210.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E0210, length must be greater than 0.", new [] { "E0210" });
            }

            if (this.E0220 != null) {
                // E0220 (string) pattern
                Regex regexE0220 = new Regex(@"^$|^[a-zA-Z]{1,2}$", RegexOptions.CultureInvariant);
                if (!regexE0220.Match(this.E0220).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E0220, must match a pattern of " + regexE0220, new [] { "E0220" });
                }
            }

            // E0230 (string) maxLength
            if (this.E0230 != null && this.E0230.Length > 10)
            {
                yield return new ValidationResult("Invalid value for E0230, length must be less than 10.", new [] { "E0230" });
            }

            // E0230 (string) minLength
            if (this.E0230 != null && this.E0230.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E0230, length must be greater than 0.", new [] { "E0230" });
            }

            // E0240 (string) maxLength
            if (this.E0240 != null && this.E0240.Length > 200)
            {
                yield return new ValidationResult("Invalid value for E0240, length must be less than 200.", new [] { "E0240" });
            }

            // E0240 (string) minLength
            if (this.E0240 != null && this.E0240.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E0240, length must be greater than 0.", new [] { "E0240" });
            }

            if (this.E0310 != null) {
                // E0310 (string) pattern
                Regex regexE0310 = new Regex(@"^(|[0-9]{8})$", RegexOptions.CultureInvariant);
                if (!regexE0310.Match(this.E0310).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E0310, must match a pattern of " + regexE0310, new [] { "E0310" });
                }
            }

            // E0320 (string) maxLength
            if (this.E0320 != null && this.E0320.Length > 40)
            {
                yield return new ValidationResult("Invalid value for E0320, length must be less than 40.", new [] { "E0320" });
            }

            // E0320 (string) minLength
            if (this.E0320 != null && this.E0320.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E0320, length must be greater than 0.", new [] { "E0320" });
            }

            // E0330 (string) maxLength
            if (this.E0330 != null && this.E0330.Length > 4)
            {
                yield return new ValidationResult("Invalid value for E0330, length must be less than 4.", new [] { "E0330" });
            }

            // E0330 (string) minLength
            if (this.E0330 != null && this.E0330.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E0330, length must be greater than 0.", new [] { "E0330" });
            }

            // E0410 (string) maxLength
            if (this.E0410 != null && this.E0410.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E0410, length must be less than 1.", new [] { "E0410" });
            }

            // E0410 (string) minLength
            if (this.E0410 != null && this.E0410.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E0410, length must be greater than 0.", new [] { "E0410" });
            }

            if (this.E6210 != null) {
                // E6210 (string) pattern
                Regex regexE6210 = new Regex(@"^(|[0-9]{8})$", RegexOptions.CultureInvariant);
                if (!regexE6210.Match(this.E6210).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E6210, must match a pattern of " + regexE6210, new [] { "E6210" });
                }
            }

            // E8110 (string) maxLength
            if (this.E8110 != null && this.E8110.Length > 4)
            {
                yield return new ValidationResult("Invalid value for E8110, length must be less than 4.", new [] { "E8110" });
            }

            // E8110 (string) minLength
            if (this.E8110 != null && this.E8110.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E8110, length must be greater than 0.", new [] { "E8110" });
            }

            // E8120 (string) maxLength
            if (this.E8120 != null && this.E8120.Length > 7)
            {
                yield return new ValidationResult("Invalid value for E8120, length must be less than 7.", new [] { "E8120" });
            }

            // E8120 (string) minLength
            if (this.E8120 != null && this.E8120.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E8120, length must be greater than 0.", new [] { "E8120" });
            }

            // E8210 (string) maxLength
            if (this.E8210 != null && this.E8210.Length > 4)
            {
                yield return new ValidationResult("Invalid value for E8210, length must be less than 4.", new [] { "E8210" });
            }

            // E8210 (string) minLength
            if (this.E8210 != null && this.E8210.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E8210, length must be greater than 0.", new [] { "E8210" });
            }

            if (this.E8220 != null) {
                // E8220 (string) pattern
                Regex regexE8220 = new Regex(@"^(|[0-9]{8})$", RegexOptions.CultureInvariant);
                if (!regexE8220.Match(this.E8220).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E8220, must match a pattern of " + regexE8220, new [] { "E8220" });
                }
            }

            // E8230 (string) maxLength
            if (this.E8230 != null && this.E8230.Length > 40)
            {
                yield return new ValidationResult("Invalid value for E8230, length must be less than 40.", new [] { "E8230" });
            }

            // E8230 (string) minLength
            if (this.E8230 != null && this.E8230.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E8230, length must be greater than 0.", new [] { "E8230" });
            }

            // E8310 (string) maxLength
            if (this.E8310 != null && this.E8310.Length > 6)
            {
                yield return new ValidationResult("Invalid value for E8310, length must be less than 6.", new [] { "E8310" });
            }

            // E8310 (string) minLength
            if (this.E8310 != null && this.E8310.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E8310, length must be greater than 0.", new [] { "E8310" });
            }

            if (this.E8320 != null) {
                // E8320 (string) pattern
                Regex regexE8320 = new Regex(@"^(|[0-9]{8})$", RegexOptions.CultureInvariant);
                if (!regexE8320.Match(this.E8320).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E8320, must match a pattern of " + regexE8320, new [] { "E8320" });
                }
            }

            if (this.E8330 != null) {
                // E8330 (string) pattern
                Regex regexE8330 = new Regex(@"^(|[0-9]{8})$", RegexOptions.CultureInvariant);
                if (!regexE8330.Match(this.E8330).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E8330, must match a pattern of " + regexE8330, new [] { "E8330" });
                }
            }

            // E8410 (string) maxLength
            if (this.E8410 != null && this.E8410.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E8410, length must be less than 1.", new [] { "E8410" });
            }

            // E8410 (string) minLength
            if (this.E8410 != null && this.E8410.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E8410, length must be greater than 0.", new [] { "E8410" });
            }

            if (this.E8510 != null) {
                // E8510 (string) pattern
                Regex regexE8510 = new Regex(@"^(|[0-9]{8})$", RegexOptions.CultureInvariant);
                if (!regexE8510.Match(this.E8510).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E8510, must match a pattern of " + regexE8510, new [] { "E8510" });
                }
            }

            if (this.E8610 != null) {
                // E8610 (string) pattern
                Regex regexE8610 = new Regex(@"^(|[0-9]{8})$", RegexOptions.CultureInvariant);
                if (!regexE8610.Match(this.E8610).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E8610, must match a pattern of " + regexE8610, new [] { "E8610" });
                }
            }

            yield break;
        }
    }

}
