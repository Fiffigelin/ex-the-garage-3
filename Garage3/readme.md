# C# Övning 5 - Garage 1.0  
 
Ett första övergripande projekt. För att koppla samman mycket av det ni lärt er, så skall vi nu bygga en garage / Consoleapplikation. 

Denna applikation skall tillhandahålla den funktionalitet som ett system kan behöva om det skall användas för att simulera ett enkelt garage.

Det skall alltså gå att parkera fordon, hämta ut fordon, se efter vilka fordon som finns där och vilka egenskaper de har. Allt detta i en konsolapplikation med huvudmeny och 
undermenyer.  
  
Anledningen till att ni skall programmera ett garage är att det är enkelt att förankra uppdelningen i det hela. Vi kan huvudsakligen dela upp ett garage i följande delar:  
  
**Garaget:**  En representation av själva byggnaden. Garaget är en plats där ett antal 
fordon kan förvaras. Garaget kan därför representeras som en samling av fordon.  
  
**Fordon:** Bilar, motorcyklar, enhjulingar eller vad för typ av fordon man nu vill ställa in i garaget.  
  
Detta är de två huvudsakliga *objekttyperna* i ett fysiskt garage. Om vi tittar närmare bör 
det också finnas subklasser till fordon, alltså att varje fordonstyp är en egen subklass i 
systemet. Utöver detta krävs funktionalitet som hanterar att fordon ställs in i garaget, att 
fordon kan tas ut ur garaget samt att vi kan presentera och söka bland fordonen.
  
### I mer programmerings-vänliga termer skall vi alltså som minimum ha:  
  
* En kollektion av fordon; klassen **Garage**.  
* En fordonsklass, klassen **Vehicle**.  
* Ett antal subklasser till fordon.  
* Ett användargränssnitt som låter oss använda funktionaliteten hos ett garage. Här 
sker all interaktion med användaren.  
  
## Kravspecifikation  
Fordonen ska implementeras som klassen Vehicle och subklasser till den.  
* Vehicle innehåller samtliga egenskaper som ska finnas i samtliga fordonstyper. 
T.ex. registreringsnummer, färg, antal hjul och andra egenskaper ni kan komma på.  
* Registreringsnumret är unikt  
* Det måste minst finnas följande subklasser: 
  * Motorcycle  
  * Airplane 
  * Car  
  * Bus  
  * Boat  
* Subklasserna **skall** implementera minst en egen egenskap var, t.ex:  
  * Number of Engines  
  * Cylinder volume  
  * Fueltype (Gasoline/Diesel)  
  * Number of seats  
  * Length
 
Klassen behöver inte ärva från någon annan klass eller implementera något annat interface.  

Samlingen av fordon ska internt i klassen **Garage** hanteras som en **List<Vehicle>**. Listan ska vara privat. Vid instansieringen av ett nytt garage måste kapaciteten anges som argument till konstruktorn. Vid instansieringen av ett nytt garage måste kapaciteten anges som argument till konstruktorn.
  
**VI SKA ANVÄNDA OSS AV EN List<Vehicle> INTERNT I GARAGE-KLASSEN**

## Ni ska inte använda:
* En array internt i Garage-klassen.
* Generics, exempelvis Garage<T> where T : Vehicle.
* LINQ.
* Enhetstester.

## Funktionalitet
Det ska gå att:
* Lista samtliga parkerade fordon.
* Lista fordonstyper och hur många av varje typ som står i garaget.
* Lägga till och ta bort fordon ur garaget.
* Sätta en kapacitet, alltså antal parkeringsplatser, vid instansieringen av ett nytt 
garage.
* Möjlighet att populera garaget med ett antal fordon från start.
* Hitta ett specifikt fordon via registreringsnumret. Sökningen ska fungera oavsett 
om användaren skriver ABC123, Abc123 eller AbC123.
* Söka efter fordon utifrån en eller flera egenskaper från basklassen Vehicle. 
  * Alla svarta fordon med fyra hjul.
  * Alla motorcyklar som är rosa och har tre hjul.
  * Alla lastbilar.
  * Alla röda fordon.
* Användaren ska få tydlig feedback på om något gått bra eller dåligt. Till exempel ska 
användaren få bekräftelse när ett fordon har parkerats. Om det inte går ska användaren 
få veta varför.

### Användargränssnitt
Programmet ska vara en konsolapplikation med ett textbaserat användargränssnitt.
Från gränssnittet ska det gå att:
* Navigera till all funktionalitet i garaget.
* Skapa ett garage med en användarspecificerad storlek.
* Stänga av applikationen.
Applikationen ska hantera felaktiga indata på ett robust sätt, så att den inte kraschar vid 
felaktig inmatning eller användning.
Förslag på extra funktionalitet
Följande funktionalitet är inte ett krav, men kan läggas till för att utöka projektet:
* Möjlighet att även söka på de fordonsspecifika egenskaperna.
* Hantera flera garage med olika typer av fordon, till exempel en hangar, ett vanligt 
garage och ett motorcykelgarage.
* Möjlighet att växla mellan olika garage i användargränssnittet, så att tidigare 
nämnda funktioner bara utförs på det garage som för tillfället är valt.
* Tydlig visning av vilket garage användaren för närvarande arbetar med.
* Ett garage som består av parkeringsplatser, där varje parkeringsplats kan hålla ett 
fordon.
* Spara och läsa garage till och från filsystemet, exempelvis via meny eller 
automatiskt vid start och avstängning.
* Olika fordon tar olika mycket plats. Till exempel kan en bil ta en plats, en båt två 
platser, ett flygplan tre platser och en motorcykel en tredjedels plats.
* När man parkerar ska endast de fordon som garaget har plats för visas som 
alternativ.
* Läsa in garagets storlek via konfiguration.
* Valfri ytterligare funktionalitet som ni tycker borde finnas.

## Lycka till