# Org.OpenAPITools.Model.Foutmelding

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Type** | **string** | Unieke (RFC7807) foutcode voor het type van de opgetreden fout. Een namespace URI-pad gevolgd door een type fout id in de vorm VVV-DDDD-FFFF waarbij VVV gelijk is aan de afkorting van de voorziening (in dit geval BBA voor BRP Berichten API). DDDD een indicatie geeft van de operatie/domein waar de fout is opgetreden en FFFF een opdeling betreft van specifieke fouten (types) welke binnen een domein zijn identificeert.  | 
**Title** | **string** | De titel (RFC7807) geeft een korte uitleg van het soort fout en in sommige gevallen een toelichting van de oorzaak.  | 
**Detail** | **string** | Dit (RFC7807) veld geeft toelichting die kan helpen om de oorzaak van de fout te achterhalen, of geeft achtergrondinformatie over de foutsituatie.  | [optional] 
**Status** | **int** | Dit (RFC7807) veld beschrijft de HTTP status-code die door de BRP-Berichten-API is toegekend aan de fout (indien van toepassing). | 
**Instance** | **string** | Dit (RFC7807) veld beschrijft het request waarbij dit probleem is opgetreden (indien van toepassing). | [optional] 
**InvalidParameters** | [**List&lt;InvalidParametersInner&gt;**](InvalidParametersInner.md) |  | [optional] 
**DateTime** | **DateTime** | Het tijdstip en de datum (ISO date/time) waarop de fout is afgevangen en het foutmelding object is samengesteld.  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

