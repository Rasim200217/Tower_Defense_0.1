using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgrateSystem
{
    public void Upgrade(TowerScript tower)
    {
        switch ((int)tower.myTower)
        {
            case 0: CrossbowUpgrate(tower); break;
            case 1: ArchersUpgrate(tower); break;
            case 2 : ExplosionGunUpdate(tower); break;
            case 3: ExplosionGunSpeed(tower); break;
            default:
                break;
        }
    }


    private void CrossbowUpgrate(TowerScript tower)
    {
        tower._bulletDamage = 13;
        tower.myEffects = TowerScript.Effects.poison;
        tower.UpgrateMesh(1);
    }

    private void ArchersUpgrate(TowerScript tower)
    {
        tower._bulletDamage = 8;
        tower.myEffects = TowerScript.Effects.fire;
        tower.UpgrateMesh(0);
    }

    private void ExplosionGunUpdate(TowerScript tower)
    {
        tower._bulletDamage = 21;
        tower._explosionRadius = 8;
        tower.UpgrateMesh(2);
    }

    private void ExplosionGunSpeed(TowerScript tower)
    {
        tower._bulletDamage = 16;
        tower._explosionRadius = 6;
        tower.UpgrateMesh(3);
    }
}
