# polygon-editor/edytor-wielokątów
Edytor wielokątów w C# Windows Forms.
Projekt zrealizowany w ramach przedmiotu Grafika Komputerowa 1.

<img src="/Screenshots/app1.png">

## Podstawowa specyfikacja:
<ul>
  <li>Możliwość dodawania nowego wielokąta, usuwania oraz edycji.</li>
  <li>Przy edycji:
    <ul>
      <li>przesuwanie wierzchołka,</li>
      <li>usuwanie wierzchołka,</li>
      <li>dodawanie wierzchołka w środku wybranej krawędzi,</li>
      <li>przesuwanie całej krawędzi,</li>
      <li>przesuwanie całego wielokąta.</li>     
    </ul>
  </li>
  <li>Dodawanie ograniczeń (relacji) dla wybranej pary krawędzi (niekoniecznie na jednym wielokącie):
    <ul>
      <li>zadana długość krawędzi,</li>
      <li>krawędzie równoległe,</li>
      <li>dodawanie wierzchołka na krawędzi lub usuwanie wierzchołka - usuwa ograniczenia "przyległych" krawędzi,</li>
      <li>mozliwość usuwania relacji.</li>
    </ul>
  </li>
  <li>Rysowanie odcinków - algorytm biblioteczny i algorytm Bresenhama (do wyboru).</li>
</ul>

## Instrukcja obsługi
- Wszystkie działania w programie wykonujemy za pomocą myszki i odpowiednich przycisków w aplikacji.
- Rysowanie wielokąta: należy kliknąć w zielony przycisk Polygon i zacząć rysować na płótnie klikając lewym 
  przyciskem myszy w dowolne miejsce.
- Przerwanie rysowanie wielokąta: należy kliknąć prawym przyciskiem myszy w dowolne miejsce.
- Zaznaczenie krawędzi/wierzchołka/wielokąta: kliknięcie prawym przyciskiem myszy w daną krawędź/wierzchołek/
  wielokąt.
- Poruszanie krawędzią/wierzchołkiem/wielokątem: kliknąć prawym przyciskiem myszy w daną krawędź/wierzchołek/
  wielokąt, trzymając naciśnięty prawy przycisk myszy, poruszać myszą.
- Dodanie wierzchołka w środku wybranej krawędzi: zaznaczyć odpowiednią krawędź i klinkąć następnie w zielony
  przycisk Vertex.
- Dodanie relacji równoległości: zaznaczyć pierwszą krawędź, kliknąć w zielony przycisk Parallel edges i
  zaznaczyć drugą krawędź.
- Przerwanie dodawania relacji równoległości: kliknąć prawym przyciskiem myszy w dowolne miejsce.
- Dodanie relacji zadana długość krawędzi: zaznaczyć krawędź, klilknąć w zielony przycisk Given length edge, w 
  pojawiającym się oknie wpisać długość i nacisnąć okej.
- Usuwanie wielokąta: należy zaznaczyć wielokąt i kliknąć w pomarańczowy przycisk Polygon.
- Usuwanie wierzchołka: należy zaznaczyć wierzchołek i kliknąć w pomarańczowy przycisk Vertex.
- Usuwanie relacji równoległości: należy zaznaczyć odpowiednią krawędź, kliknąć w pomarańczowy przycisk parallel edges oraz
  zaznaczyć drugą krawędź.
- Usuwanie relacji zadana długość krawędzi: zaznaczyć krawędź i kliknąć w pomarańczowy przycisk given length edge.
- Kliknięcie w przycisk reset przywraca płótno do stanu początkowego.


## Opis algorytmu "relacji":
Przy jakimkolwiek ruchu, relacje są dodawane do stosu. Relacje ze stosu są pobierane i na bieżąco poprawiane 
i kolejne dodawane. Jeśli ilość iteracji relacji ze stosu przekroczy 40 to rzucany jest wyjątek CannotSetException.
