using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

namespace ScrollShooter.Supports
{
    public class SettingsSaveData : MonoBehaviour
    {
        public Slider soundSlider;
        public Slider musicSlider;

        public AudioSource musicAudioSource;

        private float musicVolume;
        private float soundVolume;

        public float MusicVolume
        {
            get
            {
                return musicVolume;
            }
            set
            {
                musicVolume = value;

                musicAudioSource.volume = musicVolume;

                try
                {
                    musicSlider.value = musicVolume;
                }
                catch
                {
                    Debug.LogError("Music slider is not assigned");
                }
            }
        }

        public float SoundVolume
        {
            get
            {
                return soundVolume;
            }
            set
            {
                soundVolume = value;


                OnSetSoundVolume(soundVolume);
                try
                {
                    soundSlider.value = soundVolume;
                }
                catch
                {
                    Debug.LogError("Sound slider is not assigned");
                }
            }
        }

        public void SetSoundVolume(Slider slider)
        {
            SetVolume(ref soundVolume, slider);

            OnSetSoundVolume(soundVolume);
        }

        public void SetMusicVolume(Slider slider)
        {
            SetVolume(ref musicVolume, slider);

            musicAudioSource.volume = musicVolume;
        }

        private void Start()
        {
            LoadGame();
        }

        private void LoadGame()
        {
            if (File.Exists(Application.persistentDataPath
              + "/MySaveData.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file =
                  File.Open(Application.persistentDataPath
                  + "/MySaveData.dat", FileMode.Open);
                SettingsData data = (SettingsData)bf.Deserialize(file);
                file.Close();
                SoundVolume = data.soundVolume;
                MusicVolume = data.musicVolume;
                Debug.Log("Game data loaded!");
            }
            else
                Debug.LogError("There is no save data!");
        }

        private void SaveGame()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath
              + "/MySaveData.dat");
            SettingsData data = new SettingsData();
            data.soundVolume = soundVolume;
            data.musicVolume = musicVolume;
            bf.Serialize(file, data);
            file.Close();
            Debug.Log("Game data saved!");
        }

        private void SetVolume(ref float vulume, Slider slider)
        {
            vulume = slider.value;
            SaveGame();
        }

        private void OnSetSoundVolume(float value)
        {
            List<AudioSource> soundsAudioSources = FindObjectsOfType<AudioSource>().ToList();

            foreach(AudioSource audioSource in soundsAudioSources)
            {
                if (audioSource.gameObject.CompareTag("Sound"))
                {
                    audioSource.volume = value;
                }
            }
        }
    }

    [Serializable]
    class SettingsData
    {
        public float soundVolume;
        public float musicVolume;
    }
}