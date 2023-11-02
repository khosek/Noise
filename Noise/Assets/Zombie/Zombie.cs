using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Zombie : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;
    Vector3 moveDirection;
    [SerializeField] float acceleration;

    int health;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
        health = 3;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            moveDirection = player.transform.position - transform.position;
            moveDirection.Set(moveDirection.x, moveDirection.y, 0);
            moveDirection.Normalize();

            // Turn to face the player
            this.transform.right = player.transform.position - transform.position;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection * acceleration);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) {
            collision.collider.gameObject.GetComponent<Player>().Damage();
        }
    }

    public void Damage() {
        health--;
        if (health <= 0) { GameObject.Destroy(this.gameObject); }
    }
}
