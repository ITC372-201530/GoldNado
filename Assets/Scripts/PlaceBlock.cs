using UnityEngine;
using System.Collections;

public class PlaceBlock : MonoBehaviour {
	public GameObject goldBrick;
	public GameObject Building;
	public GameObject camera;
	public GameObject shadow;
	
	private GameObject tmpBlock;
	private GameObject tmpShadow;
	
	private float lockPos = 0;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.B)) {
			this.showBlock();
		}
		
		if(Input.GetKeyUp(KeyCode.B)) {
			this.placeBlock();
		}
		
		if(Input.GetKey(KeyCode.C)) {
			if(this.tmpBlock !=null) {
				Quaternion rot =this.tmpBlock.transform.rotation;
				rot.y -=0.01f;
				this.tmpBlock.transform.rotation =rot;
			}
		}
		
		if(Input.GetKey(KeyCode.V)) {
			if(this.tmpBlock !=null) {
				Quaternion rot =this.tmpBlock.transform.rotation;
				rot.y +=0.01f;
				this.tmpBlock.transform.rotation =rot;
			}
		}
		
		if(Input.GetAxis("Mouse ScrollWheel") !=0) {
			if(this.tmpBlock !=null) {
				this.tmpBlock.transform.Translate(Vector3.forward *Input.GetAxis("Mouse ScrollWheel"));
			}
		}
		
		if(Input.GetAxis("Mouse Y") !=0) {
			//rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			//rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			if(this.tmpBlock !=null) {
				Vector3 upP =this.tmpBlock.transform.position;
				upP.y +=Input.GetAxis("Mouse Y");
				this.tmpBlock.transform.position =upP;
			}
		}
		
	}
	
	void showBlock() {
		Vector3 sPos =new Vector3(0,0,0);
		Quaternion sRot =Quaternion.identity;
		//sRot.x =90;
		
		Vector3 pos =this.camera.transform.position;
		Vector3 dir =this.camera.transform.forward;
		Quaternion rot =this.camera.transform.rotation;
		rot.x =0;
		float dist =5.0f;
		
		Vector3 spawnPos =pos +dir *dist;
		
		this.tmpBlock =Instantiate(this.goldBrick, spawnPos, rot) as GameObject;
		this.tmpShadow =Instantiate(this.shadow, spawnPos, this.shadow.transform.rotation) as GameObject;
		this.tmpShadow.transform.parent =this.tmpBlock.transform;
		
		this.tmpBlock.AddComponent("PlaceBlockDetection");
		PlaceBlockDetection spt =(PlaceBlockDetection)this.tmpBlock.GetComponent("PlaceBlockDetection");
		spt.detectionColor =new Color(0.5f, 0, 0, 0.5f);
		this.tmpBlock.transform.parent =this.transform;
		
		//GameObject child =this.tmpBlock.transform.Find("goldBrick").gameObject;
		
		this.tmpBlock.transform.rotation =Quaternion.Euler(this.lockPos, this.tmpBlock.transform.rotation.eulerAngles.y, this.lockPos); //In order to stop the block from rotating around to the camera rotation we need to reset the rotation back after we init the block
		//child.transform.rotation =Quaternion.Euler(this.lockPos, this.tmpBlock.transform.rotation.eulerAngles.y, this.lockPos); //In order to stop the block from rotating around to the camera rotation we need to reset the rotation back after we init the block
		Color col =this.tmpBlock.renderer.material.color;
		col.a =0.5f;
		this.tmpBlock.renderer.material.color =col;
	}
	
	void placeBlock() {
		Destroy(this.tmpShadow);
		//Before we can place a block we need to make sure it isnt inside another object
		PlaceBlockDetection spt =(PlaceBlockDetection)this.tmpBlock.GetComponent("PlaceBlockDetection");
		if(this.tmpBlock.transform.position.y <0 || spt.IsInideObject() ==true) { //Destroy the block
			Vector3 tmpPos =this.tmpBlock.transform.position;
			tmpPos.y =-1000;
			this.tmpBlock.transform.position =tmpPos;
			
			Destroy(this.tmpBlock, 0.1f);
			tmpBlock =null;
		} else {
			this.tmpBlock.transform.parent =this.Building.transform;
			this.tmpBlock.collider.isTrigger =false; //Because we have place the block onto the game scene we no longer want it to just be a trigger
			this.tmpBlock.collider.attachedRigidbody.useGravity =true; //Allow gravity to affect the block
			Color col =this.tmpBlock.renderer.material.color;
			col.a =1;
			this.tmpBlock.renderer.material.color =col;
			//Destroy(this.tmpBlock.GetComponent("PlaceBlockDetection"));
			
			spt.detectionColor =new Color(0.5f, 0.5f, 0);
			spt.col =col;
			
			this.tmpBlock =null;
		}
	}
	
}
