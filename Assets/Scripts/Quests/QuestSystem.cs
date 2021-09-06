using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Need to move this to a new file
//Individual quest information
[System.Serializable]
public struct Quest
{
    public string questName;

    [TextArea(1, 3)]
    public string questDescription;

    public int requirementCode;
    public int rewardCode;
    public bool readyForDeliver;
    public bool completed;
    public NPC triggerNPC;
    public NPC finishNPC;
    public string[] expectingDialog;
    public string[] completedDialog;
}

public class QuestSystem : MonoBehaviour
{
    [SerializeField] private List<Quest> activeQuests;
    [SerializeField] private List<QuestItemInfo> questInventory;
    private List<Quest> completedQuests;

    // Start is called before the first frame update
    void Start()
    {
        completedQuests = new List<Quest>();
        questInventory = new List<QuestItemInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReciveQuest(Quest currentQuest)
    {
        if (!activeQuests.Contains(currentQuest))
        {
            activeQuests.Add(currentQuest);
            Debug.Log("I just got a quest");
        }
    }

    //Checking if an Item fullfils an active quest
    public bool CheckQuests(QuestItem newItem)
    {
        foreach (Quest currentQuest in activeQuests)
        {
            if(currentQuest.requirementCode == newItem.questItemInfo.m_Code)
            {
                Debug.Log("You got an item, deliver it to " + currentQuest.finishNPC.GivenName);
                return true;
            }
        }

        return false;
    }

    //Checking if a quest if fullfiled by an intem in the inventory
    public bool CheckItems(Quest newQuest)
    {
        foreach (QuestItemInfo currentItem in questInventory)
        {
            if (currentItem.m_Code == newQuest.requirementCode)
            {
                Debug.Log("I already have that item");
                return true;
            }
        }

        return false;
    }

    //I think I'm not using this one
    public void MarkQuestReady(Quest currentQuest)
    {
        currentQuest.readyForDeliver = true;
    }

    //Exchange the full GameObejct with an intance of it's info on the inventory
    public void LootItem(QuestItem item)
    {
        if (!questInventory.Contains(item.questItemInfo))
        {
            questInventory.Add(new QuestItemInfo(item.questItemInfo));
            item.SetForDestruction();
        }
    }

    public void FinishQuest(Quest finishedQuest)
    {
        if (!completedQuests.Contains(finishedQuest))
        {
            //Adding quest to completed list and taking it from active quests
            completedQuests.Add(finishedQuest);

            if (activeQuests.Contains(finishedQuest))
            {
                activeQuests.Remove(finishedQuest);
            }

            //Starts on a negative to check if it failed
            int itemIndex = -1;

            foreach(QuestItemInfo item in questInventory)
            {
                if(item.m_Code == finishedQuest.requirementCode)
                {
                    itemIndex = questInventory.IndexOf(item);
                }
            }

            //Remove the item that fullfilled the quest
            if(itemIndex >= 0)
            {
                questInventory.RemoveAt(itemIndex);
            }

            //Do whatever was defined as a reward
            ClaimReward(finishedQuest.rewardCode);
        }
    }

    private void ClaimReward(int rewardCode)
    {
        //Reward options
        switch (rewardCode)
        {
            case 0:
                GetComponent<LeafBlowerChar>().coins += 5;
                Debug.Log("I got 5 coins");
                break;
        }
    }

    public bool IsCompleted(Quest currentQuest)
    {
        if (completedQuests.Contains(currentQuest))
            return true;
        else 
            return false;
    }
}
