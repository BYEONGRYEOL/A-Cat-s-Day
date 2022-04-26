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
        //������ ���� �̸�
        private static readonly string filename = "Isometric.sav";
        //savefile�� �̸��� �����ϴ� �Լ�
        public static string GetSaveFilename()
        {
            
            return Application.persistentDataPath + "/" + filename;
        }

        public void Save(SaveData data)
        {
            //�ؽ��� ����
            String hashValue = String.Empty;
            //savedata�� �������� �Ҵ�� ������ jsonutility�� ���� json���� �޴´�.
            string json = JsonUtility.ToJson(data);
            // hashvalue�� json string ���� SHA256 �ڵ带 �Ҵ�
            hashValue = GetSHA256(json);
            // 
            json = JsonUtility.ToJson(data);
            
            string saveFilename = GetSaveFilename();
            //SAVEFILE ����
            FileStream filestream = new FileStream(saveFilename, FileMode.Create);
            Debug.Log(saveFilename);
            //������ SaveFile�� saveData�� �ۼ��ϴ� StreamWriter�� ����, ������ json�����ͷ� ������ json ���ڿ��� ����. (json ���ڿ��� Dictionary ������ string �������� �����Ͱ� �����)
            using (StreamWriter writer = new StreamWriter(filestream))
            {
                writer.Write(json);
            }
        }
        // ������ �ε�
        public bool Load(SaveData data)
        {
            //Isometric.sav ������ loadfilename���� �޾Ƽ�
            string loadFilename = GetSaveFilename();
            //�ش� ������ �̹� �����Ѵٸ�, �� ���̺��� ���� �ִٸ�.
            if (File.Exists(loadFilename))
            {
                //StreamReader�� �̿��Ͽ� �ش� ������ ������ �о� json ���ڿ��� �޾ƿ´�.
                using(StreamReader reader = new StreamReader(loadFilename))
                {
                    string json = reader.ReadToEnd();

                    // chech hash before reading
                    // �������� hash���� üũ�Ͽ� ������ ���ٸ�,
                   
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
        //������ ���� ���� �ڵ�
        public void Delete()
        {
            File.Delete(GetSaveFilename());
        }
        //�������� SHA256 �ڵ� �񱳸� ���� hash���� ������ �������� check
        private bool CheckData(string json)
        {
            //���� �ε��� �����Ϳ� 
            SaveData tempSaveData = new SaveData();
            string oldHash = GetSHA256(json);
            //�ε��� �����͸� �ٽñ� SHA256�ڵ�� �ؽ�ȭ �� ����� �ؽ��ڵ带 ������ ��,
            JsonUtility.FromJsonOverwrite(json, tempSaveData);
            string tempJson = JsonUtility.ToJson(tempSaveData);
            string newHash = GetSHA256(tempJson);
            // �� ���� �ؽ��ڵ尡 ���ƾ� �Ѵ�.
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

