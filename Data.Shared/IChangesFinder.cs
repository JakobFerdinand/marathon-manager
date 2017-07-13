using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data.Shared
{
    public interface IChangesFinder
    {
        IEnumerable<ChangeLog> GetChanges(DbContext context);
    }
}