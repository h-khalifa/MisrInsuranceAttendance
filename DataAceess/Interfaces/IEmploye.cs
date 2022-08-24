using DataAceess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAceess.Interfaces
{
    public interface IEmployeRepository : IBaseRepository<Employe>
    {
        Employe GetEmployeByCode(string code);
    }
}
