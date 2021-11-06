using UnityEngine;
using System.Collections.Generic;

public class Override : MonoBehaviour {
	public List<ParticleEmitterInfo> particles;

	public void Trigger(Vector2 src, Vector2 des) {
		foreach (var eff in particles) {
			if (eff.emitToDesc) {
				eff.system.transform.position = des;
				var shape = eff.system.shape;
				shape.position = src-des;
			} else {
				eff.system.transform.position = src;
			}
			eff.system.Play();
		}
	}

	[System.Serializable]
	public class ParticleEmitterInfo {
		public ParticleSystem system;
		public bool emitToDesc;
	}
}
