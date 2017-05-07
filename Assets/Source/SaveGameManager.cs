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
        PlayerData playerData = new PlayerData(savedHighScore);

        // Create the save file
        FileStream fileStream = new FileStream(Application.persistentDataPath + "/" + levelName + "_LevelScore.SAVE", FileMode.Create);

        // Serialize the player data class
        // Set the saved high score
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
            PlayerData playerDataRef = (PlayerData) fileReader.Deserialize(fileStream);

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

// The player data class
// Acts as a container for the player's data
[Serializable]
public class PlayerData
{
    // The saved high score
    public int savedHighScore;

    // Constructor
    public PlayerData(int savedLevelScore)
    {
        setSavedHighScore(savedLevelScore);
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
    