using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float _mySpeed;
    private int _myDamage;
    private float _myExplosionRadius;
    private Transform _myTarget;
    private TowerScript.Effects _myEffect;


    public void Settings(float newSpeed, int newDamage, float newExplosionRadius, Transform newTarget, TowerScript.Effects effect)
    {
        _mySpeed = newSpeed;
        _myDamage = newDamage;
        _myExplosionRadius = newExplosionRadius;
        _myTarget = newTarget;
        _myEffect = effect;
    }

    private void Update()
    {
        if (!_myTarget) { Destroy(gameObject); return; }

        Vector3 direction = _myTarget.position - transform.position;
        float distanceThisFrame = _mySpeed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget(); return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(_myTarget);
    }

    private void HitTarget()
    {
        if (_myExplosionRadius > 0)
        {
            Explode();
        }
        else
        {
            Damage(_myTarget.GetComponent<Enemy>());
        }

        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, _myExplosionRadius);
        
        if (collider.Length > 0)
        {
            for (int i = 0; i < collider.Length; i++)
            {
                if (collider[i].tag == "Enemy")
                {
                    Damage(collider[i].GetComponent<Enemy>());
                }
            }
            Debug.Log("Урона получено: " + _myDamage);
        }
    }
    

    private void Damage(Enemy enemy)
    {
        switch ((int)_myEffect)
        {
            case 1: enemy.SetFreezeEffect(); break;
            case 2: enemy.SetFireEffect(); break;
            case 3: enemy.SetPoisonEffect(); break;
            default: break;
        }
        enemy.TakeDamage(_myDamage);
        Debug.Log(enemy);
    }
}
