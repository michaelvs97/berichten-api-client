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
    /// Af01PlDataC07Inner
    /// </summary>
    [DataContract(Name = "Af01_plData_c07_inner")]
    public partial class Af01PlDataC07Inner : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Af01PlDataC07Inner" /> class.
        /// </summary>
        /// <param name="e6620">De datum waarop de gemeente de PL naar een andere gemeente of de RNI stuurt. ⦿ Groep: Blokkering (66) ⦿ Element: Datum ingang blokkering PL (66.20).</param>
        /// <param name="e6710">De datum waarop de bijhouding van de PL (gedeeltelijk) is opgeschort. ⦿ Groep: Opschorting (67) ⦿ Element: Datum opschorting bijhouding (67.10).</param>
        /// <param name="e6720">Een aanduiding van de reden waarom de bijhouding van de PL geheel of gedeeltelijk is opgeschort. ⦿ Groep: Opschorting (67) ⦿ Element: Omschrijving reden opschorting bijhouding (67.20).</param>
        /// <param name="e6810">Dit is de datum vanaf wanneer de persoon is ingeschreven in de BRP. ⦿ Groep: Opname (68) ⦿ Element: Datum eerste inschrijving BRP (68.10).</param>
        /// <param name="e6910">Een code, opgenomen in Tabel 33, Gemeententabel, die aangeeft in welke gemeente de PK zich bevindt. ⦿ Groep: Gemeente PK (69) ⦿ Element: Gemeente waar de PK zich bevindt (69.10).</param>
        /// <param name="e7010">Een aanduiding die aangeeft dat gegevens wel of niet verstrekt mogen worden. ⦿ Groep: Geheim (70) ⦿ Element: Indicatie geheim (70.10).</param>
        /// <param name="e7110">De datum waarop de verificatie van gegevens op de PL in de RNI heeft plaatsgevonden. ⦿ Groep: Verificatie (71) ⦿ Element: Datum verificatie (71.10).</param>
        /// <param name="e7120">De omschrijving op welke wijze de verificatie van gegevens op de PL in de RNI heeft plaatsgevonden. ⦿ Groep: Verificatie (71) ⦿ Element: Omschrijving verificatie (71.20).</param>
        /// <param name="e8010">Een nummer waarmee de versie van de PL aangegeven wordt. ⦿ Groep: Synchroniciteit (80) ⦿ Element: Versienummer (80.10).</param>
        /// <param name="e8020">Dit is de datum en tijd waarop de laatste wijziging of de eerste inschrijving van de PL heeft plaatsgevonden. De op te nemen tijd is Greenwich Mean Time (GMT). ⦿ Groep: Synchroniciteit (80) ⦿ Element: Datumtijdstempel (80.20).</param>
        /// <param name="e8710">Een aanduiding dat gegevens over alle kinderen van de PK zijn opgenomen in de BRP. ⦿ Groep: PK-conversie (87) ⦿ Element: PK-gegevens volledig meegeconverteerd (87.10).</param>
        /// <param name="e8810">Een code, voorkomend in Tabel 60, RNI-deelnemerstabel, die aangeeft welke RNI-deelnemer (een deel van) de gegevens in de betrokken categorie heeft aangeleverd. ⦿ Groep: RNI-deelnemer (88) ⦿ Element: RNI-deelnemer (88.10).</param>
        /// <param name="e8820">Een aanduiding van het verdrag op basis waarvan (een deel van) de gegevens in de betrokken categorie door een buitenlandse zusterorganisatie van een RNI-deelnemer aan die deelnemer zijn aangeleverd. ⦿ Groep: RNI-deelnemer (88) ⦿ Element: Omschrijving verdrag (88.20).</param>
        /// <param name="historie">historie.</param>
        public Af01PlDataC07Inner(string e6620 = default, string e6710 = default, string e6720 = default, string e6810 = default, string e6910 = default, string e7010 = default, string e7110 = default, string e7120 = default, string e8010 = default, string e8020 = default, string e8710 = default, string e8810 = default, string e8820 = default, List<Af01PlDataC07InnerAllOfHistorieInner> historie = default)
        {
            this.E6620 = e6620;
            this.E6710 = e6710;
            this.E6720 = e6720;
            this.E6810 = e6810;
            this.E6910 = e6910;
            this.E7010 = e7010;
            this.E7110 = e7110;
            this.E7120 = e7120;
            this.E8010 = e8010;
            this.E8020 = e8020;
            this.E8710 = e8710;
            this.E8810 = e8810;
            this.E8820 = e8820;
            this.Historie = historie;
        }

        /// <summary>
        /// De datum waarop de gemeente de PL naar een andere gemeente of de RNI stuurt. ⦿ Groep: Blokkering (66) ⦿ Element: Datum ingang blokkering PL (66.20)
        /// </summary>
        /// <value>De datum waarop de gemeente de PL naar een andere gemeente of de RNI stuurt. ⦿ Groep: Blokkering (66) ⦿ Element: Datum ingang blokkering PL (66.20)</value>
        [DataMember(Name = "e6620", EmitDefaultValue = false)]
        public string E6620 { get; set; }

        /// <summary>
        /// De datum waarop de bijhouding van de PL (gedeeltelijk) is opgeschort. ⦿ Groep: Opschorting (67) ⦿ Element: Datum opschorting bijhouding (67.10)
        /// </summary>
        /// <value>De datum waarop de bijhouding van de PL (gedeeltelijk) is opgeschort. ⦿ Groep: Opschorting (67) ⦿ Element: Datum opschorting bijhouding (67.10)</value>
        [DataMember(Name = "e6710", EmitDefaultValue = false)]
        public string E6710 { get; set; }

        /// <summary>
        /// Een aanduiding van de reden waarom de bijhouding van de PL geheel of gedeeltelijk is opgeschort. ⦿ Groep: Opschorting (67) ⦿ Element: Omschrijving reden opschorting bijhouding (67.20)
        /// </summary>
        /// <value>Een aanduiding van de reden waarom de bijhouding van de PL geheel of gedeeltelijk is opgeschort. ⦿ Groep: Opschorting (67) ⦿ Element: Omschrijving reden opschorting bijhouding (67.20)</value>
        [DataMember(Name = "e6720", EmitDefaultValue = false)]
        public string E6720 { get; set; }

        /// <summary>
        /// Dit is de datum vanaf wanneer de persoon is ingeschreven in de BRP. ⦿ Groep: Opname (68) ⦿ Element: Datum eerste inschrijving BRP (68.10)
        /// </summary>
        /// <value>Dit is de datum vanaf wanneer de persoon is ingeschreven in de BRP. ⦿ Groep: Opname (68) ⦿ Element: Datum eerste inschrijving BRP (68.10)</value>
        [DataMember(Name = "e6810", EmitDefaultValue = false)]
        public string E6810 { get; set; }

        /// <summary>
        /// Een code, opgenomen in Tabel 33, Gemeententabel, die aangeeft in welke gemeente de PK zich bevindt. ⦿ Groep: Gemeente PK (69) ⦿ Element: Gemeente waar de PK zich bevindt (69.10)
        /// </summary>
        /// <value>Een code, opgenomen in Tabel 33, Gemeententabel, die aangeeft in welke gemeente de PK zich bevindt. ⦿ Groep: Gemeente PK (69) ⦿ Element: Gemeente waar de PK zich bevindt (69.10)</value>
        [DataMember(Name = "e6910", EmitDefaultValue = false)]
        public string E6910 { get; set; }

        /// <summary>
        /// Een aanduiding die aangeeft dat gegevens wel of niet verstrekt mogen worden. ⦿ Groep: Geheim (70) ⦿ Element: Indicatie geheim (70.10)
        /// </summary>
        /// <value>Een aanduiding die aangeeft dat gegevens wel of niet verstrekt mogen worden. ⦿ Groep: Geheim (70) ⦿ Element: Indicatie geheim (70.10)</value>
        [DataMember(Name = "e7010", EmitDefaultValue = false)]
        public string E7010 { get; set; }

        /// <summary>
        /// De datum waarop de verificatie van gegevens op de PL in de RNI heeft plaatsgevonden. ⦿ Groep: Verificatie (71) ⦿ Element: Datum verificatie (71.10)
        /// </summary>
        /// <value>De datum waarop de verificatie van gegevens op de PL in de RNI heeft plaatsgevonden. ⦿ Groep: Verificatie (71) ⦿ Element: Datum verificatie (71.10)</value>
        [DataMember(Name = "e7110", EmitDefaultValue = false)]
        public string E7110 { get; set; }

        /// <summary>
        /// De omschrijving op welke wijze de verificatie van gegevens op de PL in de RNI heeft plaatsgevonden. ⦿ Groep: Verificatie (71) ⦿ Element: Omschrijving verificatie (71.20)
        /// </summary>
        /// <value>De omschrijving op welke wijze de verificatie van gegevens op de PL in de RNI heeft plaatsgevonden. ⦿ Groep: Verificatie (71) ⦿ Element: Omschrijving verificatie (71.20)</value>
        [DataMember(Name = "e7120", EmitDefaultValue = false)]
        public string E7120 { get; set; }

        /// <summary>
        /// Een nummer waarmee de versie van de PL aangegeven wordt. ⦿ Groep: Synchroniciteit (80) ⦿ Element: Versienummer (80.10)
        /// </summary>
        /// <value>Een nummer waarmee de versie van de PL aangegeven wordt. ⦿ Groep: Synchroniciteit (80) ⦿ Element: Versienummer (80.10)</value>
        [DataMember(Name = "e8010", EmitDefaultValue = false)]
        public string E8010 { get; set; }

        /// <summary>
        /// Dit is de datum en tijd waarop de laatste wijziging of de eerste inschrijving van de PL heeft plaatsgevonden. De op te nemen tijd is Greenwich Mean Time (GMT). ⦿ Groep: Synchroniciteit (80) ⦿ Element: Datumtijdstempel (80.20)
        /// </summary>
        /// <value>Dit is de datum en tijd waarop de laatste wijziging of de eerste inschrijving van de PL heeft plaatsgevonden. De op te nemen tijd is Greenwich Mean Time (GMT). ⦿ Groep: Synchroniciteit (80) ⦿ Element: Datumtijdstempel (80.20)</value>
        [DataMember(Name = "e8020", EmitDefaultValue = false)]
        public string E8020 { get; set; }

        /// <summary>
        /// Een aanduiding dat gegevens over alle kinderen van de PK zijn opgenomen in de BRP. ⦿ Groep: PK-conversie (87) ⦿ Element: PK-gegevens volledig meegeconverteerd (87.10)
        /// </summary>
        /// <value>Een aanduiding dat gegevens over alle kinderen van de PK zijn opgenomen in de BRP. ⦿ Groep: PK-conversie (87) ⦿ Element: PK-gegevens volledig meegeconverteerd (87.10)</value>
        [DataMember(Name = "e8710", EmitDefaultValue = false)]
        public string E8710 { get; set; }

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
        public List<Af01PlDataC07InnerAllOfHistorieInner> Historie { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Af01PlDataC07Inner {\n");
            sb.Append("  E6620: ").Append(E6620).Append("\n");
            sb.Append("  E6710: ").Append(E6710).Append("\n");
            sb.Append("  E6720: ").Append(E6720).Append("\n");
            sb.Append("  E6810: ").Append(E6810).Append("\n");
            sb.Append("  E6910: ").Append(E6910).Append("\n");
            sb.Append("  E7010: ").Append(E7010).Append("\n");
            sb.Append("  E7110: ").Append(E7110).Append("\n");
            sb.Append("  E7120: ").Append(E7120).Append("\n");
            sb.Append("  E8010: ").Append(E8010).Append("\n");
            sb.Append("  E8020: ").Append(E8020).Append("\n");
            sb.Append("  E8710: ").Append(E8710).Append("\n");
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
            if (this.E6620 != null) {
                // E6620 (string) pattern
                Regex regexE6620 = new Regex(@"^(|[0-9]{8})$", RegexOptions.CultureInvariant);
                if (!regexE6620.Match(this.E6620).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E6620, must match a pattern of " + regexE6620, new [] { "E6620" });
                }
            }

            if (this.E6710 != null) {
                // E6710 (string) pattern
                Regex regexE6710 = new Regex(@"^(|[0-9]{8})$", RegexOptions.CultureInvariant);
                if (!regexE6710.Match(this.E6710).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E6710, must match a pattern of " + regexE6710, new [] { "E6710" });
                }
            }

            // E6720 (string) maxLength
            if (this.E6720 != null && this.E6720.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E6720, length must be less than 1.", new [] { "E6720" });
            }

            // E6720 (string) minLength
            if (this.E6720 != null && this.E6720.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E6720, length must be greater than 0.", new [] { "E6720" });
            }

            if (this.E6810 != null) {
                // E6810 (string) pattern
                Regex regexE6810 = new Regex(@"^(|[0-9]{8})$", RegexOptions.CultureInvariant);
                if (!regexE6810.Match(this.E6810).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E6810, must match a pattern of " + regexE6810, new [] { "E6810" });
                }
            }

            // E6910 (string) maxLength
            if (this.E6910 != null && this.E6910.Length > 4)
            {
                yield return new ValidationResult("Invalid value for E6910, length must be less than 4.", new [] { "E6910" });
            }

            // E6910 (string) minLength
            if (this.E6910 != null && this.E6910.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E6910, length must be greater than 0.", new [] { "E6910" });
            }

            // E7010 (string) maxLength
            if (this.E7010 != null && this.E7010.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E7010, length must be less than 1.", new [] { "E7010" });
            }

            // E7010 (string) minLength
            if (this.E7010 != null && this.E7010.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E7010, length must be greater than 0.", new [] { "E7010" });
            }

            if (this.E7110 != null) {
                // E7110 (string) pattern
                Regex regexE7110 = new Regex(@"^(|[0-9]{8})$", RegexOptions.CultureInvariant);
                if (!regexE7110.Match(this.E7110).Success)
                {
                    yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for E7110, must match a pattern of " + regexE7110, new [] { "E7110" });
                }
            }

            // E7120 (string) maxLength
            if (this.E7120 != null && this.E7120.Length > 50)
            {
                yield return new ValidationResult("Invalid value for E7120, length must be less than 50.", new [] { "E7120" });
            }

            // E7120 (string) minLength
            if (this.E7120 != null && this.E7120.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E7120, length must be greater than 0.", new [] { "E7120" });
            }

            // E8010 (string) maxLength
            if (this.E8010 != null && this.E8010.Length > 4)
            {
                yield return new ValidationResult("Invalid value for E8010, length must be less than 4.", new [] { "E8010" });
            }

            // E8010 (string) minLength
            if (this.E8010 != null && this.E8010.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E8010, length must be greater than 0.", new [] { "E8010" });
            }

            // E8020 (string) maxLength
            if (this.E8020 != null && this.E8020.Length > 17)
            {
                yield return new ValidationResult("Invalid value for E8020, length must be less than 17.", new [] { "E8020" });
            }

            // E8020 (string) minLength
            if (this.E8020 != null && this.E8020.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E8020, length must be greater than 0.", new [] { "E8020" });
            }

            // E8710 (string) maxLength
            if (this.E8710 != null && this.E8710.Length > 1)
            {
                yield return new ValidationResult("Invalid value for E8710, length must be less than 1.", new [] { "E8710" });
            }

            // E8710 (string) minLength
            if (this.E8710 != null && this.E8710.Length < 0)
            {
                yield return new ValidationResult("Invalid value for E8710, length must be greater than 0.", new [] { "E8710" });
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
