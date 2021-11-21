using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class TentacleGroup : MonoBehaviour {
	List<Tentacle> tentacles;
	List<TentacleOriginalInfo> infos = new List<TentacleOriginalInfo>();
	public float Length = 0;
	public float wiggleIntervalMult = 1;

	void Awake() {
		tentacles = new List<Tentacle>(GetComponentsInChildren<Tentacle>());
		foreach (Tentacle ten in tentacles)
			infos.Add(new TentacleOriginalInfo(ten));
	}

	void Update() {
		for (int i = 0;  i < tentacles.Count; i++) {
			Tentacle ten = tentacles[i];
			TentacleOriginalInfo info = infos[i];

			ten.TailLength = Length;
			ten.wiggleInterval = wiggleIntervalMult * info.interval;
		}
	}

	struct TentacleOriginalInfo {
		public float amplitude;
		public float interval;

		public TentacleOriginalInfo(Tentacle ten) {
			ten.Init();
			amplitude = ten.wiggleAmplitude;
			interval = ten.wiggleInterval;
		}
	}
}