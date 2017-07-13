using Core.Models;
using Data.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SampleData
{
    internal class EmptyChangesFinder : IChangesFinder
    {
        public IEnumerable<ChangeLog> GetChanges(DbContext context)
        {
            return new List<ChangeLog>();
        }
    }
}
