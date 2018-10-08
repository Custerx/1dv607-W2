using System;
using System.IO;

namespace Controller
{
    public class MasterController
    {
        private View.MemberView _memberView;
        private View.BoatView _boatView;
        private View.MenuView _menuView;
        private Controller.MemberController _memberController;
        private Controller.BoatController _boatController;

        public MasterController() {
            this._memberView = new View.MemberView();
            this._boatView = new View.BoatView();
            this._menuView = new View.MenuView();
            this._memberController = new Controller.MemberController(this._memberView);
            this._boatController = new Controller.BoatController(this._boatView, this._memberView, this._memberController);
        }

        public void Run()
        {          
            this._menuView.ReadMenuInput(this._memberView, this._memberController, this._boatView, this._boatController);
        }
    }
}