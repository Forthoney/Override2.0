using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(LineRenderer))]
public class Tentacle : MonoBehaviour {
	[SerializeField] int boneNumbers = 10;
	[SerializeField] float tailLength = 2f;
	public float TailLength {
		get => tailLength;
		set => tailLength = value;
	}
	[SerializeField, MinMaxSlider(1, 200f)] Vector2 smoothCoeficient;
	[SerializeField] Transform targetDir;
	[SerializeField] float jointMaxSpeed = 10f;

	[Header("Wiggle")]
	[SerializeField, MinMaxSlider(0f, 60f)] Vector2 wiggleAmplitudeRange;
	[SerializeField, MinMaxSlider(0f, 1f)] Vector2 wiggleIntervalRange;

	float randomizer;
	float startRotation;
	[HideInInspector] public float wiggleAmplitude;
	[HideInInspector] public float wiggleInterval;
	Vector3[] segmentPoses;
	Vector3[] segmentV;

	public LineRenderer Renderer { get; private set; }

	void Awake() {
		Renderer = GetComponent<LineRenderer>();
		if (targetDir == null) targetDir = transform;
	}

	bool _init = false;
	public void Init() {
		if (!_init) {
			if (targetDir == null) targetDir = transform;
			_init = true;
			randomizer = Random.Range(0f, 1f);
			startRotation = targetDir.localEulerAngles.z;
			wiggleAmplitude = Random.Range(wiggleAmplitudeRange.x, wiggleAmplitudeRange.y);
			wiggleInterval = Random.Range(wiggleIntervalRange.x, wiggleIntervalRange.y);
		}
	}

	void Start() {
		Renderer.positionCount = boneNumbers;
		segmentPoses = new Vector3[boneNumbers];
		for (int i = 0; i < boneNumbers; i++)
			segmentPoses[i] = targetDir.position;
		segmentV = new Vector3[boneNumbers];
		Init();
	}

	void Update() {
		targetDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(2 * Mathf.PI * ((Time.unscaledTime / wiggleInterval) + randomizer)) * wiggleAmplitude + startRotation);
		segmentPoses[0] = targetDir.position;

		for (int i = segmentPoses.Length - 1; i >= 1; i--)
			segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i],
				segmentPoses[i - 1] - targetDir.right * tailLength / (boneNumbers - 1),
				ref segmentV[i],
				Mathf.Lerp(1 / smoothCoeficient.y, 1 / smoothCoeficient.x, (i - 1) / (boneNumbers - 1)), jointMaxSpeed);

		Renderer.SetPositions(segmentPoses);
	}

	public void ResetPos() {
		for (int i = 0; i < boneNumbers; i++)
			segmentPoses[i] = targetDir.position;
		Renderer.SetPositions(segmentPoses);
	}
}