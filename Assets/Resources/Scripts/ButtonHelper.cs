using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  [SerializeField] [Multiline(1)] private string _towerName;
  [SerializeField] [Multiline(3)] private string _towerDescription;
  [SerializeField] [Multiline(9)] private string _towerParametrs;
  [SerializeField] [Multiline(1)] private string _towerCost;

    public void OnPointerEnter(PointerEventData eventData)
    {
        DescPanel.SetSwitch(true);
        DescPanel.Change(_towerName, _towerDescription, _towerParametrs, _towerCost);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DescPanel.SetSwitch(false);
    }
}
