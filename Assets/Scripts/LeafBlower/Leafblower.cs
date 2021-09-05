using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

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
        if (GetComponentInParent<LeafBlowerChar>().blowerAOE.enabled)
        {
            foreach(Rigidbody bodie in bodies)
            {
                float distance = Mathf.Abs(bodie.transform.position.x - transform.position.x) + Mathf.Abs(bodie.transform.position.z - transform.position.z);

                distance = distance * 2;

                bodie.AddForce((new Vector3(this.transform.forward.x * (1 / distance), distance/28 , this.transform.forward.z * (1 / distance)))/4);

                float xRotation = Random.value;
                float yRotation = Random.Range(-1,1);
                float zRotation = Random.value;

                Transform parentTransform = GetComponentInParent<Transform>();


                if(parentTransform.position.x > bodie.position.x)
                {
                    zRotation = -zRotation;
                    
                }

                if(parentTransform.position.z > bodie.position.z)
                {
                    xRotation = -xRotation;
                }


                bodie.AddTorque(new Vector3(xRotation, 0, zRotation));
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

                if (currentBody.tag == "Leaf")
                {
                    Leaf newLeaf = currentBody.GetComponent<Leaf>();

                    newLeaf.SetBlowerOnRange(this);

                    if(!newLeaf.setToDestroy)
                        bodies.Add(currentBody);
                }
                else
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

                if(TryGetComponent<FlexibleObject>(out FlexibleObject newLeaf))
                {
                    newLeaf.SetBlowerOutOfRange();
                }

                bodies.Remove(currentBody);
            }
        }
    }



    public void RemoveLeaf(Rigidbody leaf)
    {
        if (bodies.Contains(leaf))
        {
            bodies.Remove(leaf);
        }
    }


}
