//using BergerFlowTrading.Shared.Models;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace BergerFlowTrading.BusinessTier.Services.DataServices
//{
//    public class LimitArbitrageStrategy4SettingsCRUDService
//    {
//        private ApplicationDbContext ctxt;
//        private IJWTAuthorizationService authz;

//        public LimitArbitrageStrategy4SettingsCRUDService(ApplicationDbContext ctxt, IJWTAuthorizationService authz)
//        {
//            this.ctxt = ctxt;
//            this.authz = authz;
//        }


//        public IQueryable<LimitArbitrageStrategy4Settings> Query(string jwt)
//        {
//            if (authz.Authorize(jwt, "Admin") || authz.Authorize(jwt, "BasicPlan"))
//            {
//                string uid = authz.GetUserID(jwt);
//                return this.ctxt.LimitArbitrageStrategy4Settings.Include(x => x.Exchange_1).Where(x => x.User_ID == uid);
//            }
//            else
//            {
//                throw new UnauthorizedAccessException();
//            }
//        }

//        public List<LimitArbitrageStrategy4Settings> GetList(string jwt)
//        {
//            return Query(jwt).ToList();
//        }

//        public LimitArbitrageStrategy4Settings Get(int id, string jwt)
//        {
//            return Query(jwt).FirstOrDefault(x => x.ID == id);
//        }

//        public LimitArbitrageStrategy4Settings Create(LimitArbitrageStrategy4Settings e, string jwt)
//        {
//            if (authz.Authorize(jwt, "BasicPlan"))
//            {
//                string uid = authz.GetUserID(jwt);
//                e.User_ID = uid;
//                e.Active = false;
//                e.ManagementBalanceON = false;
//                this.ctxt.LimitArbitrageStrategy4Settings.Add(e);
//                this.ctxt.SaveChanges();

//                return this.Get(e.ID.Value, jwt);
//            }
//            else
//            {
//                throw new UnauthorizedAccessException();
//            }
//        }

//        public LimitArbitrageStrategy4Settings ChangeActive(int? id, string jwt)
//        {
//            LimitArbitrageStrategy4Settings e = this.Get(id.Value, jwt);
//            e.Active = !e.Active;
//            return this.Update(e, jwt);
//        }

//        public LimitArbitrageStrategy4Settings ChangeBalanceON(int? id, string jwt)
//        {
//            LimitArbitrageStrategy4Settings e = this.Get(id.Value, jwt);
//            e.ManagementBalanceON = !e.ManagementBalanceON;
//            return this.Update(e, jwt);
//        }

//        public LimitArbitrageStrategy4Settings Update(LimitArbitrageStrategy4Settings e, string jwt)
//        {
//            if (authz.Authorize(jwt, "BasicPlan"))
//            {
//                string uid = authz.GetUserID(jwt);
//                e.User_ID = uid;
//                this.ctxt.LimitArbitrageStrategy4Settings.Attach(e);
//                this.ctxt.Entry(e).State = EntityState.Modified;
//                this.ctxt.SaveChanges();

//                return this.Get(e.ID.Value, jwt);
//            }
//            else
//            {
//                throw new UnauthorizedAccessException();
//            }
//        }

//        public bool Delete(int id, string jwt)
//        {
//            if (authz.Authorize(jwt, "BasicPlan"))
//            {
//                LimitArbitrageStrategy4Settings e = this.Get(id, jwt);
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
