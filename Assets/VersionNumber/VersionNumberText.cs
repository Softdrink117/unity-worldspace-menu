using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Softdrink{
	[AddComponentMenu("Scripts/Utility/Version Number Text")]
	[RequireComponent(typeof(Text))]
	public class VersionNumberText : MonoBehaviour {

		[SerializeField]
		[MultilineAttribute(3)]
		[TooltipAttribute("Supplemental Text to include before the version Number. \nUseful for specific information about the current build.")]
		string prependText = "";

		[SerializeField]
		[MultilineAttribute(3)]
		[TooltipAttribute("Supplemental Text to include after the version Number. \nUseful for specific information about the current build.")]
		string appendText = "";

		// Internal reference to the attached UI Text component
		private Text _text;

		void Awake(){
			GetReferences();
		}

		// Update to reflect any changes
		void OnValidate(){
			if(_text == null) GetReferences();

			SetContent();
		}

		// Assign references to text component
		void GetReferences(){
			_text = gameObject.GetComponent(typeof(Text)) as Text;
			if(_text == null) Debug.LogError("ERROR! The VersionNumberText could not assocaite a reference to the attached Text component!", this);
		}

		// Set the display content that is fed to the text component
		private string displayContent = "";
		void SetContent(){
			displayContent = "";
			if(appendText != "" || prependText != ""){
				displayContent += prependText;
				displayContent += VersionNumber.nf();
				displayContent += appendText;
			}else{
				displayContent = VersionNumber.nf();
			}

			_text.text = displayContent;
		}

	}

}
