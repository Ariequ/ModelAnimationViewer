using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimationInfo : MonoBehaviour
{

	Animator animator;
	Text info;
	AnimatorStateInfo state;
	GameObject animationList;
	Text lastText;

	// Use this for initialization
	void Awake ()
	{
		animator = GetComponent<Animator> ();
		info = GameObject.Find("AnimationNameContent").GetComponent<Text>();
		animationList = GameObject.Find("AnimationList");

		while (animationList.transform.childCount > 0)
		{
			Transform obj = animationList.transform.GetChild(0);
			obj.parent = null;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		state = animator.GetCurrentAnimatorStateInfo(0);
		if (state.IsName("idle"))
		{
			info.text = "idle";

			addTypeToList("idle");
		}
		else if (state.IsName("attack01"))
		{
			info.text = "attack01";

			addTypeToList("attack01");
		}
		else if (state.IsName("attack02"))
		{
			info.text = "attack02";

			addTypeToList("attack02");
		}
		else if (state.IsName("die"))
		{
			info.text = "die";

			addTypeToList("die");
		}
		else if (state.IsName("fall"))
		{
			info.text = "fall";

			addTypeToList("fall");
		}
		else if (state.IsName("fly"))
		{
			info.text = "fly";

			addTypeToList("fly");
		}
		else if (state.IsName("kill01"))
		{
			info.text = "kill01";

			addTypeToList("kill01");
		}
		else if (state.IsName("fight"))
		{
			info.text = "fight";

			addTypeToList("fight");
		}
		else if (state.IsName("walk"))
		{
			info.text = "walk";

			addTypeToList("walk");
		}
		else if (state.IsName("kill02"))
		{
			info.text = "kill02";
			
			addTypeToList("kill02");
		}
		else if (state.IsName("run"))
		{
			info.text = "run";
			
			addTypeToList("run");
		}
	}

	void addTypeToList(string type)
	{
		if (lastText != null)
		{
			lastText.color = Color.white;
		}

		Transform child = animationList.transform.FindChild(type);

		GameObject text;

		if (child == null)
		{
			text = GameObject.Find(type);

			if (text == null)
			{
				text = GameObject.Instantiate(Resources.Load ("Text")) as GameObject;
			}

			text.SetActive(true);
			text.transform.parent = animationList.transform;
			text.name = type;
			text.GetComponent<Text>().text = type;
			text.GetComponent<Text>().color = Color.blue;

			child = text.transform;
		}
		else
		{
			child.gameObject.GetComponent<Text>().color = Color.black;
		}

		lastText = child.gameObject.GetComponent<Text>();
	}
}
