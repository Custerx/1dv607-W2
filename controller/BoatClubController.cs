using System;
using System.IO;

namespace Controller
{
    public class BoatClubController
    {
        private View.MemberView _memberView;
        private View.BoatView _boatView;
        private View.MenuView _menuView;
        private Controller.MemberController _memberController;
        private Controller.BoatController _boatController;

        public MasterController() {
            this._boatView = new View.BoatView();
            this._memberView = new View.MemberView();
            this._menuView = new View.MenuView();
            this._memberController = new Controller.MemberController(this._memberView);
            this._boatController = new Controller.BoatController(this._boatView);
        }

        public void Run()
        {          
            int userNavigationChoice = this._menuView.ReadMenuInput();
            this.Navigation(userNavigationChoice);
        }

        private void Navigation(int userPreviousChoice)
        {
            int userNavigationChoice;
            if (userPreviousChoice == 0)
            {
                userNavigationChoice = this._menuView.ReadMenuMemberInput();
                this.memberNavigation(userNavigationChoice);
            }

            if (userPreviousChoice == 1)
            {
                userNavigationChoice = this._menuView.ReadMenuBoatInput();
                this.boatNavigation(userNavigationChoice);
            }

            if (userPreviousChoice == 2)
            {
                this._memberView.Authorization();
            }

            if (userPreviousChoice == 3)
            {
                this._menuView.ExitMessage();
            }
        }

        private void boatNavigation(int userPreviousChoice)
        {
            if (userPreviousChoice == 0)
            {
                this._memberView.viewAllBoats(this._boatController);
            }

            if (userPreviousChoice == 1)
            {
                this._boatController.SaveBoatList();
            }

            if (userPreviousChoice == 2)
            {
                this._boatController.deleteOrUpdateBoatFromList("Update");
            }

            if (userPreviousChoice == 3)
            {
                this._boatController.deleteOrUpdateBoatFromList("Delete");
            }

            if (userPreviousChoice == 4)
            {
                this.Run(); // Display start menu.
            }

            this.Navigation(1); // Display boat-menu.
        }

        private void memberNavigation(int userPreviousChoice)
        {
            if (userPreviousChoice == 0)
            {
                this._memberView.compactList(this._memberController);
            } 
            
            if (userPreviousChoice == 1)
            {
                this._memberView.verboseList(this._memberController);
            }   
            
            if (userPreviousChoice == 2) // Register member.
            {
                this._memberController.SaveMemberList();
            }   
            
            if (userPreviousChoice == 3)
            {
                this._memberController.UpdateMemberOnList();
            }   
            
            if (userPreviousChoice == 4)
            {
                this._memberController.DeleteMemberFromList();
            }  
            
            if (userPreviousChoice == 5)
            {
                this.Run(); // Display start-menu.
            }
            
            this.Navigation(0); // Display member-menu.
        }
    }
}