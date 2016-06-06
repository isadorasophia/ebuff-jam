using UnityEngine;
using System.Collections;
using InControl;

public class StateController : MonoBehaviour {

	public enum Direction {Still, Left, Right, Up, Down};
	public enum WeaponType {Standard, Weapon1, Weapon2};

	private Animator animator;

	public PlayerManager.Team team;
	public Direction walkDirection { get; private set;}
	public Direction aimDirection { get; private set;}
	public WeaponType weaponType;
	public bool isMoving { get; private set;}

	public PlayerActions actions;

	void Awake() {
		walkDirection = Direction.Right;
		aimDirection = Direction.Right;
		animator = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        animatorUpdate();

    }


    void animatorUpdate()
    {
    }


	public void SetMovement (bool itIsMoving) {
		
		if (itIsMoving != isMoving) {
			isMoving = itIsMoving;
			UpdateState ();
		}
	}


	public void SetWalkDirection(Direction d) {
		if (d != walkDirection)
			isMoving = false;

		walkDirection = d;
	}


	public void SetAimDirection (Direction d) {
		
		if (d != aimDirection) {
			aimDirection = d;
			UpdateState ();
		}
	}


	void UpdateState() {
		if (!isMoving) {
			if (aimDirection == Direction.Still) {
				animator.SetTrigger ("Idle");
			} if (aimDirection == Direction.Right) {
				animator.SetTrigger ("Idle");
				transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x),
					transform.localScale.y, transform.localScale.z);
			} else if (aimDirection == Direction.Left) {
				animator.SetTrigger ("Idle");
				transform.localScale = new Vector3 (-Mathf.Abs (transform.localScale.x),
					transform.localScale.y, transform.localScale.z);
			} else if (aimDirection == Direction.Up) {
				animator.SetTrigger ("UpIdle");
			} else if (aimDirection == Direction.Down) {
				animator.SetTrigger ("DownIdle");
			}

		} else {

			if (aimDirection == Direction.Still) {

				if (walkDirection == Direction.Left) {
					animator.SetTrigger ("Side");
					transform.localScale = new Vector3 (-Mathf.Abs (transform.localScale.x),
						transform.localScale.y, transform.localScale.z);
				} else if (walkDirection == Direction.Right) {
					animator.SetTrigger ("Side");
					transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x),
						transform.localScale.y, transform.localScale.z);

				} else if (walkDirection == Direction.Up) {
					animator.SetTrigger ("Back");
				}else if (walkDirection == Direction.Down) {
					animator.SetTrigger ("Front");
				}

			} else if (aimDirection == Direction.Right) {

				if (walkDirection == Direction.Left) {
					animator.SetTrigger ("SideInverted");
				} else {
					animator.SetTrigger ("Side");
				}
				transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x),
					transform.localScale.y, transform.localScale.z);

			} else if (aimDirection == Direction.Left) {

				if (walkDirection == Direction.Right) {
					animator.SetTrigger ("SideInverted");
				} else {
					animator.SetTrigger ("Side");
				}
				transform.localScale = new Vector3 (-Mathf.Abs (transform.localScale.x),
					transform.localScale.y, transform.localScale.z);

			} else if (aimDirection == Direction.Up) {

				if (walkDirection == Direction.Down) {
					animator.SetTrigger ("BackInverted");
				} else {
					animator.SetTrigger ("Back");
				}
			} else if (aimDirection == Direction.Down) {

				if (walkDirection == Direction.Up) {
					animator.SetTrigger ("FrontInverted");
				} else {
					animator.SetTrigger ("Front");
				}
			}
		}
	}
}
