using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Device : MonoBehaviour
{
    public GameObject player, hackPanel;

    public Material onPlayerMaterial;
    public Material onAntivirusMaterial;

    public bool playerIsHere, isHackeable, isShooteable, canjump;

    public OpenGate gateToOpen;

    [SerializeField] Renderer rendererReference;
    [SerializeField] int rendererMaterialIndex = 0;
    Material _defaultMaterial;
    Color _IAColor = new Color(0, 0.8705882f, 1);
    Color _AVColor = Color.HSVToRGB(1, 0, 0);
    Color _defaultEmmisiveColor;

    virtual protected void Update()
    {
        CheckPlayerIsHere();
    }

    virtual protected void Start()
    {
        canjump = true;
        player = FindObjectOfType<Player>().gameObject;

        if (rendererReference != null)
        {
            _defaultMaterial = rendererReference.materials[rendererMaterialIndex];
            _defaultEmmisiveColor = _defaultMaterial.GetColor("_EmissionColor");
        }
        else if (TryGetComponent(out rendererReference))
        {
            _defaultMaterial = rendererReference.materials[rendererMaterialIndex];
            _defaultEmmisiveColor = _defaultMaterial.GetColor("_EmissionColor");
        }
        else
            Debug.LogWarning(this.name + ": " + "Falta la referencia al renderer");
    }



    //Metodo interno
    virtual protected void ChangeMaterial(Material material)
    {
        var _materials = rendererReference.materials;
        _materials[rendererMaterialIndex] = material;
        rendererReference.materials = _materials;
    }
    virtual protected void ChangeColor(Color c)
    {

    }

    //Metodo externo
    public void SetMaterial(Material material)
    {
        ChangeMaterial(material);
    }
    /// <summary>
    /// Ejecuta cuando la entidad entra al dispositivo
    /// </summary>
    /// <param name="selection">0 corresponde al Player, 1 corresponde a la AV</param>
    virtual public void OnEntityEnter(int selection)
    {
        switch (selection)
        {
            case 0:
                ChangeColor(_IAColor);
                break;
            case 1:
                ChangeColor(_AVColor);
                break;
            default:
                Debug.LogWarning("La selección es " + selection + ". Sólo se puede elegir entre 0 y 1");
                break;
        }
    }
    /// <summary>
    /// Ejecuta cuando la entidad entra al dispositivo
    /// </summary>
    virtual public void OnEntityExit()
    {
        
    }

    virtual public void LifeController(float damage)
    {

    }

    virtual public void CheckKeys()
    {

    }

    public void SetDefaultMaterial()
    {
        ChangeMaterial(_defaultMaterial);
    }

    virtual public void HackDevice()
    {
        if (isHackeable)
        {
            hackPanel.SetActive(true);
            hackPanel.GetComponent<HackPanel>().gateToOpen = gateToOpen;
            _defaultMaterial = onPlayerMaterial;
        }

    }

    public void CheckPlayerIsHere()
    {
        if (player == null)
        {
            return;
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 1)
        {
            playerIsHere = true;
        }
        else
        {
            playerIsHere = false;
        }
    }


}