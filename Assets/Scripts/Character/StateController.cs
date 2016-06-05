using UnityEngine;
using System.Collections;
using InControl;

public class StateController : MonoBehaviour {

	public enum Direction {Left, Right, Up, Down, UpLeft, UpRight, DownLeft, DownRight};
	public enum WeaponType {Standard, Weapon1, Weapon2};

	private Animator animator;

	public Direction walkDirection;
	public Direction aimDirection;
	public WeaponType weaponType;

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
}
