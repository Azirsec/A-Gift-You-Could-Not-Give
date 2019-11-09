using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 direction = new Vector3(0, 0, 0);
    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 acceleration = new Vector3(0, 0, 0);

    public float friction;
    public float speed;
    public float maxSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        direction = direction.normalized;
        acceleration = direction * speed * Time.deltaTime;
        velocity += acceleration;
        velocity *= friction;

        if (velocity.magnitude < maxSpeed)
        {
            gameObject.transform.position += velocity;
        }
    }
}
