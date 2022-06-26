using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GXPEngine;

public class CollectableSystem : Pivot
{

  
    public int currentStarsLevel;

    int[] stars;

    public CollectableSystem()
    {

        MyGame myGame = ((MyGame)game);
        int levels = myGame.GetLevelCount;

        stars = new int[levels];

        LoadStars();

    }



    public void AddStarsLevel() {
        currentStarsLevel++;
     //   HUD hud = ((MyGame)game).GetHUD;
    //    hud.UpdateCol(currentStarsLevel);
    }

    public void RestartStarsLevel() {
        currentStarsLevel = 0;
   //     HUD hud = ((MyGame)game).GetHUD;
   //     hud.UpdateCol(currentStarsLevel);
    }

    public void EndStarsLevel() {
        
        //Levels start at 2 my arrays start at 0 so scene - 2
        CheckStars(((MyGame)game).GetCurrentScene - 2, currentStarsLevel);
    }


    //Change if levels start with 1 otherwise keep the same
    public void CheckStars(int index, int amountStars)
    {
        if (amountStars > stars[index]) stars[index] = amountStars;

        SaveStars();
    }


    public int GetStars(int level) { 
        return stars[level];
    }

    public void PrintStars() {
        for (int i = 0; i < stars.Length; i++)
        {
            Console.WriteLine(stars[i]);
        }

    }

    public void SaveStars()
    {


        if (!File.Exists("stars.txt"))
        {
            Console.WriteLine("No save file found!");
            return;
        }
        try
        {
            // StreamReader: For reading a text file - requires System.IO namespace:
            // Note: the "using" block ensures that resources are released (reader.Dispose is called) when an exception occurs
            using (StreamWriter writer = new StreamWriter("stars.txt"))
            {
                for (int i = 0; i < stars.Length; i++)
                {
                    writer.WriteLine("Stars Level {0} = {1}", i, stars[i]);
                }

                writer.Close();

                Console.WriteLine("Saved from stars.txt successful.");
            }
        }
        catch (Exception error)
        {
            Console.WriteLine("Error while reading save file: {0}", error.Message);
        }

    }


    public void LoadStars()
    {

            if (!File.Exists("stars.txt"))
            {
                Console.WriteLine("No save file found!");
                return;
            }
            try
            {
                // StreamReader: For reading a text file - requires System.IO namespace:
                // Note: the "using" block ensures that resources are released (reader.Dispose is called) when an exception occurs
                using (StreamReader reader = new StreamReader("stars.txt"))
                {

                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        // Here's a demo of different string parsing methods:

                        // Find the position of the first '=' symbol (-1 if doesn't exist)
                        int splitPos = line.IndexOf('=');
                        if (splitPos >= 0)
                        {
            
                        // Everything before the '=' symbol:
                            string key = line.Substring(splitPos - 2, 2);
         

                            // Everything after the '=' symbol:
                            string number = line.Substring(splitPos + 2);
              

                            int numberOfStars = int.Parse(number);
                            int levelKey = int.Parse(key);
                            stars[levelKey] = numberOfStars;
                        

                        }
                        line = reader.ReadLine();
                    }
                    reader.Close();

                    Console.WriteLine("Load from stars.txt successful ");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error while reading save file: {0}", error.Message);
            }

        }






}


