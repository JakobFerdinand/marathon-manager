using Newtonsoft.Json;
using System.Collections.Generic;
using UI.JsonImport.Interfaces;
using Core.Models;
using AutoMapper;

namespace UI.JsonImport.Services
{
    internal class JsonDeserializer : IJsonDeserializer
    {
        private readonly IMapper _mapper;

        public JsonDeserializer(IMapper mapper) => _mapper = mapper;

        public IEnumerable<Runner> Deserialize(string json)
        {
            var data = JsonConvert.DeserializeObject<IEnumerable<Models.Runner>>(json);

            var runners = _mapper.Map<IEnumerable<Runner>>(data);
            return  runners;
        }
    }
}
