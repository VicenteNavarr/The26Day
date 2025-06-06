using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    //[SerializeField] private AudioMixer audioMixer;  // Mezclador de audio (comentado)
    [SerializeField] private AudioManager audioManager;  // Administrador de audio

    [SerializeField] private Slider musicSlider; // Referencia al slider de música
    [SerializeField] private Slider sfxSlider; // Referencia al slider de efectos de sonido

    private void Start()
    {
        // Cargar y asignar los valores guardados de los sliders
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f); // Valor por defecto 0.75
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f); // Valor por defecto 0.75

        // Asignar los valores a los sliders
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        // Establecer los volúmenes iniciales en el AudioManager
        audioManager.SetMusicVolume(musicVolume);
        audioManager.SetSFXVolume(sfxVolume);
    }

    // Método para cambiar el volumen de la música
    public void ChangeMusicVolume(float volume)
    {
        audioManager.SetMusicVolume(volume);
        PlayerPrefs.SetFloat("MusicVolume", volume); // Guardar el valor del volumen de la música
    }

    // Método para cambiar el volumen de los efectos de sonido
    public void ChangeSFXVolume(float volume)
    {
        audioManager.SetSFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume); // Guardar el valor del volumen de los efectos de sonido
    }
}

