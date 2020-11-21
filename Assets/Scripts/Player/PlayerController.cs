using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LanternController lantern;
    public float speed;

    private Rigidbody rb;

    private float xMovement;
    private float zMovement;
        

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        zMovement = Input.GetAxisRaw("Vertical");

        if(Input.GetMouseButtonDown(1))
        {
            lantern.SwitchActive();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector3(xMovement * speed, rb.velocity.y, zMovement * speed);
    }
}
