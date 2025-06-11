using System;
using UnityEngine;



public class Bullet : MonoBehaviour
{
    private Vector3 _shootDir; 
    private readonly float moveSpeed = 5f;
    private int _damage;
    private CombatManager.Teams _bulletTeam;
    public void Setup(Vector3 shootDir, int damage, CombatManager.Teams alliedTeam)
    {
        this._shootDir = shootDir;
        _damage = damage;
        _bulletTeam = alliedTeam;
        Destroy(gameObject, 2.0f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Argenimal target = collision.GetComponent<Argenimal>();
        if (target != null && target.team != _bulletTeam)
        {
            Destroy(this.gameObject);
            target.TakeDamage(_damage);
        }
    }
    // Update is called once per frame
    private void Update()
    {
        transform.position += _shootDir * (moveSpeed * Time.deltaTime);
    }
}
