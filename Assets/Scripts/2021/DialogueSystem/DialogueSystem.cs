using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueSystem : MonoBehaviour
{
    Image _screenImage, _background;
    Text _textComponent;
    RectTransform _textRectTransform;

    List<StorytellingScreen> _screenList;
    public List<StorytellingScreen> ScreenList { get => _screenList; }
    string _dialogueText; 
    [SerializeField] bool _playOnAwake;
    [SerializeField] float _textDelay; 
    [SerializeField] float _screenDelay; 
    [SerializeField] float _fadeDelay;
    bool _isFading;
    Color _initialColorScreen;
    float _maxAlpha;

    void Awake() 
    {
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            if (image.gameObject.name == "Image Screen")
            {
                _screenImage = image;
            }
            else if (image.gameObject.name == "Storytelling Panel")
            {
                _background = image;
            }
        }
        _textComponent = GetComponentInChildren<Text>();
        _textRectTransform = _textComponent.gameObject.GetComponent<RectTransform>();   
    }

    void Start() 
    {
        _screenList = GetStorytellingFromJson("StorytellingList").list;

        _initialColorScreen = _screenImage.color;

        if (_playOnAwake)
        {
            StartCoroutine(Show(0));   
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    ScreenList GetStorytellingFromJson(string jsonFileName)
    {
        string _filePath = Resources.Load<TextAsset>("Json/" + jsonFileName).text;
        ScreenList screenList = JsonUtility.FromJson<ScreenList>(_filePath);

        return screenList;
    }

    public IEnumerator Show(int screen)
    {
        gameObject.SetActive(true);

        _screenImage.sprite = Resources.Load<Sprite>("Images/Storytelling/" + _screenList[screen].imagePath);
        _dialogueText = _screenList[screen].dialogueText;
        _textRectTransform.localPosition = new Vector3(_screenList[screen].xPositionText, _screenList[screen].yPositionText);
        _textComponent.fontSize = _screenList[screen].sizeText;
        if (_screenList[screen].BlackColorText)
        {
            _textComponent.color = Color.black;
            //_background.color = Color.white;
            _maxAlpha = 1f;
        }
        else
        {
            _textComponent.color = Color.white;
            //_background.color = Color.black;
            _maxAlpha = _initialColorScreen.a;
        }
        _textComponent.text = "";

        StartCoroutine(FadeIn());
        while (_isFading)
        {
            yield return null;
        }

        //animación texto 
        for (int i = 0; i < _dialogueText.Length; i++)
        {
            _textComponent.text += _dialogueText[i].ToString();      
            yield return new WaitForSeconds(_textDelay); 
        }

        //si la pantalla tiene botón, lo activa
        if (_screenList[screen].hasButton)
        {
            yield return new WaitForSeconds(0.3f);
        }
        //si no, efecto fadeOut siguiente pantalla
        else
        {
            yield return new WaitForSeconds(_screenDelay);

            StartCoroutine(FadeOut());
            while (_isFading)
            {
                yield return null;
            }

            //si no es la última pantalla, pasa a la siguiente
            if (!_screenList[screen].lastScreen)
            {
                StartCoroutine(Show(screen + 1));   
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }  

    IEnumerator FadeIn()
    {
        _isFading = true;

        float screenColorAlpha = 0f;

        while (screenColorAlpha < _maxAlpha) 
        {
            screenColorAlpha += 0.01f;
            _screenImage.color = new Color(_screenImage.color.r, _screenImage.color.g, _screenImage.color.b, screenColorAlpha); 
            yield return new WaitForSeconds(_fadeDelay);
        }

        _isFading = false;
    }

    IEnumerator FadeOut()
    {
        _isFading = true;

        float screenColorAlpha = _maxAlpha; 
        float textColorAlpha = 1f;

        while (textColorAlpha > 0f)
        {
            screenColorAlpha -= 0.01f;
            _screenImage.color = new Color(_screenImage.color.r, _screenImage.color.g, _screenImage.color.b, screenColorAlpha); 
            textColorAlpha -= 0.01f;
            _textComponent.color = new Color(_textComponent.color.r, _textComponent.color.g, _textComponent.color.b, textColorAlpha);
            yield return new WaitForSeconds(_fadeDelay);
        }

        _isFading = false;
    }
}
