using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public Leafblower blowerOnRange;
    public int compostingTime;
    public bool setToDestroy;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        compostingTime = 0;
        setToDestroy = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void SetForDestruction()
    {
        if (!setToDestroy)
        {
            setToDestroy = true;
            if (blowerOnRange != null)
            {
                Debug.Log("Removing leaf from list beacasue of destroy");
                blowerOnRange.RemoveLeaf(GetComponent<Rigidbody>());
            }

            GameObject.Destroy(gameObject, 1);
        }
    }

    public void SetBlowerOnRange(Leafblower blower)
    {
        blowerOnRange = blower;
        Debug.Log("Blower is on Range");
    }

    public void SetBlowerOutOfRange()
    {
        Debug.Log("blower is out of range");
        blowerOnRange = null;
    }
}
