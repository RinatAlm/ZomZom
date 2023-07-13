using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using UnityEngine.AI;
using System;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
    public GameObject inventoryPanel;
    public Text scoreText;
    public GameObject statsPanel;
    public GameObject expStatSlider;
    public GameObject timeStatText;
    public GameObject levelStatText;
    public GameObject perksPanel;
    public GameObject actionsPanel;
    public GameObject weaponExchangeSlot;
    public GameObject perkSelectionPanel;
    public GameObject airdropPanel;




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
    public GameObject airDropObjectReference;
    public int expPercentage;
    public int level;
    public List<GameObject> weapons = new List<GameObject>();
    public GameObject airDropPrefab;
    public GameObject ArrowObjectSet;
    public GameObject guns;
    public bool isAirbox = false;
    private bool isInputEnabled = true;

    private NavMeshTriangulation triangulation;
    private int _seconds;
    private int _minutes;


    private void Awake()
    {
        instance = this;
        Time.timeScale = 0;
        CharacterCamera.transform.SetParent(UICamera.transform);
        Character.SetActive(false);
        mainMenueUI.SetActive(true);
        gameUI.SetActive(false);
        triangulation = NavMesh.CalculateTriangulation();
    }
    private void Start()
    {
        
        UICamera.transform.position = positionsToAttend[0];
        destination = positionsToAttend[0];
        CalculateNextPoint();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            IncreaseLevel();
        }
    }

    public void OpenNewPerksSelectionPanel()
    {
        if (!perksPanel.activeSelf)
        {
            CloseGameUI();
            actionsPanel.SetActive(false);
            perkSelectionPanel.SetActive(true);        
            PerkManager.instance.AssignNewPerks();
            PerkManager.instance.ResetSelectedPerks();
 
        }
            
    }

    private void LateUpdate()
    {
        if (Time.timeScale == 0)//Moving camera from place to place in Menue mode
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
        positionChange = (destination - UICamera.transform.position) / positionChangeSpeed;
    }


    public void StartTheGame()//Used by button
    {
        Character.SetActive(true);
        Time.timeScale = 1;       
        OpenGameUI();
        totalyKilled = 0;
        StartCoroutine(ProgressTimer());
    }

    public void OpenGameUI()
    {
        StartCoroutine(TimeScaler(0f, 1f, 0.75f));
        airdropPanel.SetActive(true);
        CharacterCamera.transform.SetParent(Character.transform);
        mainMenueUI.SetActive(false);
        gameUI.SetActive(true);
        joyStick.SetActive(true);
        GameOverPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        openInventoryButton.SetActive(true);
        statsPanel.SetActive(true);    
        perksPanel.SetActive(false);
        actionsPanel.SetActive(false);
        perkSelectionPanel.SetActive(false);
    }

    private void CloseGameUI()
    {
        StartCoroutine(TimeScaler(1f, 0f, 0.75f));
        airdropPanel.SetActive(false);
        actionsPanel.SetActive(true);
        openInventoryButton.SetActive(false);
        mainMenueUI.SetActive(false);
        joyStick.SetActive(false);               
        statsPanel.SetActive(false);
        
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
    }

    IEnumerator GameOverTimer(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            counter--;
        }
        Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OpenCloseInventory()
    {
        if(!inventoryPanel.activeSelf)
        {
            ClosePerksPanel();
            OpenInventory();           
        }   
    }

    public void OpenClosePerksPanel()
    {
        if (!perksPanel.activeSelf)
        {
            CloseInventory();
            OpenPerksPanel();            
        }
    }
    public void OpenInventory()
    {
        CloseGameUI();
        inventoryPanel.SetActive(true);
        if (isAirbox)
            weaponExchangeSlot.SetActive(true);
        else
            weaponExchangeSlot.SetActive(false);
    }
    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);        
    }
    
    public void OpenPerksPanel()
    {
        perksPanel.SetActive(true);    
    }
    public void ClosePerksPanel() 
    {
        perksPanel.SetActive(false);              
    }
    public void OpenCharacterPanels()
    {
        if(isInputEnabled)
        {           
            OpenInventory();
        }             
    }
    public void CloseCharacterPanels()
    {
        if (isInputEnabled)
        {
            DestroyAirbox();
            OpenGameUI();
        }        
    }

    private void DestroyAirbox()
    {
        if (airDropObjectReference != null)//Destroy Airdrop when it was used
        {
            weaponExchangeSlot.SetActive(false);
            inventoryManager.RemoveWeaponExchangeSlot();
            isAirbox = false;
            Destroy(airDropObjectReference.gameObject);
        }
    }
    private IEnumerator TimeScaler(float from,float to,float timeTransformSpeed)
    {
        if(Time.timeScale!=to)
        {
            isInputEnabled = false;
            float difference = to - from;
            if (difference > 0)
            {
                difference = 1;
                while (true)
                {
                    yield return new WaitForSecondsRealtime(timeTransformSpeed / 10);
                    float time = Time.timeScale;
                    time += difference * 0.1f;
                    Time.timeScale = (float)Math.Round(time, 2);
                    if (Time.timeScale >= to)
                    {
                        Time.timeScale = to;
                        isInputEnabled = true;
                        break;
                    }
                }
            }

            else if (difference < 0)
            {
                difference = -1;
                while (true)
                {
                    yield return new WaitForSecondsRealtime(timeTransformSpeed / 10);
                    float time = Time.timeScale;
                    time += difference * 0.1f;
                    Time.timeScale = (float)Math.Round(time, 2);
                    if (Time.timeScale <= to)
                    {
                        Time.timeScale = to;
                        isInputEnabled = true;
                        break;
                    }
                }
            }
        }     
    }

    public void IncreaseLevelBar()
    {
        expStatSlider.GetComponent<Slider>().value++;
        if (expStatSlider.GetComponent<Slider>().value == expStatSlider.GetComponent<Slider>().maxValue)
        {
            expStatSlider.GetComponent<Slider>().value = 0;
            IncreaseLevel();
        }
    }

    public void IncreaseLevel()
    {
        expStatSlider.GetComponent<Slider>().maxValue += expStatSlider.GetComponent<Slider>().maxValue * expPercentage / 50;
        level++;
        levelStatText.GetComponent<Text>().text = "LVL:" + level;
        if (level % 1 == 0)
        {          
            if (airDropObjectReference == null)
                CallAirDrop();
            OpenNewPerksSelectionPanel();
        }
    }

    IEnumerator ProgressTimer()
    {
        while (Time.timeScale != 0)
        {
            yield return new WaitForSecondsRealtime(1);
            _seconds++;
            if (_seconds >= 60)
            {
                _seconds = 0;
                _minutes++;
            }
            if (_minutes >= 0 && _minutes < 10)
            {
                timeStatText.GetComponent<Text>().text = "0" + _minutes.ToString() + ":";
            }
            else
            {
                timeStatText.GetComponent<Text>().text = _minutes.ToString() + ":";
            }
            if (_seconds >= 0 && _seconds < 10)
            {
                timeStatText.GetComponent<Text>().text += "0" + _seconds.ToString();
            }
            else
            {
                timeStatText.GetComponent<Text>().text += _seconds.ToString();
            }
        }

    }

    public void CallAirDrop()
    {
        AudioManager.instance.Play("AirplaneFlyBy");
        int VertexIndex = UnityEngine.Random.Range(0, triangulation.vertices.Length);
        if (NavMesh.SamplePosition(triangulation.vertices[VertexIndex], out NavMeshHit Hit, 2f, -1))
        {
            airDropObjectReference = Instantiate(airDropPrefab, airDropPrefab.transform.position, airDropPrefab.transform.rotation);//SPAWNING AIRDROP 
            airDropObjectReference.GetComponent<Airdrop>().box.Warp(Hit.position);
            airDropObjectReference.GetComponent<Airdrop>().ArrowObjectSet = ArrowObjectSet;//AIRDROP VARIABLES ARE SET
            airDropObjectReference.GetComponent<Airdrop>().pointerIcon = ArrowObjectSet.GetComponent<PointerIcon>();
            airDropObjectReference.GetComponent<Airdrop>().pointerIcon.Show();
            airDropObjectReference.GetComponent<Airdrop>().playerTransform = GameObject.Find("Character").GetComponent<Transform>();
            airDropObjectReference.GetComponent<Airdrop>()._camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            airDropObjectReference.GetComponent<Airdrop>().worldPointer = GameObject.Find("3DAirdropPointer").GetComponent<Transform>();
            airDropObjectReference.GetComponent<Airdrop>().arrowIconTransform = GameObject.Find("ArrowRotator").GetComponent<RectTransform>();
            airDropObjectReference.GetComponent<Airdrop>().isReady = true;
        }
    }

}
