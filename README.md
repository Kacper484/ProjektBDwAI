# README 
Oliver Koœmider     Kacper Kaszuba
           Mateusz Banasiak

## Wymagania
- **.NET SDK:** Wersja 6.0 lub wy¿sza.
- **Edytor:** Visual Studio (zalecane) lub dowolny edytor obs³uguj¹cy .NET.

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
   - Lub w Visual Studio: Otwórz projekt i wybierz "Start Debugging".
3. **Dostêp:**
   Aplikacja bêdzie dostêpna pod adresem:
   ```
   https://localhost:5281
   ```

## Konfiguracja
- **£añcuch po³¹czenia z baz¹:** Domyœlnie aplikacja korzysta z bazy In-Memory. W `Startup.cs` mo¿na zmieniæ bazê na SQL Server:
  ```csharp
  options.UseSqlServer("<Twoje_po³¹czenie_SQL_Server>");
  ```
- **Testowi u¿ytkownicy:**
  - L:admin@example.com
    H:admin123

## Opis dzia³ania
1. **Logowanie do aplikacji:**
   - U¿ytkownik loguje siê za pomoc¹ formularza, je¿eli nie posiada konta to rejstruje siê.
2. **Funkcje aplikacji:**
   - **Zarz¹dzanie u¿ytkownikami:** Dodawanie, przegl¹danie listy(poza interfejsem)
   - **Produkty:** Dodawanie, usuwanie, sprawdzanie, edytowanie produktów.
   - **Zamówienia:** Tworzenie zamówieñ powi¹zanych z produktami i u¿ytkownikami.
   - **Magazyny:** Tworzenie oraz usuwanie magazynów.
3.**Funkcjonalnoœæ**: Zale¿na jest od roli u¿ytkownika, rola Admina plasuje siê jako zarz¹dca magazynu/magazynów, a zwyk³y user 
   jako zwyk³y pracownik wybranego magazynu(hermetyczna funkcjonalnoœæ).

