using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoSystem : MonoBehaviour
{
    float maxAmmo;
    float currentAmmo;
    public Text textDisplay;
    // Start is called before the first frame update
    void Start()
    {
        maxAmmo = 0;
        currentAmmo = 0;
        textDisplay.text = currentAmmo.ToString() + " / " + maxAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ModifAmmo(float newAmmo, float newMaxAmmo)
    {
        currentAmmo = newAmmo;
        maxAmmo = newMaxAmmo;
        textDisplay.text = currentAmmo.ToString() + " / " + maxAmmo.ToString();
    }
}
