# README 
Oliver Ko�mider     Kacper Kaszuba
           Mateusz Banasiak

## Wymagania
- **.NET SDK:** Wersja 6.0 lub wy�sza.
- **Edytor:** Visual Studio (zalecane) lub dowolny edytor obs�uguj�cy .NET.

## Instalacja
1. **Klonowanie projektu:**
   ```bash
   git clone <repository_url>
   cd NewProjektBDwAI
   ```
2. **Uruchomienie aplikacji:**
   - W terminalu:
     ```bash
     dotnet run
     ```
   - Lub w Visual Studio: Otw�rz projekt i wybierz "Start Debugging".
3. **Dost�p:**
   Aplikacja b�dzie dost�pna pod adresem:
   ```
   https://localhost:5281
   ```

## Konfiguracja
- **�a�cuch po��czenia z baz�:** Domy�lnie aplikacja korzysta z bazy In-Memory. W `Startup.cs` mo�na zmieni� baz� na SQL Server:
  ```csharp
  options.UseSqlServer("<Twoje_po��czenie_SQL_Server>");
  ```
- **Testowi u�ytkownicy:**
  - L:admin@example.com
    H:admin123

## Opis dzia�ania
1. **Logowanie do aplikacji:**
   - U�ytkownik loguje si� za pomoc� formularza, je�eli nie posiada konta to rejstruje si�.
2. **Funkcje aplikacji:**
   - **Zarz�dzanie u�ytkownikami:** Dodawanie, przegl�danie listy(poza interfejsem)
   - **Produkty:** Dodawanie, usuwanie, sprawdzanie, edytowanie produkt�w.
   - **Zam�wienia:** Tworzenie zam�wie� powi�zanych z produktami i u�ytkownikami.
   - **Magazyny:** Tworzenie oraz usuwanie magazyn�w.
3.**Funkcjonalno��**: Zale�na jest od roli u�ytkownika, rola Admina plasuje si� jako zarz�dca magazynu/magazyn�w, a zwyk�y user 
   jako zwyk�y pracownik wybranego magazynu(hermetyczna funkcjonalno��).

