using Godot;

public partial class Button : Godot.Button
{
    public override void _Ready()
    {
        this.Pressed += OnButtonPressed;
    }

    public void OnButtonPressed()
    {
        OurMainLoop.Get().GetLevelManager().Load("res://level.tscn");
        OurMainLoop.Get().GetSaveManager().LoadGame("res://savegame.json");
    }
}
