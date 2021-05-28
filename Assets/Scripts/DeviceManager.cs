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

    private void Awake()
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
    
    //ZIP - tal vez ponerle indices a algo

    //IA2-P2”

    public static List<Device> GetNearMe(Vector3 myPos, float range) //Dada la posición de la IA y un rango de búsqueda, toma los 6 devices más cercanos dentro de un rango, los ordena y los devuelve para pasar como objetivos de la IA.
    {
        List<Tuple<Device, float>> newList = new List<Tuple<Device, float>>();

        for (int i = 0; i < deviceList.Count; i++) {
            var distance = Vector3.Distance(deviceList[i].transform.position, myPos);
            if (distance < range)
            newList.Add(new Tuple<Device, float>(deviceList[i], distance));
        }

        var returnList = newList
            .Where(x => x.Item2 <= range)//filtra los que estan demasiado lejos
            .OrderBy(x => x.Item2).ToList() //ordena por distancia
            .Select(x => x.Item1) //selecciona solo los devices
            .Take(6) //toma solo 6
            .ToList();//lo manda a la lista

        return returnList;
    }

    public static String GetObjectiveNameAndDistance(Vector3 myPos, List<Device> myDevices, Device myDevice)
    {
        List<float> newList = new List<float>();

        for (int i = 0; i < myDevices.Count; i++) {
            var distance = Vector3.Distance(myDevices[i].transform.position, myPos);
            newList.Add(distance);
        }
        
        int index = myDevices.FindIndex(a => a == myDevice);

        var returnString = newList //filtra los que estan demasiado lejos
            .Skip(index - 1)
            .Zip(myDevices, (x, y) => "Going to: " + y.gameObject.name + "\n" + ", distance:" + x)
            .Take(1)
            .FirstOrDefault();

         return returnString;
    }

    public static Device GetNearestDevice(Vector3 myPos, float range)
    {
         List<Tuple<Device, float>> newList = new List<Tuple<Device, float>>();

            for (int i = 0; i < deviceList.Count; i++) {
                var distance = Vector3.Distance(deviceList[i].transform.position, myPos);
                if (distance < range)
                    newList.Add(new Tuple<Device, float>(deviceList[i], distance));
            }

            var returnDevice = newList
                .Where(x => x.Item2 <= range) //filtra los que estan demasiado lejos
                .OrderBy(x => x.Item2).ToList() //ordena por distancia
                .Take(6)
                .Aggregate((a, b) => a.Item2 < b.Item2 ? a : b).Item1;

            return returnDevice;
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
