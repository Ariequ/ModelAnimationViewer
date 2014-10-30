using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RotateController : MonoBehaviour {

	public GameObject root;
	Slider slider;
	float mouseX;

	void Start()
	{
		slider = GetComponent<Slider>();
	}

	public void OnValueChange (Slider slider)
	{
		root.transform.rotation = Quaternion.Euler(new Vector3(0, (slider.value)*360, 0));
	}

	void Update()
	{
		if (Input.GetMouseButton(0) && Input.mousePosition.x > Screen.width / 4)
		{
			float x = Input.GetAxis("Mouse X")/20f;
			slider.value += x;
		}
	}
}
