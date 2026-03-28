using UnityEngine;

public class TripWireTeleport : MonoBehaviour
{
  [Header("Where should the player go?")]
  [Tooltip("Type the exact spawnID of the destination TripWireSpawn.")]
  public string targetSpawnID;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      TripWireSpawn[] allSpawns = FindObjectsOfType<TripWireSpawn>();
      foreach (TripWireSpawn spawn in allSpawns)
      {
        if (spawn.spawnID == targetSpawnID)
        {
          collision.transform.position = spawn.transform.position;
          return;
        }
      }

      Debug.LogWarning("Teleport failed! Could not find a TripWireSpawn with the ID: " + targetSpawnID);
    }
  }
}
