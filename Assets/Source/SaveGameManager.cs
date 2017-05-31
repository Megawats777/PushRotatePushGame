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

    // Save if audio is enabled
    public static void saveIfAudioIsEnabled(bool status)
    {
        BinaryFormatter fileWriter = new BinaryFormatter();

        // Create an instance of the OptionsData class
        OptionsData optionsDataRef = new OptionsData();

        // Create the save file
        FileStream fileStream = new FileStream(Application.persistentDataPath + "/PlayerOptions.SAVE", FileMode.Create);

        // Set the saved option
        // Serialize the OptionsData class
        optionsDataRef.isAudioEnabled = status;
        fileWriter.Serialize(fileStream, optionsDataRef);

        // Stop writing to the file
        fileStream.Close();

    }

    // Get if audio is enabled
    public static bool getIsAudioEnabled()
    {
        BinaryFormatter fileReader = new BinaryFormatter();
        bool audioEnabledStatus = true;

        // If the save file exists
        if (File.Exists(Application.persistentDataPath + "/PlayerOptions.SAVE"))
        {
            // Open the file
            FileStream fileStream = new FileStream(Application.persistentDataPath + "/PlayerOptions.SAVE", FileMode.Open);

            // Deserialize the file
            OptionsData optionsDataRef = (OptionsData)fileReader.Deserialize(fileStream);

            // Return the saved option
            audioEnabledStatus = optionsDataRef.isAudioEnabled;

            // Stop reading the file
            fileStream.Close();

            return audioEnabledStatus;
        }
        // If the save file does not exist
        else
        {
            print("Options Save File not found");
            audioEnabledStatus = true;
        }

        return audioEnabledStatus;
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

// Player options data
// Acts as a container for any options the player may have set
[Serializable]
public class OptionsData
{
    // Is audio enabled
    public bool isAudioEnabled;

    // Constructor
    public OptionsData()
    {

    }
}
    