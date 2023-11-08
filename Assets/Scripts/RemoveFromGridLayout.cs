using UnityEngine;
using UnityEngine.UI;

public class RemoveFromGridLayout : MonoBehaviour
{
    public HorizontalLayoutGroup gridLayout; // —сылка на компонент GridLayoutGroup
    public GameObject objectToRemove; // ќбъект, который вы хотите удалить

    public void RemoveObject()
    {
        if (gridLayout != null && objectToRemove != null)
        {
            gridLayout.transform.DetachChildren();
            Destroy(objectToRemove);
        }
    }
}
