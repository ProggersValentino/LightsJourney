using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class interactHoverUI : MonoBehaviour
{
    public Text interactText;
    public GameObject textHolder;
    
    
    // Start is called before the first frame update
    void Start()
    {
        textHolder.SetActive(false);
    }

    public bool isDisplayed = false;

    //activates text when hovering
    public void SetUp(string promptText)
    {
        textHolder.SetActive(true);
        interactText.text = promptText;
        isDisplayed = true;
    }

    //stop text 
    public void close()
    {
        textHolder.SetActive(false);
        isDisplayed = false;
    }
}
