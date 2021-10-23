using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	[SerializeField] float _smoothTime = 2f;
	[SerializeField] Image _healthBar;
	[SerializeField] Slider _slider, _catchupSlider;
	[SerializeField] Color _fullColor, _emptyColor;
	float _catchupVel;

	void Update() {
		if (_slider != null && _catchupSlider != null && _healthBar != null) {
			ShipControlComponent Player = GameManager.PlayerShip.GetComponent<ShipControlComponent>();
			GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Player.ShipBody.MaxHealth * 16);
			_slider.value = (float) Player.ShipBody.CurrHealth / Player.ShipBody.MaxHealth;
			_catchupSlider.value = Mathf.SmoothDamp(_catchupSlider.value, _slider.value, ref _catchupVel, _smoothTime);
			_healthBar.color = Color.Lerp(_emptyColor, _fullColor, _slider.value);
		}
	}
}