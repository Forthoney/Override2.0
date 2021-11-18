using UnityEngine;

namespace FMOD_Thuleanx {
	public class AudioSettingsManipulator : MonoBehaviour {
		public void SetMasterVolume(float value) {
			AudioManager.Instance?.SetMasterVolume(value);
		}

		public void SetMusicVolume(float value) {
			AudioManager.Instance?.SetMusicVolume(value);
		}

		public void SetSFXVolume(float value) {
			AudioManager.Instance?.SetSFXVolume(value);
		}
	}
}