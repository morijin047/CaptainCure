//using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public float InitialMaxSpeed { private set; get; }
    public float InitialAcceleration { private set; get; }
    public float DistanceToAttack { private set; get; } = 2.0f;
    public bool SeePlayer { get; set; } = false;
    public bool InChaseMode { get; set; } = false;
    public float TimeToWanderAfterLosingSightOfPlayer { get; } = 2.0f;
    private float throwForce { get; } = 450.0f; //150

    public Transform player;
    public NavMeshAgent zombieAgent;
    public Animator zombieAnim;
    public Collider zombieCollider;

    BehaviorTree behaviorTree;
    Vector3 randomPlaceToThrowObject;
    private void Awake()
    {
        instance = this;
        this.behaviorTree = this.GetComponent<BehaviorTree>();
        this.InitialMaxSpeed = zombieAgent.speed;
        this.InitialAcceleration = zombieAgent.acceleration;
        this.normalAngleOfVision = this.AngleOfVision;
        randomPlaceToThrowObject = new Vector3(Random.Range(-250, 250), 0, Random.Range(-250, 250));
    }


    float currentTime = 0;
    float lastPlayedSound = 0;
    float timeToPlaySound = 4;
    private bool TimeToPlaySound => this.currentTime - this.lastPlayedSound > this.timeToPlaySound;
    void Update()
    {
        behaviorTree.RestartWhenComplete = true; //when not using repeater node after entry node
        this.DrawDetectionLine();
        this.DrawFieldOfView();
        this.PlayMovementAnim();

        this.currentTime += Time.deltaTime;
        if (this.TimeToPlaySound)
        {
            this.zombieSound.Play();
            this.lastPlayedSound = this.currentTime;
            this.currentTime = 0;
        }
    }



    #region UTILITIES
    private float DetectionDistance { get; } = 25.0f;
    public float AngleOfVision { get; set; } = 90.0f;
    private float normalAngleOfVision;
    public LineRenderer detectionDistanceLine;
    public LineRenderer leftAngleOfVisionLine;
    public LineRenderer rightAngleOfVisionLine;
    private void DrawFieldOfView()
    {
        //leftAngleOfVisionLine.SetPosition(0, this.transform.position);
        Vector3 leftLine = new Vector3(-Mathf.Sin(0.5f * this.AngleOfVision * Mathf.PI / 180), 0, Mathf.Cos(0.5f * this.AngleOfVision * Mathf.PI / 180)); // z is the cos axis, we rotate from z
        //Debug.DrawRay(this.transform.position, 10 * leftLine);
        leftAngleOfVisionLine.SetPosition(1, leftLine * this.DetectionDistance);

        //rightAngleOfVisionLine.SetPosition(0, this.transform.position);
        Vector3 rightLine = new Vector3(Mathf.Sin(0.5f * this.AngleOfVision * Mathf.PI / 180), 0, Mathf.Cos(0.5f * this.AngleOfVision * Mathf.PI / 180));
        //Debug.DrawRay(this.transform.position, 10 * rightLine);
        rightAngleOfVisionLine.SetPosition(1, rightLine * this.DetectionDistance);
    }
    private void DrawDetectionLine()
    {
        detectionDistanceLine.SetPosition(0, this.transform.position);
        detectionDistanceLine.SetPosition(1, this.transform.position + this.transform.forward * this.DetectionDistance);
    }
    public float GetDectectionDistance()
    {
        return this.DetectionDistance;
    }
    public void SetAngleOfVisionToTotal()
    {
        this.AngleOfVision = 360.0f;
    }
    public void ResetAngleOfVisionToNormal()
    {
        this.AngleOfVision = this.normalAngleOfVision;
    }
    public bool CanSeeWall()
    {
        Vector3 directionToPlayer = player.position - this.transform.position;
        bool canSeeWall = false;
        if (this.InChaseMode)
        {
            RaycastHit rch;
            if (Physics.Raycast(this.transform.position, directionToPlayer, out rch))
            {
                if (rch.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    canSeeWall = true;
                }
            }
        }
        else
        {
            RaycastHit[] rchs = Physics.RaycastAll(this.transform.position, directionToPlayer.normalized, directionToPlayer.magnitude);
            foreach (RaycastHit rch in rchs)
            {
                if (rch.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    canSeeWall = true;
                }
            }
        }
        return canSeeWall;
    }
    public float DistanceToPlayer()
    {
        return Vector3.Distance(player.position, this.transform.position);
    }
    public float AngleToPlayer()
    {
        Vector3 directionToPlayer = player.position - this.transform.position;
        float angle = Vector3.Angle(directionToPlayer, this.transform.forward);
        return angle;
    }
    #endregion



    #region THROWABLE
    public Transform rightHand;
    public bool DetectThrowable => this.HasDetectedThrowable() /*!= null*/ && this.DetectedThrowable.IsGrabbable;
    private float ThrowableDetectionLength { get; } = 2.0f;
    public Throwable DetectedThrowable { set; get; } = null;
    public bool HasDetectedThrowable()
    {
        Collider[] detectedThrowables = Physics.OverlapSphere(this.transform.position, this.ThrowableDetectionLength, LayerMask.GetMask("Throwable"));
        //Collider closestThrowable = null;
        float minDistance = 10000;
        for (int i = 0; i < detectedThrowables.Length; i++)
        {
            float detectedDistance = Vector3.Distance(this.transform.position, detectedThrowables[i].transform.position);
            if (detectedDistance < minDistance)
            {
                minDistance = detectedDistance;
                //closestThrowable = detectedThrowables[i];
                this.DetectedThrowable = detectedThrowables[i].GetComponent<Throwable>();
            }
        }
        if (this.DetectedThrowable)
            return true;
        return false;
        //return closestThrowable;
    }
    public void GrabThrowableToPlayer()
    {
        this.DetectedThrowable.GrabBy(this.rightHand);
        StartCoroutine(this.WaitForTurnBeforeThrowTo(player.transform.position));
    }
    public void ThrowThrowableToPlayer()
    {
        this.DetectedThrowable.ThrowTo(player.transform.position, this.throwForce);
    }

    public void GrabThrowableToRandomPlace()
    {
        this.DetectedThrowable.GrabBy(this.rightHand);
        StartCoroutine(this.WaitForTurnBeforeThrowTo(randomPlaceToThrowObject));
    }
    public void ThrowThrowableToRandomPlace()
    {
        this.DetectedThrowable.ThrowTo(randomPlaceToThrowObject, this.throwForce);
        randomPlaceToThrowObject = new Vector3(Random.Range(-250, 250), 0, Random.Range(-250, 250));
    }

    //private bool isFacingCorrectly()
    //{
    //    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 15 * Time.deltaTime);
    //    return false;
    //}
    IEnumerator WaitForTurnBeforeThrowTo(Vector3 target)
    {
        this.zombieAgent.enabled = false;
        Vector3 toTarget = target - this.transform.position;
        while (!(Vector3.Angle(this.transform.forward, toTarget) <= 5))
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(toTarget), 5 * Time.deltaTime);
            //Debug.Log(string.Format("Current: {0}, LookRot: {1}", transform.rotation.eulerAngles, Quaternion.LookRotation(toPlayer).eulerAngles));
            yield return null;
        }
        //Debug.Log("Angle found");
        //OR yield return new WaitUntil(isFacingCorrectly);
        this.zombieAgent.enabled = true;
    }
    //public bool DetectThrowable()
    //{
    //    return this.GetDetectedThrowable() != null && this.GetDetectedThrowable().GetComponent<Throwable>().IsGrabbable();
    //}
    public void PlayThrowAnimToPlayer()
    {
        this.zombieAnim.SetTrigger("Throw");
    }
    public void PlayThrowAnimToRandomPlace()
    {
        this.zombieAnim.SetTrigger("ThrowRandom");
    }
    private void PlayMovementAnim()
    {
        this.zombieAnim.SetFloat("Speed", Mathf.Lerp(0, 1, zombieAgent.velocity.magnitude / InitialMaxSpeed));
    }
    #endregion



    #region BREAKABLE
    public bool DetectBreakable => this.HasDetectedBreakable() /*!= null */&& this.DetectedBreakable.IsBreakable;
    private float breakableDetectionLength = 10;
    public Breakable DetectedBreakable { set; get; } = null;
    public bool HasDetectedBreakable()
    {
        Collider[] detectedBreakables = Physics.OverlapSphere(this.transform.position, this.breakableDetectionLength, LayerMask.GetMask("Breakable"));
        //Collider closestBreakableObject = null;
        float minDistance = 10000;
        for (int i = 0; i < detectedBreakables.Length; i++)
        {
            float detectedDistance = Vector3.Distance(this.transform.position, detectedBreakables[i].transform.position);
            if (detectedDistance < minDistance)
            {
                minDistance = detectedDistance;
                //closestBreakableObject = detectedBreakables[i];
                this.DetectedBreakable = detectedBreakables[i].GetComponent<Breakable>();
            }
        }
        if (this.DetectedBreakable)
            return true;
        return false;
        //return closestBreakableObject;
    }
    //public void AnimationFinished(AnimationCallbackType animType)  //3rd method for letting break anim run once (exist edge case when transition)
    //{
    //    if (animType == AnimationCallbackType.BreakAnim)
    //    {
    //        this.zombieAgent.enabled = true;
    //    }
    //}

    //bool isInBreakingAnim = false;
    public void PlayBreakAnim()
    {
        this.zombieAnim.SetTrigger("Break");
    }
    public void BreakBreakable()
    {
        this.zombieAnim.ResetTrigger("Break");
        this.zombieAgent.enabled = false;
        this.DetectedBreakable.Break();
        this.zombieAgent.enabled = true;
        //StartCoroutine(WaitFor(1));
    }
    IEnumerator WaitFor(float second)
    {
        yield return new WaitForSeconds(second);
        this.zombieAgent.enabled = true;
        //behaviorTree.enabled = true; //2nd method for letting break anim run once
    }
    #endregion


    #region STUNNED
    private float stunImmunityEnd = 0;
    private float stunEnd = 0;
    private readonly float stunTime = 4;
    private readonly float immuneTime = 2;
    public bool IsStunned => Time.time < this.stunEnd;
    public bool CanBeStunned => Time.time > this.stunImmunityEnd;
    public AudioSource zombieSound;
    public void GetHit()
    {
        if (CanBeStunned)
        {
            this.zombieSound.Play();
            this.zombieAgent.enabled = false;
            this.zombieAnim.SetTrigger("Stunned");
            this.stunEnd = Time.time + this.stunTime;
            this.stunImmunityEnd = this.stunEnd + this.immuneTime;
            StartCoroutine(ToggleOnAgent(this.stunTime));
        }
    }
    IEnumerator ToggleOnAgent(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        this.zombieAgent.enabled = true;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
    //    {
    //        this.GetHit();
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
    //    {
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ThrowableByPlayer"))
        {
            this.GetHit();
        }
    }
    #endregion


    #region ATTACK
    public void PlayAttackAnim()
    {
        this.zombieAnim.SetTrigger("Attack");
    }
    //public void OnAttackAnimBegin()
    //{
    //    StartCoroutine(this.WaitForTurnBeforeAttack());
    //}
    //public IEnumerator WaitForTurnBeforeAttack()
    //{
    //    Vector3 toPlayer = player.transform.position - this.transform.position;
    //    while (!(Vector3.Angle(this.transform.forward, toPlayer) <= 5.0f))
    //    {
    //        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(toPlayer), 10 * Time.deltaTime);
    //        yield return null;
    //    }
    //}
    #endregion


    public void GetShot()
    {
        this.GetHit();
    }


    #region SOUNDABLE
    Vector3 lastSoundHeard;
    public bool heardSound { get; set; } = false;
    public float TimeHeardSound { get; private set; }
    public void AddSoundHeard(Vector3 position)
    {
        this.lastSoundHeard = position;
        this.TimeHeardSound = Time.time;
        this.heardSound = true;
    }
    public Vector3 GetSoundHeard()
    {
        return this.lastSoundHeard;
    }
    #endregion


    public void GoTo(Vector3 position)
    {
        if (this.zombieAgent.enabled)
            zombieAgent.SetDestination(position);
    }
    //public void Pursue(Transform target)
    //{
    //    Vector3 targetDir = target.position - this.transform.position;
    //    float targetSpeed = target.GetComponent<Rigidbody>().velocity.magnitude;
    //    float lookAhead = targetDir.magnitude / (this.zombieAgent.speed + targetSpeed);
    //    this.GoTo(target.position + target.forward * lookAhead * 20);
    //}




    //void MoveToPlayer()
    //{
    //    //Vector3 lookAtVector = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
    //    //this.transform.LookAt(lookAtVector);
    //    Vector3 direction = player.position - this.transform.position;
    //    direction.Normalize();
    //    zombieHead.rotation = Quaternion.Slerp(zombieHead.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
    //    if (Vector3.Distance(this.transform.position, player.position) > minDistance)
    //    {
    //        //this.transform.Translate(0, 0, runningSpeed * Time.deltaTime);
    //        zombieAgent.SetDestination(player.position);
    //    }
    //    zombieAnim.SetFloat("Speed", Mathf.Lerp(0, 1, zombieAgent.velocity.magnitude / maxSpeed));
    //}

    private static Zombie instance;
    public static Zombie Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Zombie>();
            }
            return instance;
        }
    }
}
