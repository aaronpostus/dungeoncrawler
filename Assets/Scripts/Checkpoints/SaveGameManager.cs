using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using DunGen;
using System;
using Assets.Scripts.Checkpoints;

public class SaveGameManager : MonoBehaviour
{

    //load/save game code was created from https://www.youtube.com/watch?v=aUi9aijvpgs&t=561s
    public EnemyPrefabRegistry enemyPrefabRegistry;
    [Header("File Storage Config")]

    [SerializeField] private string fileName;

    public GameData gameData;

    private List<ISaveData> saveDataObjects;

    private FileDataHandler dataHandler;

    public static SaveGameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Save Game Manager in the scene. Newest one was destroyed.");
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        instance = this;

        //Debug.Log(Application.dataPath);
        this.dataHandler = new FileDataHandler(Application.dataPath + "/Save", fileName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public int AdvanceFloor() {
        return ++gameData.currentLevel;
    }
    public void SolveChest(int chestNumber) {
        gameData.chestsSolved[chestNumber] = (true, false);
    }
    public void TransitionAwayFromMainScene(string scene) {
        SaveGame();
        SceneManager.LoadScene(scene);
    }
    public void ReturnToMainScene() {
        Debug.Log(gameData);
        Debug.Log(gameData.currentLevel);
        SaveGame();
        SceneManager.LoadScene(gameData.currentLevel + "");
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded");
        LoadGame();
    }

    private void LoadAfterDunGen(DunGen.DungeonGenerator generator, GenerationStatus status)
    {
        if (status == GenerationStatus.Complete) {
            LoadGame();
        }
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        gameData.currentLevel = 1;
        SaveGame();
        ReturnToMainScene();
    }

    public void LoadGame()
    {
        GameData temp = this.gameData;
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("null game data!");
            return;
        }
        this.saveDataObjects = FindAllISaveDataObjects();

        DeserializeCheckpoints();
        DeserializeChests();
        PersistentPropIDAssigner.ReassignChestIDs();

        foreach (ISaveData saveDataObject in saveDataObjects)
        {
            if (saveDataObject is Chest) {
                Debug.Log("About to load a chest");
            }
            saveDataObject.LoadData(gameData);
        }

        var dunGen = FindObjectOfType<RuntimeDungeon>();
        // if we are in a scene with dungen we need to wait for dungen to create all of our objects
        if (dunGen)
        {
            dunGen.Generator.OnGenerationStatusChanged += LoadAfterDunGen;
        }
    }

    public void SaveGame()
    {
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        saveDataObjects = FindAllISaveDataObjects();

        foreach (ISaveData saveDataObject in saveDataObjects)
        {
            saveDataObject.SaveData(gameData);
        }

        SerializeCheckpoints();
        SerializeChests();

        dataHandler.Save(gameData);
    }

    public void DeleteSave()
    {
        dataHandler.Delete();
    }

    private List<ISaveData> FindAllISaveDataObjects()
    {
        //needs to implement MonoBehaviour and ISaveData to be found like this
        IEnumerable<ISaveData> saveDataObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISaveData>();

        return new List<ISaveData>(saveDataObjects);

    }

    private void SerializeChests()
    {
        gameData.chestKeys.Clear();
        gameData.chestSolved.Clear();
        gameData.chestItemDropped.Clear();

        foreach (var pair in gameData.chestsSolved)
        {
            gameData.chestKeys.Add(pair.Key);
            gameData.chestSolved.Add(pair.Value.solved);
            gameData.chestItemDropped.Add(pair.Value.itemDropped);
        }
    }

    private void DeserializeChests()
    {
        gameData.chestsSolved.Clear();

        if (gameData.chestKeys.Count != gameData.chestSolved.Count)
        {
            throw new System.Exception("Number of keys and values do not match!");
        }

        for (int i = 0; i < gameData.chestKeys.Count; i++)
        {
            Debug.Log("Putting : " + gameData.chestKeys[i] + " " + gameData.chestSolved[i] + " " + gameData.chestItemDropped[i]);
            gameData.chestsSolved.Add(gameData.chestKeys[i], (gameData.chestSolved[i], gameData.chestItemDropped[i]));
        }
    }

    private void SerializeCheckpoints()
    {
        gameData.checkpointKeys.Clear();
        gameData.checkpointValues.Clear();

        foreach(var pair in gameData.checkpoints)
        {
            gameData.checkpointKeys.Add(pair.Key);
            gameData.checkpointValues.Add(pair.Value);
        }
    }

    private void DeserializeCheckpoints()
    {
        gameData.checkpoints.Clear();

        if(gameData.checkpointKeys.Count != gameData.checkpointValues.Count)
        {
            throw new System.Exception("Number of keys and values do not match!");
        }

        for(int i = 0; i < gameData.checkpointKeys.Count; i++)
        {
            gameData.checkpoints.Add(gameData.checkpointKeys[i], gameData.checkpointValues[i]);
        }
    }

    public bool HasGameData()
    {
        return gameData != null;
    }
}
