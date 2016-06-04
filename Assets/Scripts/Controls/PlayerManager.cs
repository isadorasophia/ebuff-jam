using System;
using UnityEngine;
using System.Collections.Generic;
using InControl;

// This example iterates on the basic multiplayer example by using action sets with
// bindings to support both joystick and keyboard players. It would be a good idea
// to understand the basic multiplayer example first before looking a this one.
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerManager : MonoBehaviour
{
	public enum State {WaitingForMatchStart, InMatch, MatchEnded};
	public static int winner = 0;
	public int minimumPlayersForMatch;

	public State state = State.WaitingForMatchStart;
	public Text infoText;

	public GameObject playerPrefab;
	public List<RuntimeAnimatorController> animators;

	public int maxPlayers = 2;

	public List<Transform> playerPositions;

	private List<StateController> players;

	PlayerActions keyboardListener;
	PlayerActions keyboardListener_alt;
	PlayerActions joystickListener;


	void OnEnable()
	{
		players = new List<StateController> ();
		InputManager.OnDeviceDetached += OnDeviceDetached;
		keyboardListener = PlayerActions.CreateWithKeyboardBindings();
		keyboardListener_alt = PlayerActions.CreateWithKeyboardBindings_alt();
		joystickListener = PlayerActions.CreateWithJoystickBindings();
	}


	void OnDisable()
	{
		InputManager.OnDeviceDetached -= OnDeviceDetached;
		joystickListener.Destroy();
		keyboardListener.Destroy();
	}


	void Update()
	{
		if (state == State.WaitingForMatchStart) {

			if (players.Count < minimumPlayersForMatch) {
				if (infoText != null) {
					infoText.enabled = true;
					infoText.text = "Press any button to enter game";
					infoText.transform.position = transform.position;
					//infoText.transform.localScale = Vector3.one * 3;
				}
			} else {
				if (infoText != null) {
					infoText.enabled = true;
					infoText.text = "Press Start to Begin";
					infoText.transform.position = transform.position;
				}
			}

			if (JoinButtonWasPressedOnListener (joystickListener)) {
				var inputDevice = InputManager.ActiveDevice;

				if (ThereIsNoPlayerUsingJoystick (inputDevice)) {
					CreatePlayer (inputDevice);
				}
			}

			if (JoinButtonWasPressedOnListener (keyboardListener)) {
				if (ThereIsNoPlayerUsingKeyboard ()) {
					CreatePlayer (null);
				}

				/*
				if (ThereIsNoPlayerUsingKeyboard_alt ()) {
					CreatePlayer (null,1);
				}
				 */
			}
	
		} else if (state == State.InMatch) {
			if (players.Count <= 1) {
				state = State.MatchEnded;
			}
		} else {
			infoText.enabled = true;
			infoText.text = "You are the last one standing";
			infoText.transform.position = transform.position;

			if (StartMatchButtonWasPressed ()) {
				Scene scene = SceneManager.GetActiveScene();
				SceneManager.LoadScene(scene.name);
			}
		}
	}


	bool JoinButtonWasPressedOnListener( PlayerActions actions )
	{
		return actions.Attack.WasPressed || actions.Start.WasPressed;
	}


	StateController FindPlayerUsingJoystick( InputDevice inputDevice )
	{
		var playerCount = players.Count;
		for (int i = 0; i < playerCount; i++)
		{
			var player = players[i];
			if (player.actions.Device == inputDevice)
			{
				return player;
			}
		}

		return null;
	}


	bool ThereIsNoPlayerUsingJoystick( InputDevice inputDevice )
	{
		return FindPlayerUsingJoystick( inputDevice ) == null;
	}


	StateController FindPlayerUsingKeyboard()
	{
		var playerCount = players.Count;
		for (int i = 0; i < playerCount; i++)
		{
			var player = players[i];
			if (player.actions == keyboardListener)
			{
				return player;
			}
		}

		return null;
	}

	StateController FindPlayerUsingKeyboard_alt()
	{
		var playerCount = players.Count;
		for (int i = 0; i < playerCount; i++)
		{
			var player = players[i];
			if (player.actions == keyboardListener_alt)
			{
				return player;
			}
		}

		return null;
	}


	bool ThereIsNoPlayerUsingKeyboard()
	{
		return FindPlayerUsingKeyboard() == null;
	}


	void OnDeviceDetached( InputDevice inputDevice )
	{
		var player = FindPlayerUsingJoystick( inputDevice );
		if (player != null)
		{
			RemovePlayer( player );
		}
	}


	StateController CreatePlayer( InputDevice inputDevice, int keyBoardCode=0)
	{
		if (players.Count < maxPlayers)
		{
			
			var playerPosition = playerPositions[players.Count];

			var gameObject = (GameObject) Instantiate( playerPrefab, playerPosition.position, Quaternion.identity );
            gameObject.name = "Player" + (players.Count+1);

          	var player = gameObject.GetComponent<StateController>();
			gameObject.GetComponent<Animator> ().runtimeAnimatorController = animators[players.Count];

			if (inputDevice == null)
			{
				// We could create a new instance, but might as well reuse the one we have
				// and it lets us easily find the keyboard player.
				if (keyBoardCode == 0)
					player.actions = keyboardListener;
				else if (keyBoardCode == 1)
					player.actions = keyboardListener_alt;
			}
			else
			{
				// Create a new instance and specifically set it to listen to the
				// given input device (joystick).
				var actions = PlayerActions.CreateWithJoystickBindings();
				actions.Device = inputDevice;

				player.actions = actions;
			}
				
			players.Add( player );

			return player;
		}

		return null;
	}


	public void RemovePlayer( StateController player )
	{
		playerPositions.Insert(0, player.transform);
		players.Remove( player );
		player.actions = null;
		Destroy( player.gameObject );
	}


	void OnGUI()
	{
		/*const float h = 22.0f;
		var y = 10.0f;

		GUI.Label( new Rect( 10, y, 300, y + h ), "Active players: " + players.Count + "/" + maxPlayers );
		y += h;

		if (players.Count < maxPlayers)
		{
			GUI.Label( new Rect( 10, y, 300, y + h ), "Press a button or a/s/d/f key to join!" );
			y += h;
		}*/
	}




	bool StartMatchButtonWasPressed() {
		if ((joystickListener.Start.WasPressed && !ThereIsNoPlayerUsingJoystick(InputManager.ActiveDevice))
			|| keyboardListener.Start.WasPressed && !ThereIsNoPlayerUsingKeyboard()) {
			return true;
		} else {
			return false;
		}
	}
}