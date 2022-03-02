using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public Rigidbody rb;
    private BoxCollider collider;
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    void Update()
    {

    }

    private void MakeKinematic()
    {
        rb.isKinematic = true;
    }
    private void DisableCollider()
    {
        collider.isTrigger = true;
    }
    private void EnableCollider()
    {
        collider.isTrigger = false;
    }
    private void MakeActive()
    {
        rb.isKinematic = false;
    }
    public bool IsKinematic()
    {
        return rb.isKinematic;
    }

    private bool isGrabbable = true;
    public bool IsGrabbable => this.isGrabbable;
    public void GrabBy(Transform grabber)
    {
        this.MakeKinematic();
        DisableCollider();
        this.transform.SetParent(grabber);
        this.transform.localPosition = new Vector3(0, 0, 0);
        this.isGrabbable = false;
    }
    public void ThrowTo(Vector3 target, float throwForce)
    {
        this.transform.SetParent(null);
        this.MakeActive();
        EnableCollider();
        Vector3 direction = (target + Vector3.up * 2) - this.transform.position;
        this.AddForce(direction, throwForce);
        this.isGrabbable = true;
    }
    public void AddForce(Vector3 direction, float forceAmount)
    {
        rb.AddForce(direction.normalized * forceAmount, ForceMode.Impulse);
    }
    //public bool IsGrabbable()
    //{
    //    return this.grabbable;
    //}
}