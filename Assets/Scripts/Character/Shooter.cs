using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {


	public GameObject projectilePrefab;
	public GameObject specialProjectilePrefab;
	public GameObject purpleProjectileInfo, greenProjectileInfo;
	public float numberOfBullets;

	public float shotDelay;
	public AudioClip[] clips;

	private float timeOfLastShot;
	private Vector2 currentDirection;

	private StateController controller;

	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent<StateController> ();
		purpleProjectileInfo = transform.GetChild (1).gameObject;
		greenProjectileInfo =  transform.GetChild (0).gameObject ;
	}


	void Update() {

		currentDirection = Vector2.zero;


		if (controller.actions.Left_alt.IsPressed) {
			currentDirection.x--;
		}
		if (controller.actions.Right_alt.IsPressed) {
			currentDirection.x++;
		}
		if (controller.actions.Up_alt.IsPressed) {
			currentDirection.y++;
		}
		if (controller.actions.Down_alt.IsPressed) {
			currentDirection.y--;
		}

		currentDirection = currentDirection.normalized;

		SetState ();


		if (Time.timeSinceLevelLoad > timeOfLastShot + shotDelay) {
			if (controller.actions.Left_alt.IsPressed ||controller.actions.Right_alt.IsPressed ||
				controller.actions.Up_alt.IsPressed || controller.actions.Down_alt.IsPressed) {
				Shoot ();
				timeOfLastShot = Time.timeSinceLevelLoad;
			}
		}


		if (numberOfBullets <= 0) {
			purpleProjectileInfo.SetActive (false);
			greenProjectileInfo.SetActive (false);
		} else {
			PlayerManager.Mode mode = specialProjectilePrefab.GetComponent<ProjectileBehavior> ().mode;
			if (mode == PlayerManager.Mode.ABuff) {
				greenProjectileInfo.SetActive (true);
				purpleProjectileInfo.SetActive (false);
			} else if (mode == PlayerManager.Mode.BBuff) {
				purpleProjectileInfo.SetActive (true);
				greenProjectileInfo.SetActive (false);
			}
		}
	}


	void Shoot() {


		GameObject currentProjectile;
		if (numberOfBullets <= 0) {
			currentProjectile = Instantiate (projectilePrefab) as GameObject;
		} else {
			currentProjectile = Instantiate (specialProjectilePrefab) as GameObject;
			numberOfBullets--;
		}

		PlayClip ();


		ProjectileBehavior pb = currentProjectile.GetComponent<ProjectileBehavior> ();
		pb.team = controller.team;

		//currentProjectile.transform.parent = transform;
		pb.transform.position = transform.TransformPoint (Vector3.up * 3);

		if (controller.aimDirection == StateController.Direction.Up) {
			pb.currentDirection = Vector2.up;
			pb.transform.Rotate (new Vector3 (0, 0, 90));
		} else if (controller.aimDirection == StateController.Direction.Down) {
			pb.currentDirection = Vector2.down;
			pb.transform.Rotate (new Vector3 (0, 0, -90));
		} else if (controller.aimDirection == StateController.Direction.Left) {
			pb.currentDirection = Vector2.left;
			pb.transform.Rotate (new Vector3 (0, 0, 180));
		} else if (controller.aimDirection == StateController.Direction.Right) {
			pb.currentDirection = Vector2.right;
		}

	}


	void SetState() {

		if (currentDirection.x == 0 && currentDirection.y == 0) {
			controller.SetAimDirection (StateController.Direction.Still);
		} else if (currentDirection.x < 0 && currentDirection.y == 0) {
			controller.SetAimDirection (StateController.Direction.Left);
		} else if (currentDirection.x > 0 && currentDirection.y == 0) {
			controller.SetAimDirection (StateController.Direction.Right);
		} else if (currentDirection.y > 0 && currentDirection.x == 0) {
			controller.SetAimDirection (StateController.Direction.Up);
		} else if (currentDirection.y < 0 && currentDirection.x == 0) {
			controller.SetAimDirection (StateController.Direction.Down);
		}
	}

	public void PlayClip() {
		AudioSource.PlayClipAtPoint (clips [Random.Range (0, clips.Length)], Vector3.forward * (-100), 0.5f);
	}
}
