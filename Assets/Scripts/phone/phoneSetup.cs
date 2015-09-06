using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class phoneSetup : MonoBehaviour
{
	private Camera _cam;
	private RenderTexture _tex;
	private GUIStyle _textStyle;
	private Text _textComponent;
	private float _height;
	
	private List<string> messages = new List<string>();
	
	void Start ()
	{
		messages.Add("hello");
		messages.Add("I'm testing things");
		messages.Add("and more");
		messages.Add("and nothing is better than this!!!");
		
		// Find the 'main' camera object.
		GameObject original = GameObject.FindWithTag ("MainCamera");
		
		// main camera should render everything BUT layer 11
		((Camera)original.GetComponent<Camera> ()).cullingMask = ~(1 << LayerMask.NameToLayer ("renderTexCam"));
		
		// Create a new camera to use, copying from the main camera
		_cam = (Camera)Camera.Instantiate (
			original.GetComponent<Camera> (), 
			new Vector3 (0, 0, -10), 
			Quaternion.FromToRotation (new Vector3 (0, 0, 1), new Vector3 (0, 0, 1)));
		
		// Set the background of the new one and disable audioListener
		_cam.backgroundColor = Color.green;
		((AudioListener)_cam.GetComponent (typeof(AudioListener))).enabled = false;
		
		//enable camera
		_cam.enabled = true;
		
		// add to render layer
		_cam.gameObject.layer = LayerMask.NameToLayer ("renderTexCam");
		
		// new camera should render only layer 11
		_cam.cullingMask = (1 << LayerMask.NameToLayer ("renderTexCam"));
		
		// create new render texture
		_tex = new RenderTexture (1024, 1024, 32, RenderTextureFormat.ARGB32);
		_tex.Create ();
		_cam.targetTexture = _tex;
		
		// textStyle
		_textStyle = new GUIStyle();
		_textStyle.normal.textColor = Color.black;
		_textStyle.wordWrap = true;
		_textStyle.fontSize = 22;
		_textStyle.font = (Font)Resources.Load("MunroSmall");

		// canvas
		GameObject canvas = new GameObject ("Canvas");
		canvas.layer = LayerMask.NameToLayer ("renderTexCam");
		canvas.AddComponent<Canvas> ();
		Canvas myCanvas = canvas.GetComponent<Canvas> ();
		myCanvas.renderMode = RenderMode.ScreenSpaceCamera;
		myCanvas.worldCamera = _cam;

		// text
		GameObject myText = new GameObject ("Text");
		myText.layer = LayerMask.NameToLayer ("renderTexCam");
		myText.AddComponent<RectTransform> ();
		myText.AddComponent<CanvasRenderer> ();
		myText.AddComponent<Text> ();

		RectTransform _textRect = myText.GetComponent<RectTransform> ();
		_textRect.localScale = new Vector3(1, 1, 1);
		myText.transform.parent = myCanvas.transform;
		_textRect.localPosition = new Vector3(-_tex.width/2+90/2, 0, 0);
		_textRect.sizeDelta = new Vector2(90, _tex.height);

		_textComponent = myText.GetComponent<Text> ();
		_textComponent.font = _textStyle.font;
		_textComponent.fontSize = _textStyle.fontSize;

		// update text every couple of seconds
		StartCoroutine(UpdateNews());
	}
	
	IEnumerator UpdateNews ()
	{
		_cam.backgroundColor = new Color(Random.value, Random.value, Random.value);

		//yield return new WaitForEndOfFrame(); // fix for: "ReadPixels was called to read pixels from system frame buffer, while not inside drawing frame."
		Texture2D tx2d = new Texture2D (_tex.height, _tex.width, TextureFormat.RGB24, false);
		RenderTexture.active = _tex;
		Rect sizeRect = new Rect (0, 0, 90, _height);
		tx2d.ReadPixels (sizeRect, 0, 0);
		tx2d.Apply ();
		RenderTexture.active = null;
		
		SpriteRenderer sr = new GameObject ("note").AddComponent<SpriteRenderer> ();
		sr.sprite = Sprite.Create (tx2d, sizeRect, new Vector2 (0.5f, 0.5f));

		// prepare next idle message
		string message = messages[Random.Range(0, messages.Count)];
		_textComponent.text = message;

		_height = _textStyle.CalcHeight(new GUIContent (message), 90);
		_textStyle.fontSize = _textComponent.fontSize;
		print ("\n" + message + ":  " + _height);
		
		yield return new WaitForSeconds(2);
		StartCoroutine( UpdateNews() );
	}
}	