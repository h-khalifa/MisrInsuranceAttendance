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
    public class EmployesRepo : BaseRepository<Employe>, IEmployeRepository
    {
        //public EmployesRepo(DbContext ctx):base(ctx)
        //{
        //    //_ctx = ctx;
        //}
        public EmployesRepo():base()
        {

        }
        
        public Employe GetEmployeByCode(string code)
        {
            return _ctx.Set<Employe>().SingleOrDefault(e => e.Code == code);
        }
    }
}
