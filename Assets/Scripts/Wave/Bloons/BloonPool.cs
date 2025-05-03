using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Utilities;
using ServiceLocator.Player;
using ServiceLocator.Sound;

/*  This script demonstrates the implementation of Object Pool design pattern.
 *  If you're interested in learning about Object Pooling, you can find
 *  a dedicated course on Outscal's website.
 *  Link: https://outscal.com/courses
 * */

namespace ServiceLocator.Wave.Bloon
{
    public class BloonPool : GenericObjectPool<BloonController>
    {
        private BloonView bloonPrefab;
        private List<BloonScriptableObject> bloonScriptableObjects;
        private Transform bloonContainer;
        private PlayerService player_service;
        private WaveService wave_service;
        private SoundService sound_service;

        public BloonPool(WaveScriptableObject waveScriptableObject, PlayerService player_service, WaveService wave_service, SoundService sound_service)
        {
            this.player_service = player_service;
            this.sound_service = sound_service;
            this.wave_service = wave_service;

            this.bloonPrefab = waveScriptableObject.BloonPrefab;
            this.bloonScriptableObjects = waveScriptableObject.BloonScriptableObjects;
            this.player_service = player_service;
            bloonContainer = new GameObject("Bloon Container").transform;
        }

        public BloonController GetBloon(BloonType bloonType)
        {
            BloonController bloon = GetItem();
            BloonScriptableObject scriptableObjectToUse = bloonScriptableObjects.Find(so => so.Type == bloonType);
            bloon.Init(scriptableObjectToUse);
            return bloon;
        }

        protected override BloonController CreateItem() => new BloonController(bloonPrefab, bloonContainer,player_service,wave_service,sound_service);
    }
}