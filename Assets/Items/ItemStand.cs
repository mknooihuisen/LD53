using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStand : MonoBehaviour
{
    public GameObject itemModel;

    public GameObject itemOnStand;

    public Item item;

    public ParticleSystem poofer;

    public bool itemTaken { get; private set; }

    // Use this if the item's position on the stand needs to be modified - added
    public Vector3 positionCorrection = new Vector3(0f, 0f, 0f);

    // Use this if the scale needs to be modified - multiplied
    public Vector3 scaleCorrection = new Vector3(1f, 1f, 1f);

    // Time to wait before particle effect
    private readonly float timeToWait1 = 1f;

    // Time to wait before item reappears
    private readonly float timeToWait2 = 2f;

    private float timeSinceTaken = 0f;

    void Awake()
    {
        poofer.Pause();
    }

    void Start()
    {
        

        if (itemModel != null)
        {
            Transform t = this.gameObject.transform;
            itemOnStand = Instantiate(itemModel, 
                new Vector3(0f + positionCorrection.x, 1.5f + positionCorrection.y, 0f + positionCorrection.z), 
                Quaternion.identity, t);
            itemOnStand.transform.localScale = new Vector3(
                (1f/t.localScale.x) * itemModel.transform.localScale.x * scaleCorrection.x, 
                (1f/t.localScale.y) * itemModel.transform.localScale.y * scaleCorrection.y, 
                (1f/t.localScale.z) * itemModel.transform.localScale.z * scaleCorrection.z);
        }

        TakeItem();
    }

    void Update()
    {
        if (itemTaken)
        {
            timeSinceTaken += Time.deltaTime;

            if (timeSinceTaken > timeToWait1)
            {
                Poof();
            }

            if (timeSinceTaken > timeToWait2)
            {
                itemOnStand.GetComponent<MeshRenderer>().enabled = true;
                itemTaken = false;
            }
        }
    }

    public void Poof()
    {
        poofer.Play();
    }

    public void TakeItem()
    {
        itemOnStand.GetComponent<MeshRenderer>().enabled = false;
        itemTaken = true;
        timeSinceTaken = 0f;
    }
}