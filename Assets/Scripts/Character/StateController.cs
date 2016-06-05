using UnityEngine;
using System.Collections;
using InControl;

public class StateController : MonoBehaviour {

	public enum Direction {Still, Left, Right, Up, Down, UpLeft, UpRight, DownLeft, DownRight};
	public enum WeaponType {Standard, Weapon1, Weapon2};

	private Animator animator;

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

			if (!itIsMoving) {
				animator.SetTrigger ("Idle");
			} else {
				if (walkDirection == Direction.Right) {
					animator.SetTrigger ("Side");
					transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x),
						transform.localScale.y, transform.localScale.z);
				} else if (walkDirection == Direction.Left) {
					animator.SetTrigger ("Side");
					transform.localScale = new Vector3 (-Mathf.Abs (transform.localScale.x),
						transform.localScale.y, transform.localScale.z);
				} else if (walkDirection == Direction.Up) {
					animator.SetTrigger ("Back");
				} else if (walkDirection == Direction.Down) {
					animator.SetTrigger ("Front");
				}
			}
		}
	}


	public void SetWalkDirection(Direction d) {
		if (d != walkDirection)
			isMoving = false;

		walkDirection = d;
	}


	public void SetAimDirection (Direction d) {
		aimDirection = d;
	}
}
