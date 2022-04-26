using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ChoETL;
using UnityEngine;

public abstract class SaveManager : MonoBehaviour
{
    public string folderName;
    public string fileName;
    public string fileSource;

    private string _persistantPath;
    private string _fullPath;
    public string fullFolder;
    protected string _json;
    private string _extension;

    public AudioSource audioSource;
    public AudioClip saveAudio;

    public GameStateManager gameStateManager;

    protected abstract string gameType { get; set; }
    
    public List<Snapshot> _snapshots = new List<Snapshot>();

    private void Start()
    {
        UpdateSaveInfo();
    }

    /// <summary>
    /// Save the object to the applications path
    /// </summary>
    /// <param name="obj">The object to save</param>
    /// <exception cref="NotImplementedException">Filename needs to be generated with GenerateFileName</exception>
    public void Save()
    {
         var saveThread = new Thread(SaveTask);
         saveThread.Start();
    }

    private void SaveTask()
    {
        try
        {
            Logger.Log($"Saving data to: {_fullPath}");

            _snapshots.ForEach(snap => _json += $"{JsonUtility.ToJson(snap, true)},");
            _json = $"[{_json.Substring(0, _json.Length - 1)}]";
        
            Directory.CreateDirectory(fullFolder);
            File.WriteAllText(_fullPath, _json);
            Debug.Log(fileSource);
        
            using (var p = ChoJSONReader.LoadText(_json))
            {
                using (var w = new ChoCSVWriter($"{fullFolder}/{fileSource}.csv")
                           .WithFirstLineHeader()
                      )
                {
                    w.Write(p);
                }
            }

            _json = "";
            EmptyBuffer();
        
            Debug.Log("Game Successfully Saved.");
        }
        catch (Exception e)
        {
            Debug.Log("ERROR WITH SAVING!: " + e);
            throw;
        }
        
    }

    public void EmptyBuffer()
    {
        _snapshots.Clear();
    }

    /// <summary>
    /// Take a snapshot of the current game variables.
    /// </summary>
    /// <param name="time">Time taken of the snapshot (i.e., Time.time)</param>
    public void TakeSnapshot(float time, Snapshot snapshot)
    {
        snapshot.time = time;
        _snapshots.Add(snapshot);
    }
    
    /// <summary>
    /// Generates a file name appended with the company, date and time.
    /// </summary>
    /// <param name="gameType">The current gametype to generate the file</param>
    /// <returns>The generated file name.</returns>
    public string GenerateDefaultFileName(GameStateManager.Games gameType)
    {
        var prepend = Application.companyName;
        var middle = folderName;

        var datetime = DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss");
        var append = $"{datetime}_{Application.version}";

        fileSource = $"{prepend}_{middle}_{append}";
        fileName = $"{fileSource}.json";
        
        return fileName;
    }

    public void UpdateSaveInfo()
    {
        _persistantPath = Application.persistentDataPath;

        // set a default name if none is specified
        GenerateDefaultFileName(gameStateManager.GetCurrentGame());

        if (String.IsNullOrEmpty(folderName))
            folderName = gameType;

        fullFolder = $"{_persistantPath}/{folderName}";
        _fullPath = $"{fullFolder}/{fileName}";
    }
    
    
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public void SnapshotsToJSON(Snapshot snapshot)
    {
        _json = JsonUtility.ToJson(snapshot);
    }

    public void InitProperties()
    {
        
    }

    public void playSaveAudio()
    {
        audioSource.PlayOneShot(saveAudio);
    }
}
