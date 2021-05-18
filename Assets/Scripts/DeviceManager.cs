using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEditor.Hardware;
using UnityEngine;
using Random = System.Random;

public class DeviceManager : MonoBehaviour
{
    //IA2-P3
    public static DeviceManager instance;
    public static List<Device> deviceList;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        deviceList = GameObject.FindObjectsOfType<Device>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<Device> GetDevices()
    {
        return deviceList;
    }

    public static List<Device> GetNearMe(Vector3 myPos, int range) //Dada la posición de la IA y un rango de búsqueda, toma los 6 devices más cercanos dentro de un rango, los ordena y los devuelve para pasar como objetivos de la IA.
    {
        List<Tuple<Device, float>> newList = new List<Tuple<Device, float>>();

        for (int i = 0; i < deviceList.Count; i++)
        {
            var distance = Vector3.Distance(deviceList[i].transform.position, myPos);
            if (distance < range)
            newList.Add(new Tuple<Device, float>(deviceList[i], distance));
        }

        var returnList = newList
            .OrderBy(x => x.Item2).ToList() //ordena por distancia
            .Select(x => x.Item1) //selecciona solo los devices
            .Take(6) //toma solo 6
            .ToList();

        return returnList;
    }

    public static List<Device> GetDeviceOnPlayer()
    {
        List<Device> returnList = new List<Device>();

        returnList = deviceList.SkipWhile(x => x.playerIsHere == false).ToList();

        return returnList;
    }

    public static List<Device> GetPcDevices()
    {
        List<Device> returnList = new List<Device>();
        
        returnList = deviceList
            .Where(x => x.gameObject.GetComponent<PCDevice>())
            .Take(10)
            .ToList();

        return returnList;
    }
    
    
}
