using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{

    public class Ledge : MonoBehaviour
    {
        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //animator.SetTrigger("LedgeGrab");
                other.GetComponent<PlayerMovement>().TriggerLedgeAnim();  
            }
        }
    }
}
