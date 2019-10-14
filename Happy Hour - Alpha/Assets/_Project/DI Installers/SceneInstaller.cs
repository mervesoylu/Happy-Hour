using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] List<Player> _players;
        [SerializeField] List<GameObject> _characters;
        [SerializeField] List<Transform> _spawnPoints;
        [SerializeField] GameObject _boardUIPrefab;
        [SerializeField] Round _round;

        public override void InstallBindings()
        {
            Container.BindInstances(_players, _characters, _spawnPoints, _round);
            Container.Bind<BoardController>().FromComponentInNewPrefab(_boardUIPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<Game>().AsSingle();
        }
    } 
}