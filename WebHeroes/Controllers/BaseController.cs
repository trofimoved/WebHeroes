using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHeroes.Entities;
using WebHeroes.Code;


namespace WebHeroes.Controllers
{
    public class BaseController : Controller
    {
        //private const string _mainSceneKey = "__mainScene";
        private const string _playerKey = "__player";

        private const string _gameLoopKey = "__gameLoop";
        private const string _selectedActionKey = "__selectedAction";


        protected GameLoop GameLoop { get { return GetGameLoop(); } set { SaveGameLoop(value); } }
        protected GameLoop GetGameLoop()
        {
            var gameLoop = new GameLoop();
            var sessionGameLoop = Session[_gameLoopKey] as GameLoop;
            if (sessionGameLoop != null)
            {
                gameLoop = sessionGameLoop;
            }
            else
            {
                gameLoop.Scene.CreateScene();
                gameLoop.StartLoop();
                SaveGameLoop(gameLoop);
            }
            return gameLoop;
        }
        protected void SaveGameLoop(GameLoop gameLoop)
        {
            Session[_gameLoopKey] = gameLoop;
        }

        protected string ActionName { get { return GetActionName(); } set { SaveActionName(value); } }
        protected string GetActionName()
        {
            return Session[_selectedActionKey] as string;
        }
        protected void SaveActionName(string name)
        {
            Session[_selectedActionKey] = name;
        }

        //protected Player GetPlayer()
        //{
        //    var player = new Player();
        //    var sessionPlayer = Session[_playerKey] as Player;
        //    if (sessionPlayer != null)
        //        player = sessionPlayer;

        //    return player;
        //}
    }
}