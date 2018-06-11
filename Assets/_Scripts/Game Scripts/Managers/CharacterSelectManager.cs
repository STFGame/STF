using UI;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private CursorInputModule _cursorInputModule;
    [SerializeField] private PlayerPortrait[] _playerPortrait;

    // Use this for initialization
    private void Awake()
    {
        _button.gameObject.SetActive(false);
    }

    private void Update()
    {
        int controllerActiveCount = 0;
        for (int i = 0; i < _cursorInputModule.Count; i++)
        {
            if (_cursorInputModule.GetCursor(i).IsActive)
                controllerActiveCount++;
        }

        int charactersPicked = 0;
        for (int i = 0; i < controllerActiveCount; i++)
        {
            if (!_playerPortrait[i].CharacterPicked)
                break;
            charactersPicked++;
        }

        if (charactersPicked == controllerActiveCount)
            _button.gameObject.SetActive(true);
        else
            _button.gameObject.SetActive(false);

    }
}
