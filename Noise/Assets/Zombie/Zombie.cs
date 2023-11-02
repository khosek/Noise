using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Zombies : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;

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
        Vector3 moveDirection = player.transform.position - transform.position;
        moveDirection.Set(moveDirection.x, moveDirection.y, 0);
        moveDirection.Normalize();
        rb.AddForce(moveDirection * (acceleration * Time.deltaTime));
    }
}
