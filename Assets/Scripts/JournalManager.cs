using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private Transform entryContainer;
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;

    private List<string> entries = new List<string>();
    private bool hasLoaded = false;

    void Start()
    {
        PlayerPrefs.DeleteKey("journal_count");
        submitButton.onClick.AddListener(SubmitEntry);
        openButton.onClick.AddListener(() => journalPanel.SetActive(true));
        closeButton.onClick.AddListener(() => journalPanel.SetActive(false));

        if (!hasLoaded)
        {
            hasLoaded = true;
            int count = PlayerPrefs.GetInt("journal_count", 0);
            for (int i = 0; i < count; i++)
            {
                string saved = PlayerPrefs.GetString($"journal_{i}", "");
                if (!string.IsNullOrEmpty(saved)) SpawnEntry(saved);
            }
        }
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            journalPanel.SetActive(!journalPanel.activeSelf);
    }

    void SpawnEntry(string text)
    {
        entries.Add(text);
        GameObject newEntry = Instantiate(entryPrefab, entryContainer);
        newEntry.GetComponentInChildren<TMP_Text>().text = $"• {text}";
    }

    public void SubmitEntry()
    {
        string text = inputField.text.Trim();
        if (string.IsNullOrEmpty(text)) return;

        SpawnEntry(text);
        PlayerPrefs.SetString($"journal_{entries.Count - 1}", text);
        PlayerPrefs.SetInt("journal_count", entries.Count);
        PlayerPrefs.Save();

        inputField.text = "";
        inputField.ActivateInputField();
    }
}