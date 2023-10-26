using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] KeyCode shoot;
    [SerializeField] Transform target;
    [SerializeField] Camera myCamera;
    [SerializeField] int shotVolume;
    [SerializeField] int shotMaxNoise;

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
        transform.Translate(movement, Space.World);

        moveTarget();
        // target.SetPositionAndRotation(Input.mousePosition, Quaternion.identity);

        if (Input.GetKeyDown(shoot)) Shoot();
    }

    /*private void LateUpdate()
    {
        myCamera.transform.rotation = Quaternion.Euler(0,0,0);
    }*/

    void Shoot() 
    {
        Debug.Log("Shooting!");
        // Vector is mouse position - player position
        // transform.right = new Vector2(0, 1);
        NoiseController.instance.Sound.Invoke(NoiseController.instance.formatSound(shotVolume, shotMaxNoise));
    }

    void moveTarget() 
    {
        var mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        target.transform.position = mousePos;
        this.transform.right = mousePos - transform.position;
    }
}
