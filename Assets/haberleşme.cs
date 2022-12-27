using UnityEngine;
using System.IO;  
using System.Runtime.Serialization.Formatters.Binary;

//oyucuya at public int verii;
//start
//  verii = veri.verii;

public static class haberleşme
{
    static string fileName = "C:\\Users\\EXCALİBUR\\Desktop\\Logs.bin";

    public static void save(){
        string veriler = veri.x.ToString() + " " + veri.z.ToString() + " " + veri.haritalama.ToString() + " " + veri.haritaX.ToString() + " " + veri.haritaY.ToString(); 

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
        //File.AppendAllText(fileName, veri.verii.ToString());
        formatter.Serialize(fs, veriler);
        fs.Close();
    }
    public static void save2(){
        string veriler = veri.x.ToString() + " " + veri.z.ToString() + " " + veri.haritalama.ToString() + " " + veri.haritaX.ToString() + " " + veri.haritaY.ToString(); 

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Write);
        //File.AppendAllText(fileName, veri.verii.ToString());
        formatter.Serialize(fs, veriler);
        fs.Close();
    }

    public static void load(){
        if(File.Exists(fileName)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(fileName, FileMode.Open);
            string veriler = formatter.Deserialize(fs).ToString();
            

            string[] words = veriler.Split(' ');

            Debug.Log(words);

            fs.Close(); 
        }
        else{
            Debug.Log("dosya yok");
        }
        
    }

}

public class veri{
    public static int verii;
    public static float x;
    public static float z;
    public static int haritalama;
    public static float haritaX;
    public static float haritaY;
}