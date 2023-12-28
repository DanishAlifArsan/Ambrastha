using UnityEngine;

public class ActivateGameObject : MonoBehaviour
{
    public void Deactivate() {
        gameObject.SetActive(false);
    }
}
