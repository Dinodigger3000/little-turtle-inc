using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject closeButton;  
    public GameObject openButton;   

    void Start()
    {
        helpPanel.SetActive(false);
        closeButton.SetActive(false); 
        openButton.SetActive(true);
    }

    public void closeMenu()
    {
        helpPanel.SetActive(false);
        openButton.SetActive(true);
        closeButton.SetActive(false);
    }

    public void openMenu()
    {
        helpPanel.SetActive(true);
        openButton.SetActive(false);
        closeButton.SetActive(true);
    }

}
