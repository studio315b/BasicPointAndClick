using Godot;

public partial class BaseInteractive : Control {
    [Export]
    FlagCheck IsShown;
    GameState state;

	public override void _Ready() 
    {
		state = GetNode<GameState>("/root/state");
        // Hides the object if the shown flag check fails, no flag means always show
        if(IsShown != null) {
            if(state.GetFlag(IsShown.Flag) != IsShown.IsTrue) {
                Hide();
            }
        }
    }

    // This is the laziest possible way to do this. You'll probably want to use Godot Dialogue Manager for this instead.
    // https://github.com/nathanhoad/godot_dialogue_manager

    public void OnNoteInteract() {
        state.ShowMessage("A Note! The handwriting is so bad I can't read it.");
        state.SetFlag(Flag.have_note, true);
        Hide();
    }

    public void OnSafeInteract() {
        if(state.GetFlag(Flag.have_key)) {
            state.ShowMessage("The safe is empty.");
        } else if(state.GetFlag(Flag.button_pressed)) {
            state.ShowMessage("The safe is unlocked! Inside I found a key!");
            state.SetFlag(Flag.have_key, true);
        } else {
            state.ShowMessage("The safe is locked, there must be some way to open it");
        }
    }

    public void OnDoorInteract() {
        if(state.GetFlag(Flag.have_key)) {
            state.ShowMessage("The key fits the door! I'm free!");
        } else {
            state.ShowMessage("Locked...");
        }
    }

    public void OnButtonInteract() {
        if(!state.GetFlag(Flag.button_pressed)) {
            state.ShowMessage("I heard something behind me");
            state.SetFlag(Flag.button_pressed, true);
        } else{
            state.ShowMessage("I've already pressed the button");
        }
    }

    public void OnTurnFromDoor() {
        state.ChangeScene("res://scenes/room_safe/room_safe.tscn");
    }

    public void OnTurnFromSafe() {
        state.ChangeScene("res://scenes/room_door/room_door.tscn");
    }

}