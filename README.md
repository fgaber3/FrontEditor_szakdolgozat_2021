# Hogyan futtatható a FrontEditor projekt?
 - .NET Core 3.1 SDK telepítése
 - Fronteditor.Client mappa megnyitása CLI-ben
 - fronteditorclient image buildelése az alábbi parancsal:
```docker build -t fronteditorclient .```
 - Docker mappa megnyitása CLI-ben
 - konténer futtatása alábbi parancsal:
```docker-compose up -d```

Ha a konténer elindul, akkor http://localhost:3000/ elérhető az alkalmazás.
