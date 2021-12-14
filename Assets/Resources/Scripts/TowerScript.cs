using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    public enum Towers
    { 
        crossbow = 0,
        archers = 1,
        explosionGun = 2,
        explosionGunSpeed = 3
    }
    public Towers myTower = Towers.crossbow; 


    [Header("Параметры")]
    [SerializeField] public int _bulletDamage;
    [SerializeField] public float _bulletSpeed;
    [SerializeField] public float _fireRange;
    [SerializeField] public float _fireRite;
    [SerializeField] public int _myCost;
    [SerializeField] public int _updateCost;

    [Header("Эффекты")]
    [SerializeField] public float _explosionRadius;
    

    public enum Effects
    { 
        nothing = 0,
        freeze = 1,
        fire = 2,
        poison = 3
    }
    public Effects myEffects = Effects.nothing;

    [Header("Другие настройки")]
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private Transform[] _firePoint;
    [SerializeField] private Transform _target;


    public Build myBuildPoint;
    private bool _isUpgraded;
    private float _fireCoutDown;
    private GameObject _myCanvas;

    private void Awake()
    {
        _myCanvas = transform.GetChild(0).gameObject;
        _myCanvas.SetActive(false);
    }
    
    // Меняет модельку
    
    public void UpgrateMesh(int tower) {
        MeshFilter updateMesh = transform.GetComponent<MeshFilter>();
        switch (tower)
        {
            case 0: updateMesh.mesh = Resources.Load<Mesh>("Prefabs/UpdateTower/UpgradeArcher"); break;
            case 1: updateMesh.mesh = Resources.Load<Mesh>("Prefabs/UpdateTower/UpgradeCrossbow"); break;
            case 2: updateMesh.mesh = Resources.Load<Mesh>("Prefabs/UpdateTower/TB_CanonTower_Lvl4_B"); break;
            case 3: updateMesh.mesh = Resources.Load<Mesh>("Prefabs/UpdateTower/TB_CanonTower_Lvl4_A"); break;
        }
        
    }
    

    private void Update()
    {
        if (!_target) return;

        if (_fireCoutDown <= 0)
        {
            Shoot();
            _fireCoutDown = _fireRite;
        }
        _fireCoutDown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        UpdateTarget();
    }

    private void Shoot()
    {
        for (int i = 0; i < _firePoint.Length; i++)
        {
            Bullet bullet = Instantiate(_bulletPref, _firePoint[i].position, Quaternion.identity).GetComponent<Bullet>();
            bullet.Settings(_bulletSpeed, _bulletDamage, _explosionRadius, _target, myEffects);
        }
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        for (int i = 0; i < enemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemies[i].transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemies[i];
            }
        }

        if (nearestEnemy && shortestDistance <= _fireRange)
        {
            _target = nearestEnemy.transform;
        }
        else
        {
            _target = null;
        }
    }

    private void OnMouseDown()
    {
        Button but = _myCanvas.transform.GetChild(0).GetChild(0).GetComponent<Button>();
        but.interactable = PlayerStats.Money >= _updateCost && !_isUpgraded;

        Manager.TowerMenu(_myCanvas);
    }


    public void Upgrade()
    {
        if(PlayerStats.Money >= _updateCost)
        {
            PlayerStats.Money -= _updateCost;
            _isUpgraded = true;

            UpgrateSystem up = new UpgrateSystem();
            up.Upgrade(this);

            Manager.TowerMenu(_myCanvas);
        }
    }


    public void Sell()
    {
        PlayerStats.Money += _myCost / 2;
        if (_isUpgraded) PlayerStats.Money += _updateCost / 2;
      myBuildPoint.isBuilded = false;
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _fireRange);
    }
}
