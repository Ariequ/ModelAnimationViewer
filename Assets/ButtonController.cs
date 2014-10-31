using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour 
{
	private string PrefabPath = "Prefabs";
	GameObject root;
	AnimationSpeedController animationSpeedController;

	void Start()
	{
		root = GameObject.Find("root");
		animationSpeedController = root.GetComponent<AnimationSpeedController>();
	}

	public void DrawPrefab()
	{
		for(int i = 0; i< transform.parent.childCount; i++)
		{
			transform.parent.GetChild(i).SendMessage("EnableButton");
		}

		Button button = GetComponent<Button>() as Button;
		button.interactable = false;

		while(root.transform.childCount > 0)
		{
			Transform o = root.transform.GetChild(0);
			o.parent = null;
			Destroy(o.gameObject);
		}

		string prefabPath = PrefabPath + "/" + transform.name;

		GameObject go = Resources.Load (prefabPath, typeof(GameObject)) as GameObject;
		
		GameObject obj = GameObject.Instantiate(go) as GameObject;

		SkinnedMeshRenderer render = obj.GetComponentInChildren<SkinnedMeshRenderer> ();
		
		if (render != null) {
			render.sharedMaterial.shader = Shader.Find ("Transparent/Cutout/Diffuse");
        }
		else
		{
			Debug.Log("render is null");
		}

		obj.AddComponent<AnimationInfo>();

		obj.transform.parent = root.transform;
		obj.transform.position = Vector3.zero;
		obj.transform.rotation = Quaternion.identity;

		animationSpeedController.OnAnimationChange();
	}

	public void EnableButton()
	{
		Button button = GetComponent<Button>() as Button;
		button.interactable = true;
	}
}
