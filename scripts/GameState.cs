using Godot;
using System;
using System.Collections.Generic;

/*
 * Global state management node. Anything you want to share between scenes should be handled here.
 * This includes save/load functionality, scene transition functionality, broadcast signals, etc.
 */
public partial class GameState : Node
{
	private Dictionary<Flag, bool> Flags;
	private string CurrentScene;

	public void NewGame() {
		LoadGame(new Dictionary<Flag, bool>(), "res://scenes/room_door/room_door.tscn");
	}

	public void LoadGame(Dictionary<Flag, bool> flags, string currentScene) {
		Flags = flags;
		ChangeScene(currentScene);
	}

	public bool GetFlag(Flag flag) {
		if(Flags.ContainsKey(flag)) return Flags[flag];
		return false;
	}

	public void SetFlag(Flag flag, bool value) {
		if(!Flags.ContainsKey(flag)) Flags.Add(flag, value);
		else Flags[flag] = value;
	}

	public void ChangeScene(string newScene) {
		CurrentScene = newScene;
		/* This swaps out the whole tree, which may end up not being what you want later on (especially if you want
		 * music to carry over between rooms). There's a good page in the godot docs on alternatives:
		 * https://docs.godotengine.org/en/stable/tutorials/scripting/change_scenes_manually.html
		 */
		 GD.Print(newScene);
		GetTree().ChangeSceneToFile(newScene);
	}

	/* Rather than every element knowing how to show a message in it's specific circumstance, we use a signal and a 
	 * helper function to broadcast the "ShowMessage" command to whatever node knows how to do that
	 */
	[Signal]
	public delegate void OnShowMessageEventHandler(string message);
	public void ShowMessage(string message) {
		EmitSignal("OnShowMessage", message);
	}


}
