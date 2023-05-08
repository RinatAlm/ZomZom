using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[Header("Noise")]
    //PerlinNoise noise = new PerlinNoise();
    //public Image gameOverNoiseImage;
    //[Space(20)]
    [Header("UI components")]
    public GameObject gameUI;
    public GameObject GameOverPanel;
    public GameObject mainMenueUI;
    public GameObject joyStick;
    public GameObject Character;
    public GameObject CharacterCamera;
    public GameObject UICamera;
    public GameObject gunManager;
    public GameObject openInventoryButton;
    public InventoryManager inventoryManager;
    public Text scoreText;
    [Space(20)]
    [Header("UI GunManager")]
    public GameObject inventoryPanel;
    [Space(20)]
    public List<Vector3> positionsToAttend = new List<Vector3>();
    public Vector3 destination;
    public Vector3 positionChange;
    public int posIndex = 0;
    public bool isChanged = false;
    public int positionChangeSpeed = 30;
    public float gameOverTimeScale = 0.1f;
    public int GameOverTicker = 3;
    public static int totalyKilled;
    
    private void Awake()
    {
        Time.timeScale = 0;
        CharacterCamera.transform.SetParent(UICamera.transform);
        mainMenueUI.SetActive(true);
        gameUI.SetActive(false);
        joyStick.SetActive(true);
       


    }
    private void Start()
    {
          
        UICamera.transform.position = positionsToAttend[0];
        destination = positionsToAttend[0];
        CalculateNextPoint();
    }

    private void Update()
    {
      
    }
    private void LateUpdate()
    {
        if(Time.timeScale == 0)
        {
            UICamera.transform.position += positionChange * Time.unscaledDeltaTime;
            if ((destination - UICamera.transform.position).magnitude <= 0.1f)
            {
                if (posIndex < positionsToAttend.Count - 1 && isChanged == false)
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
       
    }

    public void CalculateNextPoint()
    {
        positionChange = (destination - UICamera.transform.position)/ positionChangeSpeed;
    }
    

    public void StartTheGame()
    {
        CharacterCamera.transform.SetParent(Character.transform);
        Time.timeScale = 1;
        mainMenueUI.SetActive(false);
        gameUI.SetActive(true);
        joyStick.SetActive(true);
        GameOverPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        totalyKilled = 0;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverTimer(GameOverTicker));
        Time.timeScale = gameOverTimeScale;
        Character.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gunManager.SetActive(false);
        scoreText.text = "Kills:" + totalyKilled.ToString();
        GameOverPanel.SetActive(true);
        inventoryPanel.SetActive(false);
        joyStick.SetActive(false);
        openInventoryButton.SetActive(false);
        //StartCoroutine(NoiseMaker());
        
    }
  
    IEnumerator GameOverTimer(int seconds)
    {
        int counter = seconds;
        while(counter>0)
        {
            yield return new WaitForSecondsRealtime(1);
            //Do stuff
            counter--;
        }
        Time.timeScale = 0;
    }

    //public void BlackWhiteNoise()
    //{
    //    noise.offsetX = Random.Range(0, 9999f);
    //    noise.offsetY = Random.Range(0, 9999f);
    //    gameOverNoiseImage.material.mainTexture = noise.GenerateTexture();
    //}

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    //IEnumerator NoiseMaker()
    //{
    //    WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.1f);
    //    while(true)
    //    {
    //        BlackWhiteNoise();
    //        yield return wait; 
    //    }
    //}

    public void CloseInventory()
    {
        Time.timeScale = 1;
        joyStick.SetActive(true);
        inventoryPanel.SetActive(false);
      openInventoryButton.SetActive(true);
    }

    public void OpenInventory()
    {
        Time.timeScale = 0.1f;
        openInventoryButton.SetActive(false);
        mainMenueUI.SetActive(false);
        joyStick.SetActive(false);
        inventoryPanel.SetActive(true);
        inventoryManager.exchangeSlot.SetActive(false);
    }


}
