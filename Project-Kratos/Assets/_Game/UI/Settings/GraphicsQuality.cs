using UnityEngine;

public class GraphicsQuality : MonoBehaviour {

  public void SetQuality (int qualityIndex) 
  { 
      QualitySettings.SetQualityLevel (qualityIndex);
  }
  
}
