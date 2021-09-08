using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Need to move this to a new file
//Separate quest info from quest item for inv management ease
[System.Serializable]
public class QuestItemInfo
{
    public int m_Code;

    public QuestItemInfo(QuestItemInfo itemInfo)
    {
        m_Code = itemInfo.m_Code;
    }

}

//This needs to be added to the quest item
public class QuestItem : MonoBehaviour
{
    [SerializeField] private float turnSpeed;
    public GameObject interactionClue;
    public QuestItemInfo questItemInfo;

    public void SetForDestruction()
    {
        GameObject.Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, turnSpeed, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        //Allows interaction when in range
        if (other.tag == "Player")
        {

            if (!interactionClue.activeSelf)
            {
                other.GetComponent<LeafBlowerChar>().SetItemInRange(this);
                //The set in range is working but there's a problem with interaction clue
                interactionClue.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (interactionClue.activeSelf)
            {
                other.GetComponent<LeafBlowerChar>().SetItemInRange(null);
                interactionClue.SetActive(false);
            }
        }
    }
}
