using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void MarkQuestReady(Quest currentQuest)
    {
        currentQuest.readyForDeliver = true;
    }

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
            completedQuests.Add(finishedQuest);

            if (activeQuests.Contains(finishedQuest))
            {
                activeQuests.Remove(finishedQuest);
            }

            Debug.Log("Award reward from quest here");
        }
    }
}
