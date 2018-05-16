using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.NETMVCCRUD.Models;
using System.Data.Entity;

namespace Asp.NETMVCCRUD.Controllers
{
    public class PlayerController : Controller
    {
        // GET: /Player/
        public ActionResult Index()
        {
            return View();
        }

        //GetData
        public ActionResult GetData()
        {
            using (DBModel2 db = new DBModel2())
            {
                List<Player> playList = db.Players.ToList<Player>();
                return Json(new { data = playList }, JsonRequestBehavior.AllowGet);
            }
        }

        //AddOrEdit
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Player());
            else
            {
                using (DBModel2 db = new DBModel2())
                {
                    return View(db.Players.Where(x => x.PlayerID==id).FirstOrDefault<Player>());
                }
            }
        }

        //AddOrEdit 
        [HttpPost]
        public ActionResult AddOrEdit(Player play)
        {
            using (DBModel2 db = new DBModel2())
            {
                if (play.PlayerID == 0)
                {
                    db.Players.Add(play);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    db.Entry(play).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        //Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel2 db = new DBModel2())
            {
                Player emp = db.Players.Where(x => x.PlayerID == id).FirstOrDefault<Player>();
                db.Players.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}