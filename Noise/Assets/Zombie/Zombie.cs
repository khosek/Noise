using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Zombies : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;
    Vector3 moveDirection;

    [SerializeField] float acceleration;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        moveDirection = player.transform.position - transform.position;
        moveDirection.Set(moveDirection.x, moveDirection.y, 0);
        moveDirection.Normalize();
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection * acceleration);
    }
}
