using UnityEngine;

public static class CoolFunctions {
	public static float Heartbeat(float t) {
		return OneHeartBeat(t) + .2f * OneHeartBeat(t - .8f);
	}
	public static float OneHeartBeat(float x) => Mathf.Pow(Mathf.Sin(x * 2 * Mathf.PI-1.666f), 63)*Mathf.Sin(x * 2 * Mathf.PI+1.5f-1.666f)*8;
}