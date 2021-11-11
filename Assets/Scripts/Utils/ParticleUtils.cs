using UnityEngine;

public static class ParticleUtils {
	public static void EmitOnce(GameObject system, Transform transform) => EmitOnce(system,transform.position, transform.rotation);
	public static void EmitOnce(GameObject system, Vector2 pos) => EmitOnce(system, pos, Quaternion.identity);
	public static void EmitOnce(GameObject system, Vector2 pos, Quaternion rotation) {
		GameObject instance = GameObject.Instantiate(system, pos, rotation);
		instance.GetComponent<ParticleCombo>().PlayOnce();
	}
}