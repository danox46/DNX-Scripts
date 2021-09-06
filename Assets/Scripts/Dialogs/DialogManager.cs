using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//need to move this to a separate file
//All the information for each dialog option to be triggered by NPCs
[System.Serializable]
public class Dialog
{
    [TextArea(1, 3)]
    public string[] sentences;

    //The title will be used as the text to call on the dialog option during an engagement
    public string dialogTitle;

    //Dialogs can optionally be quests and contain the needed info
    public bool isQuest;
    public Quest quest;
}

public class DialogManager : MonoBehaviour
{
    //Dialog control
    private List<Dialog> currentDialogOptions;
    private Queue<string> activeSentences;
    private Dialog activeDialog;
    
    //Participants
    private NPC m_Npc;
    [SerializeField] private LeafBlowerChar m_Char;

    //UI management
    public GameObject dialogBox;
    public Text dialogText;
    public GameObject dialogButton;
    public GameObject endInteractionButton;
    public GameObject[] optionButtons;

    // Start is called before the first frame update
    void Start()
    {
        currentDialogOptions = new List<Dialog>();
        activeSentences = new Queue<string>();
    }

    //This can be called by the player when there's an NPC in range
    public void LaunchDialogSequence(NPC npc)
    {
        //The char will pass the NPC in range for general use in the class
        m_Npc = npc;

        //Getting Dialogs from NPC
        currentDialogOptions = npc.Dialogs;

        //Telling chars not to move
        npc.SetNPCEngaged(m_Char.transform);
        m_Char.EngageChar();

        //Loading Engagement info into the UI
        activeSentences.Clear();
        dialogText.text = npc.greeting;

        dialogBox.SetActive(true);
        dialogButton.SetActive(false);

        LoadDialogOptions();

        

    }

    //This is called from the Main canvas
    public void EndDialogSequence()
    {

        m_Npc.DisengageNPC();
        m_Char.DisengageChar();

        activeSentences.Clear();

        //Clearing UI to initial state

        foreach (GameObject currentButton in optionButtons)
        {
            currentButton.SetActive(false);
        }

        dialogBox.SetActive(false);
    }

    //The multiporpouse continue buttom
    public void DialogButton()
    {
        //If there are more sentences in the conversation load the next sentence
        if(activeSentences.Count > 0)
        {
            NextSentence();
        }
        else //if the conversation ended 
        {
            if (activeDialog != null) 
            {
                //Check if the conversation was a quest
                if (activeDialog.isQuest)
                {
                    //Check if the quest is not set as completed
                    if (!m_Char.m_QuestSystem.IsCompleted(activeDialog.quest))
                    {
                        if (m_Char.m_QuestSystem.CheckItems(activeDialog.quest))
                        {
                            //If the player has the needed item Set the quest as finished
                            UpdateQuestDialog(true);
                            m_Char.m_QuestSystem.FinishQuest(activeDialog.quest);
                            SelectOption(currentDialogOptions.IndexOf(activeDialog));
                            m_Npc.availableDialogs.Remove(activeDialog);

                            //Break the function early to override regular option load and jump to quest completion dialog
                            return;
                        }
                        else
                        {
                            //Or just add the quest to the quest list
                            UpdateQuestDialog(false);
                            m_Char.m_QuestSystem.ReciveQuest(activeDialog.quest);
                            Debug.Log("You recived a quest");
                        }
                    }
                }
            }

            LoadDialogOptions();

        }
    }

    private void LoadDialogOptions()
    {
        //Clear the UI
        foreach(GameObject currentButton in optionButtons)
        {
            currentButton.gameObject.SetActive(false);
        }

        activeDialog = null;
        endInteractionButton.SetActive(true);
        dialogButton.SetActive(false);

        GameObject[] buttons = optionButtons;

        //Getting the current Dialog options loaded on launch sequence
        for(int i = 0; i < currentDialogOptions.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);

            //Update button with trigger text
            buttons[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = currentDialogOptions[i].dialogTitle;
        }

    }

    private void UpdateQuestDialog(bool finished)
    {
        //If a dialog is a quest, it will change twice, once for delivery, and once for completion
        int dialogIndex = m_Npc.availableDialogs.IndexOf(activeDialog);

        if (finished)
        {
            m_Npc.availableDialogs[dialogIndex].dialogTitle = activeDialog.quest.questName;
            m_Npc.availableDialogs[dialogIndex].sentences = activeDialog.quest.completedDialog;
        }
        else
        {
            m_Npc.availableDialogs[dialogIndex].dialogTitle = "Deliver";
            m_Npc.availableDialogs[dialogIndex].sentences = activeDialog.quest.expectingDialog;
        }
    }

    public void SelectOption(int index)
    {
        //Load the selected dialog into the UI
        dialogButton.SetActive(true);
        endInteractionButton.SetActive(false);
        activeSentences.Clear();
        
        if(index < currentDialogOptions.Count)
        {
            activeDialog = currentDialogOptions[index];

            //Loading active sentences
            foreach(string sentence in currentDialogOptions[index].sentences)
            {
                activeSentences.Enqueue(sentence);
            }

            NextSentence();

        }

        //Clearing UI
        foreach (GameObject currentButton in optionButtons)
        {
            currentButton.SetActive(false);
        }
    }

    public void NextSentence()
    {
        if (activeSentences.Count > 0) 
        {
            //As a sentence is displayed its also removed from the active sentences
            dialogText.text = activeSentences.Dequeue();
            
        }
        
    }



}
