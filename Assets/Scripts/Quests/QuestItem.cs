using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestItemInfo
{
    public int m_Code;

    public QuestItemInfo(QuestItemInfo itemInfo)
    {
        m_Code = itemInfo.m_Code;
    }

}

public class QuestItem : MonoBehaviour
{
    
    public GameObject interactionClue;
    public QuestItemInfo questItemInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetForDestruction()
    {
        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (!interactionClue.activeSelf)
            {
                other.GetComponent<LeafBlowerChar>().SetItemInRange(this);
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
