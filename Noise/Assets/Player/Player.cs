using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] KeyCode shoot;
    [SerializeField] Transform target;
    SpriteRenderer sprite;
    
    [SerializeField] int shotVolume;
    [SerializeField] int shotMaxNoise;
    [SerializeField] float shotForce;
    
    [SerializeField] float maxXDistance;
    [SerializeField] float maxYDistance;

    [SerializeField] GameObject bullet;

    [SerializeField] GameObject resetMenu;
    [SerializeField] GameObject gameUI;

    int health;
    // i-variables refer to invincibility
    float iTimer;
    [SerializeField] float maxITime;
    IEnumerator iFrameBlink()
    {
        for (float timer = 0; timer < maxITime; timer += 0.25f)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(0.25f);
        }
        sprite.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        iTimer = 0;
        sprite = GetComponent<SpriteRenderer>();
        // UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        // UnityEngine.Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.zero;
        movement.x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        movement.y = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(movement, Space.World);

        // Making sure the player doesn't move beyond the world bounds
        if (transform.position.x > maxXDistance) { transform.Translate(new Vector2(-movement.x, 0), Space.World); }
        if (transform.position.x < -maxXDistance) { transform.Translate(new Vector2(-movement.x, 0), Space.World); }
        if (transform.position.y > maxYDistance) { transform.Translate(new Vector2(0, -movement.y), Space.World); }
        if (transform.position.y < -maxYDistance) { transform.Translate(new Vector2(0, -movement.y), Space.World); }

        
        // target.SetPositionAndRotation(Input.mousePosition, Quaternion.identity);

        iTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        MoveTarget();

        if (Input.GetKeyDown(shoot)) Shoot();
    }

    void Shoot() 
    {
        Debug.Log("Shooting!");
        // Vector is mouse position - player position
        // transform.right = new Vector2(0, 1);
        NoiseController.instance.Sound.Invoke(NoiseController.instance.formatSound(shotVolume, shotMaxNoise));

        // Shooting the bullet
        Vector3 bulletForce = target.transform.position - transform.position;
        bulletForce.Set(bulletForce.x, bulletForce.y, 0);
        bulletForce = bulletForce.normalized * shotForce;
        GameObject newBullet = Object.Instantiate(bullet);
        newBullet.transform.position = transform.position;
        newBullet.GetComponent<Rigidbody2D>().AddForce(bulletForce, ForceMode2D.Impulse);
    }

    void MoveTarget() 
    {
        var mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        target.transform.position = mousePos;
        this.transform.right = mousePos - transform.position;
    }

    public void Damage()
    {
        if (iTimer <= 0)
        {
            iTimer = maxITime;
            StartCoroutine("iFrameBlink");
            health--;
            Debug.Log("Health: " + health);

            if (health <= 0) { 
                GameObject.Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        gameUI.SetActive(false);
        resetMenu.SetActive(true);
        // UnityEngine.Cursor.visible = true;
    }
}
