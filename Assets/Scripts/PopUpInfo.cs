using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpInfo : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text titulo;
    [SerializeField] private Text descricao;
    [SerializeField] private Image imagem;

    private bool popupAberto = false;

    private Queue<PopupData> filaPopups = new Queue<PopupData>();

    private class PopupData
    {
        public string titulo;
        public string descricao;
        public Sprite imagem;

        public PopupData(string titulo, string descricao, Sprite imagem)
        {
            this.titulo = titulo;
            this.descricao = descricao;
            this.imagem = imagem;
        }
    }

    public void Mostrar(string tituloTexto, string descricaoTexto, Sprite sprite)
    {
        filaPopups.Enqueue(
            new PopupData(
                tituloTexto,
                descricaoTexto,
                sprite
            )
        );

        VerificarFila();
    }

    private void VerificarFila()
    {
        if (popupAberto)
            return;

        if (filaPopups.Count == 0)
            return;

        PopupData popup = filaPopups.Dequeue();

        titulo.text = popup.titulo;
        descricao.text = popup.descricao;
        imagem.sprite = popup.imagem;

        popupAberto = true;
        gameObject.SetActive(true);
    }

    public void btnContinuar()
    {
        gameObject.SetActive(false);

        popupAberto = false;

        VerificarFila();
    }

    public int GetQuantidadePopups()
    {
        return filaPopups.Count;
    }

    public bool PossuiPopupAberto()
    {
        return popupAberto;
    }
    
}
