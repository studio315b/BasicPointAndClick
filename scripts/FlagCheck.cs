using Godot;

/*
 * Making this a Global Class and a Resource means it will show up and be editable inside Godot.
 */

[GlobalClass]
public partial class FlagCheck : Resource {

    [Export]
    public Flag Flag;

    [Export]
    public bool IsTrue;
}