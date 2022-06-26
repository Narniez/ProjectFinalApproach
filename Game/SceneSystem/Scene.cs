using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
public class Scene : GameObject
{
    public bool isActive = true;


    public virtual void LoadScene()
    {
       Start();
    }

    public virtual void UnLoadScene()
    {


        foreach (GameObject gameObject in GetChildren())
        {

            
            if (gameObject != SceneManager.instance)
            {
                Console.WriteLine("Clearing scene: destroying object -" + gameObject);
                gameObject.LateDestroy();
                MyGame myGame = ((MyGame)game);

                myGame._movers.Clear();
                myGame._lines.Clear();
            }              
        }
        isActive = false;
        visible = false;
    }

    protected virtual void Update()
    {
        if (!isActive) return;
    }

    protected virtual void Start()
    {
        visible = true;
        isActive = true;
        GetChildren().ForEach(x => x.visible = true);
    }

}

