using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using System.IO;
using System.Collections.Generic;

public class AnimationViewer : Editor
{
	private static string ModelPath = Application.dataPath + "/Model";
	private static string animationControllerPath = "Assets/AnimationController";
	private static string PrefabPath = "Assets/Resources/Prefabs";
	private static string FileName = "ModelNames";  // This contains the name of the file. Don't add the ".txt"
	// Assign in inspector
//	private static TextAsset asset; // Gets assigned through code. Reads the file.
	private static StreamWriter writer; // This is the writer that writes to the file
	
	static void AppendString (string appendString)
	{
//		writer = new StreamWriter(Application.dataPath + "/Resources/" + FileName + ".txt"); // Does this work?
//		writer.Flush();
//		writer.WriteLine(appendString);

		File.WriteAllText (Application.dataPath + "/Resources/" + FileName + ".txt", appendString);
	}

	[MenuItem("AnimationViewer/ViewAnimation")]
	static void DoCreateAnimationAssets ()
	{
		DirectoryInfo raw = new DirectoryInfo (ModelPath);		
		string appendString = "";
		foreach (DirectoryInfo dictory in raw.GetDirectories()) {
			appendString += ":" + dictory.Name;
			State lastState = null;

			AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath (animationControllerPath + "/" + dictory.Name + ".controller");
			//得到它的Layer， 默认layer为base 你可以去拓展
			AnimatorControllerLayer layer = animatorController.GetLayer (0);
			List<State> stateList = new List<State>();
			foreach (FileInfo file in dictory.GetFiles()) {
				//每个文件夹就是一组帧动画，这里把每个文件夹下的所有图片生成出一个动画文件
				//创建animationController文件，保存在Assets路径下

				if (file.Name.Contains ("@") && !file.Name.Contains (".meta")) {
					//把动画文件保存在我们创建的AnimationController中
					string fbxPath = "Assets/Model/" + dictory.Name + "/" + file.Name;
//					Debug.Log (fbxPath);
					lastState = AddStateTransition (fbxPath, layer, lastState);
					stateList.Add(lastState);
				}
			}

			UnityEditorInternal.StateMachine sm = layer.stateMachine;
			sm.AddTransition(stateList[stateList.Count -1], stateList[0]);

			BuildPrefab (dictory, animatorController);
		}

		Debug.Log (appendString + "===========");
		AppendString (appendString);
//		DrawPrefabToSceen ();
	}
	
	private static State AddStateTransition (string path, AnimatorControllerLayer layer, State lastState)
	{
		UnityEditorInternal.StateMachine sm = layer.stateMachine;
		//根据动画文件读取它的AnimationClip对象
		AnimationClip newClip = AssetDatabase.LoadAssetAtPath (path, typeof(AnimationClip)) as AnimationClip;
		AnimationUtility.SetAnimationType (newClip, ModelImporterAnimationType.Generic);
		//取出动画名子 添加到state里面
		State state = sm.AddState (newClip.name);
		state.SetAnimationClip (newClip, layer);
		//把state添加在layer里面
		if (lastState == null) {

//			sm.AddAnyStateTransition (state);
		} else {
			sm.AddTransition (lastState, state);
		}

		return state;
	}

	static void BuildPrefab (DirectoryInfo dictory, AnimatorController animatorCountorller)
	{
		string modelPath = "Assets/Model/" + dictory.Name + "/" + dictory.Name + ".FBX";

		GameObject go = AssetDatabase.LoadAssetAtPath (modelPath, typeof(GameObject)) as GameObject;
		go.name = dictory.Name;

		Animator animator = go.GetComponent<Animator> ();
		animator.runtimeAnimatorController = animatorCountorller;

		PrefabUtility.CreatePrefab (PrefabPath + "/" + go.name + ".prefab", go);
//		DestroyImmediate(go);
	}

	static void DrawPrefabToSceen ()
	{
		DirectoryInfo raw = new DirectoryInfo (Application.dataPath + "/Resources/Prefabs");		
		int cout = 0;
		foreach (FileInfo file in raw.GetFiles()) {
			if (!file.Name.Contains (".meta")) {
				string prefabPath = PrefabPath + "/" + file.Name;

				GameObject go = AssetDatabase.LoadAssetAtPath (prefabPath, typeof(GameObject)) as GameObject;

				GameObject obj = GameObject.Instantiate (go) as GameObject;

				obj.transform.position = Vector3.forward * cout++ * 5;
			}

		}
	}
}
