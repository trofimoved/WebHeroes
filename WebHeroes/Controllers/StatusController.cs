using System.Linq;
using System.Web.Mvc;

namespace WebHeroes.Controllers
{
    public class StatusController : BaseController
    {
        public ActionResult GetStatus(int? id)
        {
            var entity = GameLoop.Scene.Entities.FirstOrDefault(x => x.Id == id.Value);
            if (entity == null)
                return null;
            return PartialView("_Status", entity.Status);
        }
    }
}