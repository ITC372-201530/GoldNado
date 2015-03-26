using UnityEngine;
using System.Collections;

public class PlaceBlock : MonoBehaviour {
	public GameObject goldBrick;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.B)) {
			print ("Build");
			this.placeBlock();
		}
		
		if(Input.GetKeyUp(KeyCode.B)) {
			print ("End Build");
		}
	}
	
	void placeBlock() {
		Instantiate(this.goldBrick, this.gameObject.transform.position, this.gameObject.transform.rotation);
	}
}
