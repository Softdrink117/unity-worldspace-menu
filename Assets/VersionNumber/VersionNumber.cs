using UnityEngine;
using System.Collections;

// Inspired by https://forum.unity3d.com/threads/automatic-version-increment-script.144917/
namespace Softdrink{

	// Class storing actual version number info
	[System.Serializable]
	public class VersionNum{
		[SerializeField]
		private int majorNumber = 0;

		[SerializeField]
		private int minorNumber = 0;

		[SerializeField]
		private int buildNumber = 0;

		[SerializeField]
		private string suffix = "";

		// CONSTRUCTOR
		public VersionNum(VersionNum input){
			majorNumber = input.getMajor();
			minorNumber = input.getMinor();
			buildNumber = input.getBuild();
			suffix = input.getSuffix();
		}

		// METHODS

		public void incrementBuild(){
			buildNumber++;
		}

		// GETTERS

		public int getMajor(){
			return majorNumber;
		}

		public int getMinor(){
			return minorNumber;
		}

		public int getBuild(){
			return buildNumber;
		}

		public string getSuffix(){
			return suffix;
		}

		// STRING FORMAT GETTER

		private string formatted = "";
		public string nf(){
			formatted = "";

			formatted += majorNumber;
			formatted += ".";

			formatted += minorNumber.ToString("D2");
			formatted += ".";

			formatted += buildNumber.ToString("D3");

			formatted += suffix;

			return formatted;
		}

		// SETTERS

		public void set(int majIn, int minIn, int buildIn, string suffixIn){
			majorNumber = majIn;
			minorNumber = minIn;
			buildNumber = buildIn;
			suffix = suffixIn;
		}

		public void set(int majIn, int minIn, int buildIn){
			majorNumber = majIn;
			minorNumber = minIn;
			buildNumber = buildIn;
			suffix = "";
		}

		public void set(int majIn, int minIn){
			majorNumber = majIn;
			minorNumber = minIn;
			suffix = "";
		}

		public void setMajor(int majIn){
			majorNumber = majIn;
		}

		public void setMinor(int minIn){
			minorNumber = minIn;
		}

		public void setBuild(int buildIn){
			buildNumber = buildIn;
		}

		public void resetBuild(){
			buildNumber = 0;
		}

		public void setSuffix(string suffixIn){
			suffix = suffixIn;
		}

		
	}

	[AddComponentMenu("Scripts/Utility/Version Number")]
	public class VersionNumber : MonoBehaviour {

		public static VersionNum version;

		public VersionNum versionNumber;

		void Awake(){
			version = new VersionNum(versionNumber);
		}

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		void OnValidate(){
			version = new VersionNum(versionNumber);
		}

		public void ResetBuildNumber(){
			versionNumber.resetBuild();
			version = new VersionNum(versionNumber);
		}

		public static string nf(){
			// if(version == null) version = new VersionNum(versionNumber);
			if(version == null) return "";
			return version.nf();
		}
	}
}
