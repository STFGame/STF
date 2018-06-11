using Managers;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerPortrait : MonoBehaviour
{
    [SerializeField] private int m_playerPortraitIndex = 0;
    [SerializeField] private bool m_resetOnDisable = false;

    private Image m_characterImage = null;

    public bool CharacterPicked { get; private set; } = false;

    private void Awake()
    {
        m_characterImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (m_resetOnDisable)
        {
            PlayerSettings.SetCharacter(null, m_playerPortraitIndex);
            PlayerSettings.SetCharacterPortrait(null, m_playerPortraitIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Sprite characterPortrait = PlayerSettings.GetCharacterPortrait(m_playerPortraitIndex);

        m_characterImage.sprite = characterPortrait;

        CharacterPicked = (characterPortrait != null);
    }
}
