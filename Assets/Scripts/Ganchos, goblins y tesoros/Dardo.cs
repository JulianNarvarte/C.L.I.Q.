using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dardo : MonoBehaviour
{
    public float Vel;
    void Update()
    {
        //transform.localEulerAngles = new Vector3(0, 0, 180);

        transform.Translate(new Vector2(0, -Vel * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
