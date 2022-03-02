using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoundScript : MonoBehaviour
{
    AudioSource audioSrc;
    public Zombie zombieAi;
    //NavMeshAgent zombieAgent;
    //Animator zombieAnim;
    public float timeToPlayAudioSrc;
    float maxSpeed;
    public List<Collider> awareZombiesColliders;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        timeToPlayAudioSrc = Random.Range(3, 8);
        //zombieAgent = zombieAi.GetComponent<NavMeshAgent>();
        //zombieAnim = zombieAi.GetComponent<Animator>();
        maxSpeed = zombieAi.GetComponent<NavMeshAgent>().speed;
    }
    private void MakeSound()
    {
        audioSrc.PlayOneShot(audioSrc.clip);
        Collider[] zombiesHitColliders = Physics.OverlapSphere(this.transform.position, audioSrc.maxDistance, LayerMask.GetMask("Zombie"));
        foreach (Collider zCollider in zombiesHitColliders)
        {
            Zombie z = zCollider.GetComponent<Zombie>();
            if (z)
                z.AddSoundHeard(this.transform.position);

            //if (!this.awareZombiesColliders.Contains(zCollider))
            //    this.awareZombiesColliders.Add(zCollider);
        }
        //if (this.awareZombiesColliders.Count > 0)
        //{
        //    //Who is the zombie that heard the sound? I.Don't.Care!
        //    foreach (Collider zombieCollider in this.awareZombiesColliders)
        //    {
        //        zombieCollider.GetComponent<NavMeshAgent>().SetDestination(this.transform.position);
        //    }
        //}
    }

    public List<Collider> getZombieCollliders()
    {
        return this.awareZombiesColliders;
    }
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) //OR CAN BE ANYTHING ACTUALLY!!!
        //{
            this.MakeSound();
        //}
    }

    public void MakeSoundForZombie()
    {
        this.MakeSound();
    }
}
