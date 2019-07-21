using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Utf8Json;

[System.Serializable]
public class DynamicTable : Serialize.TableBase<string, string, DynamicPair> {
}

[System.Serializable]
public class DynamicPair: Serialize.KeyAndValue<string, string> {
    public DynamicPair(string key, string value) : base(key, value){}
}
public class ListToDynamic : MonoBehaviour {
    [SerializeField]
    private DynamicTable dynamicTable;
    private Button button;
    void Start() {
        var filepath = $"{Application.dataPath}/type02.json";
        Debug.Log(filepath);
        button = this.gameObject.GetComponent<Button>();
        button.OnClickAsObservable().Subscribe(_ => {
            var table = dynamicTable.GetTable();
            //Serialize.Serialization<string, string> serialization = new Serialize.Serialization<string, string>(table);
            var json02 = JsonSerializer.ToJsonString(table);
            JsonWrite(filePath: filepath, json: json02);
        });
    }
    void JsonWrite(string filePath, string json) {
        using(FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                using(StreamWriter s = new StreamWriter(fs))
                    s.WriteLine(json);
    }
}