using Godot;
using System;

public partial class Menu : Control
{
	GameState state;

	public override void _Ready()
	{
		state = GetNode<GameState>("/root/state");
	}

	public void OnNewGameButtonPressed() {
		state.NewGame();
	}
}
