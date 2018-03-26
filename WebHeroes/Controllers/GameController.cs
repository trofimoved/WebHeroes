using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHeroes.Entities;
using WebHeroes.Code;
using WebHeroes.Playground;
using Newtonsoft.Json;

namespace WebHeroes.Controllers
{
    public class GameController : BaseController
    {
        // GET: Game
        public ActionResult Index()
        {
            return View("Index");
        }
        /// <summary>
        /// Пользовательский ввод (действие)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Input(string input)
        {
            var inputKey = new Input().Key(input);
            try
            {
                GameLoop.Turn.ProcessInput(inputKey);
            }
            catch (Exceptions.CustomException ex)
            {
                ViewBag.ErrorMessage = ex.ErrorMessage;
            }
            ViewBag.Turn = GameLoop.Turn;

            return PartialView("_Board", GameLoop.Scene);
        }
        /// <summary>
        /// Пользовательский ввод (позиция на доске)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ActionResult InputPosition(int x, int y)
        {
            Position inputPosition = new Position(x, y);
            string spellName = ActionName;
            if (spellName == null)
            {
                var entity = GameLoop.Scene.Entities.FindByPosition(inputPosition);
                if(entity != null)
                return RedirectToAction("GetStatus", "Status", new { id = entity.Id});
            }
                
            try
            {
                GameLoop.Turn.ProcessInput(ActionName, inputPosition);
            }
            catch (Exceptions.CustomException ex)
            {
                ViewBag.ErrorMessage = ex.ErrorMessage;
            }
            ViewBag.Turn = GameLoop.Turn;

            return PartialView("_Board", GameLoop.Scene);
        }
        /// <summary>
        /// Выбрана ячейка на доске
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public ActionResult ClickCell(int x, int y)
        {
            //если был выбран скилл с действиен по области или в таргет
            return RedirectToAction("InputPosition", new { x = x, y = y });
        }


        public ActionResult SpellBook()
        {
            var entity = GameLoop.Scene.Entities.FirstOrDefault(x => x.Id == GameLoop.Turn.EntityId);
            if (entity == null)
                return null;
            return PartialView("_SpellBook", entity.Actions);
        }

        public JsonResult SelectSpell(string name)
        {
            object data = null;
            var entity = GameLoop.Turn.CurrentEntity;
            if (entity == null)
                return null;

            var action = entity.Actions.FirstOrDefault(x => x.Name == name);
            if (action.NeedTarget)
                data = "NeedTarget";
            else if (action.NeedArea)
                data = "NeedArea";
            ActionName = action.Name;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}