using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            this.transform.position += new Vector3(-1, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
            this.transform.position += new Vector3(1, 0, 0);
        if (Input.GetKey(KeyCode.UpArrow))
            this.transform.position += new Vector3(0, 0, -1);
        if (Input.GetKey(KeyCode.DownArrow))
            this.transform.position += new Vector3(0, 0, 1);
    }
}
