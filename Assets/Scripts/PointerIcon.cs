using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerIcon : MonoBehaviour
{
    public Image _image;
    public bool isShown;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
      
    public void Show()
    {
        gameObject.SetActive(true);
        isShown = true;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        isShown = false;
    }
}
