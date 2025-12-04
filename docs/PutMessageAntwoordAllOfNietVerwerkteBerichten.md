# Org.OpenAPITools.Model.PutMessageAntwoordAllOfNietVerwerkteBerichten

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Ontvanger** | **int** |  | [optional] 
**BerichtId** | **string** | Het unieke volgnummer dat aan het uitgaande bericht wordt toegekend door de afzender. De \&quot;BRP berichten API\&quot; voert geen inhoudelijke controles uit op dit BerichtId. Maximaal 12 posities.  | 
**BerichtTransportId** | **Guid** | Dit is de referentie naar het bericht zoals deze bekend is bij de &#x60;BRP berichten API&#x60; (UUID). Bij elke interactie met deze dienst omtrent een bericht, wordt deze waarde gebruikt. Het mag, net zoals &#x60;berichtVolgnummer&#x60; gebruikt worden om het bericht uniek te identificeren. | 
**Foutmeldingen** | [**List&lt;PutMessageAntwoordAllOfFoutmeldingen&gt;**](PutMessageAntwoordAllOfFoutmeldingen.md) |  | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

