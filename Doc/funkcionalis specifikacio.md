# 1. Bevezetés

A projekt célja, hogy megoldást nyújtson a következő problémára: **megtalálni a legolcsóbb alkatrészeket egy asztali PC összeállításához**. Ennek megvalósítására egy olyan alkalmazást szeretnénk készíteni, amely segít a számítógép alkatrészek árainak összehasonlításában, és megkönnyíti a vásárlók számára a legjobb ajánlatok megtalálását. Egy saját árösszehasonlító robotot hozok létre, mely lehetővé teszi a folyamatok automatizálását és a felhasználó számára időmegtakarítást jelent. A robot a felhasználó helyett végzi el a webshopok böngészését, az árak lekérdezését és az eredmények rögzítését. Ezáltal hatékonyabbá és egyszerűbbé válik az alkatrészek árainak összehasonlítása és a legjobb vásárlási lehetőség megtalálása.

# 2. Projekt Áttekintés:

**Projektmegnevezés**: Árgép Robot

**Rövid leírás**: A projekt célja egy olyan árgép robot létrehozása, amely segít összeállítani egy asztali PC-t a lehető legolcsóbb áron. Az árgép robot adatbázisokból dolgozik, amelyekben tárolva vannak az alkatrészek, árak és webshopok információi. A felhasználók egyszerűen felsorolják a kívánt alkatrészeket egy Excel fájlban, majd elindítják a robotot, amely leellenőrzi a webshopokat az aktuális árakért. Végül a robot riportot készít, amelyben megjelenít egy összehasonlítást arról, hogy melyik webshopban találhatók a legolcsóbb alkatrészek.

**Elvárt kimenetek**:

1. Az árgép robot sikeresen leellenőrzi a webshopokat és összegyűjti az aktuális árakat az alkatrészekről.

2. Az adatbázis frissül az új árak alapján.

3. A robot létrehoz egy részletes riportot, amely tartalmazza az alkatrészek listáját, az áraikat és a webshopok neveit.

4. A riport automatikusan elküldésre kerül emailben.

5. A felhasználók könnyen használhatják az árgép robotot egy egyszerű Excel fájl feltöltésével és a robot elindításával.

# 3. Projektterv

**Cselekvési terv**: A projekt megvalósításához szükség van a következő lépésekre és idővonalra:

1. **Projekt Előkészítés (Szeptember - Október)**:
   - Kutatások elvégzése a projekt keretében.
   - Szükséges források és technológiák meghatározása.
   - Szakdolgozat keretének kidolgozása.
   - Adatbázis létrehozása és Microsoft SQL Server Express telepítése és konfigurálása.

2. **Adatbázis Felépítése (November - December)**:
   - Adatbázis táblák létrehozása.
   - Mezők meghatározása és kapcsolat kialakítása az alkalmazás és az adatbázis között.
   - Excel fájl létrehozása az alkatrészek felsorolásához.

3. **Adatok Betöltése és Árak Letöltése (Január - Február)**:
   - Adatok betöltése az adatbázisba.
   - Webshopok áradatainak letöltése.
   - Microsoft Office Interop technológia használata az Excel fájlok beolvasásához és adatok adatbázisba való mentéséhez.
   - Webshopok böngészése, alkatrészek keresése és árak lekérdezése.

4. **Riport Generálása és Finomítás (Március - Április)**:
   - Class library által generált riport készítése a tárolt adatok alapján.
   - Riport tartalmazza a legolcsóbb árakat és webshopokat.
   - E-mail integráció befejezése a felhasználónak történő riport küldéséhez.
   - Általános alkalmazás tesztelése és hibajavítás.

**Erőforrások**: A projekt során a következő erőforrásokra van szükség:

- **Emberi Erőforrások**:
   - Külső Konzulens: Nagy Péter
   - Belső Konzulens: Dr. Kovásznai Gergely
   - Szakdolgozat Író: Kis Sándor

- **Anyagi Erőforrások**:
   - Szükséges hardver- és szoftvereszközök a fejlesztéshez és teszteléshez.

# 4. Adatbázis kialakítása

## Adatbázis tervezés

Az adatbázis kialakítása a projekt egyik kulcsfontosságú része, amelyben tároljuk a termékek, árak és webshopok információit. Az adatbázis tervezése során a következő táblákat és mezőket használjuk:

### Categories tábla:

- **Id** *(INT)*: Egyedi azonosító, amely minden kategóriát egyértelműen azonosít.
- **CategoryName** *(NVARCHAR(MAX))*: A kategória nevét tárolja.

### ComputerParts tábla:

- **Id** *(INT)*: Egyedi azonosító, amely minden számítógép alkatrészt azonosít.
- **ComputerPartName** *(NVARCHAR(MAX))*: Az alkatrész nevét tárolja.
- **CategoryId** *(INT)*: Az alkatrész kategóriájának azonosítója, amely a Categories táblához kapcsolódik.

### WebShops tábla:

- **Id** *(INT)*: Egyedi azonosító, amely minden webshopot azonosít.
- **WebShopName** *(NVARCHAR(MAX))*: A webshop nevét tárolja.
- **WebShopURL** *(NVARCHAR(MAX))*: A webshop URL-jét tárolja.

### SearchResults tábla:

- **Id** *(INT)*: Egyedi azonosító, amely minden keresési eredményt azonosít.
- **ComputerPartId** *(INT)*: Az adott keresési eredményhez tartozó alkatrész azonosítója, amely a ComputerParts táblához kapcsolódik.
- **Description** *(NVARCHAR(MAX))*: Az alkatrész leírását tárolja.
- **ComputerPartPrice** *(DECIMAL(10, 2))*: Az alkatrész árát tárolja.
- **Currency** *(NVARCHAR(MAX))*: Az ár pénzneme.
- **WebShopId** *(INT)*: Az árhoz kapcsolódó webshop azonosítója, amely a WebShops táblához kapcsolódik.
- **ProductUrl** *(NVARCHAR(MAX))*: Az adott termék URL-jét tárolja a webshopban.


Az adatbázis tervezése során figyelembe vesszük az adatok struktúráját és azok közötti kapcsolatokat, hogy hatékonyan tudjuk tárolni és kezelni az információkat. A Categories, ComputerParts, WebShops és SearchResults táblák megfelelően definiáltak az adatok tárolásához és szervezéséhez.


# 5. Asztali alkalmazás fejlesztése

## Az asztali alkalmazás célja

Az alkalmazás célja egy könnyen használható asztali alkalmazás létrehozása, amelynek fő célja az Excel fájlból származó adatok hatékony és pontos feltöltése az adatbázisba. Az alkalmazás közvetlenül kapcsolódik az adatbázishoz, amelyben tárolva vannak az alkatrészek és árak adatai, valamint a webshopok információi. Az alkalmazás felhasználóbarát felülettel rendelkezik, amely lehetővé teszi a felhasználók számára az Excel fájlok kiválasztását, az adatok beolvasását és azok gyors és megbízható tárolását az adatbázisban.

## Adatok importálása és tárolása

Az alkalmazás fejlesztése során két fő feladat van:

1. **Adatok importálása az Excel fájlból**: Az alkalmazás beolvassa az Excel fájlt, amelyben felsorolják a kívánt alkatrészeket, és az alkalmazás az Excel fájlból beolvassa az alkatrészek adatait, például nevüket és az azonosítójukat. Ezeket az adatokat az alkalmazás az adatbázisba menti.

2. **Adatok tárolása az adatbázisban**: Az alkalmazás az adatokat az adatbázisban tárolja és kezeli. Az adatbázisban megtalálhatók a termékek, árak és webshopok adatai. A termékek és árak közötti kapcsolatot a megfelelő táblák és kulcsok segítségével rögzíti az alkalmazás.

## Felhasználói felület

Az alkalmazás egy felhasználóbarát asztali felülettel rendelkezik, ahol a felhasználók könnyen kezelhetik az alkalmazást. Az adatok importálásához és az adatbázisban tárolt információkhoz való hozzáférés egyszerű és intuitív módon történik.

## Funkcionalitások

Az alkalmazás fejlesztése során az adatok importálása és tárolása az adatbázisban a fő tevékenységek, amelyek lehetővé teszik az alkalmazás számára a naprakész információk kezelését és megjelenítését a felhasználók számára.

# 6. Árgép Robot fejlesztése

## Célja és működése

Az árgép robot célja az asztali PC alkatrészek árainak összehasonlítása és az árak adatbázisban történő rögzítése. A robot az adatbázisból származó adatokkal dolgozik, majd meglátogatja a webshopokat, és lekérdezi az alkatrészek árait. Az elérhető árakat az adatbázisba menti a további elemzés és riportkészítés céljából. A robot az alábbi feladatokat fogja ellátni:

- **Adatbáziskapcsolat**: Először is, a robot csatlakozik az adatbázishoz, és kiolvassa onnan az adatokat. Ez a lépés lehetővé teszi számára az alkatrészek és webshopok információinak előkészítését a webhelyek böngészése és az árak lekérdezése előtt.
- **Webshopok böngészése**: A robot a tárolt webshopokhoz navigál, és bejárja azokat egyesével. Ez lehetővé teszi számára, hogy elérje a webshopok terméklistáit és készen álljon a termékek árainak lekérdezésére.
- **Árak lekérdezése**: A robot a webshopokon keresztül keresi meg az egyes termékeket, és lekérdezi az áraikat. 
- **Adatok mentése az adatbázisba**: Miután a robot sikeresen lekérdezte az árakat, azokat az adatokat az adatbázisba menti. Ez lehetővé teszi az árak összehasonlítását és a további riportok készítését.

## Technológiák és eszközök

Az Árgép Robot fejlesztése során különböző technológiákat és eszközöket alkalmazunk:

- **UiPath Studio**: Az UiPath Studio segítségével hozzuk létre az árgép robotot és használjuk a webshopok böngészésére, az adatok lekérdezésére és az adatbázisba történő mentésre.
- **Web Scraping**: A weboldalakról történő adatok megszerzése és elemzése érdekében web scraping technológiát alkalmazunk.
- **Adatbázis kapcsolat**: Az adatok sikeres mentéséhez és lekérdezéséhez kapcsolódunk az adatbázishoz.

## Tesztelési terv

Az Árgép Robot tesztelése elengedhetetlen a pontos és megbízható működés biztosítása érdekében. A tesztelési terv a következő lépéseket foglalja magában:
1. **Funkcionális tesztelés**: Ellenőrizni fogjuk, hogy a robot helyesen működik-e az alkatrész árak lekérdezésében, az adatok helyes mentésében és az adatbázisból való olvasásban.
2. **Egyes modulok tesztelése**: Ellenőrizzük az egyes robotmodulok működését, például a webshopok böngészését, az árak lekérdezését és az adatbázisba történő mentést.
3. **Adatok összehasonlítása**: Összehasonlítjuk az árgép robot által megszerzett árakat a webshopok eredeti áraival, hogy ellenőrizzük az adatok pontosságát.
4. **Hibajavítás és finomhangolás**: Az esetlegesen felmerülő hibák javítása és az alkalmazás finomhangolása a tesztek során.
5. **UAT**

A tesztelés során biztosítjuk, hogy az árgép robot megbízhatóan működik és pontos adatokat szolgáltat az adatbázisba.

# 7. Riportkészítés

## Class Library használata

A riportok elkészítéséhez használni fogjuk a létrehozott Class Library-t. Ez a Class Library felelős az adatok feldolgozásáért és a riportok generálásáért. Az árgép robot használja ezt a könyvtárat a különböző webshopok áraihoz kapott adatok összegyűjtésére és elemzésére. A Class Library képes lesz az árakat összehasonlítani, és létrehozni egy riportot, amely tartalmazza a legolcsóbb ajánlatokat és a webshopokat.

## Riport formátuma

A riportokat elektronikus formában kapjuk meg, és PDF formátumban lesznek elérhetők. A riportok tartalmazni fogják az alábbiakat:

- A legolcsóbb alkatrészek: A riportban felsoroljuk azokat az alkatrészeket, amelyek a legolcsóbbak voltak a különböző webshopokban.

- Webshopok listája: Megadunk egy listát a webshopokról, amelyeket a robot ellenőrzött, és bemutatjuk azok adatait, például a webshop nevét és webcímét.

- Árak és termékadatok: A riport tartalmazza az összegyűjtött árakat és a termékek további adatait, például a termék nevét és a webshopot, ahol megtalálták.

Ez a riport segít a felhasználóknak gyorsan és hatékonyan megtalálni a legjobb ajánlatokat, és összehasonlítani a különböző webshopok árait az asztali PC alkatrészekhez. A PDF formátum lehetővé teszi a könnyű megosztást és nyomtatást, így a felhasználók kényelmesen hozzáférhetnek a legfrissebb árinformációhoz és webshop adatokhoz.


# Fogalomszótár
### **Microsoft SQL**
Az adatbázis-kezelő rendszerként a Microsoft SQL Server Express verzióját fogom használni. Ez a verzió ingyenesen elérhető, és kiválóan alkalmas kisebb méretű projektekhez. A Microsoft SQL Server számos fejlett funkciót és lehetőséget biztosít az adatok hatékony tárolásához, lekérdezéséhez és kezeléséhez. Ez a rendszer kiválóan skálázható, tehát alkalmazkodik a projekt méretéhez és igényeihez. Az adatok biztonságát is biztosítja, lehetőséget kínál a hozzáférési jogosultságok beállítására, valamint a tranzakciók kezelésére.

### **Microsoft Office Interop**
Az Office Interop technológia lehetővé teszi az Excel fájlok kezelését és manipulálását. Ezáltal az alkalmazás könnyedén beolvashatja és kezelheti a kívánt alkatrészeket tartalmazó Excel fájlt. Az Office Interop API-k széles körű funkcionalitást nyújtanak, beleértve az adatok olvasását, írását, formázását és többi műveletét. Az Office Interop technológia további műveleteket is kínál, mint például a munkalapok és táblázatok kezelése, a cellák tartalmának keresése vagy rendezése, a diagramok és grafikonok létrehozása és módosítása, valamint a fájlba való adatbeszúrás vagy adatkivágás. Ezek a funkciók lehetővé teszik az alkalmazás számára a teljes körű és rugalmas adatmanipulációt az Excel fájlokon belül.

### **C#**
A C# egy modern, objektumorientált nyelv a Microsoft .NET keretrendszerhez. Támogatja a hatékony adatkezelést, adatbázis műveleteket, kivételkezelést és nagy szintű absztrakciót. Ezenkívül a C# egy erőteljes nyelv a kódolási feladatok hatékony megoldásához.

### **UiPath Studio**
Az UiPath Studio egy grafikus fejlesztői környezet az RPA (Robotic Process Automation) alkalmazások készítéséhez. Ez a technológia segíti a felhasználókat a robotfolyamatok kialakításában és végrehajtásában. Az UiPath Studio továbbá támogatja a különböző adatforrások integrálását és kezelését. A felhasználók könnyedén kapcsolódhatnak adatbázisokhoz, Excel fájlokhoz, API-khoz és más rendszerekhez, így lehetőség nyílik az adatok átvételére, feldolgozására és felhasználására a robotfolyamatok során. Ez elősegíti a széleskörű adatfeldolgozást és az alkalmazások komplexitásának növelését.

### **UiPath Orchestrator**
Az UiPath Orchestrator egy felhőalapú platform a UiPath robotok központi kezelésére és felügyeletére. Ez a technológia lehetővé teszi a robotok ütemezését, végrehajtásuk nyomon követését, a robotfolyamatok irányítását és a riportok generálását. Az Orchestrator segítségével a robotfolyamatokat könnyedén és hatékonyan kezelhetik. Ennek a technologiának a segítségével a felhasználók egyszerűen ütemezhetik a robotfolyamatokat, ami azt jelenti, hogy megadhatják, hogy mikor és hogyan induljanak el a kívánt feladatok. Ez lehetővé teszi a tevékenységek hatékony ütemezését és optimalizálását, például az éjszakai órákban vagy kevésbé kihasznált időszakokban történő végrehajtást. Az Orchestrator riportokat is generál, amelyek segítségével a felhasználók átfogó képet kaphatnak a robotfolyamatok teljesítményéről és eredményeiről. Ezek a riportok részletes adatokat tartalmaznak, például a folyamatok végrehajtási idejét, a sikeres és sikertelen végrehajtások arányát, valamint egyéb releváns metrikákat. Ez segít a hatékonyság és teljesítmény optimalizálásában, valamint a döntéshozatalban és a stratégia kidolgozásában.

### **NuGet**
A NuGet egy rendkívül hasznos csomagkezelő rendszer, amely lehetővé teszi a .NET keretrendszerhez tartozó külső könyvtárak, komponensek és eszközök telepítését és kezelését a projektekben. A NuGet számos előnnyel jár, amelyek hozzájárulnak a fejlesztés hatékonyságához és a projektek kényelmesebb kezeléséhez.
