﻿using System;
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
		actions.Down.AddDefaultBinding( Key.S );
		actions.Left.AddDefaultBinding( Key.A );
		actions.Right.AddDefaultBinding( Key.D );

		actions.Up_alt.AddDefaultBinding( Key.F);
		actions.Down_alt.AddDefaultBinding( Key.V);
		actions.Left_alt.AddDefaultBinding(Key.C);
		actions.Right_alt.AddDefaultBinding( Key.B);


		actions.Start.AddDefaultBinding (Key.Return);

		return actions;
	}


	public static PlayerActions CreateWithKeyboardBindings_alt()
	{
		var actions = new PlayerActions();


		actions.Attack.AddDefaultBinding( Key.Space);
		actions.Attack.AddDefaultBinding (Mouse.LeftButton);

		actions.Up.AddDefaultBinding( Key.I );
		actions.Down.AddDefaultBinding( Key.K );
		actions.Left.AddDefaultBinding( Key.J );
		actions.Right.AddDefaultBinding( Key.L );

		actions.Up_alt.AddDefaultBinding( Key.UpArrow);
		actions.Down_alt.AddDefaultBinding( Key.DownArrow);
		actions.Left_alt.AddDefaultBinding(Key.LeftArrow);
		actions.Right_alt.AddDefaultBinding( Key.RightArrow);


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

		actions.Up_alt.AddDefaultBinding( InputControlType.RightStickUp);
		actions.Down_alt.AddDefaultBinding( InputControlType.RightStickDown);
		actions.Left_alt.AddDefaultBinding(InputControlType.RightStickLeft);
		actions.Right_alt.AddDefaultBinding( InputControlType.RightStickRight );

		actions.Up_alt.AddDefaultBinding( InputControlType.Action4);
		actions.Down_alt.AddDefaultBinding( InputControlType.Action1);
		actions.Left_alt.AddDefaultBinding(InputControlType.Action3);
		actions.Right_alt.AddDefaultBinding( InputControlType.Action2 );



		actions.Start.AddDefaultBinding (InputControlType.Start);

		return actions;
	}
}

