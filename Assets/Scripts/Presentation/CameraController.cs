using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] private CustomFloatEvent changeCameraSizeEvent;
#pragma warning restore 0649

	private Camera myCamera;

	private void Awake()
	{
		myCamera = GetComponent<Camera>();
		changeCameraSizeEvent.Event += ChangeCameraSize;
	}

	private void ChangeCameraSize(float newSize)
	{
		myCamera.orthographicSize = newSize;
	}
}
