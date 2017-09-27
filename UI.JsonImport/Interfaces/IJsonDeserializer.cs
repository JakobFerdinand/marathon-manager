using System.Collections.Generic;
using Core.Models;

namespace UI.JsonImport.Interfaces
{
    internal interface IJsonDeserializer
    {
        IEnumerable<Runner> Deserialize(string json);
    }
}
