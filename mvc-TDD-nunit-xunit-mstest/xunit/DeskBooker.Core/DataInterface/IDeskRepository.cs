using DeskBooker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeskBooker.Core.DataInterface
{
    public interface IDeskRepository
    {
        IEnumerable<Desk> GetAvailableDesks(DateTime date);
    }
}
