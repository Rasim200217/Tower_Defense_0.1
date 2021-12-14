using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave 
{
    public float pauseVoln = 1.5f;
    public Enemies[] packs;

    public enum EnemiesTypes
    {
        soldier = 0,
        knight = 1,
        rouge = 2,
        boss = 3
    }

    [System.Serializable]
    public class Enemies
    {
        public float pausePack = 0.5f;
        public EnemiesTypes enemie;
        public int count;
    }
}
