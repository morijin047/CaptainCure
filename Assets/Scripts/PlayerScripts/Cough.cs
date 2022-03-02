using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cough : MonoBehaviour
{
    public float timerRetry = 5f;
    float lastTime;
    Animator animator;
    AudioSource audioSrc;
    public AudioClip cough;
    public Transform zombiePos;
    public float DistanceBetween;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        lastTime = Time.time;
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTime > timerRetry )
        {
            DistanceBetween = Vector3.Distance(transform.position, zombiePos.position);
            if (DistanceBetween > 30)
                DistanceBetween = 30;

            int rng = Random.Range(1, 101);
            if (rng <= DistanceBetween)
            {
                animator.SetTrigger("Cough");
                audioSrc.clip = cough;
                audioSrc.maxDistance = 3000f;
                gameObject.GetComponent<SoundScript>().MakeSoundForZombie();
            }
            lastTime = Time.time;
        }
    }
}
