using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PerkButtonHandler : MonoBehaviour
{
    public int index;
    public Image selectedImage;
    public Image perkImage;
    public Perk perk;
    public Text perkLevelText;
   public void SelectPerk()
    {
        if(selectedImage.color == PerkManager.instance.inactiveColor)
        {
            PerkManager.instance.ResetSelectedPerks();
            selectedImage.color = PerkManager.instance.activeColor;
            PerkManager.instance.perkText.text = PerkManager.instance.perkList[PerkManager.instance.perkNames[index]].perkName;
            PerkManager.instance.descriptionText.text = PerkManager.instance.perkList[PerkManager.instance.perkNames[index]].perkDescription;
        }
        else
        {
            PerkManager.instance.ResetSelectedPerks();
        }
        
    }

    public void SetPerkImage()
    {
        perkImage.sprite = perk.perkSprite;
    }
}
