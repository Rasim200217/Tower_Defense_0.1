using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescPanel : MonoBehaviour
{
 private static Text _desname; 
 private static Text _desDescription;
 private static Text _desParamets;
 private static Text _desnameCost;

 private static GameObject _gameObject;

 private void Awake()
 {
  _desname = transform.GetChild(0).GetComponent<Text>();
  _desDescription = transform.GetChild(1).GetComponent<Text>();
  _desParamets = transform.GetChild(2).GetComponent<Text>();
  _desnameCost = transform.GetChild(3).GetComponent<Text>();
  
  _gameObject = gameObject;
  
  SetSwitch(false);
 }
 
 public static void Change(string newName, string newDecription, string newParametrs, string newCost)
 {
  _desname.text = newName;
  _desDescription.text = newDecription;
  _desParamets.text = newParametrs;
  _desnameCost.text = newCost;
 }

 public static void SetSwitch(bool state)
 {
  _gameObject.SetActive(state);
 }
}
