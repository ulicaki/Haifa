using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	public Transform camTransformHor;

	// How long the object should shake for.
	public float shakeDuration;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount;
	public float decreaseFactor = 1.0f;

	Vector3 originalPos;
	Vector3 originalPosHor;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}

		if (camTransformHor == null)
		{
			camTransformHor = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{

		originalPos = camTransform.localPosition;
		originalPosHor = camTransformHor.localPosition;
	}





	void Update()
	{
		if (shakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			camTransformHor.localPosition = originalPosHor + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
			camTransformHor.localPosition = originalPosHor;
		}
	}
}