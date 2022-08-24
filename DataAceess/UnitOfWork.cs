using DataAceess.Interfaces;
using DataAceess.Models;
using DataAceess.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAceess
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmployeRepository EmployeRepo { get; set; }
        public IBaseRepository<Branch> BranchesRepo { get; set; }
        public IBaseRepository<FingerPrintDevice> DevicesRepo { get; set; }
        public IBaseRepository<AttendanceLog> LogsRepo { get; set; }

        public DbContext _ctx { get; private set; }

        public UnitOfWork(AttendanceEntities CTX)
        {
            _ctx = CTX;

            EmployeRepo = new EmployesRepo();
            BranchesRepo = new BaseRepository<Branch>();
            DevicesRepo = new BaseRepository<FingerPrintDevice>();
            LogsRepo = new BaseRepository<AttendanceLog>();


            EmployeRepo.SetContext(_ctx);
            BranchesRepo.SetContext(_ctx);
            DevicesRepo.SetContext(_ctx);
            LogsRepo.SetContext(_ctx);
        }

        public int Submit()
        {
            return _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
