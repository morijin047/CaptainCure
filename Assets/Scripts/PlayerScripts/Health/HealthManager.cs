using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public enum HealthStatus { INJURED, HEALTHY, DEAD = -1 }

    Animator animator;

    HealthStatus healthStatus;
    [SerializeField] float currentTimer;
    [SerializeField] float endTimer = 5f;

    //public Color damageColor;
    //public Image damageImage;
    //float colorSmoothing = 6f;
    public GameObject gotImageDamage;

    private void Awake()
    {
        healthStatus = HealthStatus.HEALTHY;
        animator = GetComponent<Animator>();
        //set valeur healthy
        animator.SetFloat("HP", (int)healthStatus);
        currentTimer = endTimer;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            HurtPlayer();
            //add damage effect
            var color = gotImageDamage.GetComponent<Image>().color;
            color.a = .5f;
            gotImageDamage.GetComponent<Image>().color = color;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            HillPlayer();
        }
    }
    public void HurtPlayer()
    {
        //if value of HealthStatus reach -1 it's good
        if (healthStatus == HealthStatus.DEAD)
        {
            return;
        }
        healthStatus--;

        if (healthStatus == HealthStatus.INJURED)
        {
            StartCoroutine(StartAutoHealth(endTimer));
            Debug.Log("Give value: " + (int)healthStatus);
        }
        //Debug.Log("Give value: " + (int)healthStatus);
        animator.SetFloat("HP", (int)healthStatus);
    }

    void HillPlayer()
    {
        healthStatus = HealthStatus.HEALTHY;
        animator.SetFloat("HP", (int)healthStatus);
    }

    IEnumerator StartAutoHealth(float timer)
    {
        yield return new WaitForSeconds(timer);
        healthStatus = HealthStatus.HEALTHY;
        animator.SetFloat("HP", (int)healthStatus);
        //damage effect removes
        var color = gotImageDamage.GetComponent<Image>().color;
        color.a = 0f;
        gotImageDamage.GetComponent<Image>().color = color;
    }

}
