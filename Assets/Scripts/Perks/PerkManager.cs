using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    public static PerkManager instance;
    [Header("All perks")]
    public Dictionary<string, Perk> perkList = new();
    public HashSet<string> newPerksSet = new();
    public List<Sprite> perkSprites = new();
    public List<GameObject> perkButtonObjects = new();
    public List<string> perkNames = new();
    public List<string> perksDescription = new();
    public GameObject allPeksPanel;
    public GameObject viewportContent;
    public GameObject perkPrefab;  
    public List<Image> currentPerksImages = new();
    public Text perkText;
    public Text descriptionText;
    public List<GameObject> newPerksToSelect = new();

    [Header("New Perk Selection")]
    public GameObject perkSelectionPanel;
    public GameObject acceptButton;
    public Text newPerkText;
    public Text newPerkDescriptionText;
    public List<Image> newPerkSelectionImages = new();
    public Color inactiveColor;
    public Color activeColor = Color.white;
    public Perk selectedNewPerk;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        InitializePerks(perkSprites.Count);
    }

    private void InitializePerks(int numOfPerks)
    {
        for(int i = 0;i<numOfPerks; i++)
        {
            GameObject createdPerk = Instantiate(perkPrefab, viewportContent.transform.position, Quaternion.identity);         
            createdPerk.transform.SetParent(viewportContent.transform, false);           
            perkButtonObjects.Add(createdPerk);
        }
        #region Perk Creation
        Perk perk1 = new Perk("All shooting damage Up",0.05f);
        Perk perk2 = new Perk("Fire Rate Up", -0.05f);
        Perk perk3 = new Perk("Protection Up", 0.1f);
        Perk perk4 = new Perk("Aiming Up", 0.1f);
        Perk perk5 = new Perk("Fire damage Up", 0.1f);
        Perk perk6 = new Perk("Machine guns Damage Up", 0.1f);
        Perk perk7 = new Perk("Max Hp Up", 50);
        Perk perk8 = new Perk("Regeneration Up",1);
        Perk perk9 = new Perk("Pistols damage Up", 0.1f);
        Perk perk10 = new Perk("Rockets damage Up", 0.1f);
        Perk perk11 = new Perk("Shotguns damage Up", 0.1f);
        perkList.Add(perk1.perkName,perk1);
        perkList.Add(perk2.perkName,perk2);
        perkList.Add(perk3.perkName, perk3);
        perkList.Add(perk4.perkName, perk4);
        perkList.Add(perk5.perkName, perk5);
        perkList.Add(perk6.perkName, perk6);
        perkList.Add(perk7.perkName, perk7);
        perkList.Add(perk8.perkName, perk8);
        perkList.Add(perk9.perkName, perk9);
        perkList.Add(perk10.perkName, perk10);
        perkList.Add(perk11.perkName, perk11);
        #endregion
        for (int i = 0;i<numOfPerks;i++)
        {
            string perkName = perkNames[i];
            perkList[perkName].SetSprite(perkSprites[i]);
            perkList[perkName].perkDescription = perksDescription[i];
            perkButtonObjects[i].GetComponent<PerkButtonHandler>().index = i;
            perkButtonObjects[i].GetComponent<PerkButtonHandler>().perk = perkList[perkName];
            perkButtonObjects[i].GetComponent<PerkButtonHandler>().SetPerkImage();
            currentPerksImages.Add(perkButtonObjects[i].GetComponent<PerkButtonHandler>().selectedImage);
            perkButtonObjects[i].GetComponent<PerkButtonHandler>().perkLevelText.text = $"LVL {perkList[perkName].perkLevel}";
        }
    }
    public void UpdateLevelText()
    {
        for (int i = 0; i < perkButtonObjects.Count; i++)
        {
            string perkName = perkNames[i];
            if(perkList[perkName].perkLevel!=0)
                perkButtonObjects[i].GetComponent<PerkButtonHandler>().perkLevelText.text = $"<color=green>LVL {perkList[perkName].perkLevel}</color>";
            else
                perkButtonObjects[i].GetComponent<PerkButtonHandler>().perkLevelText.text = $"LVL {perkList[perkName].perkLevel}";
        }
    }
    #region NewPerkSelection
    public void ResetSelectedPerks()
    {
        if(perkSelectionPanel.activeSelf)
        {
            ClearImages(newPerkSelectionImages);
        }
        if(allPeksPanel.activeSelf)
        {
            ClearImages(currentPerksImages);
        }
    }
    public void AssignNewPerks()
    {
        ResetSelected();
        newPerksSet.Clear();
        for (int i = 0;i<newPerkSelectionImages.Count;i++)
        {
        again:
            string name = perkNames[Random.Range(0, perkNames.Count)];
            if (!newPerksSet.Contains(name))
            {
                newPerksSet.Add(name);
                newPerksToSelect[i].GetComponent<PerkSelectionHandler>().perk = perkList[name];        
                newPerksToSelect[i].GetComponent<PerkSelectionHandler>().SetPerkImage();                              
            }
            else
            {
                goto again;
            }            
        }
    }
    private void ClearImages(List<Image> images)
    {
        foreach (Image img in images)
        {
            img.color = inactiveColor;
        }
    }
    public void AcceptNewPerk()
    {
        //apply changes
        perkList[selectedNewPerk.perkName].perkLevel++;
        UpdateLevelText();
        GameManager.instance.OpenGameUI();
    }

    public void ResetSelected()
    {
        newPerkDescriptionText.text = string.Empty;//Set texts to null
        newPerkText.text = string.Empty;
        selectedNewPerk = null;      

    }
    #endregion


}
public class Perk
{
    public string perkName;
    public string perkDescription;
    public int perkLevel;
    public float updatePerLevel;
    public Sprite perkSprite;
    public Perk(string perkName, float updatePerLevel)
    {
        PerkManager.instance.perkNames.Add(perkName);
        this.perkName = perkName;
        this.perkLevel = 0;
        this.updatePerLevel = updatePerLevel;
        
    }
    public void SetSprite(Sprite sprite)
    {
        this.perkSprite = sprite;
    }

    
}



