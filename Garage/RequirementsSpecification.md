# C# Övning 5 - Garage 1.0  
  
### Måsten
  
[x] En kollektion av fordon; klassen Garage.  
[x] En fordonsklass, klassen Vehicle.  
[] Ett antal subklasser till fordon.  
[] Ett användargränssnitt som låter oss använda funktionaliteten hos ett garage. *Här sker all interaktion med användaren.*  
  
## Kravspecifikation  
[] Fordonen ska implementeras som klassen Vehicle och subklasser till den.  
[x] Vehicle innehåller samtliga egenskaper som ska finnas i samtliga fordonstyper. 
*T.ex. registreringsnummer, färg, antal hjul och andra egenskaper ni kan komma på.*  
[] Registreringsnumret är unikt  
[] Det måste minst finnas följande subklasser: 
  [] Motorcycle  
  [] Airplane 
  [x] Car  
  [] Bus  
  [] Boat  
[] Subklasserna **skall** implementera minst en egen egenskap var. *t.ex: Number of Engines Cylinder volume, Fueltype (Gasoline/Diesel), Number of seats, Length*
 

[x] Samlingen av fordon ska internt i klassen hanteras som en array. Den interna arrayen ska var privat. Vid instansieringen av ett nytt garage måste kapaciteten anges som argument till konstruktorn.
  
Vi ska **EJ** använda oss av en List<Vehicle> internt i 

## Garage klassen!!!!  
### Funktionalitet  
Det ska gå att:  
[x] Lista samtliga parkerade fordon  
[] Lista fordonstyper och hur många av varje som står i garaget  
[x] Lägga till och ta bort fordon ur garaget  
[x] Sätta en kapacitet (antal parkeringsplatser) vid instansieringen av ett nytt garage  
[] Möjlighet att populera (sätta in bilar) i garaget med ett antal fordon från start.  
[x] Hitta ett specifikt fordon via registreringsnumret. Det ska fungera med både ABC123 
samt Abc123 eller AbC123.  
[x] Söka efter fordon utifrån en egenskap eller flera (alla möjliga kombinationer från 
basklassen Vehicle). *Exempelvis: Alla svarta fordon med fyra hjul. Alla motorcyklar som är rosa och har 3 hjul. Alla lastbilar, Alla röda fordon.* 
[x] Användaren ska få feedback på att saker gått bra eller dåligt. *Till exempel när vi parkerat ett fordon vill vi få en bekräftelse på att fordonet är parkerat. Om det inte går 
vill användaren få veta varför.* 

Programmet ska vara en konsolapplikation med ett textbaserat användargränssnitt.  
### Från gränssnittet skall det gå att:  
[] Navigera till samtlig funktionalitet från garage via gränssnittet  
[] Skapa ett garage med en användar-specificerad storlek  
[] Det skall gå att stänga av applikationen från gränssnittet

**Applikationen skall fel hantera indata på ett robust sätt, så att den inte kraschar vid felaktig inmatning eller användning.**
  
## Förslag på Extra funktionalitet (ej krav):  
[] Möjlighet att också kunna söka på de fordonsspecifika egenskaperna.  
[] Hantera flera garage som kan ha olika typer av fordon i sig exempelvis en hangar ett vanligt garage samt ett motorcykelgarage.  
*Detta kommer medföra att man ska kunna manövrera sig mellan dom olika garagen i ui:t för att kunna göra dom tidigare nämnda funktionerna de ska ske på bara det garaget som man har för tillfället valt. Det ska tydligt visas vilket garage man för närvarande arbetar med.  Ett garage består inte längre av fordon utan av parkeringsplatser som i sin tur kan hålla 
fordon.* 
[] Det går att via C# skriva och läsa till filsystemet från er applikation. Ta reda på hur man gör för 
att kunna spara ert garage (via meny eller automatiskt vid avstängning) och ladda in ert garage 
(via meny eller automatiskt vid start av applikationen)  
[] Olika fordon tar olika stor plats tex en bil tar 1 plats en båt tar 2 platser ett flygplan kräver 3 
platser osv en motorcykel tar endast 1/3 dels plats.  
[] När man parkerar ska endast de fordon som garaget har plats för visas som alternativ.  
[] Läsa in storleken på garaget via konfiguration.  

Valfri funktionalitet ni tycker borde finnas.  
Lycka till!  