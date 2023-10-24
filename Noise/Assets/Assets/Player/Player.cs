using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.zero;
        movement.x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        movement.y = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(movement);
    }
}
