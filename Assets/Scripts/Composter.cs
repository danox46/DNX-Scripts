using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composter : MonoBehaviour
{
    private List<Leaf> leaves;

    [SerializeField] private int requiredCompost;
    [SerializeField] private float compostTime;
    public float compostTimer;

    [SerializeField] private int CompostCounter;

    // Start is called before the first frame update
    void Start()
    {
        leaves = new List<Leaf>();
        compostTimer = compostTime;
        CompostCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (leaves.Count > 0)
        {
            compostTimer -= Time.deltaTime;

            if (compostTimer <= 0)
            {
                List<Leaf> auxLeaves = new List<Leaf>(leaves);

                foreach (Leaf currentLeaf in auxLeaves)
                {
                    currentLeaf.compostingTime++;
                    if (currentLeaf.compostingTime >= requiredCompost)
                    {
                        CompostCounter++;
                        leaves.Remove(currentLeaf);
                        currentLeaf.SetForDestruction();
                    }
                }

                compostTimer = compostTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Leaf")
        {
            if (!leaves.Contains(other.GetComponent<Leaf>()))
            {
                leaves.Add(other.GetComponent<Leaf>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Leaf")
        {
            if (leaves.Contains(other.GetComponent<Leaf>()))
            {
                leaves.Remove(other.GetComponent<Leaf>());
            }
        }
    }
}
