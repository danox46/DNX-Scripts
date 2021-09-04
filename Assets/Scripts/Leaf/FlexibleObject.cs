using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexibleObject : Leaf
{
    Rigidbody m_Rigidbody;
    Cloth m_Cloth;
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
    protected override void Update()
    {
        base.Update();

        if(m_Rigidbody.velocity.magnitude > 0.05)
        {
            constraints = m_Cloth.coefficients;
            
            for(int i = 0; i <= constraints.Length - 1; i++)
            {
                if(constraints[i].maxDistance > 0)
                {
                    constraints[i].maxDistance = constraints[i].maxDistance * m_Rigidbody.velocity.magnitude * flexibilityMultiplier;

                    if (constraints[i].maxDistance > 0.2)
                        constraints[i].maxDistance = 0.2f;

                }
            }

            m_Cloth.coefficients = constraints;

        }
        else
        {
            m_Cloth.coefficients = initialConstraint;
        }
        
    }



}
