using UnityEngine;
using UnityEngine.UI;

public class RemoveFromGridLayout : MonoBehaviour
{
    public HorizontalLayoutGroup gridLayout; // ������ �� ��������� GridLayoutGroup
    public GameObject objectToRemove; // ������, ������� �� ������ �������

    public void RemoveObject()
    {
        if (gridLayout != null && objectToRemove != null)
        {
            gridLayout.transform.DetachChildren();
            Destroy(objectToRemove);
        }
    }
}
