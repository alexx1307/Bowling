using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramideBuilder : MonoBehaviour
{
    //obiekt, z którego będziemy budować piramidę (sześcian, może w przyszłości kręgiel)
    public GameObject prefab;

    //wielkość piramidy, czyli liczba obiektów w podstawie i jednocześnie wysokość piramidy (lizcba pięter)
    public int size;

    //przesunięcie w ramach pojedynczego wiersza, czyli odstęp między obiektami w wierszu
    public Vector3 objectsOffset;

    //przesunięcie pomiędzy wierszami, czyli o ile przesunięty jest pierwszy obiekt w wierszu względem pierwszego obiektu poprzedniego wiersza
    public Vector3 rowOffset;

    //pozycja startowa od której będziemy budować piramidę. Ta zmienna jest PRYWATNA, czyli nie da jej się ustawić w Inspectorze. 
    //To dlatego, że jej wartość ustalimy ustawiając obiekt zawierający ten skrypt - budowniczego piramidy. Tam gdzie go ustawimy - tam zbuduje się piramida
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        //Tutaj właśnie przypisujemy pozycję game objectu (transform.position), jako pozycję startową
        //Ten transform to jest dokłądnie ten transform, który ma game object budowacza piramid w inspektorze.
        startPosition = transform.position;
    }

    void Update()
    {
        //jeśli w danej klatce użytkownik nacisnął spację, zbuduj piramidę
        if (Input.GetKeyDown(KeyCode.C))
        {
            buildPyramide();
        }
    }

    //funkcja budująca piramidę
    void buildPyramide()
    {
        //pętla wchodząca na kolejne rzędy (piętra)
        for (int row = 0; row < size; row++)
        {
            //W danym piętrze uzupełnia się cały wiersz. Z każdym wierszem będzie mniej elementów.
            //Niech size, który ustawia się w inspectorze wynosi 5
            //Wtedy w pierwszym rzędzie i zaczyna w 0 a kończy jak osiągnie size - row, czyli 5 - 0 = 5
            //W drugim rzędzie row wynosi już 1, więc i zaczyna w zero a kończy jak osiątnie 5 - 1 = 4, będzie zatem o jeden element mniej
            //Ostatni raz ta pętla się wykona kiedy row = 4 (row<size w zewnętrznej pętli)
            //Wtedy i startuje w 0, a kończy w 5 - 4 = 1, czyli Instantiate wykona się tylko raz dla i = 0, więc w ostatnim rzędzie będzie tylko jeden element
            for (int i = 0; i < size - row; i++)
            {
                //Żeby wyznaczyć pozycję obiektu (kręgla, kostki) dodamy do poczatkowej pozycji jego przesunięcia w ramach wiersza i pomiędzy wirszami
                //Pierwszy obiekt powstanie, kiedy row = 0 oraz i = 0. Zatem pierwszy oniekt powstanie w miejscu startPosition.
                //W pierwszym przebiegu całej tej pętli zmienia się tylko "i", a row pozostaje na 0. Dlatego kolejne obiekty będą powstawać w odstępach wynoszących objectsOffset
                //W kolejnym wierszu, row = 1, dojdzie składnik rowOffset, który powinien określać odstęp między wierszami
                Vector3 prefabPos = startPosition + row * rowOffset + i * objectsOffset;

                //Stworzenie klona obiektu podanego w inspektorze, na pozycji wyliczonej powyżej, bez zmiany rotacji (ostatni parametr)
                Instantiate(prefab, prefabPos, Quaternion.identity);
            }
        }
    }
}
