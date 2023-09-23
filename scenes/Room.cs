using System;
using Godot;

public partial class Room: Node {

    GameState.OnShowMessageEventHandler onShowMessage;
    GameState state;
    Label messageLabel;
	public override void _Ready()
	{
        GD.Print("Ready!");
        messageLabel = GetNode<Label>("./Message");
        onShowMessage = (message) => {
            messageLabel.Text = message;
        };
		state = GetNode<GameState>("/root/state");
        // This is marked as an error in VSCode but works because of "Godot Magic"
        state.OnShowMessage += onShowMessage;
	}

    protected override void Dispose(bool disposing)
    {
        // Weird C# cleanup bug
        state.OnShowMessage -= onShowMessage;
        base.Dispose(disposing);
    }
}