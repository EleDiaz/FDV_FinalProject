using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Dialog", IsNullable = false)]
public class Dialog
{

    [XmlArray("Sections")]
    public Section[] Sections;

    [XmlIgnore]
    private int _currentSection = 0;

    public string GetNextPhrase(string eventName) {
        Statement statement = Sections[_currentSection].TryGetFollowing(eventName);
        return statement != null ? statement.Phrase : null;
    }

    public bool openSection(string name) {
        bool found = false;
        int ix = 0;
        while (!found && ix < Sections.Length)
        {
            if (Sections[ix].Name == name) {
                _currentSection = ix;
                found = true;
            }
        }
        return found;
    }
}

// NOTE: Enumerator, Enumerables or iterator doesn't fill all the functionality. MoveNext(string EventName) 
public class Section : MonoBehaviour
{
    [XmlAttribute]
    public string Name;

    [XmlArray("Statements")]
    public Statement[] Statements;
    
    [XmlIgnore]
    private bool _timeOutEnded = true;
    [XmlIgnore]
    private int _currentIndex = 0;
    [XmlIgnore]
    private bool triggeredEnd = true;

    public delegate void OnTimeOut(Statement statement);
    private event OnTimeOut onTimeOut; 


    private IEnumerator SetTimer(float timeOut) {
        yield return new WaitForSeconds(timeOut);
        _timeOutEnded = true;
        // TODO: Checkout for event queue
        onTimeOut(Statements[_currentIndex]);
    }


    public Statement TryGetFollowing(string eventName) {
        if (_currentIndex >= Statements.Length - 1) {
            return null;
        }

        Statement currentStatement = Statements[_currentIndex];
        Statement statement = Statements[_currentIndex + 1];

        if ((currentStatement.Skippable && eventName == "Skip") 
            || (_timeOutEnded && triggeredEnd && eventName == statement.TriggerStart)
            || (_timeOutEnded && triggeredEnd && eventName == null)) 
        {
            if (currentStatement.TimeOut.HasValue) {
                _timeOutEnded = false;
                StartCoroutine(SetTimer(currentStatement.TimeOut.Value));
            }
            if (currentStatement.TriggerEnd != null) {
                triggeredEnd = false;
            }

            _currentIndex++;
            return statement;
        }
        else if (currentStatement.TriggerEnd == eventName) {
            triggeredEnd = true;
        }

        return null;
    }
}

public class Statement
{
    /// Event to trigger the text
    [XmlAttribute]
    public string TriggerStart;
    /// Event to remove the text
    [XmlAttribute]
    public string TriggerEnd;
    /// Is a skippable text
    [XmlAttribute]
    public bool Skippable;
    /// Time out fade out the text
    [XmlAttribute]
    public float? TimeOut;

    // TODO: We should replace this type with some kind StyledText generator, which parse input string and lookup for translations
    //       and necessary icons.  
    [XmlText]
    public string Phrase;
}