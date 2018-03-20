using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebHeroes.Controllers
{
    public class InventoryController : BaseController
    {
        public ActionResult GetInventory(int? id)
        {
            var entity = GameLoop.Scene.Entities.FirstOrDefault(x => x.Id == id.Value);
            if (entity == null)
                return null;
            return PartialView("_Inventory", entity.Inventory);
        }
    }
}