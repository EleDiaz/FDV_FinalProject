using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text info;
    public Button skipButton;

    public TextAsset dialogFile;
    private Dialog dialog;

    public delegate string OnNext(string eventName);
    public event OnNext onNext;

    void Awake() {
        ReadDialogFile();
    }

    void Start()
    {
        skipButton.onClick.AddListener(SkipInfo);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SkipInfo() {
        info.text = dialog.GetNextPhrase(null);
    }

    private void ReadDialogFile() {
        XmlSerializer serializer = new XmlSerializer(typeof(Dialog));
        serializer.UnknownNode+= new XmlNodeEventHandler(
            (object sender, XmlNodeEventArgs e) => 
                Debug.Log("Unknown Node: " + e.Name + "\t" + e.Text)
        );

        serializer.UnknownAttribute+= new XmlAttributeEventHandler(
            (object sender, XmlAttributeEventArgs e) => {
                XmlAttribute attr = e.Attr;
                Debug.Log("Unknown Attribute: " + attr.Name + " = " + attr.Value);
            }
        );

        XmlReaderSettings settings = new XmlReaderSettings();
        settings.IgnoreComments = true;
        dialog = (Dialog) serializer.
            Deserialize(XmlReader.Create(new StringReader(dialogFile.text), settings));
    }
}
