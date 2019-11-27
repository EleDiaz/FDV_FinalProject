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

    public Section.StatementInfo GetNextPhrase(string eventName) {
        return Sections[_currentSection].TryGetFollowing(eventName);
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
public class Section
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

    // This allows to return the statement and a function to setup in some cases. 
    public class StatementInfo {
        public Statement statement;
        private Section _section;

        public StatementInfo(Section self) {
            _section = self;
        }
        
        public void TimeOutEnded() {
            _section._timeOutEnded = true;
        }
    }

    public StatementInfo TryGetFollowing(string eventName) {
        if (_currentIndex >= Statements.Length - 1) {
            return null;
        }
        StatementInfo statementInfo = new StatementInfo(this);

        Statement currentStatement = Statements[_currentIndex];
        Statement statement = Statements[_currentIndex + 1];

        if ((currentStatement.Skippable && eventName == "Skip") 
            || (_timeOutEnded && triggeredEnd && eventName == statement.TriggerStart)
            || (_timeOutEnded && triggeredEnd && eventName == null)) 
        {
            statementInfo.statement = statement;
            if (currentStatement.TimeOut != 0) {
                _timeOutEnded = false;
            }
            if (currentStatement.TriggerEnd != null) {
                triggeredEnd = false;
            }

            _currentIndex++;
            return statementInfo;
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
    public float TimeOut;

    // TODO: We should replace this type with some kind StyledText generator, which parse input string and lookup for translations
    //       and necessary icons.  
    [XmlText]
    public string Phrase;
}