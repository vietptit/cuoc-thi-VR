using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFire : MonoBehaviour
{
    [Header("Prefab & timing")]
    [Tooltip("Prefab để instantiate. Nếu là chính object, clone sẽ chứa script này => nguy cơ clone cũng spawn.")]
    [SerializeField] private GameObject prefab;
    [Tooltip("Giây giữa các lần spawn")]
    [SerializeField] private float spawnInterval = 5f;

    [Header("Spawn area (relative to this object's position)")]
    [SerializeField] private float rangeX = 0.5f;
    [SerializeField] private float rangeY = 0.0f;
    [SerializeField] private float rangeZ = 0.5f;

    private Coroutine spawnRoutine;
    private bool isSpawning = false;
    
    private void Start()
    {
        // nếu không gán prefab, mặc định lấy chính object hiện tại (CẢNH BÁO - xem lưu ý ở trên)
        if (prefab == null)
        {
            prefab = this.gameObject;
            Debug.LogWarning("SpawnerFire: prefab not assigned — defaulting to this GameObject. Be careful to disable SpawnerFire on the prefab clone to avoid cascading spawns.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isSpawning) StopSpawning();
            else StartSpawning();
        }
    }

    private void StartSpawning()
    {
        if (prefab == null)
        {
            Debug.LogWarning("SpawnerFire: prefab is null, cannot spawn.");
            return;
        }

        if (spawnRoutine != null) StopCoroutine(spawnRoutine);
        spawnRoutine = StartCoroutine(SpawnLoop());
        isSpawning = true;
    }

    private void StopSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
        isSpawning = false;
    }

    private IEnumerator SpawnLoop()
    {
        // spawn ngay lập tức
        SpawnObject();

        // sau đó chờ rồi spawn tiếp theo interval, lặp vô hạn tới khi StopSpawning()
        while (true)
        {
            yield return new WaitForSeconds(Mathf.Max(0.0001f, spawnInterval));
            SpawnObject();
        }
    }

    public void SpawnObject()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-rangeX, rangeX),
            Random.Range(-rangeY, rangeY),
            Random.Range(-rangeZ, rangeZ)
        );

        Vector3 spawnPos = transform.position + randomOffset;
        GameObject instance = Instantiate(prefab, spawnPos, Quaternion.identity);

        // Nếu prefab là chính object và bạn KHÔNG muốn clone tiếp tục spawn, tắt component SpawnerFire trên clone:
        var spawnerComponent = instance.GetComponent<SpawnerFire>();
        if (spawnerComponent != null)
        {
            // tắt script trên clone để clone không tự spawn
            spawnerComponent.enabled = false;
            // hoặc: Destroy(spawnerComponent); nếu bạn muốn xóa component hẳn
            // Destroy(spawnerComponent);
        }

        // Nếu cần, đặt parent, scale hay init fireController ở đây:
        // instance.transform.parent = someParent;
        // var fire = instance.GetComponent<fireController>();
        // if (fire) fire.Init(...);
    }

    // public API để điều khiển từ code khác
    public void ToggleSpawning() { if (isSpawning) StopSpawning(); else StartSpawning(); }
    public void ForceSpawnOnce() { SpawnObject(); }
}
