using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class Leafblower : MonoBehaviour
{
    public float YSensitivity = 2f;
    private Quaternion m_BlowerTargetRot;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool clampVerticalRotation = true;
    public bool smooth;
    public float smoothTime = 5f;

    private List<Rigidbody> bodies;

    // Start is called before the first frame update
    void Start()
    {
        bodies = new List<Rigidbody>();
        m_BlowerTargetRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<FirstPersonController>().blowerAOE.enabled)
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

                /*if(parentTransform.position.x < bodie.position.x)
                {
                    if(parentTransform.position.z < bodie.transform.position.z)
                    {

                    }
                    else
                    {

                    }
                }
                else
                {

                }*/


                bodie.AddTorque(new Vector3(xRotation, 0, zRotation));
            }
            
        }
        else
        {
            bodies = new List<Rigidbody>();
        }

        LookAtYLocation();
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

    public void LookAtYLocation()
    {
        float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

        m_BlowerTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

        if (clampVerticalRotation)
            m_BlowerTargetRot = ClampRotationAroundXAxis(m_BlowerTargetRot);

        if (smooth)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, m_BlowerTargetRot,
                smoothTime * Time.deltaTime);
        }
        else
        {
            transform.localRotation = m_BlowerTargetRot;
        }

    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
