using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Wave.Bloon;
using System.Threading.Tasks;
using ServiceLocator.Main;
using ServiceLocator.Events;
using ServiceLocator.UI;
using ServiceLocator.Map;
using ServiceLocator.Sound;

namespace ServiceLocator.Wave
{
    public class WaveService
    {
        private WaveScriptableObject waveScriptableObject;
        private BloonPool bloonPool;

        private int currentWaveId;
        private List<WaveData> waveDatas;
        private List<BloonController> activeBloons;

        private EventService event_service;
        private UIService ui_service;
        private MapService map_service;
        private SoundService sound_service;

        public WaveService(WaveScriptableObject waveScriptableObject)
        {
            this.waveScriptableObject = waveScriptableObject;
        }

        public void Init(EventService event_service, UIService ui_service, MapService map_service, SoundService sound_service)
        {
            this.event_service = event_service;
            this.ui_service = ui_service;
            this.map_service = map_service;
            this.sound_service = sound_service;

            InitializeBloons();
            SubscribeToEvents();
        }

        private void InitializeBloons()
        {
            bloonPool = new BloonPool(waveScriptableObject);
            activeBloons = new List<BloonController>();
        }

        private void SubscribeToEvents() => event_service.OnMapSelected.AddListener(LoadWaveDataForMap);

        private void LoadWaveDataForMap(int mapId)
        {
            currentWaveId = 0;
            waveDatas = waveScriptableObject.WaveConfigurations.Find(config => config.MapID == mapId).WaveDatas;
            ui_service.UpdateWaveProgressUI(currentWaveId, waveDatas.Count);
        }

        public void StarNextWave()
        {
            currentWaveId++;
            var bloonsToSpawn = GetBloonsForCurrentWave();
            var spawnPosition = map_service.GetBloonSpawnPositionForCurrentMap();
            SpawnBloons(bloonsToSpawn, spawnPosition, 0, waveScriptableObject.SpawnRate);
        }

        public async void SpawnBloons(List<BloonType> bloonsToSpawn, Vector3 spawnPosition, int startingWaypointIndex, float spawnRate)
        {
            foreach(BloonType bloonType in bloonsToSpawn)
            {
                BloonController bloon = bloonPool.GetBloon(bloonType);
                bloon.SetPosition(spawnPosition);
                bloon.SetWayPoints(map_service.GetWayPointsForCurrentMap(), startingWaypointIndex);

                AddBloon(bloon);
                await Task.Delay(Mathf.RoundToInt(spawnRate * 1000));
            }
        }

        private void AddBloon(BloonController bloonToAdd)
        {
            activeBloons.Add(bloonToAdd);
            bloonToAdd.SetOrderInLayer(-activeBloons.Count);
        }

        public void RemoveBloon(BloonController bloon)
        {
            bloonPool.ReturnItem(bloon);
            activeBloons.Remove(bloon);
            if (HasCurrentWaveEnded())
            {
                sound_service.PlaySoundEffects(Sound.SoundType.WaveComplete);
                ui_service.UpdateWaveProgressUI(currentWaveId, waveDatas.Count);

                if(IsLevelWon())
                    ui_service.UpdateGameEndUI(true);
                else
                    ui_service.SetNextWaveButton(true);
            }
        }

        private List<BloonType> GetBloonsForCurrentWave() => waveDatas.Find(waveData => waveData.WaveID == currentWaveId).ListOfBloons;

        private bool HasCurrentWaveEnded() => activeBloons.Count == 0;

        private bool IsLevelWon() => currentWaveId >= waveDatas.Count;
    }
}