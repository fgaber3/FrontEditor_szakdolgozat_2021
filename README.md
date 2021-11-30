# Hogyan futtatható a FrontEditor projekt?
 - .NET Core 3.1 SDK telepítése 
 - Docker Desktop telepítése
 - Fronteditor.Client mappa megnyitása CLI-ben
 - Fronteditorclient image buildelése az alábbi parancsal:\
```docker build -t fronteditorclient .```
 - Docker mappa megnyitása CLI-ben
 - Konténer futtatása alábbi parancsal:\
```docker-compose up -d```
 - Projekt leállítása adatok megtartásával:\
```docker-compose down```
 - Projekt leállítása adatok törlésével:\
 ```docker-compose down -v```

Ha a konténer elindul, akkor http://localhost:3000/ elérhető az alkalmazás.
