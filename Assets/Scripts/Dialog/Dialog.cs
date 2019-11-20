using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("Dialog", IsNullable = false)]
public class Dialog
{

    [XmlArray("Sections")]
    public Section[] Sections;

    [XmlIgnore]
    public int currentSection = 0;

    [XmlIgnore]
    public int currentPhrase = 0;

    public string GetNextPhrase(string eventName) {
        // TODO improve overall logic on this stuff
        if (Sections.Length <= currentSection) {
            return null;
        }
        Section sec = Sections[currentSection];
        if (sec.Statements.Length <= currentPhrase) {
            return null;
        }
        if (eventName == null || 
            Array.Exists(sec.Statements[currentSection].Dispatcher, 
                dispatcherEvName => eventName == dispatcherEvName))
        {
            return sec.Statements[currentPhrase++].Phrase;
        }
        return null;
    }

    public bool openSection(string name) {
        bool found = false;
        int ix = 0;
        while (!found && ix < Sections.Length)
        {
            if (Sections[ix].Name == name) {
                currentSection = ix;
                currentPhrase = 0;
                found = true;
            }
        }
        return found;
    }
}

public class Section
{
    [XmlAttribute]
    public string Name;

    [XmlArray("Statements")]
    public Statement[] Statements;
}

public class Statement
{
    [XmlAttribute]
    public string[] Dispatcher;

    [XmlText]
    public string Phrase;
}