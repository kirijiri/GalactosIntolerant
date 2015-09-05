using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class phoneSetup : MonoBehaviour
{
	private Camera _cam;
	private RenderTexture _tex;
	private GUIText _text;
	private Rect _sizeRect;
	
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
			new Vector3 (0, 0, 0), 
			Quaternion.FromToRotation (new Vector3 (0, 0, 0), new Vector3 (0, 0, 1)));
		
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
		_tex = new RenderTexture (256, 256, 32, RenderTextureFormat.ARGB32);
		_tex.Create ();
		_cam.targetTexture = _tex;
		
		// text
		GameObject bananas = new GameObject ("text");
		_text = bananas.AddComponent<GUIText> ();
		_text.transform.position = new Vector3 (0.5f, 1.0f, 0f);
		_text.color = Color.black;
		
		// put into custom layer as well
		bananas.layer = LayerMask.NameToLayer ("renderTexCam");
		
		// update text every couple of seconds
		StartCoroutine(UpdateNews());
	}
	
	IEnumerator UpdateNews ()
	{
		string message = messages[Random.Range(0, messages.Count)];
		print (message);

		_cam.backgroundColor = new Color(Random.value, Random.value, Random.value);

		GUIContent content = new GUIContent(message);
		
		GUIStyle textStyle = new GUIStyle();

		//int fontSize = 500; // random number of how big I guess it should be on screen (will be relative to screen size then anyway
		//textStyle.fontSize = Mathf.Min(Mathf.FloorToInt(256 * fontSize/1000), Mathf.FloorToInt(200 * fontSize/1000));
		//_text.fontSize = textStyle.fontSize;

		_text.fontSize = Mathf.RoundToInt (100.0f * 256.0f / (256.0f * 1.0f));
		
		// align top center
		_text.anchor = TextAnchor.LowerLeft;
		
		// center is 900/2
		//_sizeRect = new Rect (_tex.width/2 - 900/2, 0,  900, 200);
		_sizeRect = new Rect (0, 0, _tex.width, 200);
		print (_sizeRect);
		
		((GUIText)_text.GetComponent<GUIText> ()).text = message;
		
		GUI.Label(_sizeRect, "GEWFWQWDQWDW", textStyle);

		yield return new WaitForEndOfFrame(); // fix for: "ReadPixels was called to read pixels from system frame buffer, while not inside drawing frame."
		Texture2D tx2d = new Texture2D (_tex.height, _tex.width, TextureFormat.RGB24, false);
		RenderTexture.active = _tex;
		tx2d.ReadPixels (_sizeRect, 0, 0);
		tx2d.Apply ();
		RenderTexture.active = null;
		
		SpriteRenderer sr = new GameObject ("note").AddComponent<SpriteRenderer> ();
		sr.sprite = Sprite.Create (tx2d, _sizeRect, new Vector2 (0.5f, 0.5f));
		
		yield return new WaitForSeconds(1);
		//yield return new WaitForEndOfFrame();
		StartCoroutine( UpdateNews() );
	}
}
