using UnityEngine;
using System.Collections;

public class PlaceBlock : MonoBehaviour {
	public GameObject goldBrick;
	public GameObject Building;
	
	private GameObject tmpBlock;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.B)) {
			print ("Build");
			this.showBlock();
		}
		
		if(Input.GetKeyUp(KeyCode.B)) {
			print ("End Build");
			this.placeBlock();
		}
		
		if(Input.GetAxis("Mouse ScrollWheel") !=0) {
			print ("Move");
			this.tmpBlock.transform.Translate(Vector3.forward *Input.GetAxis("Mouse ScrollWheel"));
		}
	}
	
	void showBlock() {
		Vector3 pos =this.gameObject.transform.position;
		Vector3 dir =this.gameObject.transform.forward;
		Quaternion rot =this.gameObject.transform.rotation;
		float dist =5.0f;
		
		pos.y -=0.75f;
		
		Vector3 spawnPos =pos +dir *dist;
		
		this.tmpBlock =Instantiate(this.goldBrick, spawnPos, rot) as GameObject;
		this.tmpBlock.transform.parent =this.transform;
	}
	
	void placeBlock() {
		this.tmpBlock.transform.parent =this.Building.transform;
		this.tmpBlock.collider.isTrigger =false; //Because we have place the block onto the game scene we no longer want it to just be a trigger
		this.tmpBlock.collider.attachedRigidbody.useGravity =true; //Allow gravity to affect the block
	}
	
}
