using System.IO;
using UnityEngine;

namespace ResourcesMining
{
    public class SaveLoadService
    {
        private const string FileName = "savedata.sav";

        private readonly ProgressService _progressService;

        public SaveLoadService(ProgressService progressService)
        {
            _progressService = progressService;
        }

        public void SaveProgress() => 
            File.WriteAllText(GetFilePath(), _progressService.PlayerProgress.ToJson());

        public PlayerProgress LoadProgress()
        {
            if (File.Exists(GetFilePath()))
                return File.ReadAllText(GetFilePath()).ToDeserialized<PlayerProgress>();
            else
                return null;
        }

        private string GetFilePath() =>
            Application.persistentDataPath + "/" + FileName;
    }
}