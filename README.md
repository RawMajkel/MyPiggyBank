[![Build status](https://ci.appveyor.com/api/projects/status/y3ldnpju1ssp7ljp?svg=true)](https://ci.appveyor.com/project/RawMajkel/mypiggybank)

# MyPiggyBank
Aplikacja budżetowa na potrzeby przedmiotu "Zespołowe przedsięwzięcie programistyczne"

## Opis wymagań
Celem projektu jest stworzenie aplikacji internetowej, która służyć ma do notowania swoich wydatków i przychodów. Pozwoli to na analizę oraz lepsze zarządzanie swoimi finansami. 

#### Wymagania funkcjonalne:
- wyświetlanie obecnego i historycznego stanu swoich rachunków (wykres)
- wyświetlanie wydatków (planowanych, cyklicznych)
- wyświetlanie historii swoich transakcji
- wprowadzanie nowych transakcji
- prowadzenie analityki swojego konta
- rejestracja i zarządzanie kontem użytkownika
- zarządzanie kategoriami wydatków
- zarządzanie swoimi rachunkami

#### Wymagania niefunkcjonalne:
- interfejs: aplikacja internetowa
- dostęp z poziomu desktopowej oraz mobilnej przeglądarki internetowej
- przeznaczona dla wielu użytkowników
- system nie może udostępniać informacji o użytkownikach, jeśli nie wyrażono na to zgody
- system otwarty na rozbudowę
- system oraz klient muszą mieć dostęp do internetu

## Opis architektury i wybór technologii
Aplikacja hostowana będzie jako Web API pod ASP.NET Core wraz z połączeniem bazodanowym do MSSQL. Front-end aplikacji planujemy stworzyć w oparciu o ReactJS.

#### Używane technologie:
- ASP.NET Core Web API (C#)
- Entity Framework Core
- Baza danych MSSQL
- JWT Authentication
- FluentValidation
- Front-end aplikacji: ReactJS (może ulec zmianie)

## Standardowy przypadek użycia
- Użytkownik tworzy konto w systemie oraz się loguje
- Użytkownik podaje listę swoich rachunków - czyli miejsc w których gromadzi pieniądze - np. karta mBank, karta Revolut, gotówka - i podaje ich stan.
- Następnie użytkownik przygotowuje zestawienie swoich wydatków cyklicznych - rocznych / kwartalnych / miesięcznych.
- Użytkownik na bieżąco dodaje swoje transakcje (wydatki oraz przychody)
- Użytkownik ma stały dostęp do podglądu swojego konta oraz statystyk dochodów/wydatków.
