﻿using System;
using System.Collections.Generic;
using NM.Services;
using NM.Services.Factory;
using NM.Services.GameLoop;
using NM.Services.StaticData;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NM.UnityLogic.LevelExit
{
    public class LevelExitZone : MonoBehaviour
    {
        private List<MinionContainer> _enteredMinions = new List<MinionContainer>();

        private GameFactory _gameFactory;
        private GameLoopService _gameLoopService;
        
        private int _neededMinionsCount;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<GameFactory>();
            _gameLoopService = AllServices.Container.Single<GameLoopService>();
            Init();
        }
        private void Init()
        {
            var staticDataService = AllServices.Container.Single<StaticDataService>();
            var sceneName = SceneManager.GetActiveScene().name;
            var levelData = staticDataService.GetLevelData(sceneName);
            _neededMinionsCount = levelData.MinionSpawners.Count;   
        }
        private void OnEnable() => _gameFactory.OnCleanedUp += ClearData;
        private void OnDisable() => _gameFactory.OnCleanedUp -= ClearData;
        private void ClearData() => _enteredMinions.Clear();
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MinionContainer minion) &&
                !_enteredMinions.Contains(minion))
            {
                _enteredMinions.Add(minion);
                if (_enteredMinions.Count >= _neededMinionsCount)
                {
                    _gameLoopService.CompleteLevel();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out MinionContainer minion) &&
                _enteredMinions.Contains(minion))
            {
                _enteredMinions.Remove(minion);
            }
        }
    }
}