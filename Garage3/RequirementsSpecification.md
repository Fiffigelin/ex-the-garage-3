# C# Övning 9 - Garage 3.0  

**Garaget:** En representation av själva byggnaden. Garaget är en plats där en mängd av fordon 
kan förvaras. Garaget kan alltså representeras som en samling av fordon.  
**Fordon:** Bilar, motorcyklar, enhjulingar eller vad för typ av fordon man nu vill ställa in i garaget.
  
## Minimum krav-specifikation:  
* En kollektion av fordon; klassen Garage.  
* En fordonsklass, klassen Vehicle.  
* Ett antal subklasser till fordon.  
* Ett användargränssnitt som låter oss använda funktionaliteten hos ett garage. 
* En GarageHandler. För att abstrahera ett lager så att det inte finns någon direkt 
kontakt mellan användargränssnittet och garage klassen. Detta görs lämpligen 
genom en klass som hanterar funktionaliteten som gränssnittet behöver ha tillgång 
till.  
* Vi programmerar inte direkt mot konkreta typer så vi använder oss av Interfaces för 
det tex IUI, IHandler, IVehicle.

## Kravspecifikation  
Fordonen ska implementeras som klassen Vehicle och subklasser till den.  
[x] Vehicle innehåller samtliga egenskaper som ska finnas i samtliga fordonstyper. 
[x] Registreringsnumret är unikt  
[x] Det måste minst finnas följande subklasser: Bil, Motorcykel, Buss, Båt och Flygplan
[x] Dessa skall implementera minst en egen egenskap var. 
[] Själva garaget ska implementeras som en generisk samling av fordon:  
class Garage<T>  
[x] Dessutom ska den generiska typen begränsas med hjälp av en constraint:  
class Garage<T> where ....  
[x] Vidare ska det gå att iterera över en instans av Garage med hjälp av foreach. Det innebär att 
Garage ska implementera den generiska varianten av interfacet IEnumerable:  
class Garage<T> : ....  
[x] Klassen behöver inte ärva från någon annan klass eller implementera något annat interface.  
[x] Vid instansieringen av ett nytt garage måste kapaciteten anges som argument till 
konstruktorn.  


## Funktionalitet  
Det ska gå att:  
[x] Lista samtliga parkerade fordon  
[x] Lista fordonstyper och hur många av varje som står i garaget  
[x] Lägga till och ta bort fordon ur garaget  
[x] Sätta en kapacitet (antal parkeringsplatser) vid instansieringen av ett nytt garage  
[x] Möjlighet att populera garaget med ett antal fordon från start.  
[x] Hitta ett specifikt fordon via registreringsnumret. Det ska gå fungera med både 
ABC123 samt Abc123 eller AbC123.  
[x] Söka efter fordon utifrån en egenskap eller flera (alla möjliga kombinationer från 
basklassen Vehicle).
[x] Användaren ska få feedback på att saker gått bra eller dåligt.
[x] Programmet ska vara en konsolapplikation med ett textbaserat användargränssnitt.  

## Från gränssnittet skall det gå att:  
[x] Navigera till samtlig funktionalitet från garage via gränssnittet  
[x] Skapa ett garage med en användar-specificerad storlek  
[x] Det skall gå att stänga av applikationen från gränssnittet  
[] Applikationen skall fel hantera indata på ett robust sätt, så att den inte kraschar vid felaktig 
inmatning eller användning.  

## Förslag på Extra funktionalitet (ej krav):  
[] Möjlighet att också kunna söka på de fordonsspecifika egenskaperna.  
[] Hantera flera garage som kan ha olika typer av fordon i sig exempelvis en hangar ett vanligt 
garage samt ett motorcykelgarage.  
[] Det ska tydligt visas vilket garage man för närvarande arbetar med.  
[] Ett garage består inte längre av fordon utan av parkeringsplatser som i sin tur kan hålla 
fordon.  
[] Det går att via C# skriva och läsa till filsystemet från er applikation.
[] Olika fordon tar olika stor plats tex en bil tar 1plats en båt tar 2 platser ett flygplan kräver 3 
platser osv en motorcykel tar endast 1/3 dels plats.  
[] När man parkerar ska endast de fordon som garaget har plats för visas som alternativ.  
[] Läsa in storleken på garaget via konfiguration.  

## Tillägg Unit-tester: 
[x] Testen ska skapas i ett eget testprojekt.  
Vi begränsar oss till att testa de publika metoderna i klassen Garage.  
(Att skriva test för hela applikationen ses som en extra uppgift om tid finns).  

TDD: 
Testa gärna med att skriva testen före ni implementerat funktionaliteten! Använd er sedan  
ctrl . för att generera era objekt och metoder.  
Flytta dessa genererade klasser till rätt projekt. Implementera sen funktionaliteten tills testet 
går igenom.  
Strukturera testen enligt principen.  
1. Arrange här sätter ni upp det som ska testas, instansierar objekt och inputs  
2. Act här anropar ni metoden som ska testas  
3. Assert här kontrollerar ni att ni fått det förväntade resultatet Tänk även på att namnge  
testen på ett förklarande sätt.  

När ett test inte går igenom vill vi veta vad som inte fungerat enbart genom att se på 
testmetod namnet.  
Exempelvis: [MethodName_StateUnderTest_ExpectedBehavior] Public void 
Sum_NegativeNumberAs1stParam_ExceptionThrown() 

**Lycka till!**  