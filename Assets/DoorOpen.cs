using System.Collections;
using UnityEngine;

public class DoorRotate : MonoBehaviour
{
    public Transform hinge;   // Vị trí bản lề (đặt một Empty ở chỗ bản lề)
    public float angle = 90f; // Góc mở cửa
    public float duration = 1f;   // Tốc độ mở
    private bool isOpen = false;
    private Coroutine rotateRoutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Nhấn E để mở/đóng
        {
            if (rotateRoutine != null) StopCoroutine(rotateRoutine);
            rotateRoutine = StartCoroutine(RotateDoor());
        }
    }
    IEnumerator RotateDoor()
    {
        isOpen = !isOpen;

        float targetAngle = isOpen ? angle : -angle;
        float rotated = 0f; // lượng đã xoay
        float stepPerFrame = targetAngle / duration * Time.deltaTime;

        while (Mathf.Abs(rotated) < Mathf.Abs(targetAngle))
        {
            float step = targetAngle / duration * Time.deltaTime;
            transform.RotateAround(hinge.position, Vector3.up, step);
            rotated += step;

            yield return null;
        }
    }
}
