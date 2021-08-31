using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Leafblower : MonoBehaviour
{
    private List<Rigidbody> bodies;

    // Start is called before the first frame update
    void Start()
    {
        bodies = new List<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<ThirdPersonCharacter>().blowerAOE.enabled)
        {
            foreach(Rigidbody bodie in bodies)
            {
                bodie.AddForce(new Vector3(this.transform.forward.x, this.transform.forward.y + 1, this.transform.forward.z));
            }
            
        }
        else
        {
            bodies = new List<Rigidbody>();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            Rigidbody currentBody = other.attachedRigidbody;

            if (!bodies.Contains(currentBody))
            {
                bodies.Add(currentBody);
            }
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            Rigidbody currentBody = other.attachedRigidbody;

            if (bodies.Contains(currentBody))
            {
                bodies.Remove(currentBody);
            }
        }
    }
}
