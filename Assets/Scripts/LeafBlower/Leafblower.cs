using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class Leafblower : MonoBehaviour
{

    private List<Rigidbody> bodies;

    void Start()
    {
        bodies = new List<Rigidbody>();

    }

    void Update()
    {
        if (GetComponentInParent<LeafBlowerChar>().blowerAOE.enabled)
        {
            foreach(Rigidbody bodie in bodies)
            {
                //Calculating the distance to the body so it pushes less to the front and more up the farther the object it
                float distance = Mathf.Abs(bodie.transform.position.x - transform.position.x) + Mathf.Abs(bodie.transform.position.z - transform.position.z);

                //Increasing the distance to it has more impact
                distance = distance * 2;

                //This is a bit of a mess but basically the more distance the less x and z and the more y
                bodie.AddForce((new Vector3(this.transform.forward.x * (1 / distance), distance/28 , this.transform.forward.z * (1 / distance)))/4);

                //Adding some random torque but in the direction contrary to the player
                float xRotation = Random.value;
                //float yRotation = Random.Range(-1,1);
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
            //Need to change this so it only happens once 
            bodies = new List<Rigidbody>();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //Only push on rigidbodies
        if (other.attachedRigidbody != null)
        {
            Rigidbody currentBody = other.attachedRigidbody;

            //Check for multiples entries of the same body
            if (!bodies.Contains(currentBody))
            {
                //We need to take different steps for leaves, it caused a problem when "composting" or destroying the leaf
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
                //Need to check that this is actually working, the TryGetComponent was giving me trouble
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
