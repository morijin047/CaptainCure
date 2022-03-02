using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    
    private MeshCollider[] breakableColliders;
    private Rigidbody[] breakableRbs;
    private Collider collider;
    public AudioSource woodBreakAudio;

    private void Awake()
    {
        this.breakableColliders = this.gameObject.GetComponentsInChildren<MeshCollider>();
        this.breakableRbs = this.gameObject.GetComponentsInChildren<Rigidbody>();
        this.collider = this.GetComponent<Collider>(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private bool isBreakable = true;
    public bool IsBreakable => this.isBreakable;
    public void Break()
    {
        foreach (MeshCollider meshCollider in this.breakableColliders)
        {
            meshCollider.convex = true;
        }
        foreach (Rigidbody rigidbody in this.breakableRbs)
        {
            rigidbody.isKinematic = false;
            this.collider.enabled = false;
        }
        this.woodBreakAudio.PlayDelayed(.005f);
        this.isBreakable = false;
        //this.gameObject.layer = LayerMask.NameToLayer("Default");
    }



  
}
