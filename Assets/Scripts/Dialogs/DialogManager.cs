using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialog
{
    [TextArea(1, 3)]
    public string[] sentences;
    public string dialogTitle;
    public bool isQuest;
    public Quest quest;
}

public class DialogManager : MonoBehaviour
{
    private List<Dialog> currentDialogOptions;
    private Queue<string> activeSentences;
    private Dialog activeDialog;
    
    private NPC m_Npc;

    public GameObject dialogBox;
    public Text dialogText;
    public GameObject dialogButton;
    public GameObject endInteractionButton;
    public GameObject[] optionButtons;

    [SerializeField] private LeafBlowerChar m_Char;
    private NPC engagedNPC;

    // Start is called before the first frame update
    void Start()
    {
        currentDialogOptions = new List<Dialog>();
        activeSentences = new Queue<string>();
    }

    public void LaunchDialogSequence(NPC npc)
    {
        m_Npc = npc;
        currentDialogOptions = npc.Dialogs;
        npc.SetNPCEngaged(m_Char.transform);
        m_Char.EngageChar();

        activeSentences.Clear();

        dialogText.text = npc.greeting;

        dialogBox.SetActive(true);
        dialogButton.SetActive(false);

        LoadDialogOptions();

        

    }

    public void EndDialogSequence()
    {
        m_Npc.DisengageNPC();
        m_Char.DisengageChar();
        activeSentences.Clear();

        foreach (GameObject currentButton in optionButtons)
        {
            currentButton.SetActive(false);
        }

        dialogBox.SetActive(false);
    }

    public void DialogButton()
    {
        if(activeSentences.Count > 0)
        {
            NextSentence();
        }
        else
        {
            if (activeDialog != null) 
            {
                if (activeDialog.isQuest)
                {
                    if (m_Char.m_QuestSystem.CheckItems(activeDialog.quest))
                    {
                        m_Char.m_QuestSystem.FinishQuest(activeDialog.quest);
                    }
                    else
                    {
                        m_Char.m_QuestSystem.ReciveQuest(activeDialog.quest);
                        Debug.Log("You recived a quest");
                    }
                }
            }

            LoadDialogOptions();

        }
    }

    private void LoadDialogOptions()
    {
        foreach(GameObject currentButton in optionButtons)
        {
            currentButton.gameObject.SetActive(false);
        }

        activeDialog = null;
        endInteractionButton.SetActive(true);
        dialogButton.SetActive(false);

        GameObject[] buttons = optionButtons;

        for(int i = 0; i < currentDialogOptions.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);


            buttons[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = currentDialogOptions[i].dialogTitle;
        }

        

    }

    public void SelectOption(int index)
    {
        dialogButton.SetActive(true);
        endInteractionButton.SetActive(false);
        activeSentences.Clear();
        
        if(index < currentDialogOptions.Count)
        {
            activeDialog = currentDialogOptions[index];

            foreach(string sentence in currentDialogOptions[index].sentences)
            {
                activeSentences.Enqueue(sentence);
            }

            NextSentence();

        }

        foreach (GameObject currentButton in optionButtons)
        {
            currentButton.SetActive(false);
        }
    }

    public void NextSentence()
    {
        if (activeSentences.Count > 0) 
        {
            dialogText.text = activeSentences.Dequeue();
            
        }
        
    }



}
