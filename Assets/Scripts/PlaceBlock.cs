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
		Vector3 pos =this.gameObject.transform.position;
		Vector3 dir =this.gameObject.transform.forward;
		Quaternion rot =this.gameObject.transform.rotation;
		float dist =10.0f;
		Vector3 spawnPos =pos +dir *dist;
		
		GameObject go =Instantiate(this.goldBrick, spawnPos, rot) as GameObject;
		go.transform.parent =this.transform;
	}
}
