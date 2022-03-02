using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    private float throwForce = 80f;
    private float throwableDetectionLength = 10;
    public Transform rightHand;
    public Transform fowardDirection;
    private bool holding;

    float timerAction = 0.5f;
    float lastTimeAction;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        lastTimeAction = Time.time;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GrabThrowHandler()
    {
        if (Time.time - lastTimeAction > timerAction)
        {
            if (!holding)
            {
                GrabThrowable();
                animator.SetTrigger("GrabAnim");
            }
            else
            {
                ThrowThrowable();
                animator.SetTrigger("ThrowAnim");
            }
            lastTimeAction = Time.time;
        }
    }

    public Collider GetDetectedThrowable()
    {
        Collider[] detectedThrowables = Physics.OverlapSphere(this.transform.position, this.throwableDetectionLength, LayerMask.GetMask("Throwable"));
        Collider closestThrowable = null;
        float minDistance = 10000;
        for (int i = 0; i < detectedThrowables.Length; i++)
        {
            float detectedDistance = Vector3.Distance(this.transform.position, detectedThrowables[i].transform.position);
            if (detectedDistance < minDistance)
            {
                minDistance = detectedDistance;
                closestThrowable = detectedThrowables[i];
                holding = true;
            }
        }
        return closestThrowable;
    }
    public void GrabThrowable()
    {
        this.GetDetectedThrowable().GetComponent<Throwable>().GrabBy(this.rightHand);
    }
    public void ThrowThrowable()
    {
        this.GetDetectedThrowable().GetComponent<Throwable>().ThrowTo(fowardDirection.position, this.throwForce);
        holding = false;
    }
}

