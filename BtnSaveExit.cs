using Godot;
using System;

public partial class BtnSaveExit : Godot.Button
{
    public override void _Ready()
    {
        this.Pressed += OnButtonPressed;
    }

    public void OnButtonPressed()
    {
        OurMainLoop.Get().GetSaveManager().Save("res://savegame.json");
        OurMainLoop.Get().GetLevelManager().Load("res://menu.tscn");
    }
}
