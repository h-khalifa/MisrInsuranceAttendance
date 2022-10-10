using DataAceess.Interfaces;
using DataAceess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAceess.Repositories
{
    public class EmployesRepository : BaseRepository<Employe>, IEmployeRepository
    {
        //public EmployesRepo(DbContext ctx):base(ctx)
        //{
        //    //_ctx = ctx;
        //}
        public EmployesRepository():base()
        {

        }
        
        public Employe GetOrCreateEmployeByCode(string code)
        {
            var employee = _ctx.Set<Employe>().SingleOrDefault(e => e.Code == code);
            if(employee == null)
            {
                employee = new Employe()
                {
                    Code = code,
                };
                _ctx.Set<Employe>().Add(employee);
                _ctx.SaveChanges();
            }
            return employee;
        }
    }
}
