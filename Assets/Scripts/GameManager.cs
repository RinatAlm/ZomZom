using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject mainMenueUI;
    public GameObject Character;
    public GameObject CharacterCamera;
    public GameObject UICamera;
    public List<Vector3> positionsToAttend = new List<Vector3>();
    public Vector3 destination;
    public Vector3 positionChange;
    public int posIndex = 0;
    public bool isChanged = false;
    
    private void Awake()
    {
        Time.timeScale = 0;
        CharacterCamera.transform.SetParent(UICamera.transform);
        mainMenueUI.SetActive(true);
        gameUI.SetActive(false);

    }
    private void Start()
    {
        UICamera.transform.position = positionsToAttend[0];
        destination = positionsToAttend[0];
        CalculateNextPoint();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PauseTheGame();
        }
    }
    private void LateUpdate()
    {
        UICamera.transform.position += positionChange * Time.unscaledDeltaTime;
        if((destination - UICamera.transform.position).magnitude<=0.1f )
        {
            if(posIndex < positionsToAttend.Count-1 && isChanged == false)
            {
                posIndex++;
                
            }
            else
            {
                posIndex = 0;
                
            }
            isChanged = true;
            destination = positionsToAttend[posIndex];
            CalculateNextPoint();
        }
        else
        {
            isChanged = false;
        }
    }

    public void CalculateNextPoint()
    {
        positionChange = (destination - UICamera.transform.position)/30;
    }

    public void StartTheGame()
    {
        Time.timeScale = 1;
        mainMenueUI.SetActive(false);
        gameUI.SetActive(true);
        CharacterCamera.transform.SetParent(Character.transform);     
    }

    public void PauseTheGame()
    {
        Time.timeScale = 0.5f;
    }

}
