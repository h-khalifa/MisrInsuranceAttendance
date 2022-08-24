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

        public UnitOfWork(AttendanceEntities CTX,
            IEmployeRepository employeRepository,
            IBaseRepository<Branch> branchRepository,
            IBaseRepository<FingerPrintDevice> devicesRepository,
            IBaseRepository<AttendanceLog> logsRepository
            )
        {
            _ctx = CTX;

            EmployeRepo = employeRepository;
            BranchesRepo = branchRepository;
            DevicesRepo = devicesRepository;
            LogsRepo = logsRepository;


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
