using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class DemoSceneDirector : MonoBehaviour
{
  [SerializeField] private GameObject locked_chest;
  [SerializeField] private GameObject unlocked_chest;

  // Unlock chest
  [YarnCommand("Unlock_Chest")]
  public void Unlock_Chest()
  {
    locked_chest.SetActive(false);
    unlocked_chest.SetActive(true);
  }
}
