using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class Saves
{


    public static List<GameState> saveStates = new List<GameState>();

    //it's static so we can call it from anywhere
    public static void Save()
    {
        Saves.saveStates.Add(GameState.Current);
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References/savedGames.gd"); //you can call it anything you want
        bf.Serialize(file, Saves.saveStates);
        Debug.Log("Method: Saves.Save - Saving File @" + "C: Users marsh Documents BackGround Project Assets MyTutorialGame References savedGames.gd");
        file.Close();
        return;
    }

    //it's static so we can call it from anywhere
    public static void Load()
    {
        if (File.Exists(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(@"C:\Users\marsh\Documents\BackGround Project\Assets\MyTutorialGame\References/savedGames.gd", FileMode.Open);
            saveStates = (List<GameState>)bf.Deserialize(file);
            Debug.Log("Method: Saves.Load - Loading File From " + "C: Users marsh Documents BackGround Project Assets MyTutorialGame References savedGames.gd");
            file.Close();
            return;
        }
    }
}