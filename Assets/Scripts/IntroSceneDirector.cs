using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
  [SerializeField] private GameObject player;
  [SerializeField] private GameObject main_camera;
  [SerializeField] private GameObject virtual_camera;
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip gun_load;
  [SerializeField] private AudioClip gun_fire;
  [SerializeField] private GameObject gun;
  [SerializeField] private GameObject locked_chest;
  [SerializeField] private GameObject unlocked_chest;
  private Image fadeImage;

  Vector3 camera_interview_position = new Vector3(0, 3.37f, -10);

  private void Awake()
  {
    // Initialize the fade image for transitions
    Init_fadeImage();
  }

  private void Init_fadeImage() // make a black screen for fade transition, hacky implementation
  {
    Canvas canvas = gameObject.AddComponent<Canvas>();
    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    canvas.sortingOrder = 999;

    gameObject.AddComponent<CanvasScaler>();
    gameObject.AddComponent<GraphicRaycaster>();

    fadeImage = new GameObject("FadeImage").AddComponent<Image>();
    fadeImage.transform.SetParent(transform, false);
    fadeImage.color = Color.clear;
    fadeImage.raycastTarget = false;

    // Stretch to fill the canvas
    RectTransform rt = fadeImage.rectTransform;
    rt.anchorMin = Vector2.zero;
    rt.anchorMax = Vector2.one;
    rt.offsetMin = Vector2.zero;
    rt.offsetMax = Vector2.zero;
  }

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

  [YarnCommand("Sal_enters")]
  public void Sal_enters()
  {
    sal_alive.SetActive(true);
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


  [YarnCommand("Move_to_waiting_room")]
  public IEnumerator Move_to_waiting_room()
  {
    // Fade to black
    float elapsed = 0f;
    fadeImage.color = Color.clear;
    while (elapsed < 1.5f)
    {
      elapsed += Time.deltaTime;
      fadeImage.color = Color.Lerp(Color.clear, Color.black, elapsed / 1.5f);
      yield return null;
    }
    fadeImage.color = Color.black;

    yield return new WaitForSeconds(0.75f); // Wait for a moment before transitioning
    don_bill.SetActive(false);
    don_calvo.SetActive(false);
    don_lacrimoso.SetActive(false);
    don_contento.SetActive(false);
    player.SetActive(true);
    virtual_camera.SetActive(true);

    elapsed = 0f;
    while (elapsed < 1.5f)
    {
      elapsed += Time.deltaTime;
      fadeImage.color = Color.Lerp(Color.black, Color.clear, elapsed / 1.5f);
      yield return null;
    }
    fadeImage.color = Color.clear;
  }

  [YarnCommand("Move_to_interview_day_1")]
  public IEnumerator Move_to_interview_day_1()
  {
    // Fade to black
    float elapsed = 0f;
    fadeImage.color = Color.clear;
    while (elapsed < 1.5f)
    {
      elapsed += Time.deltaTime;
      fadeImage.color = Color.Lerp(Color.clear, Color.black, elapsed / 1.5f);
      yield return null;
    }
    fadeImage.color = Color.black;

    yield return new WaitForSeconds(0.75f); // Wait for a moment before transitioning
    don_bill.SetActive(true);
    don_calvo.SetActive(true);
    don_lacrimoso.SetActive(true);
    don_contento.SetActive(true);
    don_espresso.SetActive(true);
    sal_dead.SetActive(false);
    player.transform.position = new Vector2(0, -1);
    virtual_camera.SetActive(false);
    main_camera.transform.position = camera_interview_position;

    elapsed = 0f;
    while (elapsed < 1.5f)
    {
      elapsed += Time.deltaTime;
      fadeImage.color = Color.Lerp(Color.black, Color.clear, elapsed / 1.5f);
      yield return null;
    }
    fadeImage.color = Color.clear;
  }

  // Unlock chest
  [YarnCommand("Unlock_Chest")]
  public void Unlock_Chest()
  {
    locked_chest.SetActive(false);
    unlocked_chest.SetActive(true);
  }
}
