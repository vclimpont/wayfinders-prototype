using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject cast;
    public Camera mainCamera;
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

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = RayCastOnMousePosition();
            Instantiate(cast, hitInfo.point, Quaternion.identity);
            //lantern.UseLanternProperty(RayCastOnMousePosition());
        }

        if (Input.GetMouseButtonDown(1))
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

    private RaycastHit RayCastOnMousePosition()
    {
        RaycastHit hitInfo;
        Vector3 direction = Vector3.Normalize(GetMousePosition() - mainCamera.transform.position);
        Physics.Raycast(mainCamera.transform.position, direction, out hitInfo);

        return hitInfo;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        return mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 100));
    }

    public void ChangeLanternProperty(int sourceProperty, Color lightColor, Material sourceMaterial)
    {
        lantern.UpdateLanternProperty(sourceProperty);
        lantern.ChangeLightProperties(lightColor, sourceMaterial);
        lantern.ReloadLantern(0.2f);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Campfire"))
        {
            lantern.ReloadLantern(1);
        }
    }
}
