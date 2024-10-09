using Godot;

public class LevelManager
{
    public Node simultaneousScene;

    public LevelManager()
    {
        simultaneousScene = ResourceLoader.Load<PackedScene>("res://menu.tscn").Instantiate();
    }

    public void Load(string level)
    {
        //free the previous scene
        Node mainScene = OurMainLoop.Get().Root.GetChild(0);
        mainScene.QueueFree();

        //load the scene 'level'
        simultaneousScene = ResourceLoader.Load<PackedScene>(level).Instantiate();
        OurMainLoop.Get().Root.AddChild(simultaneousScene);
    }
}
