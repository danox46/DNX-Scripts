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

    //Expand the destroy function to remove body leaf from blower list, and make it so it's not added
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
    }

    public void SetBlowerOutOfRange()
    {
        blowerOnRange = null;
    }
}
