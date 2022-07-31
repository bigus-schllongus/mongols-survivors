using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed = 3f;
    public int damage = 5;
    public GameObject bullet;
    public Transform firePoint;
    public float bulletSpeed = 50;
    Vector2 lookDirection;
    float lookAngle;
    // Update is called once per frame
    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }
    private void OnCollisionEnter2D(Collision2D colision) {
        if (colision.transform.tag == "Enemy"){
            colision.gameObject.GetComponent<enemy>().TakeDamage(5);

        }
        if (colision.transform.tag != "Player"){
        Destroy(gameObject);
        
        }
         lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletClone = Instantiate(bullet);
            bulletClone.transform.position = firePoint.position;
            bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

            bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
        }
        
    }
}
