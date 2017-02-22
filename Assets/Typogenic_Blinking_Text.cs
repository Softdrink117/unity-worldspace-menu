using UnityEngine;
using System.Collections;

namespace Softdrink{

	[RequireComponent(typeof(TypogenicText))]
	public class Typogenic_Blinking_Text : MonoBehaviour {

		[SerializeField]
		[TooltipAttribute("The amount of time for the blink to go from fully off to fully on, in seconds.")]
		private float frequency = 1.0f;

		[SerializeField]
		[TooltipAttribute("The AnimationCurve that will drive the Blink. Note that 0 in X is the start (initial values) of the Blink, and 1 is the end (target values) of the Blink.")]
		private AnimationCurve interpolationCurve = new AnimationCurve(new Keyframe(0f,0f), new Keyframe(1f, 1f));
		
		[SerializeField]
		[TooltipAttribute("If checked, the animation will use Time.unscaledDeltaTime instead of Time.deltaTime to process.")]
		private bool ignoreTimescale = true;

		[SerializeField]
		[TooltipAttribute("What color should the TypogenicText Blink to? \nNote that currently only single text fill colors are supported.")]
		private Color targetColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		private Color startColor;

		[SerializeField]
		[TooltipAttribute("Should the Blink also modify the Thickness of the Typogenic shader? \nThis can be useful when trying to animate shadows.")]
		private bool modifyThickness = true;
		private float startThickness;

		[SerializeField]
		[TooltipAttribute("The target Thickness value, only used when modifyThickness is enabled.")]
		private float targetThickness = 1.0f;

		// Internal reference to the attached Typogenic Text component
		private TypogenicText _text;
		// Internal reference to the attached Material
		private Material _mat;

		void Awake(){
			_text = gameObject.GetComponent<TypogenicText>() as TypogenicText;
			if(_text == null) Debug.LogError("ERROR! The Typogenic_Blinking_Text was unable to associate a reference to the attached TypogenicText!", this);
			startColor = _text.ColorTopLeft;

			_mat = gameObject.GetComponent<Renderer>().material;
			if(_mat == null) Debug.LogError("ERROR! The Typogenic_Blinking_Text was unable to associate a reference to the attached TypogenicText's Material!", this);
			startThickness = _mat.GetFloat("_Thickness");
		}
		private float time = 0f;
		private float t = 0f;
		private bool forwards = true;
		void Update(){
			// Sample time along a 0...1...0 curve based on the specified frequency
			if(forwards){
				if(ignoreTimescale) time += Time.unscaledDeltaTime;
				else time += Time.deltaTime;
			}else{
				if(ignoreTimescale) time -= Time.unscaledDeltaTime;
				else time -= Time.deltaTime;
			}
			if(time >= frequency) forwards = false;
			else if(time <= 0f) forwards = true;

			t = time/frequency;

			// Clamp t to 0...1
			if(t < 0f) t= 0f;
			else if(t > 1f) t = 1f;

			// Resample t based on the AnimationCurve
			t = interpolationCurve.Evaluate(t);

			// Apply to the Typogenic Text Color modifier
			_text.ColorTopLeft = Color.Lerp(startColor, targetColor, t);

			if(modifyThickness){
				_mat.SetFloat("_Thickness", Mathf.Lerp(startThickness, targetThickness, t));
			}

			//Debug.Log(t);
		}
	}

}
