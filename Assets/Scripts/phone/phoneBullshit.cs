using UnityEngine;
using System.Collections;
using System.IO;

public class phoneBullshit : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		print (Application.dataPath);
		print (Application.dataPath + "/Resources/");
		string[] files = System.IO.Directory.GetFiles (Application.dataPath + "/Resources/", "*.*");
		print(files);

		DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Resources");
		print (dir);
		FileInfo[] info = dir.GetFiles("*.*");
		print (info);
		/*string fullNames = info.Select(f => f.FullName).ToArray();
		foreach (string f in info) 
		{
			print(f);
		}
		*/

		Object[] textures = (Object[]) Resources.LoadAll("Textures", typeof(Texture2D));
		print (textures);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
