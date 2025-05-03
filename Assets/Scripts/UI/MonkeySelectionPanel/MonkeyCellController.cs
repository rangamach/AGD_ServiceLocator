using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Player;
using ServiceLocator.Main;

namespace ServiceLocator.UI
{
    public class MonkeyCellController
    {
        private MonkeyCellView monkeyCellView;
        private MonkeyCellScriptableObject monkeyCellSO;

        private PlayerService player_service;

        public MonkeyCellController(Transform cellContainer, MonkeyCellView monkeyCellPrefab, MonkeyCellScriptableObject monkeyCellScriptableObject, PlayerService player_service)
        {
            this.player_service = player_service;

            this.monkeyCellSO = monkeyCellScriptableObject;
            monkeyCellView = Object.Instantiate(monkeyCellPrefab, cellContainer);
            monkeyCellView.SetController(this);
            monkeyCellView.ConfigureCellUI(monkeyCellSO.Sprite, monkeyCellSO.Name, monkeyCellSO.Cost);
        }

        public void MonkeyDraggedAt(Vector3 dragPosition)
        {
            player_service.ValidateSpawnPosition(monkeyCellSO.Cost, dragPosition);
        }

        public void MonkeyDroppedAt(Vector3 dropPosition)
        {
            player_service.TrySpawningMonkey(monkeyCellSO.Type, monkeyCellSO.Cost, dropPosition);
        }
    }
}