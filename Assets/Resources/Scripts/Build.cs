using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
   private Material MatFalse;
   private Material MatTrue;
   private Material MatSelect;

   public bool isBuilded = false;
   private bool _isSelected = false;

   private MeshRenderer _render;


    private void Start()
    {
        GetMaterials();
        _render = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
      if(!isBuilded) MaterialCheked();
    }

    private void OnMouseDown()
    {
        Vector3 rot = Vector3.right;
        if (Manager._selectedTowerBuy && !isBuilded && _isSelected)
        {
            TowerScript temp = Instantiate(Manager._selectedTowerBuy, transform.position, Quaternion.LookRotation(rot)).GetComponent<TowerScript>();
            temp.myBuildPoint = this;
            PlayerStats.Money -= Manager._selectedTowerCost;
            isBuilded = true;
            _render.material = MatFalse;
        }            
    }

    private void OnMouseEnter()
    {
            _isSelected = true;
    }

    private void OnMouseExit()
    {
        _isSelected = false;
    }

    private void MaterialCheked()
    {
            if (Manager._selectedTowerBuy)
            {
            if (_isSelected)
                _render.material = MatSelect;
            else
                _render.material = MatTrue;
             }
        else _render.material = MatFalse;
    }

    private void GetMaterials()
    {
        MatFalse = Resources.Load<Material>("Materials/BuildPointFalse");
        MatTrue = Resources.Load<Material>("Materials/Towers_Blue");
        MatSelect = Resources.Load<Material>("Materials/Environment");
    }
}
