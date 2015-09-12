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
	private float _box_height;
	private float _phone_width = 98;
	private float[] _messageMargins = new float[4]{10.0f, 10.0f, 10.0f, 10.0f}; //left, top, right, bottom
	private float _message_width;
	
	void Awake ()
	{
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
		_tex = new RenderTexture (2048, 2048, 32, RenderTextureFormat.ARGB32);
		_tex.Create ();
		_cam.targetTexture = _tex;
		
		// textStyle
		_textStyle = new GUIStyle();
		_textStyle.normal.textColor = Color.black;
		_textStyle.wordWrap = true;
		_textStyle.fontSize = 200;
		_textStyle.font = (Font)Resources.Load("MunroSmall");

		// canvas
		GameObject canvas = new GameObject ("Canvas");
		canvas.layer = LayerMask.NameToLayer ("renderTexCam");
		canvas.AddComponent<Canvas> ();
		Canvas myCanvas = canvas.GetComponent<Canvas> ();
		myCanvas.renderMode = RenderMode.ScreenSpaceCamera;
		myCanvas.worldCamera = _cam;

        float message_height = _tex.height - (_messageMargins[1] + _messageMargins[3]);
        _message_width = _tex.width - (_messageMargins[0] + _messageMargins[2]);
		_box_height = _tex.height;

		// text
		GameObject myText = new GameObject ("Text");
		myText.layer = LayerMask.NameToLayer ("renderTexCam");
		myText.AddComponent<RectTransform> ();
		myText.AddComponent<CanvasRenderer> ();
		myText.AddComponent<Text> ();

		RectTransform _textRect = myText.GetComponent<RectTransform> ();
		_textRect.localScale = new Vector3(1, 1, 1);
		myText.transform.SetParent( myCanvas.transform );
		_textRect.localPosition = new Vector3(_messageMargins[0], 0, 0); // add margin to text on left
		_textRect.sizeDelta = new Vector2(_message_width - _messageMargins[2], message_height - _messageMargins[3]); // add margin on right

		_textComponent = myText.GetComponent<Text> ();
		_textComponent.font = _textStyle.font;
		_textComponent.fontSize = _textStyle.fontSize;
	}
	
	public void AddNewMessage(string message)
	{
		StartCoroutine( CreateMessage(message) );
	}
	
	IEnumerator CreateMessage (string message)
	{
        if (message.Length == 0) yield break;

        _cam.backgroundColor = new Color(Random.value, Random.value, Random.value);
        _textComponent.text = message;
        _box_height = _textStyle.CalcHeight(new GUIContent (message), _message_width) + (_messageMargins[1] + _messageMargins[3]);
        _textStyle.fontSize = _textComponent.fontSize;
        yield return new WaitForFixedUpdate(); // actually causes display of the message in the Text object
		
		//yield return new WaitForEndOfFrame(); // fix for: "ReadPixels was called to read pixels from system frame buffer, while not inside drawing frame."
		Texture2D tx2d = new Texture2D (_tex.height, _tex.width, TextureFormat.RGB24, false);
		QualitySettings.antiAliasing = 0;
		tx2d.filterMode = FilterMode.Point;
		RenderTexture.active = _tex;
		Rect sizeRect = new Rect (0, 0, _tex.width, _box_height);
		tx2d.ReadPixels (sizeRect, 0, 0);
		tx2d.Apply ();
		RenderTexture.active = null;

		GameObject note = new GameObject ("note");
		SpriteRenderer sr = note.AddComponent<SpriteRenderer> ();
        sr.sortingLayerName = "phone";
        sr.sprite = Sprite.Create (tx2d, sizeRect, new Vector2 (0.0F, 0.0F));

        // pivot in the upper left corner
        float pivotX = - sr.sprite.bounds.center.x / sr.sprite.bounds.extents.x / 2 + 0.5f;
        float pivotY = sr.sprite.bounds.center.y / sr.sprite.bounds.extents.y / 2 + 0.5f;
        float pixelsToUnits = sr.sprite.textureRect.width / sr.sprite.bounds.size.x;
        sr.sprite = Sprite.Create (tx2d, sizeRect, new Vector2 (pivotX, pivotY) , pixelsToUnits);

        float scale = _phone_width/_tex.width;
        note.transform.localScale = new Vector3(scale, scale, 0);
        note.transform.localPosition = new Vector3(-2.26F, 0.53F, 0);

        // move all other notes down
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Note");
        foreach (GameObject n in notes)
            n.SendMessage("Move", sr.sprite.bounds.size.y*scale);

        // set attributes
        note.AddComponent<message> ();
        note.tag = "Note";
	}
}	
