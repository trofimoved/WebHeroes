using System.Collections.Generic;
using WebHeroes.Entities;
using WebHeroes.Playground;

namespace WebHeroes.Code
{
    /// <summary>
    /// Менеджер текущего состояния игры
    /// </summary>
    public class GameLoop
    {
        private List<Entity> _entities { get { return _scene.Entities; } }
        private Scene _scene;
        private Turn _turn;
        private int _currentIndex;

        public Turn Turn { get { return _turn; } }
        public Scene Scene { get { return _scene; } set { _scene = value; } }

        public void StartLoop()
        {
            _currentIndex = -1;
            if (_entities.Count > 0)
                NextTurn();
        }

        public void NextTurn()
        {
            int nextIndex = _currentIndex == _entities.Count - 1 ? 0 : _currentIndex + 1;
            _turn.TurnTo(_entities[nextIndex]);
            _currentIndex = nextIndex;
            _turn.Start();
        }

        public GameLoop()
        {
            _turn = new Turn(this);
            _scene = new Scene();
        }
    }
}