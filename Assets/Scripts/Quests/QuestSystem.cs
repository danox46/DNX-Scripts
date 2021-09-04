using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Quest
{
    public string questName;
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
    public List<Quest> activeQuests;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReciveQuest()
    {

    }

    public void CheckQuests(QuestItem newItem)
    {
        foreach (Quest currentQuest in activeQuests)
        {
            if(currentQuest.requirementCode == newItem.ItemCode)
            {
                Debug.Log("You got an item, deliver it to " + currentQuest.finishNPC.GivenName);
                MarkQuestReady(currentQuest);
            }
        }
    }



    public void MarkQuestReady(Quest currentQuest)
    {
        currentQuest.readyForDeliver = true;
    }
}
