using UnityEngine;
using System.Collections.Generic;

public class FMODAudioOneshotEmitter : MonoBehaviour {
	[FMODUnity.EventRef]
	public List<string> BankReferences;

	public void Play(int soundID) {
		if (soundID >= 0 && soundID < BankReferences.Count)
			FMOD_Thuleanx.AudioManager.Instance.PlayOneShot(BankReferences[soundID]);
	}
}
