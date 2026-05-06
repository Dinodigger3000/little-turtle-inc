using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject closeButton;  
    public GameObject openButton;   
    public GameObject journalPanel;
    public GameObject openJournalButton;  
    public GameObject closeJournalButton;

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

    public void openJournal()
    {
        journalPanel.SetActive(true);
        openJournalButton.SetActive(false);
        closeJournalButton.SetActive(true);
    }

    public void closeJournal()
    {
        journalPanel.SetActive(false);
        openJournalButton.SetActive(true);
        closeJournalButton.SetActive(false);
    }

}
