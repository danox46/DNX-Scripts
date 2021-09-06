using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexibleObject : Leaf
{
    Rigidbody m_Rigidbody;
    Cloth m_Cloth;
    [SerializeField] float maxFlexibility;
    ClothSkinningCoefficient[] constraints;
    ClothSkinningCoefficient[] initialConstraint;
    [SerializeField] float flexibilityMultiplier;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        m_Rigidbody = GetComponent<Rigidbody>();
        m_Cloth = GetComponent<Cloth>();
        initialConstraint = m_Cloth.coefficients;
    }

    // Update is called once per frame
    protected void Update()
    {
        //The flexible object displays it mesh as a cloth but has a regular RigidBody and collider on the same game object to work as skeleton and allow wide range collisions
        //If the object is moving, as in it recived any force
        if(m_Rigidbody.velocity.magnitude > 0.05)
        {
            //Take the base constraints from the cloth component
            constraints = m_Cloth.coefficients;
            
            for(int i = 0; i <= constraints.Length - 1; i++)
            {
                // setting a constraint to 0 means it should always be locked, at least one contraint should be always locked 
                if(constraints[i].maxDistance > 0)
                {
                    //Each constraint is loosen based on the speed of the body, the initial value for extra customization of movement, and a flexibility multiplier to control the speed at which the constraint lossens
                    constraints[i].maxDistance = constraints[i].maxDistance * m_Rigidbody.velocity.magnitude * flexibilityMultiplier;

                    //Adjust the maximun flexibilty we want
                    if (constraints[i].maxDistance > maxFlexibility)
                        constraints[i].maxDistance = maxFlexibility;

                }
            }

            //Apply the new constraints to the cloth
            m_Cloth.coefficients = constraints;

        }
        else
        {
            //if the object is not moving anymore it will return to its initial shape
            m_Cloth.coefficients = initialConstraint;
        }
        
    }



}
