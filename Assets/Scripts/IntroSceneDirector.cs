using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class IntroSceneDirector : MonoBehaviour
{
  [SerializeField] private GameObject don_espresso;
  [SerializeField] private GameObject don_calvo;
  [SerializeField] private GameObject don_bill;
  [SerializeField] private GameObject don_lacrimoso;
  [SerializeField] private GameObject don_contento;
  [SerializeField] private GameObject sal_alive;
  [SerializeField] private GameObject sal_dead;
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip gun_load;
  [SerializeField] private AudioClip gun_fire;
  [SerializeField] private GameObject gun;



  // Gun load
  [YarnCommand("Gun_load")]
  public void Gun_load()
  {
    gun.SetActive(true);
    audioSource.PlayOneShot(gun_load);
  }

  // Gun fire
  [YarnCommand("Gun_fire")]
  public void Gun_fire()
  {
    audioSource.PlayOneShot(gun_fire);
  }

  // Sal dies
  [YarnCommand("Sal_dies")]
  public void Sal_dies()
  {
    sal_alive.SetActive(false);
    gun.SetActive(false);
    sal_dead.SetActive(true);
  }

  // Espresso bathroom break
  [YarnCommand("Espresso_goes_bye_bye")]
  public void Espresso_goes_bye_bye()
  {
    don_espresso.SetActive(false);
  }

  // Finish intro cutscene
  [YarnCommand("End_intro_dialogue")]
  public void End_intro_dialogue()
  {
    SceneManager.LoadScene(1);
  }
}
