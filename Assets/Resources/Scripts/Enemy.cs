using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
   [Header("Parameters")]
   [SerializeField] private float _speed;
   [SerializeField] public float _mySpeed;
   [SerializeField] private int _health;
   [SerializeField] public int _cost;

   private int _index = 0;
   private WayPoint _point;
   public Image HealthBar;
   private float _personHealth;
   private Color _myColor;
   private MeshRenderer _renderer;


    //Freeze//
    private GameObject _freezePref;
    private int _freezeAmount;
    [SerializeField] int _freezeMaxAmount = 5;

    //Fire//
    private GameObject _firePref;
    private bool _isFire;

    //Poison//
    private GameObject _poisinPref;
    private bool _isPoispned;

   public enum Enemie
    {
        none = 0,
        takesGolds = 1,
    }
    public Enemie myEnemie;


    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _myColor = _renderer.material.color;
    }

    private void Start()
    {
        _speed = _mySpeed;
        _personHealth = 1f / _health;
        _firePref = transform.GetChild(0).gameObject;
        _poisinPref = transform.GetChild(1).gameObject;
        _freezePref = transform.GetChild(2).gameObject;
    }

     void Update()
    {
        Move();
    }

     void Move()
    {
        transform.LookAt(_point._wayPoints[_index]);

        transform.position = Vector3.MoveTowards(transform.position, _point._wayPoints[_index].position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _point._wayPoints[_index].position) < 0.1f)
        {
            if (_index < _point._wayPoints.Length - 1)
                _index++;
            else
            {
                switch (myEnemie)
                {
                    case Enemie.none:   Manager.PlayerDamage();
                        Manager.EnemieDiscount();
                        Destroy(gameObject);
                        break;
                    case Enemie.takesGolds:   Manager.PlayerDamage();
                        Manager.EnemieDiscount();
                        PlayerStats.Money--;
                        Destroy(gameObject);
                        break;
                }
            }
              
        }
    }

    public void SetSide(int index)
    {
        switch (index)
        {
            case 0: _point = GameObject.FindGameObjectWithTag("WayPointL").GetComponent<WayPoint>(); break;
            case 1: _point = GameObject.FindGameObjectWithTag("WayPointR").GetComponent<WayPoint>(); break;
            default: break;
        }
    }

    public void TakeDamage(int damage)
    {
 
    _health -= damage;
                
        if (HealthBar)  HealthBar.fillAmount -= _personHealth * damage;

        if (_health <= 0)
        {
            PlayerStats.Money += _cost; 
            Manager.EnemieDiscount(); 
            Destroy(gameObject);
        }
    }

    public void SetFreezeEffect()
    {
        _isFire = false;
        StopAllCoroutines();

        if (_freezeAmount < _freezeMaxAmount) _freezeAmount++;

        StartCoroutine(FreezeEffect());
    }

    public void SetFireEffect()
    {
        
        _freezeAmount = 0;

        if (_isFire) StopAllCoroutines();

        _isFire = true;
        FireColor();

        StartCoroutine(FireEffect());
    }

    public void SetPoisonEffect()
    {
        if (_isPoispned) return;
        _isPoispned = true;
        float damage = _health * 0.25f;
        TakeDamage((int)damage);

        _poisinPref.SetActive(_isPoispned);
        HealthBar.color = Color.green;
    }

    private IEnumerator FireEffect()
    {
        int ticks = 10;
        float timer = 0.5f;
        int damage;
        

        // Время горение = tick * timer
        for (int i = 0; i < ticks; i++)
        {
            if (_isPoispned) damage = 5;
            else damage = 3;
            TakeDamage(damage);
            yield return new WaitForSeconds(timer);
        }
 
        _isFire = false;
        FireColor();
    }

    private IEnumerator FreezeEffect()
    {
        float timer;
       
       
        for (int i = _freezeAmount; i > 0; i--)
        {
            FreezingColor();
            if (_isPoispned) timer = 4f;
            else timer = 3f;
            

            int bonus = 10;
            _speed = _mySpeed - (_mySpeed / 100 * bonus * _freezeAmount);

            yield return new WaitForSeconds(timer);
            _freezeAmount--;
                      
        }
        _freezeAmount = 0;
        _speed = _mySpeed;
        FreezingColor();
    }
    private void FireColor()
    {
        Color color = _myColor;
        if (_isFire) color = new Color(0.5f, 0.1f, 0.1f);
        _firePref.SetActive(_isFire);

        _renderer.material.color = color;
    }


    private void FreezingColor()
    {
        Color color = _myColor;
        if (_freezeAmount > 0)
        {
            color.r = 1f - _freezeAmount / 5f;
            color.g = 1f - _freezeAmount / 10f;
            color.b = 1f;
        }
        _freezePref.SetActive(_freezeAmount > 0);
        _renderer.material.color = color;
    }
}
