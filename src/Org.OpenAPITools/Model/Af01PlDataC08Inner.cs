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
    /// Af01PlDataC08Inner
    /// </summary>
    [DataContract(Name = "Af01_plData_c08_inner")]
    public partial class Af01PlDataC08Inner : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Af01PlDataC08Inner" /> class.
        /// </summary>
        /// <param name="e0910">Een code, opgenomen in Tabel 33, Gemeententabel, die aangeeft in welke gemeente de PL zich bevindt of de gemeente waarnaar de PL is uitgeschreven of de gemeente waar de PL voor de eerste keer is opgenomen. In categorie 16 betreft het de gemeente waar zich het tijdelijk verblijfsadres bevindt. ⦿ Groep: Gemeente (09) ⦿ Element: Gemeente van inschrijving (09.10).</param>
        /// <param name="e0920">Bij een tijdige aangifte (tussen vier weken voor en vijf dagen na de verhuizing) van vestiging in de gemeente is dit de in de aangifte vermelde datum van adreswijziging. Bij een niet tijdige aangifte is dit de aangiftedatum. Bij inschrijving op grond van een geboorteakte is dit de geboortedatum. Bij ambtshalve inschrijving is dit de datum waarop de betrokkene schriftelijk van het voornemen van ambtshalve opneming mededeling is gedaan. In categorie 16 betreft het de datum waarop betrokkene voor het eerst op het tijdelijk verblijfsadres in deze gemeente is gaan wonen, mits deze adressen in de tijd aaneengesloten zijn. ⦿ Groep: Gemeente (09) ⦿ Element: Datum inschrijving (09.20).</param>
        /// <param name="e1010">De aanduiding die aangeeft of het adres de functie heeft van woonadres of briefadres. ⦿ Groep: Adreshouding (10) ⦿ Element: Functie adres (10.10).</param>
        /// <param name="e1020">Een geografisch bepaald gebied dat een deel is van het gemeentelijk grondgebied.  Dit element wordt gebruikt als nadere plaatsbepaling van een straat of locatie, indien deze binnen de gemeente niet uniek is. ⦿ Groep: Adreshouding (10) ⦿ Element: Gemeentedeel (10.20).</param>
        /// <param name="e1030">De datum van aangifte of ambtshalve melding van verblijf en adres. Bij een tijdige aangifte (tussen vier weken voor en vijf dagen na de verhuizing) van vestiging op het adres is dit de in de aangifte vermelde datum van adreswijziging. Bij een niet tijdige aangifte is dit de aangiftedatum. Bij inschrijving op grond van een geboorteakte is dit de geboortedatum. Bij ambtshalve inschrijving is dit de datum waarop de betrokkene schriftelijk van het voornemen van ambtshalve opneming mededeling is gedaan. ⦿ Groep: Adreshouding (10) ⦿ Element: Datum aanvang adreshouding (10.30).</param>
        /// <param name="e1110">De officiële straatnaam zoals door het gemeentebestuur is vastgesteld dan wel een kopie van de inhoud van element 11.15 Naam openbare ruimte,  indien noodzakelijk afgekort volgens de NEN-5825 [2002] norm. ⦿ Groep: Adres (11) ⦿ Element: Straatnaam (11.10).</param>
        /// <param name="e1115">Een naam die aan een openbare ruimte is toegekend in een daartoe strekkend formeel gemeentelijk besluit. Een openbare ruimte is een door het bevoegde gemeentelijke orgaan als zodanig aangewezen benaming van een binnen één woonplaats gelegen buitenruimte. Voor &#39;Naam openbare ruimte&#39; mag &#39;officiële straatnaam&#39; gelezen worden. ⦿ Groep: Adres (11) ⦿ Element: Naam openbare ruimte (11.15).</param>
        /// <param name="e1120">De numerieke aanduiding zoals deze door het gemeentebestuur aan het object is toegekend dan wel een door of namens het bevoegde gemeentelijke orgaan ten aanzien van een adresseerbaar object toegekende nummering. ⦿ Groep: Adres (11) ⦿ Element: Huisnummer (11.20).</param>
        /// <param name="e1130">Een alfabetisch teken achter het huisnummer zoals dit door het gemeentebestuur is toegekend dan wel een door of namens het bevoegde gemeentelijke orgaan ten aanzien van een adresseerbaar object toegekende toevoeging aan een huisnummer in de vorm van een alfabetisch teken. ⦿ Groep: Adres (11) ⦿ Element: Huisletter (11.30).</param>
        /// <param name="e1140">Die letters of tekens die noodzakelijk zijn om, naast huisnummer en -letter, de brievenbus te vinden dan wel een door of namens het bevoegde gemeentelijke orgaan ten aanzien van een adresseerbaar object toegekende toevoeging aan een huisnummer of een combinatie van huisletter en huisnummer. ⦿ Groep: Adres (11) ⦿ Element: Huisnummertoevoeging (11.40).</param>
        /// <param name="e1150">De aanduiding die wordt gebruikt voor adressen die niet zijn voorzien van de gebruikelijke straatnaam en huisnummeraanduidingen. ⦿ Groep: Adres (11) ⦿ Element: Aanduiding bij huisnummer (11.50).</param>
        /// <param name="e1160">De door de PostNL vastgestelde code behorend bij de straatnaam en het huisnummer dan wel de door PostNL vastgestelde code behorende bij een bepaalde combinatie van een naam openbare ruimte en een huisnummer. ⦿ Groep: Adres (11) ⦿ Element: Postcode (11.60).</param>
        /// <param name="e1170">Een woonplaatsnaam is de naam van een door het bevoegde gemeentelijke orgaan als zodanig aangewezen gedeelte van het gemeentelijk grondgebied. ⦿ Groep: Adres (11) ⦿ Element: Woonplaatsnaam (11.70).</param>
        /// <param name="e1180">Een verblijfplaats kan een ligplaats, een standplaats of een verblijfsobject in een of meerdere panden zijn, waaraan respectievelijk een ligplaatsidentificatie, standplaatsidentificatie of verblijfsobjectidentificatie is toegekend.  De Identificatiecode verblijfplaats is een combinatie van een viercijferige gemeentecode, een tweecijferige objecttypecode die aangeeft of de aanduiding een verblijfsobject (01), ligplaats (02) of standplaats (03) betreft en een voor het betreffende objecttype binnen een gemeente uniek tiencijferig volgnummer. ⦿ Groep: Adres (11) ⦿ Element: Identificatiecode verblijfplaats (11.80).</param>
        /// <param name="e1190">De unieke aanduiding van een nummeraanduiding. Een nummeraanduiding is een door het bevoegde gemeentelijke orgaan als zodanig toegekende aanduiding van een adresseerbaar object.  De Identificatiecode nummeraanduiding is een combinatie van een viercijferige gemeentecode, de tweecijferige objecttypecode 20 die aangeeft dat het om een nummeraanduiding gaat en een voor het betreffende objecttype binnen een gemeente uniek tiencijferig volgnummer. ⦿ Groep: Adres (11) ⦿ Element: Identificatiecode nummeraanduiding (11.90).</param>
        /// <param name="e1210">Een geheel of gedeeltelijke omschrijving van de ligging van een object, indien dit niet kan worden aangegeven in de groep 11 Adres. ⦿ Groep: Locatie (12) ⦿ Element: Locatiebeschrijving (12.10).</param>
        /// <param name="e1310">Een code, opgenomen in Tabel 34, Landentabel, die het land (buiten Nederland) aangeeft alwaar de ingeschrevene verblijft. In tegenstelling tot bij de gemeente, kan bij de RNI kan de waarde Nederland (6030) wel voorkomen.  Bij het ingaan van een Ministerieel besluit dient hier de standaardwaarde opgenomen te worden. ⦿ Groep: Adres buitenland (13) ⦿ Element: Land adres buitenland (13.10).</param>
        /// <param name="e1320">De datum van aangifte of ambtshalve melding van verblijf op het buitenlands adres.  Bij emigratie is dit de datum van vertrek naar het buitenland. Bij ambtshalve uitschrijving is dit de datum waarop de betrokkene schriftelijk van het voornemen tot ambtshalve uitschrijving mededeling is gedaan. Bij uitschrijving wegens het ingaan van een Ministerieel besluit is dit de datum van dat besluit. In alle andere gevallen is dit de datum waarop de aangifte is ontvangen. ⦿ Groep: Adres buitenland (13) ⦿ Element: Datum aanvang adres buitenland (13.20).</param>
        /// <param name="e1330">Eerste deel van het adres in het buitenland, met uitzondering van het land. ⦿ Groep: Adres buitenland (13) ⦿ Element: Regel 1 adres buitenland (13.30).</param>
        /// <param name="e1340">Tweede deel van het adres in het buitenland, met uitzondering van het land. ⦿ Groep: Adres buitenland (13) ⦿ Element: Regel 2 adres buitenland (13.40).</param>
        /// <param name="e1350">Derde deel van het adres in het buitenland, met uitzondering van het land. ⦿ Groep: Adres buitenland (13) ⦿ Element: Regel 3 adres buitenland (13.50).</param>
        /// <param name="e1410">Een code, opgenomen in Tabel 34, Landentabel, die het land aangeeft waar de ingeschrevene verblijf hield voor (her)vestiging in Nederland. Bij het opheffen van een Ministerieel besluit dient hier de standaardwaarde opgenomen te worden. ⦿ Groep: Immigratie (14) ⦿ Element: Land vanwaar ingeschreven (14.10).</param>
        /// <param name="e1420">De datum van inschrijving in Nederland.  Bij ambtshalve inschrijving is dit de datum waarop de betrokkene schriftelijk van het voornemen tot ambtshalve inschrijving mededeling is gedaan. In alle andere gevallen is dit de datum waarop de aangifte is ontvangen. ⦿ Groep: Immigratie (14) ⦿ Element: Datum vestiging in Nederland (14.20).</param>
        /// <param name="e7210">Een aanduiding van de persoon door wie de aangifte van verblijf en adres is gedaan. ⦿ Groep: Adresaangifte (72) ⦿ Element: Omschrijving van de aangifte adreshouding (72.10).</param>
        /// <param name="e7510">Een aanduiding dat gedurende de opschorting van de bijhouding van de PL documenten zijn binnengekomen, die na de beëindiging van de opschorting verwerkt moeten worden. ⦿ Groep: Documentindicatie (75) ⦿ Element: Indicatie document (75.10).</param>
        /// <param name="e8310">Een aanduiding dat in een categorie één of meer gegevens met betrekking tot de onjuistheid of de strijdigheid met de openbare orde zijn of worden onderzocht. In categorie 08 Verblijfplaats kan de aanduiding tevens aangeven dat er is vastgesteld dat betrokkene niet op het geregistreerde adres woont. ⦿ Groep: Procedure (83) ⦿ Element: Aanduiding gegevens in onderzoek (83.10).</param>
        /// <param name="e8320">De datum waarop een onderzoek inzake de onjuistheid of de strijdigheid met de openbare orde is gestart. ⦿ Groep: Procedure (83) ⦿ Element: Datum ingang onderzoek (83.20).</param>
        /// <param name="e8330">De datum waarop een onderzoek inzake de onjuistheid of de strijdigheid met de openbare orde is beéindigd. ⦿ Groep: Procedure (83) ⦿ Element: Datum einde onderzoek (83.30).</param>
        /// <param name="e8410">Een aanduiding dat één of meer gegevens onjuist of strijdig zijn met de openbare orde. ⦿ Groep: Onjuist (84) ⦿ Element: Indicatie onjuist, dan wel strijdigheid met de openbare orde (84.10).</param>
        /// <param name="e8510">De datum waarop het geheel van gegevens geldig is geworden. ⦿ Groep: Geldigheid (85) ⦿ Element: Ingangsdatum geldigheid (85.10).</param>
        /// <param name="e8610">De datum waarop het geheel van gegevens daadwerkelijk in de BRP is opgenomen. ⦿ Groep: Opneming (86) ⦿ Element: Datum van opneming (86.10).</param>
        /// <param name="e8810">Een code, voorkomend in Tabel 60, RNI-deelnemerstabel, die aangeeft welke RNI-deelnemer (een deel van) de gegevens in de betrokken categorie heeft aangeleverd. ⦿ Groep: RNI-deelnemer (88) ⦿ Element: RNI-deelnemer (88.10).</param>
        /// <param name="e8820">Een aanduiding van het verdrag op basis waarvan (een deel van) de gegevens in de betrokken categorie door een buitenlandse zusterorganisatie van een RNI-deelnemer aan die deelnemer zijn aangeleverd. ⦿ Groep: RNI-deelnemer (88) ⦿ Element: Omschrijving verdrag (88.20).</param>
        /// <param name="historie">historie.</param>
        public Af01PlDataC08Inner(string e0910 = default, string e0920 = default, string e1010 = default, string e1020 = default, string e1030 = default, string e1110 = default, string e1115 = default, string e1120 = default, string e1130 = default, string e1140 = default, string e1150 = default, string e1160 = default, string e1170 = default, string e1180 = default, string e1190 = default, string e1210 = default, string e1310 = default, string e1320 = default, string e1330 = default, string e1340 = default, string e1350 = default, string e1410 = default, string e1420 = default, string e7210 = default, string e7510 = default, string e8310 = default, string e8320 = default, string e8330 = default, string e8410 = default, string e8510 = default, string e8610 = default, string e8810 = default, string e8820 = default, List<Af01PlDataC08InnerAllOfHistorieInner> historie = default)
        {
            this.E0910 = e0910;
            this.E0920 = e0920;
            this.E1010 = e1010;
            this.E1020 = e1020;
            this.E1030 = e1030;
            this.E1110 = e1110;
            this.E1115 = e1115;
            this.E1120 = e1120;
            this.E1130 = e1130;
            this.E1140 = e1140;
            this.E1150 = e1150;
            this.E1160 = e1160;
            this.E1170 = e1170;
            this.E1180 = e1180;
            this.E1190 = e1190;
            this.E1210 = e1210;
            this.E1310 = e1310;
            this.E1320 = e1320;
            this.E1330 = e1330;
            this.E1340 = e1340;
            this.E1350 = e1350;
            this.E1410 = e1410;
            this.E1420 = e1420;
            this.E7210 = e7210;
            this.E7510 = e7510;
            this.E8310 = e8310;
            this.E8320 = e8320;
            this.E8330 = e8330;
            this.E8410 = e8410;
            this.E8510 = e8510;
            this.E8610 = e8610;
            this.E8810 = e8810;
            this.E8820 = e8820;
            this.Historie = historie;
        }

        /// <summary>
        /// Een code, opgenomen in Tabel 33, Gemeententabel, die aangeeft in welke gemeente de PL zich bevindt of de gemeente waarnaar de PL is uitgeschreven of de gemeente waar de PL voor de eerste keer is opgenomen. In categorie 16 betreft het de gemeente waar zich het tijdelijk verblijfsadres bevindt. ⦿ Groep: Gemeente (09) ⦿ Element: Gemeente van inschrijving (09.10)
        /// </summary>
        /// <value>Een code, opgenomen in Tabel 33, Gemeententabel, die aangeeft in welke gemeente de PL zich bevindt of de gemeente waarnaar de PL is uitgeschreven of de gemeente waar de PL voor de eerste keer is opgenomen. In categorie 16 betreft het de gemeente waar zich het tijdelijk verblijfsadres bevindt. ⦿ Groep: Gemeente (09) ⦿ Element: Gemeente van inschrijving (09.10)</value>
        [DataMember(Name = "e0910", EmitDefaultValue = false)]
        public string E0910 { get; set; }

        /// <summary>
        /// Bij een tijdige aangifte (tussen vier weken voor en vijf dagen na de verhuizing) van vestiging in de gemeente is dit de in de aangifte vermelde datum van adreswijziging. Bij een niet tijdige aangifte is dit de aangiftedatum. Bij inschrijving op grond van een geboorteakte is dit de geboortedatum. Bij ambtshalve inschrijving is dit de datum waarop de betrokkene schriftelijk van het voornemen van ambtshalve opneming mededeling is gedaan. In categorie 16 betreft het de datum waarop betrokkene voor het eerst op het tijdelijk verblijfsadres in deze gemeente is gaan wonen, mits deze adressen in de tijd aaneengesloten zijn. ⦿ Groep: Gemeente (09) ⦿ Element: Datum inschrijving (09.20)
        /// </summary>
        /// <value>Bij een tijdige aangifte (tussen vier weken voor en vijf dagen na de verhuizing) van vestiging in de gemeente is dit de in de aangifte vermelde datum van adreswijziging. Bij een niet tijdige aangifte is dit de aangiftedatum. Bij inschrijving op grond van een geboorteakte is dit de geboortedatum. Bij ambtshalve inschrijving is dit de datum waarop de betrokkene schriftelijk van het voornemen van ambtshalve opneming mededeling is gedaan. In categorie 16 betreft het de datum waarop betrokkene voor het eerst op het tijdelijk verblijfsadres in deze gemeente is gaan wonen, mits deze adressen in de tijd aaneengesloten zijn. ⦿ Groep: Gemeente (09) ⦿ Element: Datum inschrijving (09.20)</value>
        [DataMember(Name = "e0920", EmitDefaultValue = false)]
        public string E0920 { get; set; }

        /// <summary>
        /// De aanduiding die aangeeft of het adres de functie heeft van woonadres of briefadres. ⦿ Groep: Adreshouding (10) ⦿ Element: Functie adres (10.10)
        /// </summary>
        /// <value>De aanduiding die aangeeft of het adres de functie heeft van woonadres of briefadres. ⦿ Groep: Adreshouding (10) ⦿ Element: Functie adres (10.10)</value>
        [DataMember(Name = "e1010", EmitDefaultValue = false)]
        public string E1010 { get; set; }

        /// <summary>
        /// Een geografisch bepaald gebied dat een deel is van het gemeentelijk grondgebied.  Dit element wordt gebruikt als nadere plaatsbepaling van een straat of locatie, indien deze binnen de gemeente niet uniek is. ⦿ Groep: Adreshouding (10) ⦿ Element: Gemeentedeel (10.20)
        /// </summary>
        /// <value>Een geografisch bepaald gebied dat een deel is van het gemeentelijk grondgebied.  Dit element wordt gebruikt als nadere plaatsbepaling van een straat of locatie, indien deze binnen de gemeente niet uniek is. ⦿ Groep: Adreshouding (10) ⦿ Element: Gemeentedeel (10.20)</value>
        [DataMember(Name = "e1020", EmitDefaultValue = false)]
        public string E1020 { get; set; }

        /// <summary>
        /// De datum van aangifte of ambtshalve melding van verblijf en adres. Bij een tijdige aangifte (tussen vier weken voor en vijf dagen na de verhuizing) van vestiging op het adres is dit de in de aangifte vermelde datum van adreswijziging. Bij een niet tijdige aangifte is dit de aangiftedatum. Bij inschrijving op grond van een geboorteakte is dit de geboortedatum. Bij ambtshalve inschrijving is dit de datum waarop de betrokkene schriftelijk van het voornemen van ambtshalve opneming mededeling is gedaan. ⦿ Groep: Adreshouding (10) ⦿ Element: Datum aanvang adreshouding (10.30)
        /// </summary>
        /// <value>De datum van aangifte of ambtshalve melding van verblijf en adres. Bij een tijdige aangifte (tussen vier weken voor en vijf dagen na de verhuizing) van vestiging op het adres is dit de in de aangifte vermelde datum van adreswijziging. Bij een niet tijdige aangifte is dit de aangiftedatum. Bij inschrijving op grond van een geboorteakte is dit de geboortedatum. Bij ambtshalve inschrijving is dit de datum waarop de betrokkene schriftelijk van het voornemen van ambtshalve opneming mededeling is gedaan. ⦿ Groep: Adreshouding (10) ⦿ Element: Datum aanvang adreshouding (10.30)</value>
        [DataMember(Name = "e1030", EmitDefaultValue = false)]
        public string E1030 { get; set; }

        /// <summary>
        /// De officiële straatnaam zoals door het gemeentebestuur is vastgesteld dan wel een kopie van de inhoud van element 11.15 Naam openbare ruimte,  indien noodzakelijk afgekort volgens de NEN-5825 [2002] norm. ⦿ Groep: Adres (11) ⦿ Element: Straatnaam (11.10)
        /// </summary>
        /// <value>De officiële straatnaam zoals door het gemeentebestuur is vastgesteld dan wel een kopie van de inhoud van element 11.15 Naam openbare ruimte,  indien noodzakelijk afgekort volgens de NEN-5825 [2002] norm. ⦿ Groep: Adres (11) ⦿ Element: Straatnaam (11.10)</value>
        [DataMember(Name = "e1110", EmitDefaultValue = false)]
        public string E1110 { get; set; }

        /// <summary>
        /// Een naam die aan een openbare ruimte is toegekend in een daartoe strekkend formeel gemeentelijk besluit. Een openbare ruimte is een door het bevoegde gemeentelijke orgaan als zodanig aangewezen benaming van een binnen één woonplaats gelegen buitenruimte. Voor &#39;Naam openbare ruimte&#39; mag &#39;officiële straatnaam&#39; gelezen worden. ⦿ Groep: Adres (11) ⦿ Element: Naam openbare ruimte (11.15)
        /// </summary>
        /// <value>Een naam die aan een openbare ruimte is toegekend in een daartoe strekkend formeel gemeentelijk besluit. Een openbare ruimte is een door het bevoegde gemeentelijke orgaan als zodanig aangewezen benaming van een binnen één woonplaats gelegen buitenruimte. Voor &#39;Naam openbare ruimte&#39; mag &#39;officiële straatnaam&#39; gelezen worden. ⦿ Groep: Adres (11) ⦿ Element: Naam openbare ruimte (11.15)</value>
        [DataMember(Name = "e1115", EmitDefaultValue = false)]
        public string E1115 { get; set; }

        /// <summary>
        /// De numerieke aanduiding zoals deze door het gemeentebestuur aan het object is toegekend dan wel een door of namens het bevoegde gemeentelijke orgaan ten aanzien van een adresseerbaar object toegekende nummering. ⦿ Groep: Adres (11) ⦿ Element: Huisnummer (11.20)
        /// </summary>
        /// <value>De numerieke aanduiding zoals deze door het gemeentebestuur aan het object is toegekend dan wel een door of namens het bevoegde gemeentelijke orgaan ten aanzien van een adresseerbaar object toegekende nummering. ⦿ Groep: Adres (11) ⦿ Element: Huisnummer (11.20)</value>
        [DataMember(Name = "e1120", EmitDefaultValue = false)]
        public string E1120 { get; set; }

        /// <summary>
        /// Een alfabetisch teken achter het huisnummer zoals dit door het gemeentebestuur is toegekend dan wel een door of namens het bevoegde gemeentelijke orgaan ten aanzien van een adresseerbaar object toegekende toevoeging aan een huisnummer in de vorm van een alfabetisch teken. ⦿ Groep: Adres (11) ⦿ Element: Huisletter (11.30)
        /// </summary>
        /// <value>Een alfabetisch teken achter het huisnummer zoals dit door het gemeentebestuur is toegekend dan wel een door of namens het bevoegde gemeentelijke orgaan ten aanzien van een adresseerbaar object toegekende toevoeging aan een huisnummer in de vorm van een alfabetisch teken. ⦿ Groep: Adres (11) ⦿ Element: Huisletter (11.30)</value>
        [DataMember(Name = "e1130", EmitDefaultValue = false)]
        public string E1130 { get; set; }

        /// <summary>
        /// Die letters of tekens die noodzakelijk zijn om, naast huisnummer en -letter, de brievenbus te vinden dan wel een door of namens het bevoegde gemeentelijke orgaan ten aanzien van een adresseerbaar object toegekende toevoeging aan een huisnummer of een combinatie van huisletter en huisnummer. ⦿ Groep: Adres (11) ⦿ Element: Huisnummertoevoeging (11.40)
        /// </summary>
        /// <value>Die letters of tekens die noodzakelijk zijn om, naast huisnummer en -letter, de brievenbus te vinden dan wel een door of namens het bevoegde gemeentelijke orgaan ten aanzien van een adresseerbaar object toegekende toevoeging aan een huisnummer of een combinatie van huisletter en huisnummer. ⦿ Groep: Adres (11) ⦿ Element: Huisnummertoevoeging (11.40)</value>
        [DataMember(Name = "e1140", EmitDefaultValue = false)]
        public string E1140 { get; set; }

        /// <summary>
        /// De aanduiding die wordt gebruikt voor adressen die niet zijn voorzien van de gebruikelijke straatnaam en huisnummeraanduidingen. ⦿ Groep: Adres (11) ⦿ Element: Aanduiding bij huisnummer (11.50)
        /// </summary>
        /// <value>De aanduiding die wordt gebruikt voor adressen die niet zijn voorzien van de gebruikelijke straatnaam en huisnummeraanduidingen. ⦿ Groep: Adres (11) ⦿ Element: Aanduiding bij huisnummer (11.50)</value>
        [DataMember(Name = "e1150", EmitDefaultValue = false)]
        public string E1150 { get; set; }

        /// <summary>
        /// De door de PostNL vastgestelde code behorend bij de straatnaam en het huisnummer dan wel de door PostNL vastgestelde code behorende bij een bepaalde combinatie van een naam openbare ruimte en een huisnummer. ⦿ Groep: Adres (11) ⦿ Element: Postcode (11.60)
        /// </summary>
        /// <value>De door de PostNL vastgestelde code behorend bij de straatnaam en het huisnummer dan wel de door PostNL vastgestelde code behorende bij een bepaalde combinatie van een naam openbare ruimte en een huisnummer. ⦿ Groep: Adres (11) ⦿ Element: Postcode (11.60)</value>
        [DataMember(Name = "e1160", EmitDefaultValue = false)]
        public string E1160 { get; set; }

        /// <summary>
        /// Een woonplaatsnaam is de naam van een door het bevoegde gemeentelijke orgaan als zodanig aangewezen gedeelte van het gemeentelijk grondgebied. ⦿ Groep: Adres (11) ⦿ Element: Woonplaatsnaam (11.70)
        /// </summary>
        /// <value>Een woonplaatsnaam is de naam van een door het bevoegde gemeentelijke orgaan als zodanig aangewezen gedeelte van het gemeentelijk grondgebied. ⦿ Groep: Adres (11) ⦿ Element: Woonplaatsnaam (11.70)</value>
        [DataMember(Name = "e1170", EmitDefaultValue = false)]
        public string E1170 { get; set; }

        /// <summary>
        /// Een verblijfplaats kan een ligplaats, een standplaats of een verblijfsobject in een of meerdere panden zijn, waaraan respectievelijk een ligplaatsidentificatie, standplaatsidentificatie of verblijfsobjectidentificatie is toegekend.  De Identificatiecode verblijfplaats is een combinatie van een viercijferige gemeentecode, een tweecijferige objecttypecode die aangeeft of de aanduiding een verblijfsobject (01), ligplaats (02) of standplaats (03) betreft en een voor het betreffende objecttype binnen een gemeente uniek tiencijferig volgnummer. ⦿ Groep: Adres (11) ⦿ Element: Identificatiecode verblijfplaats (11.80)
        /// </summary>
        /// <value>Een verblijfplaats kan een ligplaats, een standplaats of een verblijfsobject in een of meerdere panden zijn, waaraan respectievelijk een ligplaatsidentificatie, standplaatsidentificatie of verblijfsobjectidentificatie is toegekend.  De Identificatiecode verblijfplaats is een combinatie van een viercijferige gemeentecode, een tweecijferige objecttypecode die aangeeft of de aanduiding een verblijfsobject (01), ligplaats (02) of standplaats (03) betreft en een voor het betreffende objecttype binnen een gemeente uniek tiencijferig volgnummer. ⦿ Groep: Adres (11) ⦿ Element: Identificatiecode verblijfplaats (11.80)</value>
        [DataMember(Name = "e1180", EmitDefaultValue = false)]
        public string E1180 { get; set; }

        /// <summary>
        /// De unieke aanduiding van een nummeraanduiding. Een nummeraanduiding is een door het bevoegde gemeentelijke orgaan als zodanig toegekende aanduiding van een adresseerbaar object.  De Identificatiecode nummeraanduiding is een combinatie van een viercijferige gemeentecode, de tweecijferige objecttypecode 20 die aangeeft dat het om een nummeraanduiding gaat en een voor het betreffende objecttype binnen een gemeente uniek tiencijferig volgnummer. ⦿ Groep: Adres (11) ⦿ Element: Identificatiecode nummeraanduiding (11.90)
        /// </summary>
        /// <value>De unieke aanduiding van een nummeraanduiding. Een nummeraanduiding is een door het bevoegde gemeentelijke orgaan als zodanig toegekende aanduiding van een adresseerbaar object.  De Identificatiecode nummeraanduiding is een combinatie van een viercijferige gemeentecode, de tweecijferige objecttypecode 20 die aangeeft dat het om een nummeraanduiding gaat en een voor het betreffende objecttype binnen een gemeente uniek tiencijferig volgnummer. ⦿ Groep: Adres (11) ⦿ Element: Identificatiecode nummeraanduiding (11.90)</value>
        [DataMember(Name = "e1190", EmitDefaultValue = false)]
        public string E1190 { get; set; }

        /// <summary>
        /// Een geheel of gedeeltelijke omschrijving van de ligging van een object, indien dit niet kan worden aangegeven in de groep 11 Adres. ⦿ Groep: Locatie (12) ⦿ Element: Locatiebeschrijving (12.10)
        /// </summary>
        /// <value>Een geheel of gedeeltelijke omschrijving van de ligging van een object, indien dit niet kan worden aangegeven in de groep 11 Adres. ⦿ Groep: Locatie (12) ⦿ Element: Locatiebeschrijving (12.10)</value>
        [DataMember(Name = "e1210", EmitDefaultValue = false)]
        public string E1210 { get; set; }

        /// <summary>
        /// Een code, opgenomen in Tabel 34, Landentabel, die het land (buiten Nederland) aangeeft alwaar de ingeschrevene verblijft. In tegenstelling tot bij de gemeente, kan bij de RNI kan de waarde Nederland (6030) wel voorkomen.  Bij het ingaan van een Ministerieel besluit dient hier de standaardwaarde opgenomen te worden. ⦿ Groep: Adres buitenland (13) ⦿ Element: Land adres buitenland (13.10)
        /// </summary>
        /// <value>Een code, opgenomen in Tabel 34, Landentabel, die het land (buiten Nederland) aangeeft alwaar de ingeschrevene verblijft. In tegenstelling tot bij de gemeente, kan bij de RNI kan de waarde Nederland (6030) wel voorkomen.  Bij het ingaan van een Ministerieel besluit dient hier de standaardwaarde opgenomen te worden. ⦿ Groep: Adres buitenland (13) ⦿ Element: Land adres buitenland (13.10)</value>
        [DataMember(Name = "e1310", EmitDefaultValue = false)]
        public string E1310 { get; set; }

        /// <summary>
        /// De datum van aangifte of ambtshalve melding van verblijf op het buitenlands adres.  Bij emigratie is dit de datum van vertrek naar het buitenland. Bij ambtshalve uitschrijving is dit de datum waarop de betrokkene schriftelijk van het voornemen tot ambtshalve uitschrijving mededeling is gedaan. Bij uitschrijving wegens het ingaan van een Ministerieel besluit is dit de datum van dat besluit. In alle andere gevallen is dit de datum waarop de aangifte is ontvangen. ⦿ Groep: Adres buitenland (13) ⦿ Element: Datum aanvang adres buitenland (13.20)
        /// </summary>
        /// <value>De datum van aangifte of ambtshalve melding van verblijf op het buitenlands adres.  Bij emigratie is dit de datum van vertrek naar het buitenland. Bij ambtshalve uitschrijving is dit de datum waarop de betrokkene schriftelijk van het voornemen tot ambtshalve uitschrijving mededeling is gedaan. Bij uitschrijving wegens het ingaan van een Ministerieel besluit is dit de datum van dat besluit. In alle andere gevallen is dit de datum waarop de aangifte is ontvangen. ⦿ Groep: Adres buitenland (13) ⦿ Element: Datum aanvang adres buitenland (13.20)</value>
        [DataMember(Name = "e1320", EmitDefaultValue = false)]
        public string E1320 { get; set; }

        /// <summary>
        /// Eerste deel van het adres in het buitenland, met uitzondering van het land. ⦿ Groep: Adres buitenland (13) ⦿ Element: Regel 1 adres buitenland (13.30)
        /// </summary>
        /// <value>Eerste deel van het adres in het buitenland, met uitzondering van het land. ⦿ Groep: Adres buitenland (13) ⦿ Element: Regel 1 adres buitenland (13.30)</value>
        [DataMember(Name = "e1330", EmitDefaultValue = false)]
        public string E1330 { get; set; }

        /// <summary>
        /// Tweede deel van het adres in het buitenland, met uitzondering van het land. ⦿ Groep: Adres buitenland (13) ⦿ Element: Regel 2 adres buitenland (13.40)
        /// </summary>
        /// <value>Tweede deel van het adres in het buitenland, met uitzondering van het land. ⦿ Groep: Adres buitenland (13) ⦿ Element: Regel 2 adres buitenland (13.40)</value>
        [DataMember(Name = "e1340", EmitDefaultValue = false)]
        public string E1340 { get; set; }

        /// <summary>
        /// Derde deel van het adres in het buitenland, met uitzondering van het land. ⦿ Groep: Adres buitenland (13) ⦿ Element: Regel 3 adres buitenland (13.50)
        /// </summary>
        /// <value>Derde deel van het adres in het buitenland, met uitzondering van het land. ⦿ Groep: Adres buitenland (13) ⦿ Element: Regel 3 adres buitenland (13.50)</value>
        [DataMember(Name = "e1350", EmitDefaultValue = false)]
        public string E1350 { get; set; }

        /// <summary>
        /// Een code, opgenomen in Tabel 34, Landentabel, die het land aangeeft waar de ingeschrevene verblijf hield voor (her)vestiging in Nederland. Bij het opheffen van een Ministerieel besluit dient hier de standaardwaarde opgenomen te worden. ⦿ Groep: Immigratie (14) ⦿ Element: Land vanwaar ingeschreven (14.10)
        /// </summary>
        /// <value>Een code, opgenomen in Tabel 34, Landentabel, die het land aangeeft waar de ingeschrevene verblijf hield voor (her)vestiging in Nederland. Bij het opheffen van een Ministerieel besluit dient hier de standaardwaarde opgenomen te worden. ⦿ Groep: Immigratie (14) ⦿ Element: Land vanwaar ingeschreven (14.10)</value>
        [DataMember(Name = "e1410", EmitDefaultValue = false)]
        public string E1410 { get; set; }

        /// <summary>
        /// De datum van inschrijving in Nederland.  Bij ambtshalve inschrijving is dit de datum waarop de betrokkene schriftelijk van het voornemen tot ambtshalve inschrijving mededeling is gedaan. In alle andere gevallen is dit de datum waarop de aangifte is ontvangen. ⦿ Groep: Immigratie (14) ⦿ Element: Datum vestiging in Nederland (14.20)
        /// </summary>
        /// <value>De datum van inschrijving in Nederland.  Bij ambtshalve inschrijving is dit de datum waarop de betrokkene schriftelijk van het voornemen tot ambtshalve inschrijving mededeling is gedaan. In alle andere gevallen is dit de datum waarop de aangifte is ontvangen. ⦿ Groep: Immigratie (14) ⦿ Element: Datum vestiging in Nederland (14.20)</value>
        [DataMember(Name = "e1420", EmitDefaultValue = false)]
        public string E1420 { get; set; }

        /// <summary>
        /// Een aanduiding van de persoon door wie de aangifte van verblijf en adres is gedaan. ⦿ Groep: Adresaangifte (72) ⦿ Element: Omschrijving van de aangifte adreshouding (72.10)
        /// </summary>
        /// <value>Een aanduiding van de persoon door wie de aangifte van verblijf en adres is gedaan. ⦿ Groep: Adresaangifte (72) ⦿ Element: Omschrijving van de aangifte adreshouding (72.10)</value>
        [DataMember(Name = "e7210", EmitDefaultValue = false)]
        public string E7210 { get; set; }

        /// <summary>
        /// Een aanduiding dat gedurende de opschorting van de bijhouding van de PL documenten zijn binnengekomen, die na de beëindiging van de opschorting verwerkt moeten worden. ⦿ Groep: Documentindicatie (75) ⦿ Element: Indicatie document (75.10)
        /// </summary>
        /// <value>Een aanduiding dat gedurende de opschorting van de bijhouding van de PL documenten zijn binnengekomen, die na de beëindiging van de opschorting verwerkt moeten worden. ⦿ Groep: Documentindicatie (75) ⦿ Element: Indicatie document (75.10)</value>
        [DataMember(Name = "e7510", EmitDefaultValue = false)]
        public string E7510 { get; set; }

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
        /// Een code, voorkomend in Tabel 60, RNI-deelnemerstabel, die aangeeft welke RNI-deelnemer (een deel van) de gegevens in de betrokken categorie heeft aangeleverd. ⦿ Groep: RNI-deelnemer (88) ⦿ Element: RNI-deelnemer (88.10)
        /// </summary>
        /// <value>Een code, voorkomend in Tabel 60, RNI-deelnemerstabel, die aangeeft welke RNI-deelnemer (een deel van) de gegevens in de betrokken categorie heeft aangeleverd. ⦿ Groep: RNI-deelnemer (88) ⦿ Element: RNI-deelnemer (88.10)</value>
        [DataMember(Name = "e8810", EmitDefaultValue = false)]
        public string E8810 { get; set; }

        /// <summary>
        /// Een aanduiding van het verdrag op basis waarvan (een deel van) de gegevens in de betrokken categorie door een buitenlandse zusterorganisatie van een RNI-deelnemer aan die deelnemer zijn aangeleverd. ⦿ Groep: RNI-deelnemer (88) ⦿ Element: Omschrijving verdrag (88.20)
        /// </summary>
        /// <value>Een aanduiding van het verdrag op basis waarvan (een deel van) de gegevens in de betrokken categorie door een buitenlandse zusterorganisatie van een RNI-deelnemer aan die deelnemer zijn aangeleverd. ⦿ Groep: RNI-deelnemer (88) ⦿ Element: Omschrijving verdrag (88.20)</value>
        [DataMember(Name = "e8820", EmitDefaultValue = false)]
        public string E8820 { get; set; }

        /// <summary>
        /// Gets or Sets Historie
        /// </summary>
        [DataMember(Name = "historie", EmitDefaultValue = false)]
        public List<Af01PlDataC08InnerAllOfHistorieInner> Historie { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Af01PlDataC08Inner {\n");
            sb.Append("  E0910: ").Append(E0910).Append("\n");
            sb.Append("  E0920: ").Append(E0920).Append("\n");
            sb.Append("  E1010: ").Append(E1010).Append("\n");
            sb.Append("  E1020: ").Append(E1020).Append("\n");
            sb.Append("  E1030: ").Append(E1030).Append("\n");
            sb.Append("  E1110: ").Append(E1110).Append("\n");
            sb.Append("  E1115: ").Append(E1115).Append("\n");
            sb.Append("  E1120: ").Append(E1120).Append("\n");
            sb.Append("  E1130: ").Append(E1130).Append("\n");
            sb.Append("  E1140: ").Append(E1140).Append("\n");
            sb.Append("  E1150: ").Append(E1150).Append("\n");
            sb.Append("  E1160: ").Append(E1160).Append("\n");
            sb.Append("  E1170: ").Append(E1170).Append("\n");
            sb.Append("  E1180: ").Append(E1180).Append("\n");
            sb.Append("  E1190: ").Append(E1190).Append("\n");
            sb.Append("  E1210: ").Append(E1210).Append("\n");
            sb.Append("  E1310: ").Append(E1310).Append("\n");
            sb.Append("  E1320: ").Append(E1320).Append("\n");
            sb.Append("  E1330: ").Append(E1330).Append("\n");
            sb.Append("  E1340: ").Append(E1340).Append("\n");
            sb.Append("  E1350: ").Append(E1350).Append("\n");
            sb.Append("  E1410: ").Append(E1410).Append("\n");
            sb.Append("  E1420: ").Append(E1420).Append("\n");
            sb.Append("  E7210: ").Append(E7210).Append("\n");
            sb.Append("  E7510: ").Append(E7510).Append("\n");
            sb.Append("  E8310: ").Append(E8310).Append("\n");
            sb.Append("  E8320: ").Append(E8320).Append("\n");
            sb.Append("  E8330: ").Append(E8330).Append("\n");
            sb.Append("  E8410: ").Append(E8410).Append("\n");
            sb.Append("  E8510: ").Append(E8510).Append("\n");
            sb.Append("  E8610: ").Append(E8610).Append("\n");
            sb.Append("  E8810: ").Append(E8810).Append("\n");
            sb.Append("  E8820: ").Append(E8820).Append("\n");
            sb.Append("  Historie: ").Append(Historie).Append("\n");
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
            // E0910 (string) maxLength
            if (this.E0910 != null && this.E0910.Length > 4)
            {
                yield return new ValidationResult("Invalid value for E0910, length must be less than 4.", new [] { "E0910" });
            }

            // E0910 (string) minLength
            if (this.E0910 != null && this.E0910.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E0910, length must be greater than 0.", new [] { "E0910" });
            }

            if (this.E0920 != null) {
                // E0920 (string) pattern
                Regex regexE0920 = new Regex(@"^(|[0-9]{8})$", RegexOptions.CultureInvariant);
                if (!regexE0920.Match(this.E0920).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E0920, must match a pattern of " + regexE0920, new [] { "E0920" });
                }
            }

            // E1010 (string) maxLength
            if (this.E1010 != null && this.E1010.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E1010, length must be less than 1.", new [] { "E1010" });
            }

            // E1010 (string) minLength
            if (this.E1010 != null && this.E1010.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1010, length must be greater than 0.", new [] { "E1010" });
            }

            // E1020 (string) maxLength
            if (this.E1020 != null && this.E1020.Length > 24)
            {
                yield return new ValidationResult("Invalid value for E1020, length must be less than 24.", new [] { "E1020" });
            }

            // E1020 (string) minLength
            if (this.E1020 != null && this.E1020.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1020, length must be greater than 0.", new [] { "E1020" });
            }

            if (this.E1030 != null) {
                // E1030 (string) pattern
                Regex regexE1030 = new Regex(@"^(|[0-9]{8})$", RegexOptions.CultureInvariant);
                if (!regexE1030.Match(this.E1030).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E1030, must match a pattern of " + regexE1030, new [] { "E1030" });
                }
            }

            // E1110 (string) maxLength
            if (this.E1110 != null && this.E1110.Length > 24)
            {
                yield return new ValidationResult("Invalid value for E1110, length must be less than 24.", new [] { "E1110" });
            }

            // E1110 (string) minLength
            if (this.E1110 != null && this.E1110.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1110, length must be greater than 0.", new [] { "E1110" });
            }

            // E1115 (string) maxLength
            if (this.E1115 != null && this.E1115.Length > 80)
            {
                yield return new ValidationResult("Invalid value for E1115, length must be less than 80.", new [] { "E1115" });
            }

            // E1115 (string) minLength
            if (this.E1115 != null && this.E1115.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1115, length must be greater than 0.", new [] { "E1115" });
            }

            // E1120 (string) maxLength
            if (this.E1120 != null && this.E1120.Length > 5)
            {
                yield return new ValidationResult("Invalid value for E1120, length must be less than 5.", new [] { "E1120" });
            }

            // E1120 (string) minLength
            if (this.E1120 != null && this.E1120.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1120, length must be greater than 0.", new [] { "E1120" });
            }

            // E1130 (string) maxLength
            if (this.E1130 != null && this.E1130.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E1130, length must be less than 1.", new [] { "E1130" });
            }

            // E1130 (string) minLength
            if (this.E1130 != null && this.E1130.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1130, length must be greater than 0.", new [] { "E1130" });
            }

            // E1140 (string) maxLength
            if (this.E1140 != null && this.E1140.Length > 4)
            {
                yield return new ValidationResult("Invalid value for E1140, length must be less than 4.", new [] { "E1140" });
            }

            // E1140 (string) minLength
            if (this.E1140 != null && this.E1140.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1140, length must be greater than 0.", new [] { "E1140" });
            }

            // E1150 (string) maxLength
            if (this.E1150 != null && this.E1150.Length > 2)
            {
                yield return new ValidationResult("Invalid value for E1150, length must be less than 2.", new [] { "E1150" });
            }

            // E1150 (string) minLength
            if (this.E1150 != null && this.E1150.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1150, length must be greater than 0.", new [] { "E1150" });
            }

            if (this.E1160 != null) {
                // E1160 (string) pattern
                Regex regexE1160 = new Regex(@"^$|^[1-9]{1}[0-9]{3}[A-Za-z]{2}$", RegexOptions.CultureInvariant);
                if (!regexE1160.Match(this.E1160).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E1160, must match a pattern of " + regexE1160, new [] { "E1160" });
                }
            }

            // E1170 (string) maxLength
            if (this.E1170 != null && this.E1170.Length > 80)
            {
                yield return new ValidationResult("Invalid value for E1170, length must be less than 80.", new [] { "E1170" });
            }

            // E1170 (string) minLength
            if (this.E1170 != null && this.E1170.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1170, length must be greater than 0.", new [] { "E1170" });
            }

            // E1180 (string) maxLength
            if (this.E1180 != null && this.E1180.Length > 16)
            {
                yield return new ValidationResult("Invalid value for E1180, length must be less than 16.", new [] { "E1180" });
            }

            // E1180 (string) minLength
            if (this.E1180 != null && this.E1180.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1180, length must be greater than 0.", new [] { "E1180" });
            }

            // E1190 (string) maxLength
            if (this.E1190 != null && this.E1190.Length > 16)
            {
                yield return new ValidationResult("Invalid value for E1190, length must be less than 16.", new [] { "E1190" });
            }

            // E1190 (string) minLength
            if (this.E1190 != null && this.E1190.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1190, length must be greater than 0.", new [] { "E1190" });
            }

            // E1210 (string) maxLength
            if (this.E1210 != null && this.E1210.Length > 35)
            {
                yield return new ValidationResult("Invalid value for E1210, length must be less than 35.", new [] { "E1210" });
            }

            // E1210 (string) minLength
            if (this.E1210 != null && this.E1210.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1210, length must be greater than 0.", new [] { "E1210" });
            }

            // E1310 (string) maxLength
            if (this.E1310 != null && this.E1310.Length > 4)
            {
                yield return new ValidationResult("Invalid value for E1310, length must be less than 4.", new [] { "E1310" });
            }

            // E1310 (string) minLength
            if (this.E1310 != null && this.E1310.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1310, length must be greater than 0.", new [] { "E1310" });
            }

            if (this.E1320 != null) {
                // E1320 (string) pattern
                Regex regexE1320 = new Regex(@"^(|[0-9]{8})$", RegexOptions.CultureInvariant);
                if (!regexE1320.Match(this.E1320).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E1320, must match a pattern of " + regexE1320, new [] { "E1320" });
                }
            }

            // E1330 (string) maxLength
            if (this.E1330 != null && this.E1330.Length > 35)
            {
                yield return new ValidationResult("Invalid value for E1330, length must be less than 35.", new [] { "E1330" });
            }

            // E1330 (string) minLength
            if (this.E1330 != null && this.E1330.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1330, length must be greater than 0.", new [] { "E1330" });
            }

            // E1340 (string) maxLength
            if (this.E1340 != null && this.E1340.Length > 35)
            {
                yield return new ValidationResult("Invalid value for E1340, length must be less than 35.", new [] { "E1340" });
            }

            // E1340 (string) minLength
            if (this.E1340 != null && this.E1340.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1340, length must be greater than 0.", new [] { "E1340" });
            }

            // E1350 (string) maxLength
            if (this.E1350 != null && this.E1350.Length > 35)
            {
                yield return new ValidationResult("Invalid value for E1350, length must be less than 35.", new [] { "E1350" });
            }

            // E1350 (string) minLength
            if (this.E1350 != null && this.E1350.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1350, length must be greater than 0.", new [] { "E1350" });
            }

            // E1410 (string) maxLength
            if (this.E1410 != null && this.E1410.Length > 4)
            {
                yield return new ValidationResult("Invalid value for E1410, length must be less than 4.", new [] { "E1410" });
            }

            // E1410 (string) minLength
            if (this.E1410 != null && this.E1410.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E1410, length must be greater than 0.", new [] { "E1410" });
            }

            if (this.E1420 != null) {
                // E1420 (string) pattern
                Regex regexE1420 = new Regex(@"^(|[0-9]{8})$", RegexOptions.CultureInvariant);
                if (!regexE1420.Match(this.E1420).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E1420, must match a pattern of " + regexE1420, new [] { "E1420" });
                }
            }

            // E7210 (string) maxLength
            if (this.E7210 != null && this.E7210.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E7210, length must be less than 1.", new [] { "E7210" });
            }

            // E7210 (string) minLength
            if (this.E7210 != null && this.E7210.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E7210, length must be greater than 0.", new [] { "E7210" });
            }

            // E7510 (string) maxLength
            if (this.E7510 != null && this.E7510.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E7510, length must be less than 1.", new [] { "E7510" });
            }

            // E7510 (string) minLength
            if (this.E7510 != null && this.E7510.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E7510, length must be greater than 0.", new [] { "E7510" });
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

            // E8810 (string) maxLength
            if (this.E8810 != null && this.E8810.Length > 4)
            {
                yield return new ValidationResult("Invalid value for E8810, length must be less than 4.", new [] { "E8810" });
            }

            // E8810 (string) minLength
            if (this.E8810 != null && this.E8810.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E8810, length must be greater than 0.", new [] { "E8810" });
            }

            // E8820 (string) maxLength
            if (this.E8820 != null && this.E8820.Length > 50)
            {
                yield return new ValidationResult("Invalid value for E8820, length must be less than 50.", new [] { "E8820" });
            }

            // E8820 (string) minLength
            if (this.E8820 != null && this.E8820.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E8820, length must be greater than 0.", new [] { "E8820" });
            }

            yield break;
        }
    }

}
