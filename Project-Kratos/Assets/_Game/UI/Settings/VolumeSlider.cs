using System;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Audio;

namespace ProjectKratos
{
    public class VolumeSlider : MonoBehaviour
    {
        
        [SerializeField] private AudioMixerGroup _mixerGroup;
        
        [SerializeField] private string _mixerParameter;
        
        [Tooltip("The track to change volume on")]
        public MMSoundManager.MMSoundManagerTracks Track;

        public void UpdateVolume(float newVolume)
        {
            // Change the volume with a log func because decibles are logarithmic
            newVolume = Mathf.Log(newVolume) * 20;
            
            _mixerGroup.audioMixer.SetFloat(_mixerParameter, newVolume);
        }
        
    }
}
