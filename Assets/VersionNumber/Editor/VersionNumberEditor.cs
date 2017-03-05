using UnityEngine;
using UnityEditor;
namespace Softdrink{
	[InitializeOnLoad]
	[CustomEditor(typeof(VersionNumber))]
	public class VersionNumberTextEditor :  Editor{

		// Logic to proc a Build Number update every time the scene is loaded or code recompiled
		static VersionNumberTextEditor(){

			EditorApplication.update += Increment;
		}

		public static void Increment(){
			EditorApplication.update -= Increment;

			VersionNumber t =  FindObjectOfType(typeof(VersionNumber)) as VersionNumber;
			t.versionNumber.incrementBuild();
		}

		// Normal Custom Editor logic
		public override void OnInspectorGUI(){
			DrawDefaultInspector();

			VersionNumber t = target as VersionNumber;

			if(GUILayout.Button("Reset Build Number")) t.ResetBuildNumber();
		}
		
	}

}