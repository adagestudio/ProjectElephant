using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectSimple : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float rotateSpeed = 10f;
    private GameObject camara;

    // Update is called once per frame
    void Update()
    {
        /*
        transform.Translate(
            moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f,
            moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
            */
        transform.Translate(0f, 0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        transform.Rotate(0f, rotateSpeed * Input.GetAxis("Horizontal"), 0f);


    }
}
