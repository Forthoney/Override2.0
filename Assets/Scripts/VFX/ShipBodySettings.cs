using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Reflection;

public class ShipBodySettings : MonoBehaviour {

	public ShipControlComponent SCC { get; private set; }
	public SpriteRenderer Sprite, Outline;
	public Light2D OutlineLight;

	void Awake() {
		SCC = GetComponent<ShipControlComponent>();
		if (Sprite != null && Sprite.material != null) Sprite.material = Instantiate<Material>(Sprite.material);
		if (Outline != null && Outline.material != null) Outline.material = Instantiate<Material>(Outline.material);
	}

	[Header("Color Settings")]
	[ColorUsageAttribute(true, true)]
	[SerializeField] Color playerColorPrimary;
	[ColorUsageAttribute(true, true)]
	[SerializeField] Color playerColorSecondary;
	[ColorUsageAttribute(true, true)]
	[SerializeField] Color enemyColorPrimary;
	[ColorUsageAttribute(true, true)]
	[SerializeField] Color enemyColorSecondary;

	[Header("Sprite Threshold Settings")]
	public Vector2 threshold;

	Color primary, secondary;

	void Start() { SetDefaultColor(); }

	void Update() {
		if (GameManager.PlayerShip == null) return;
		Sprite?.material?.SetFloat("_Threshold", Mathf.Lerp(threshold.x, threshold.y, SCC.ShipBody.CurrHealth / SCC.ShipBody.MaxHealth));
	}

	public void SetDefaultColor() => SetColor(gameObject == GameManager.PlayerShip);
	public void SetColor(bool isPlayer) {
		SetColor(
			isPlayer ? playerColorPrimary : enemyColorPrimary,
			isPlayer ? playerColorSecondary: enemyColorSecondary
		);
	}
	public void SetColor(Color primary, Color secondary) {
		Sprite?.material?.SetColor("_GlowColor", primary);
		Sprite?.material?.SetColor("_GlowColor2", secondary);
		Outline?.material?.SetColor("_GlowColor", primary);
		if (OutlineLight != null)
			OutlineLight.color = primary;
	}
	public void ToggleDisplay(bool displayOn) {
		Sprite.enabled = displayOn;
		Outline.enabled = displayOn;
		OutlineLight.enabled = displayOn;
    // FIXME: hide the ship's point light; BAD CODE but it works for now 
    SCC.gameObject.GetComponentInChildren<Light2D>().enabled = displayOn;
	}
	public void InitShipBody(Sprite bodySprite, Material bodyMaterial, Sprite outlineSprite) {
		if (Sprite != null) {
			Sprite.sprite = bodySprite;
			Sprite.material = bodyMaterial;
		}
		if (Outline != null)
			Outline.sprite = outlineSprite;
		if (OutlineLight != null && OutlineLight.lightType == UnityEngine.Experimental.Rendering.Universal.Light2D.LightType.Sprite) {
			FieldInfo _LightCookieSprite =  typeof( Light2D ).GetField( "m_LightCookieSprite", BindingFlags.NonPublic | BindingFlags.Instance );
			_LightCookieSprite.SetValue(OutlineLight, outlineSprite);
		}
	}
}