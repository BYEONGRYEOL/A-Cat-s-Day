using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;

namespace Isometric.Data 
{
    public class JsonSaver
    {
        private static readonly string filename = "Isometric.sav";

        public static string GetSaveFilename()
        {
            return Application.persistentDataPath + "/" + filename;
        }

        public void Save(SaveData data)
        {
            String hashValue = String.Empty;

            string json = JsonUtility.ToJson(data);
            
            hashValue = GetSHA256(json);
            json = JsonUtility.ToJson(data);
            
            string saveFilename = GetSaveFilename();
            FileStream filestream = new FileStream(saveFilename, FileMode.Create);
            Debug.Log(saveFilename);
            using (StreamWriter writer = new StreamWriter(filestream))
            {
                writer.Write(json);
            }
        }

        public bool Load(SaveData data)
        {
            string loadFilename = GetSaveFilename();
            if (File.Exists(loadFilename))
            {
                using(StreamReader reader = new StreamReader(loadFilename))
                {
                    string json = reader.ReadToEnd();

                    // chech hash before reading

                    if (CheckData(json))
                    {
                        JsonUtility.FromJsonOverwrite(json, data);
                    }
                    else
                    {
                        Debug.Log("Scirpt JsonSaver Function Load : Invalid hashcode");
                    }
                }
                return true;
            }
            return false;
        }
        public void Delete()
        {
            File.Delete(GetSaveFilename());
        }

        private bool CheckData(string json)
        {
            SaveData tempSaveData = new SaveData();
            string oldHash = GetSHA256(json);
            
            JsonUtility.FromJsonOverwrite(json, tempSaveData);
            string tempJson = JsonUtility.ToJson(tempSaveData);
            string newHash = GetSHA256(tempJson);
            
            return (oldHash == newHash);
            
        }
        public string GetHexStringFromHash(byte[] hash)
        {
            string hexString = String.Empty;
            
            foreach(byte b in hash)
            {
                hexString += b.ToString("x2");
            }
            return hexString;
        }

        private string GetSHA256(string text)
        {
            byte[] texttoBytes = Encoding.UTF8.GetBytes(text);
            
            SHA256Managed mySHA256 = new SHA256Managed();

            byte[] hashValue = mySHA256.ComputeHash(texttoBytes);
            

            return GetHexStringFromHash(hashValue);
        }

        
    }

}

