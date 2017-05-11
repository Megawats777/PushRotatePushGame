using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System;

// The save game manager class
public class SaveGameManager : MonoBehaviour
{
    // Save level high score
    public static void saveLevelHighScore(string levelName, int savedHighScore)
    {
        BinaryFormatter fileWriter = new BinaryFormatter();
        
        // Create an instance of the player data class
        LevelData playerData = new LevelData();

        // Create the save file
        FileStream fileStream = new FileStream(Application.persistentDataPath + "/" + levelName + "_LevelScore.SAVE", FileMode.Create);

        // Set the saved high score
        // Serialize the player data class
        playerData.setSavedHighScore(savedHighScore);
        fileWriter.Serialize(fileStream, playerData);
        
        // Stop writing to the save file
        fileStream.Close();
    }

    // Load level high score
    public static int loadLevelHighScore(string levelName)
    {
        BinaryFormatter fileReader = new BinaryFormatter();

        // Does the save file for the given level exist
        if (File.Exists(Application.persistentDataPath + "/" + levelName + "_LevelScore.SAVE"))
        {
            // Open the file
            FileStream fileStream = new FileStream(Application.persistentDataPath + "/" + levelName + "_LevelScore.SAVE", FileMode.Open);

            // Deserialize the file
            LevelData playerDataRef = (LevelData) fileReader.Deserialize(fileStream);

            // Return the level high score
            int fileLevelScore = playerDataRef.getSavedHighScore();

            // Stop reading the file
            fileStream.Close();

            return fileLevelScore;
        }
        else
        {
            print("Save not found");
            return 0;
        }
    }
}

// The level data
// Acts as a container for the level's data
[Serializable]
public class LevelData
{
    // The saved high score
    public int savedHighScore;

    // Constructor
    public LevelData()
    {
       
    }

    /*--Getters and Setters--*/
    
    public int getSavedHighScore()
    {
        return savedHighScore;
    }

    public void setSavedHighScore(int savedHighScore)
    {
        this.savedHighScore = savedHighScore;
    }
}
    