using System;
using InControl;



public class PlayerActions : PlayerActionSet	{
	
	public PlayerAction Attack;

	public PlayerAction Left;
	public PlayerAction Right;
	public PlayerAction Up;
	public PlayerAction Down;

	public PlayerAction Left_alt;
	public PlayerAction Right_alt;
	public PlayerAction Up_alt;
	public PlayerAction Down_alt;


	public PlayerOneAxisAction Walk;
	public PlayerTwoAxisAction Aim;


	public PlayerAction Start;


	public PlayerActions()
	{

		Attack = CreatePlayerAction( "Attack" );

		Left = CreatePlayerAction( "Left" );
		Right = CreatePlayerAction( "Right" );
		Up = CreatePlayerAction( "Up" );
		Down = CreatePlayerAction( "Down" );

		Left_alt = CreatePlayerAction( "Left_alt" );
		Right_alt = CreatePlayerAction( "Right_alt" );
		Up_alt = CreatePlayerAction( "Up_alt" );
		Down_alt = CreatePlayerAction( "Down_alt" );


		Walk = CreateOneAxisPlayerAction(Left, Right);
		Aim = CreateTwoAxisPlayerAction (Left_alt, Right_alt, Down_alt, Up_alt);

		Start = CreatePlayerAction( "Start" );
	}


	public static PlayerActions CreateWithKeyboardBindings()
	{
		var actions = new PlayerActions();


		actions.Attack.AddDefaultBinding( Key.Space);
		actions.Attack.AddDefaultBinding (Mouse.LeftButton);

		actions.Up.AddDefaultBinding( Key.W );
		actions.Up.AddDefaultBinding( Key.UpArrow );
		actions.Down.AddDefaultBinding( Key.S );
		actions.Down.AddDefaultBinding( Key.DownArrow);
		actions.Left.AddDefaultBinding( Key.A );
		actions.Right.AddDefaultBinding( Key.D );

		actions.Up_alt.AddDefaultBinding( Mouse.PositiveY );
		actions.Down_alt.AddDefaultBinding( Mouse.NegativeY);
		actions.Left_alt.AddDefaultBinding(Mouse.NegativeX);
		actions.Right_alt.AddDefaultBinding( Mouse.PositiveX);


		actions.Start.AddDefaultBinding (Key.Return);

		return actions;
	}


	public static PlayerActions CreateWithJoystickBindings()
	{
		var actions = new PlayerActions();


		actions.Attack.AddDefaultBinding( InputControlType.Action3);

		actions.Up.AddDefaultBinding( InputControlType.LeftStickUp );
		actions.Down.AddDefaultBinding( InputControlType.LeftStickDown );
		actions.Left.AddDefaultBinding( InputControlType.LeftStickLeft );
		actions.Right.AddDefaultBinding( InputControlType.LeftStickRight );

		actions.Up.AddDefaultBinding( InputControlType.DPadUp );
		actions.Down.AddDefaultBinding( InputControlType.DPadDown );
		actions.Left.AddDefaultBinding( InputControlType.DPadLeft );
		actions.Right.AddDefaultBinding( InputControlType.DPadRight );

		actions.Up_alt.AddDefaultBinding( InputControlType.LeftStickUp);
		actions.Down_alt.AddDefaultBinding( InputControlType.LeftStickDown);
		actions.Left_alt.AddDefaultBinding(InputControlType.LeftStickLeft);
		actions.Right_alt.AddDefaultBinding( InputControlType.LeftStickRight );


		actions.Start.AddDefaultBinding (InputControlType.Start);

		return actions;
	}
}

