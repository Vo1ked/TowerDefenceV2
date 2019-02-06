using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    ATowerAttack _aTowerAttack;
    Queue<Bullet> _pool;

    public void Init(ATowerAttack attack,Transform target,Vector3 spawnPoint,Queue<Bullet> pool)
    {
        gameObject.SetActive(true);
        _aTowerAttack = attack;
        _pool = pool;
        transform.position = spawnPoint;
        _aTowerAttack.Attack(target,this);
        StartCoroutine(LifeTimerDelay());
    }


    private void OnTriggerEnter(Collider other)
    {
        _aTowerAttack.OnColliderHit(other);

    }


    public void Disable()
    {
        gameObject.SetActive(false);
        _pool.Enqueue(this);
    }

    IEnumerator LifeTimerDelay()
    {
        float lifeTime = 3f;
        while(lifeTime > 0)
        {
            yield return null;
            lifeTime -= Time.deltaTime;
        }
        Disable();
    }
}
