using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthBar : MonoBehaviour {
	public List<EffectInfo> Images = new List<EffectInfo>();

	[SerializeField] float _smoothTime = 2f;
	[SerializeField] Image _healthBar;
	[SerializeField] Slider _slider, _catchupSlider, _effectSlider;
	[SerializeField] Color _fullColor, _emptyColor;
	[SerializeField] float _lengthMult = 16f;

	[SerializeField] Gradient gradient;
	
	[SerializeField] float EffectLength = .4f;
	[SerializeField] float PulseBPM = 60f;
	[SerializeField] float PulseIntensity = 1f;

	float _catchupVel;
	Timer _invertColor;
	float awakeTime;

	void Awake() {
		awakeTime = Time.time;
		foreach (var info in Images)  {
			if (info.image.material != null)
				info.image.material = Instantiate<Material>(info.image.material);
		}
	}

	void Update() {
		if (_slider != null && _catchupSlider != null && _healthBar != null && GameManager.PlayerShip != null) {

			ShipControlComponent Player = GameManager.PlayerShip.GetComponent<ShipControlComponent>();
			GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Player.ShipBody.MaxHealth * _lengthMult);
			_slider.value = (float) Player.ShipBody.CurrHealth / Player.ShipBody.MaxHealth;
			_catchupSlider.value = Mathf.SmoothDamp(_catchupSlider.value, _slider.value, ref _catchupVel, _smoothTime);
			_effectSlider.value = _slider.value;
			_healthBar.color = gradient.Evaluate(_slider.value);


			// Effects
			foreach (var info in Images) {
				// if (info.invert) info.image.material.SetFloat("_Invert", _invertColor?1f:0f);
				// if (info.colorShift) info.image.material.SetColor("_Color", gradient.Evaluate(_slider.value));
				if (info.glow) {
					info.image.material.SetFloat("_Intensity", PulseIntensity * CoolFunctions.OneHeartBeat(PulseBPM / 60f * (Time.time - awakeTime)));
				}
			}
		}
	}

	public void TriggerEffect() {
		_invertColor = new Timer(EffectLength);
		_invertColor.Start();
	}

	[System.Serializable]
	public class EffectInfo {
		public Image image;
		public bool invert;
		public bool glow;
		public bool colorShift;
	}
}
