using UnityEngine;

public class SoundControler : MonoBehaviour
{
    [SerializeField] private AudioSource SomBotão;
    [SerializeField] private AudioSource MusicaPrincipal;
    public void TocarSomBotao(){
        SomBotão.Play();
    }    

    public void TocarMusicaPrincipal(){
        MusicaPrincipal.Play();
    }

    public void pararMusicaPrincipal(){
        MusicaPrincipal.Stop();
    }
}
