using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
public class SceneManager : GameObject
{
    private List<Scene> scenes = new List<Scene>();
    private Scene activeScene;

    private static SceneManager _instance = null;

    public static SceneManager instance
    {

        get
        {
            if( _instance == null)
            {
                _instance = new SceneManager();
                Console.WriteLine("creating new Scene manager");
            }
            return _instance;
        }
    }

    public SceneManager()
    {
        if (!game.GetChildren().Contains(this))
        {
            game.AddChild(this);
        }
    }

    // Add and disable the scene
    public void AddScene(Scene sceneToAdd)
    {
        scenes.Add(sceneToAdd);
        AddChild(sceneToAdd);
        sceneToAdd.visible = false;
        sceneToAdd.isActive = false;

    }

    //return currently active scene

    public Scene GetActiveScene()
    {
        return activeScene;
    }

    public void TryLoadNextScene()
    {
        int index = scenes.IndexOf(activeScene);
        if (index + 1 >= scenes.Count) return;
        index++;
      //  Console.WriteLine((index));
        LoadScene((index));
    }

    //Load scene based on provided index

    public void LoadScene(int sceneIndex)
    {
        Console.WriteLine("Loading scene: " + sceneIndex);
        if (activeScene != null)
        {
            Console.WriteLine("Unloading scene: " + activeScene);
            activeScene.UnLoadScene();
            Console.WriteLine(activeScene);
        }
        activeScene = scenes[sceneIndex];

        foreach(Scene scene in scenes)
        {
            if(scene != activeScene)
            {
                activeScene.visible = false;
                activeScene.isActive = false;
            }
        }

        activeScene.LoadScene();
        activeScene.visible = true;
        activeScene.isActive = true;



    }

    //Load scene based no a provided scene 

    public void LoadScene(Scene scene)
    {
        if (scenes.Contains(scene))
        {
            activeScene.UnLoadScene();
            activeScene = scene;
            activeScene.LoadScene();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(Key.L))
        {
            Console.WriteLine("Total Scenes: " + scenes.Count);
            Console.WriteLine("Current Scene: " + scenes.IndexOf(activeScene));
        }
    }

    //Destroy all scenes from the list

    private void WipeScenes()
    {
        scenes.ForEach(x => x.LateDestroy());
        scenes = new List<Scene>();
        MyGame myGame = (MyGame)game;
        myGame.SetUpScenes();
        
    }

    public void ReloadGame()
    {
        WipeScenes();
    }
}

