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

                float distance = Mathf.Abs(bodie.transform.position.x - transform.position.x) + Mathf.Abs(bodie.transform.position.z - transform.position.z);

                distance = distance * 2;

                bodie.AddForce((new Vector3(this.transform.forward.x * (1 / distance), this.transform.forward.y + (distance / 6f), this.transform.forward.z * (1 / distance)))/4);

                bodie.AddTorque(new Vector3(Random.value, Random.value, Random.value));
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
