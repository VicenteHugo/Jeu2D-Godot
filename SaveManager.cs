using Godot;

public class SaveManager
{
    //Save Level Method
    public void Save(string file)
    {
        using var saveFile = FileAccess.Open(file, FileAccess.ModeFlags.Write);

        var saveNodes = OurMainLoop.Get().GetNodesInGroup("Persist");
        GD.Print($"Saving {saveNodes.Count} nodes to {file}");
        foreach (Node saveNode in saveNodes)
        {
            // Call the node's save function.
            var nodeData = saveNode.Call("Save");
            // Json provides a static method to serialized JSON string.
            var jsonString = Json.Stringify(nodeData);

            // Store the save dictionary as a new line in the save file.
            saveFile.StoreLine(jsonString);
        }
    }

    //Load level Method
    public void LoadGame(string file)
    {
        //check if file exist
        if (!FileAccess.FileExists(file))
        {
            return;
        }

        var saveNodes = OurMainLoop.Get().GetNodesInGroup("Persist");
        foreach (Node saveNode in saveNodes)
        {
            saveNode.QueueFree();
        }

        using var saveFile = FileAccess.Open(file, FileAccess.ModeFlags.Read);

        while (saveFile.GetPosition() < saveFile.GetLength())
        {
            var jsonString = saveFile.GetLine();

            // Creates the helper class to interact with JSON
            var json = new Json();
            var parseResult = json.Parse(jsonString);
            if (parseResult != Error.Ok)
            {
                GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
                continue;
            }

            // Get the data from the JSON object
            var nodeData = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);

            // Firstly, we need to create the object and add it to the tree and set its position.
            var newObjectScene = GD.Load<PackedScene>(nodeData["filename"].ToString());
            var newObject = newObjectScene.Instantiate<Node>();
            OurMainLoop.Get().GetRoot().GetNode(nodeData["parent"].ToString()).AddChild(newObject);
            newObject.Set(Node2D.PropertyName.Position, new Vector2((float)nodeData["PosX"], (float)nodeData["PosY"]));

            // Now we set the remaining variables.
            foreach (var (key, value) in nodeData)
            {
                if (key == "filename" || key == "parent" || key == "PosX" || key == "PosY")
                {
                    continue;
                }
                newObject.Set(key, value);
            }
        }
    }
}
