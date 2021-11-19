using UnityEngine;

namespace FMOD_Thuleanx {
	public class AudioSettingsManipulator : MonoBehaviour {
		public float MaxValue = 1;
		public void SetMasterVolume(float value) {
			AudioManager.Instance?.SetMasterVolume(value / MaxValue);
		}

		public void SetMusicVolume(float value) {
			AudioManager.Instance?.SetMusicVolume(value / MaxValue);
		}

		public void SetSFXVolume(float value) {
			AudioManager.Instance?.SetSFXVolume(value / MaxValue);
		}
	}
}