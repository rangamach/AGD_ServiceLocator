using ServiceLocator.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.UI
{
    public class MonkeySelectionUIController
    {
        private Transform cellContainer;
        private List<MonkeyCellController> monkeyCellControllers;

        private PlayerService player_service;

        public MonkeySelectionUIController(Transform cellContainer, MonkeyCellView monkeyCellPrefab, List<MonkeyCellScriptableObject> monkeyCellScriptableObjects, PlayerService player_service)
        {
            this.player_service = player_service;

            this.cellContainer = cellContainer;
            monkeyCellControllers = new List<MonkeyCellController>();

            foreach (MonkeyCellScriptableObject monkeySO in monkeyCellScriptableObjects)
            {
                MonkeyCellController monkeyCell = new MonkeyCellController(cellContainer, monkeyCellPrefab, monkeySO, player_service);
                monkeyCellControllers.Add(monkeyCell);
            }
        }

        public void SetActive(bool setActive) => cellContainer.gameObject.SetActive(setActive);

    }
}