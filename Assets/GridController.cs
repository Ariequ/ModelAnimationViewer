using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{	
	public TextAsset asset; // Assign that variable through inspector
	private string assetText;

	public Transform root;

	// Use this for initialization
	void Start ()
	{
		assetText = asset.text;

		string[] nameArray = assetText.Split (':');
		GridLayoutGroup grid = GetComponent<GridLayoutGroup> ();
		RectTransform rect = grid.GetComponent<RectTransform> ();
		rect.sizeDelta = new Vector2 (grid.cellSize.x, grid.cellSize.y * nameArray.Length);
            
		for (int i=1; i< nameArray.Length; i++) {
			Button button = Resources.Load ("Button", typeof(Button)) as Button;

			Button b = GameObject.Instantiate (button) as Button;
			b.GetComponentInChildren<Text> ().text = nameArray [i];
			b.transform.parent = transform;
			b.name = nameArray [i];
		}

		rect.position = new Vector2(rect.position.x, -rect.sizeDelta.y);
	}
	


}
