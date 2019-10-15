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
        [SerializeField] CharacterSettings _defaultCharacterSettings;
        [SerializeField] CharacterSettings _happyHourCharacterSettings;

        public override void InstallBindings()
        {
            Container.BindInstances(_players, _characters, _spawnPoints, _round);
            Container.Bind<CharacterSettings>().WithId("defaultCharacterSettings").FromNewScriptableObject(_defaultCharacterSettings).AsTransient(); // could be AsSingle() since all characters share the same settings, but in case in the furture we wanna have individual settings per character.
            Container.Bind<CharacterSettings>().WithId("happyHourCharacterSettings").FromNewScriptableObject(_happyHourCharacterSettings).AsTransient(); // could be AsSingle() since all characters share the same settings, but in case in the furture we wanna have individual settings per character.
            Container.Bind<BoardController>().FromComponentInNewPrefab(_boardUIPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<Game>().AsSingle();
        }
    }
}