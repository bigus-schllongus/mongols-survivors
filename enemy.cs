using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed=2f;
    private Rigidbody2D rb;
    private Vector3 directionToPlayer;
    private Vector3 localScale;
    int hp=10; 
    public int hpmax;  
    // Start is called before the first frame update
    public void TakeDamage(int damage){
        hp=hp-damage;
        if (hp <= 0){
            Destroy(gameObject);
        }
    }
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      localScale = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate(){
        MoveEnemy();
    }
    void MoveEnemy(){
        directionToPlayer = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * moveSpeed;
    }
    private void LateUpdate(){
       if(rb.velocity.x > 0){
        transform.localScale = new Vector3(-localScale.x,localScale.y,localScale.z);
       }
       else if( rb.velocity.x < 0){
         transform.localScale = new Vector3(localScale.x,localScale.y,localScale.z);
       }

    }
}
