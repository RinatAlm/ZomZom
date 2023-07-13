using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PerkSelectionHandler : MonoBehaviour
{   
    public Image selectedImage;
    public Image perkImage;
    public Perk perk;
    public Text descriptionText;
    public Text newPerkText;
    private GameObject _acceptButton;

    private void Start()
    {
        descriptionText = PerkManager.instance.newPerkDescriptionText;
        newPerkText = PerkManager.instance.newPerkText;
        _acceptButton = PerkManager.instance.acceptButton;
        _acceptButton.SetActive(false);
    }
    public void SelectPerk()
    {
        if(selectedImage.color == PerkManager.instance.inactiveColor)
        {
            PerkManager.instance.ResetSelectedPerks();
            descriptionText.text = perk.perkDescription;
            selectedImage.color = PerkManager.instance.activeColor;
            newPerkText.text = perk.perkName;
            _acceptButton.SetActive(true);
            SetNewPerk();
        }
        else
        {
            _acceptButton.SetActive(false);
            PerkManager.instance.ResetSelectedPerks();
            PerkManager.instance.ResetSelected();
           
        }        
    }
    public void SetPerkImage()
    {       
        perkImage.sprite = perk.perkSprite;     
    }

    private void SetNewPerk()
    {
        PerkManager.instance.selectedNewPerk = perk;
    }
}
