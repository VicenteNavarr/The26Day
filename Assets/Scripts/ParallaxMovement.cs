using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : MonoBehaviour
{
    private Transform cam; // Cámara principal
    private Vector3 camStartPos; // Posición inicial de la cámara
    private float distance; // Distancia entre la posición inicial de la cámara y la posición actual

    private GameObject[] backgrounds; // Array de fondos para parallax
    private Material[] mat; // Materiales de los fondos
    private float[] backSpeed; // Velocidades de los fondos

    private float farthestBack; // Distancia del fondo más lejano

    [Range(0.01f, 1f)]
    public float parallaxSpeed; // Velocidad del parallax

    void Start()
    {
        cam = Camera.main.transform; // Obtiene la transformación de la cámara principal
        camStartPos = cam.position; // Guarda la posición inicial de la cámara

        int backCount = transform.childCount; // Cuenta el número de hijos (fondos) de este objeto
        mat = new Material[backCount]; // Inicializa el array de materiales
        backSpeed = new float[backCount]; // Inicializa el array de velocidades de fondos
        backgrounds = new GameObject[backCount]; // Inicializa el array de fondos

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject; // Asigna cada fondo al array
            mat[i] = backgrounds[i].GetComponent<Renderer>().material; // Asigna el material de cada fondo al array de materiales
        }

        BackSpeedCalculate(backCount); // Calcula la velocidad de cada fondo
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z; // Calcula la distancia del fondo más lejano
            }
        }

        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack; // Calcula la velocidad de cada fondo en relación con la distancia del fondo más lejano
        }
    }

    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x; // Calcula la distancia recorrida por la cámara en el eje X

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed; // Calcula la velocidad de parallax para cada fondo
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance * speed, 0)); // Ajusta el offset de la textura del fondo en función de la distancia recorrida
        }
    }
}





