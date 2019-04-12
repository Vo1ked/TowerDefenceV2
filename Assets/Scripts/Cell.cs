using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Cell: MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [Inject] SignalBus _signalBus;
    [HideInInspector]public BaseTower Tower;
    MeshRenderer _mesh;

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Cell clicked");
        _signalBus.Fire(new CellClickSignal(this));
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _mesh.material.color = Color.blue;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mesh.material.color = Color.white;
    }


}
