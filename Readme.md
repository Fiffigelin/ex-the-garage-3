# Garage 3.0

En C#-konsolapplikation som simulerar ett eller flera garage. Programmet låter användaren skapa garage, parkera och ta bort fordon samt visa och söka bland parkerade fordon.

## Innehåll

- [Funktioner](#funktioner)
- [Fordon](#fordon)
- [Projektstruktur](#projektstruktur)
- [Köra programmet](#köra-programmet)
- [Köra tester](#köra-tester)

## Funktioner

Programmet är tänkt att erbjuda följande funktioner via ett textbaserat konsolgränssnitt:

- Skapa garage med ett angivet antal parkeringsplatser.
- Parkera och ta bort fordon.
- Lista parkerade fordon.
- Visa antal fordon av respektive typ.
- Hitta fordon via registreringsnummer, utan att skilja på stora och små bokstäver.
- Söka med flera egenskaper samtidigt, exempelvis registreringsnummer, färg, antal hjul och fordonstyp.
- Ge användaren återkoppling när en operation lyckas eller misslyckas.

Vilka funktioner som är tillgängliga beror på den aktuella implementationen.

## Fordon

`Vehicle` är den abstrakta basklassen för fordon. Den innehåller gemensamma egenskaper:

- `Type` – fordonstyp.
- `Color` – färg.
- `NumberOfWheels` – antal hjul.
- `LicensePlate` – registreringsnummer.
- `ParkingCells` – parkeringsplats dividerade i celler. 1 parkeringsplats är detsamma som 3 celler.

Fordonets färg och registreringsnummer normaliseras till versaler. Registreringsnumret valideras mot ett mönster med tre bokstäver följt av två siffror och därefter en bokstav eller siffra, exempelvis `ABC123`.

Följande konkreta fordonstyper ska finnas:

- Bil (`Car`)
- Motorcykel (`Motorcycle`)
- Buss (`Bus`)
- Båt (`Boat`)
- Flygplan (`Airplane`)

Varje underklass ska även ha minst en egen egenskap.

## Projektstruktur

Projektet är uppdelat i lager för att separera användargränssnitt, applikationslogik och domänlogik. Namnen nedan beskriver den avsedda strukturen; den faktiska mappstrukturen kan skilja sig åt.

- **UI** – konsolmenyer, inmatning och utskrift.
- **Application** – `GarageHandler` och samordning av användarens operationer.
- **Domain** – `Garage`, `GarageData`, `Vehicle`, fordonsklasser och domäninterface.
- **Garage3.Tests** – separat testprojekt för enhetstester.

<img width="632" height="612" alt="garaget3 drawio" src="https://github.com/user-attachments/assets/44ebd22f-72e7-42da-857e-2dc16b312318" />

### Viktiga domänklasser

- **`Garage`** – håller fordon och ansvarar för att lägga till och ta bort fordon. Konstruktorparametrarna anger garagets namn och kapacitet.
- **`GarageData`** – håller samlingen av garage och erbjuder operationer som att lägga till garage, kontrollera om ett namn används, kontrollera registreringsnummer och hämta fordon.
- **`Vehicle`** – abstrakt basklass med gemensamma fordonsegenskaper och validering.
- **`SearchVehicle`** – beskriver valfria sökkriterier. Flera angivna kriterier kombineras så att fordonet måste matcha dem alla.
- **`VehicleSearchResult`** – innehåller både garaget och fordonet som hittades.

## Köra programmet

### Förutsättningar

- Installera en .NET SDK som passar projektets target framework.
- Öppna en terminal i projektets rotmapp, där katalogen `Garage3` finns.

### Starta konsolapplikationen

Kör:

```bash
dotnet run --project Garage3
```

Det fungerar från projektroten utan att du först behöver gå in i `Garage3`-mappen. Om kommandot inte fungerar, kontrollera att `Garage3/Garage3.csproj` finns och att .NET SDK är installerad:

```bash
dotnet --info
```

## Köra tester

Testerna ligger i ett separat projekt, `Garage3.Tests`.

Kör samtliga tester från projektroten:

```bash
dotnet test
```

Om du inte har en solution-fil eller vill köra testprojektet direkt:

```bash
dotnet test Garage3.Tests/Garage3.Tests.csproj
```

Testerna ska fokusera på publika metoder i `Garage` enligt uppgiften och följa **Arrange – Act – Assert**:

1. **Arrange** – skapa testobjekt och förbered indata.
2. **Act** – anropa metoden som testas.
3. **Assert** – kontrollera resultatet.

En funktionalitet där jag har kört TDD är att garage håller en till flera fordonstyper.
Förberedelserna var att sitta och fundera flödet, vilka metoder som påverkades, vad klassen behöver. 

Jag skrev testet och råkade göra en Assert fel men i det stora hela var det smidigt.

<img width="462" height="692" alt="garage_vehicletype" src="https://github.com/user-attachments/assets/48a53d8b-444c-4f53-91ee-d2c7ecb3d467" />


## Teknik

- C#
- .NET
- Konsolapplikation
- xUnit för enhetstester
