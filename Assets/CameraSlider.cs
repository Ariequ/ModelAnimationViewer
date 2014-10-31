using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraSlider : MonoBehaviour
{
	float originFiledOfView;
	Slider slider;

	void Start ()
	{
		Camera.main.fieldOfView = originFiledOfView = 64;
		slider = GetComponent<Slider> ();
	}

	public void OnValueChange ()
	{
		if (slider == null) {
			return;
		}
		Camera.main.fieldOfView = Mathf.Max (0.1f, originFiledOfView + (slider.value - 0.5f) * 200);
	}

	public void Update ()
	{
		if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
			slider.value += Input.GetAxis ("Mouse ScrollWheel") / 20;
		}
	}
}
