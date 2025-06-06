using UnityEngine;

public class CameraScript2 : MonoBehaviour
{
    Transform target;  // Transform del objeto objetivo que la cámara seguirá
    Vector3 velocity = Vector3.zero;  // Velocidad actual de la cámara, inicializada a cero

    public Vector3 positionOffset;  // Desplazamiento de la posición de la cámara
    //public Vector2 xLimit;  // Límite de la posición en el eje x (comentado)
    //public Vector2 yLimit;  // Límite de la posición en el eje y (comentado)

    [Range(0,1)]
    public float smoothTime;  // Tiempo de suavizado para el movimiento de la cámara

    private void Awake()
    {
        // Encuentra el objeto con la etiqueta "Player" y obtiene su Transform
        target = GameObject.FindGameObjectWithTag("Player").transform;
    } 

    private void LateUpdate()
    {
        // Calcula la posición objetivo sumando el desplazamiento a la posición del objetivo
        Vector3 targetPosition = target.position + positionOffset;
        //targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y), Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y), -10);  // Restringe la posición objetivo dentro de los límites especificados (comentado)

        // Suaviza el movimiento de la cámara hacia la posición objetivo usando SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
