using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	[SerializeField] float _smoothTime = 2f;
	[SerializeField] Image _healthBar;
	[SerializeField] Slider _slider, _catchupSlider;
	[SerializeField] Color _fullColor, _emptyColor;
	[SerializeField] float _lengthMult = 16f;

	[SerializeField] Gradient gradient;
	float _catchupVel;

	void Update() {
		if (_slider != null && _catchupSlider != null && _healthBar != null && GameManager.PlayerShip != null) {
			ShipControlComponent Player = GameManager.PlayerShip.GetComponent<ShipControlComponent>();
			GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Player.ShipBody.MaxHealth * _lengthMult);
			_slider.value = (float) Player.ShipBody.CurrHealth / Player.ShipBody.MaxHealth;
			_catchupSlider.value = Mathf.SmoothDamp(_catchupSlider.value, _slider.value, ref _catchupVel, _smoothTime);
			_healthBar.color = gradient.Evaluate(_slider.value); 
		}
	}
}