using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    public float floatSpeed = 2f; // Velocidad de flotación
    public float floatAmplitude = 0.1f; // Amplitud del movimiento (qué tan alto y bajo)

    private Vector3 startPosition; // Variable para almacenar la posición inicial del objeto

    void Start()
    {
        // Guardamos la posición inicial del objeto
        startPosition = transform.position;
    }

    void Update()
    {
        // Movimiento oscilante en el eje Y
        transform.position = startPosition + new Vector3(0f, Mathf.Sin(Time.time * floatSpeed) * floatAmplitude, 0f);
    }
}


