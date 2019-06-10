//using BergerFlowTrading.Shared.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;

//namespace BergerFlowTrading.BusinessTier.Services.DataServices
//{
//    public class ExchangeCRUDService
//    {
//        private ApplicationDbContext ctxt;

//        public ExchangeCRUDService(ApplicationDbContext ctxt)
//        {
//            this.ctxt = ctxt;
//            this.authz = authz;
//        }

//        public IQueryable<Exchange> Query(string jwt)
//        {
//            if (authz.Authorize(jwt, "Admin") || authz.Authorize(jwt, "BasicPlan"))
//            {
//                return this.ctxt.Exchanges;
//            }
//            else
//            {
//                throw new UnauthorizedAccessException();
//            }
//        }

//        public List<Exchange> GetList(string jwt)
//        {
//            return Query(jwt).ToList();
//        }

//        public List<Exchange> GetList(Expression<Func<Exchange, bool>> predicate, string jwt)
//        {
//            return Query(jwt).Where(predicate).ToList();
//        }

//        public Exchange GetExchange(int id, string jwt)
//        {
//            return Query(jwt).FirstOrDefault(x => x.ID == id);
//        }

//        public Exchange Create(Exchange e, string jwt)
//        {
//            if (authz.Authorize(jwt, "Admin"))
//            {
//                this.ctxt.Exchanges.Add(e);
//                this.ctxt.SaveChanges();

//                return this.GetExchange(e.ID.Value, jwt);
//            }
//            else
//            {
//                throw new UnauthorizedAccessException();
//            }
//        }

//        public Exchange Update(Exchange e, string jwt)
//        {
//            if (authz.Authorize(jwt, "Admin"))
//            {
//                this.ctxt.Exchanges.Attach(e);
//                this.ctxt.Entry(e).State = EntityState.Modified;
//                this.ctxt.SaveChanges();

//                return this.GetExchange(e.ID.Value, jwt);
//            }
//            else
//            {
//                throw new UnauthorizedAccessException();
//            }
//        }

//        public bool Delete(int id, string jwt)
//        {
//            if (authz.Authorize(jwt, "Admin"))
//            {
//                Exchange e = this.GetExchange(id, jwt);
//                this.ctxt.Entry(e).State = EntityState.Deleted;
//                this.ctxt.SaveChanges();
//                return true;
//            }
//            else
//            {
//                throw new UnauthorizedAccessException();
//            }
//        }
//    }
//}
