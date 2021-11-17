using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OverrideButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	bool _running = true;
  bool ishovered;
  Color startColor;
  Material startMat;
  public GameObject startShip;

  // Start is called before the first frame update
  void Start()
  {
    startColor = new Color(14, 191, 182);
	ShipBodySettings decorSettings = startShip.GetComponent<ShipBodySettings>();
	if (decorSettings != null && decorSettings.Sprite != null)
    	startMat = decorSettings.Sprite.material;
  }

  void OnEnable() {
	  _running = true;
  }

  // Update is called once per frame
  void Update()
  {
	  if (_running && startShip != null) {
			if (ishovered)
			{
			//   SpriteRenderer sprite = startShip.GetComponent<SpriteRenderer>();
			//   float rVal = Mathf.Lerp(startColor.r, 1, Time.deltaTime);

			ShipBodySettings bodySettings = startShip.GetComponent<ShipBodySettings>();
			bodySettings?.SetColor(true);
			//   sprite.color = new Color(rVal, 0, 0);
			//   sprite?.material?.SetColor("_GlowColor", Color.red);
			//   sprite?.material?.SetColor("_GlowColor2", Color.red);
			//   startShip.GetComponentsInChildren<SpriteRenderer>()[1].color = Color.red;
			}
			else
			{
			//   float rVal = Mathf.Lerp(startColor.r, 1, Time.deltaTime);
			//   SpriteRenderer sprite = startShip.GetComponent<SpriteRenderer>();

			ShipBodySettings bodySettings = startShip.GetComponent<ShipBodySettings>();
			bodySettings?.SetColor(false);
			//sprite.color = new Color(rVal, 0, 0);
			//   sprite?.material?.SetColor("_GlowColor", new Color(startColor.r / 2, startColor.g / 2, startColor.b / 2, 0));
			//   sprite?.material?.SetColor("_GlowColor2", new Color(14, 58, 191, 0));
			//   startShip.GetComponent<SpriteRenderer>().color = startColor;
			//   startShip.GetComponentsInChildren<SpriteRenderer>()[1].color = startColor;
			}
	  }
  }

  public void OnPointerEnter(PointerEventData pointerEventData)
  {
    Debug.Log("entered");
    ishovered = true;
  }

  public void OnPointerExit(PointerEventData pointerEventData)
  {
    ishovered = false;
  }

  public void OnClick() {
	  // shall we disable this on click
	  _running = false;
  }
}
