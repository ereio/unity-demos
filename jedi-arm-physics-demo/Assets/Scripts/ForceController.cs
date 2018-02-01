/** Author: Taylor Ereio
 * File: ForceController.cs
 * Date: 3/4/2015
 * Description: utilizes the GetMouseButtonDown to and sends a ray that will add force to an object
 * if it's apart of the layer "Targets". A random direction and force is added to the "force" coming
 * from the mouse ray.
 * */

using UnityEngine;
using System.Collections;

public class ForceController : MonoBehaviour {
	private int Layer = 1;
	private int RayDistDefault = 100;
	public int ForceMax = 10000;
	public int ForceMin = 2000;

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
			tempMouseHit();
	}

	void tempMouseHit(){
		// casts a ray from the camera though the script is attached to the hand
		RaycastHit hitInfo;
		Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		// Debug
		Debug.DrawRay(screenRay.origin, screenRay.direction * RayDistDefault, Color.cyan, 0.2f);

		// creates a random amount of force to add to directional vector
		int force = Random.Range(ForceMin, ForceMax);
		float randX = Random.Range (0.0f, 1.0f); // sends force forward from player, not to player
		float randY = Random.Range (-1.0f, 1.0f); // sends force in any direction on plane
		float randZ = Random.Range (-1.0f, 1.0f); // sends force in any direction on plane

		// creates a vector for direction
		Vector3 direction = new Vector3 (randX, randY, randZ);

		// sends a force ONLY if the objects hit are apart of the "Targets" Layer
		if (Physics.Raycast 
		    (screenRay, out hitInfo, RayDistDefault, (Layer << LayerMask.NameToLayer("Targets")))) {
			// multiplies the direction by the randomized force of the directional movement
			hitInfo.rigidbody.AddForce(direction * force);
		}
	}

	/*
	void OnMouseDown(){
		RaycastHit hitInfo;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Debug.DrawRay (ray.origin, ray.direction * RayDistDefault);

		if (Physics.Raycast 
		    (ray, out hitInfo, RayDistDefault, (Layer << LayerMask.NameToLayer("StaticArea")))) {
			Debug.Log (hitInfo + " Collided");
		}
	}
	*/
}
