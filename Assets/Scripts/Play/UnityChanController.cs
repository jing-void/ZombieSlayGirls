using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
       
        
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.position += new Vector3(x, 0, z)* speed * Time.deltaTime;
    }
}
