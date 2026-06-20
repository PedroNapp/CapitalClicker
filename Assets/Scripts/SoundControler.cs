using UnityEngine;

public class SoundControler : MonoBehaviour
{
    [SerializeField] private AudioSource SomBotão;

    public void TocarSomBotao(){
        SomBotão.Play();
    }    
}
