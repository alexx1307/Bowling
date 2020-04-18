using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject playerInputPrefab;
    public GameObject addPlayerButton;
   
    public List<GameObject> playerInputs;

    private Vector3 nextPlayerInputPosition = new Vector3(0, 50, 0);

    void Start()
    {
        playerInputs = new List<GameObject>();
        AddPlayerInput();
    }

    public void AddPlayerInput()
    {
        GameObject newPlayerInput = Instantiate(playerInputPrefab, nextPlayerInputPosition, Quaternion.identity);
        newPlayerInput.transform.SetParent(this.transform, false);
        playerInputs.Add(newPlayerInput);
        nextPlayerInputPosition.y -= 31;
        addPlayerButton.transform.localPosition = nextPlayerInputPosition;
    }

    public void Play()
    {
        this.transform.gameObject.SetActive(false);
    }
}
