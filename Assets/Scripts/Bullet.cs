using System;
using UnityEngine;



public class Bullet : MonoBehaviour
{
    private Vector3 _shootDir; 
    private readonly float moveSpeed = 10f;
    private float damage = 10f;
    public void Setup(Vector3 shootDir)
    {
       
        this._shootDir = shootDir;

        Destroy(gameObject, 3.0f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Argenimal target = collision.GetComponent<Argenimal>();
        if (target != null)
        {
            Destroy(this.gameObject);
            //target.TakeDamage();
        }
    }
    // Update is called once per frame
    private void Update()
    {
        transform.position += _shootDir * (moveSpeed * Time.deltaTime);
    }
}
