using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Project
{
   public class SceneInstaller : MonoInstaller
   {
      [SerializeField] List<Player> _players;
      [SerializeField] List<GameObject> _characters;
      [SerializeField] List<Transform> _spawnPoints;
      [SerializeField] GameObject _pauseControllerPrefab;
      [SerializeField] GameObject _boardUIPrefab;
      [SerializeField] GameObject _readyMenuPrefab;
      [SerializeField] ScoreMoniter _scoreMoniter;
      [SerializeField] Round _round;
      [SerializeField] CharacterSettings _defaultCharacterSettings;
      [SerializeField] CharacterSettings _happyHourCharacterSettings;
      [SerializeField] int _numberOfRoundsPerGame;

      public override void InstallBindings()
      {
         Container.BindInstances(_players, _characters, _spawnPoints, _round, _scoreMoniter);
         Container.Bind<CharacterSettings>().WithId("defaultCharacterSettings").FromNewScriptableObject(_defaultCharacterSettings).AsTransient(); // could be AsSingle() since all characters share the same settings, but in case in the furture we wanna have individual settings per character.
         Container.Bind<CharacterSettings>().WithId("happyHourCharacterSettings").FromNewScriptableObject(_happyHourCharacterSettings).AsTransient(); // could be AsSingle() since all characters share the same settings, but in case in the furture we wanna have individual settings per character.
         Container.Bind<PauseMenuController>().FromComponentInNewPrefab(_pauseControllerPrefab).AsSingle();
         Container.Bind<BoardController>().FromComponentInNewPrefab(_boardUIPrefab).AsSingle();
         Container.Bind<ReadyMenuController>().FromComponentInNewPrefab(_readyMenuPrefab).AsSingle();
         Container.Bind<SoundManager>().FromComponentInHierarchy().AsSingle();
         Container.BindInterfacesAndSelfTo<Game>().AsSingle();
         Container.BindInstance(_numberOfRoundsPerGame).WithId("numberOfRoundsPerGame");
      }
   }
}