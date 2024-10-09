using Godot;
using System;

[GlobalClass]
public partial class OurMainLoop : SceneTree {
    static OurMainLoop MAINLOOP_INSTANCE;
    LevelManager levelManager;
    SaveManager saveManager;


    // Singleton
    public static OurMainLoop Get() {
        if(MAINLOOP_INSTANCE == null) {
            MAINLOOP_INSTANCE = new OurMainLoop();
        }
        return MAINLOOP_INSTANCE;
    }

    // Constructor
    public OurMainLoop() {
        levelManager = new LevelManager();
        saveManager = new SaveManager();

        MAINLOOP_INSTANCE = this;
    }

    // GetLevelManager
    public LevelManager GetLevelManager() {
        return levelManager;
    }

    // GetSaveManager
    public SaveManager GetSaveManager() {
        return saveManager;
    }
    
}