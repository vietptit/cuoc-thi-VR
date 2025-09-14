using System.Collections;
using UnityEngine;

public class DoorRotate : MonoBehaviour
{
    public Transform hinge;        // Điểm bản lề
    public float angle = 90f;      // Góc mở
    public float duration = 1f;    // Thời gian xoay
    [SerializeField] LayerMask _player;
    float detectRadius = .5f;

    private bool isOpen = false;
    private Coroutine rotateRoutine;   // <-- chỉ khai báo một lần
    [SerializeField] AudioSource audioSource;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && CheckPlayer())
        {
            if (rotateRoutine != null) StopCoroutine(rotateRoutine);
            rotateRoutine = StartCoroutine(RotateDoor());
        }
    }

    bool CheckPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRadius, _player);
        return colliders.Length > 0;
    }

    IEnumerator RotateDoor()
    {
        isOpen = !isOpen;
        audioSource.Play();
        float targetAngle = isOpen ? angle : -angle; // xoay tương đối
        float rotated = 0f;

        while (Mathf.Abs(rotated) < Mathf.Abs(targetAngle))
        {
            float step = (targetAngle / duration) * Time.deltaTime;
            transform.RotateAround(hinge.position, Vector3.up, step);
            rotated += step;
            yield return null;
        }

        // Snap về đúng góc mục tiêu (phòng sai số do deltaTime)
        float remain = targetAngle - rotated;
        if (Mathf.Abs(remain) > 0.001f)
            transform.RotateAround(hinge.position, Vector3.up, remain);

        rotateRoutine = null;
    }
}
