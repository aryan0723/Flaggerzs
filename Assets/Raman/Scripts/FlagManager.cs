using UnityEngine;

public class FlagManager : MonoBehaviour
{
    public static FlagManager Instance;
    public int flagCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

}
