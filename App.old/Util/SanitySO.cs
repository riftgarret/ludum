using System;
using UnityEngine;

namespace App.Util
{
    public abstract class SanitySO : ScriptableObject
    {
        void OnEnable()
        {
            SanityCheck();
        }

        protected abstract void SanityCheck();

        protected void LogEmptyArray<T>(string propertyName, T[] data) where T : class
        {
            if (data == null)
            {
                LogNull(propertyName);
            }
            else if (data.Length == 0)
            {
                LogMessage(propertyName, "0 elements");
            }
            else
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] == null)
                    {
                        LogMessage(propertyName, "Null at [" + i + "]");
                    }
                }
            }
        }

        protected void LogNull(string propertyName)
        {
            LogMessage(propertyName, "NULL property");
        }

        protected void LogInvalidEnum(string propertyName)
        {
            LogMessage(propertyName, "Invalid ENUM");
        }

        protected void LogMessage(string propertyName, string message)
        {
            Debug.Log(string.Format("Sanity ({0}): {1} [{2}]", this.name, message, propertyName));
        }
    } 
}


