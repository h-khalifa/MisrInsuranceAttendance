using DataAceess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAceess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeRepository EmployeRepo { get; }
        IBaseRepository<Branch> BranchesRepo { get; }
        IBaseRepository<FingerPrintDevice> DevicesRepo { get; }
        IBaseRepository<AttendanceLog> LogsRepo { get; }

        int Submit();

        DbContext _ctx { get; }
    }
}
