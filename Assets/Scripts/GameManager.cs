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

    private NavMeshTriangulation triangulation;
    private int seconds;
    private int minutes;


    private void Awake()
    {
        instance = this;
        Time.timeScale = 0;
        CharacterCamera.transform.SetParent(UICamera.transform);
        mainMenueUI.SetActive(true);
        gameUI.SetActive(false);
        joyStick.SetActive(true);
        statsPanel.SetActive(false);
        triangulation = NavMesh.CalculateTriangulation();
    }
    private void Start()
    {
        // StartCoroutine(NoiseMaker());
        UICamera.transform.position = positionsToAttend[0];
        destination = positionsToAttend[0];
        CalculateNextPoint();
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


    public void StartTheGame()
    {
        CharacterCamera.transform.SetParent(Character.transform);
        Time.timeScale = 1;
        mainMenueUI.SetActive(false);
        gameUI.SetActive(true);
        joyStick.SetActive(true);
        GameOverPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        statsPanel.SetActive(true);
        totalyKilled = 0;
        StartCoroutine(ProgressTimer());

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
            //Do stuff
            counter--;
        }
        Time.timeScale = 0;
    }



    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void CloseInventory()
    {

        Time.timeScale = 1;
        joyStick.SetActive(true);
        inventoryPanel.SetActive(false);
        openInventoryButton.SetActive(true);
        statsPanel.SetActive(true);
        if (airDropObjectReference != null)
        {
            inventoryManager.weaponExchangeSlot.SetActive(false);
            inventoryManager.RemoveWeaponExchangeSlot();
            Destroy(airDropObjectReference.gameObject);
        }
    }

    public void OpenInventory()
    {
        Time.timeScale = 0.1f;
        openInventoryButton.SetActive(false);
        mainMenueUI.SetActive(false);
        joyStick.SetActive(false);
        inventoryPanel.SetActive(true);
        inventoryManager.exchangeSlot.SetActive(false);
        statsPanel.SetActive(false);
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

        expStatSlider.GetComponent<Slider>().maxValue += expStatSlider.GetComponent<Slider>().maxValue * expPercentage / 100;
        level++;
        levelStatText.GetComponent<Text>().text = "LVL:" + level;
        if (level % 1 == 0)
        {
            if (airDropObjectReference == null)
                CallAirDrop();
        }
    }

    IEnumerator ProgressTimer()
    {
        while (Time.timeScale != 0)
        {
            yield return new WaitForSecondsRealtime(1);
            //Do stuff
            seconds++;
            if (seconds >= 60)
            {
                seconds = 0;
                minutes++;
            }
            if (minutes >= 0 && minutes < 10)
            {
                timeStatText.GetComponent<Text>().text = "0" + minutes.ToString() + ":";
            }
            else
            {
                timeStatText.GetComponent<Text>().text = minutes.ToString() + ":";
            }
            if (seconds >= 0 && seconds < 10)
            {
                timeStatText.GetComponent<Text>().text += "0" + seconds.ToString();
            }
            else
            {
                timeStatText.GetComponent<Text>().text += seconds.ToString();
            }
        }

    }

    public void CallAirDrop()
    {
        AudioManager.instance.Play("AirplaneFlyBy");
        int VertexIndex = UnityEngine.Random.Range(0, triangulation.vertices.Length);
        NavMeshHit Hit;
        if (NavMesh.SamplePosition(triangulation.vertices[VertexIndex], out Hit, 2f, -1))
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
            airDropObjectReference.GetComponent<Airdrop>().gameManager = FindObjectOfType<GameManager>();
            airDropObjectReference.GetComponent<Airdrop>().isReady = true;
            airDropObjectReference.GetComponent<Airdrop>().gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

}
