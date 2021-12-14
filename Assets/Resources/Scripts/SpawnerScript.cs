using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public enum Sides
    {
        left = 0,
        right = 1 
    }
    public Sides mySide = Sides.left;

    private GameObject[] _enemiePrefabs;

    private void Start()
    {
        _enemiePrefabs = new GameObject[4];
        _enemiePrefabs[0] = Resources.Load<GameObject>("Prefabs/Enemies/Soldier");
        _enemiePrefabs[1] = Resources.Load<GameObject>("Prefabs/Enemies/Knight");
        _enemiePrefabs[2] = Resources.Load<GameObject>("Prefabs/Enemies/Rouge");
        _enemiePrefabs[3] = Resources.Load<GameObject>("Prefabs/Enemies/Boss");
    }

    public void StartSpawn(Wave wave)
    {
        StartCoroutine(Spawner(wave));
    }

    IEnumerator Spawner(Wave wave)
    {
        for (int i = 0; i < wave.packs.Length; i++)
        {
            for (int j = 0; j < wave.packs[i].count; j++)
            {
                Enemy enemie = Instantiate(_enemiePrefabs[(int)wave.packs[i].enemie], transform.position, Quaternion.identity).GetComponent<Enemy>();
                enemie.SetSide((int)mySide);
                yield return new WaitForSeconds(wave.pauseVoln);
            }
            yield return new WaitForSeconds(wave.packs[i].pausePack);
        }
    }
}
