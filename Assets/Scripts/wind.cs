using UnityEngine;
using System.Collections;

public class wind : MonoBehaviour {
	public GUIText output;
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("updateWind", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
	
	}
	
	void updateWind() {
		int dir =Random.Range(0, 2);
		//float windSpeed =Random.Range(-0.6f, 0.6f);
		float windSpeed =Random.Range(-0.6f, 0.6f);
		
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Gold")) {
			if(obj.constantForce !=null) {
				Vector3 gold =obj.constantForce.force;
				if(dir >=1)
					gold.x +=windSpeed;
				else
					gold.z +=windSpeed;
					
				obj.constantForce.force =gold;
				output.text ="Wind Strength X: " +gold.x.ToString() +", Z: " +gold.z.ToString();
			}
		}
		
		//Add the wind to a cloth (flag)
		if(dir >=1) {
			GameObject o =GameObject.FindGameObjectWithTag("Flag");
			Vector3 tmp =o.transform.GetComponent<Cloth>().externalAcceleration;
			tmp.x +=windSpeed *2;
			o.transform.GetComponent<Cloth>().externalAcceleration =tmp;
		} else {
			GameObject o =GameObject.FindGameObjectWithTag("Flag");
			Vector3 tmp =o.transform.GetComponent<Cloth>().externalAcceleration;
			tmp.z +=windSpeed *2;
			o.transform.GetComponent<Cloth>().externalAcceleration =tmp;
		}
	}
}
