using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverrideButton : MonoBehaviour
{

  bool ishovered;
  Color startColor;
  public GameObject startShip;

  // Start is called before the first frame update
  void Start()
  {
    GameManager.PlayerShip.GetComponent<SpriteRenderer>().color = startColor;
  }

  // Update is called once per frame
  void Update()
  {
    if (ishovered)
    {
      float rVal = Mathf.Lerp(startColor.r, 255, 10);
      startShip.GetComponent<SpriteRenderer>().color = new Color(rVal, 0, 0);
    }
    else
    {
      //float rVal = Mathf.Lerp(startColor.r, 255, 10);
      //startShip.GetComponent<SpriteRenderer>().color = new Color(startShip.GetComponent<SpriteRenderer>().color.r, startColor.r, 0);
    }

  }

  void OnMouseEnter()
  {
    Debug.Log("entered");
    ishovered = true;
  }

  void OnMouseExit()
  {
    ishovered = false;
  }
}
