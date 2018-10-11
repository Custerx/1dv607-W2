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

        public BoatClubController() {
            this._boatView = new View.BoatView();
            this._memberView = new View.MemberView();
            this._menuView = new View.MenuView();
            this._memberController = new Controller.MemberController(this._memberView);
            this._boatController = new Controller.BoatController(this._boatView);
        }

        public void Run()
        {          
            int userNavigationChoice = this._menuView.getAuthorizationMenuInput();
            this.AuthorizationNavigation(userNavigationChoice);
        }

        private void AuthorizationNavigation(int userPreviousChoice)
        {
            int userNavigationChoice = 0;
            
            if (userPreviousChoice == 0)
            {
                userNavigationChoice = this._memberController.Authorization();
            }

            if (userPreviousChoice == 1)
            {
                this._memberController.registerMemberOnList();
            }

            if (userPreviousChoice == 2) // Guest
            {
                userNavigationChoice = this._menuView.getGuestMenuInput();
                this.guestNavigation(userNavigationChoice);
            }

            if (userPreviousChoice == 3)
            {
                this._menuView.ExitMessage();
            }

            if (userNavigationChoice == 1) // Authorized user.
            {
                userNavigationChoice = this._menuView.getNavigationMenuInput();
                this.Navigation(userNavigationChoice);
            } else
            {
                this.Run();
            }
        }

        private void Navigation(int userPreviousChoice)
        {
            int userNavigationChoice;
            if (userPreviousChoice == 0)
            {
                userNavigationChoice = this._menuView.getMemberMenuInput();
                this.memberNavigation(userNavigationChoice);
            }

            if (userPreviousChoice == 1)
            {
                userNavigationChoice = this._menuView.getBoatMenuInput();
                this.boatNavigation(userNavigationChoice);
            }

            if (userPreviousChoice == 2)
            {
                userNavigationChoice = this._menuView.getSearchMenuInput();
                this.searchNavigation(userNavigationChoice, false);
            }

            if (userPreviousChoice == 3)
            {
                this._menuView.ExitMessage();
            }

            this.Run(); // Display start menu.
        }

        private void searchNavigation(int userPreviousChoice, bool guest)
        {
            int userNavigationChoice;

            if (userPreviousChoice == 0)
            {
                this._memberController.SearchAndViewMembersByName();
            }

            if (userPreviousChoice == 1)
            {
                this._memberController.SearchAndViewMembersByAge();
            }

            if (userPreviousChoice == 2)
            {
                if(guest)
                {
                    userNavigationChoice = this._menuView.getGuestMenuInput();
                    this.guestNavigation(userNavigationChoice);
                } else
                {
                    userNavigationChoice = this._menuView.getNavigationMenuInput();
                    this.Navigation(userNavigationChoice);
                }
            }

            this.Navigation(2); // Display search-menu.
        }

        private void boatNavigation(int userPreviousChoice)
        {
            int userNavigationChoice;

            if (userPreviousChoice == 0)
            {
                this._boatController.registerBoatOnList();
            }

            if (userPreviousChoice == 1)
            {
                this._boatController.delete_Update_View_BoatFromList("Update");
            }

            if (userPreviousChoice == 2)
            {
                this._boatController.delete_Update_View_BoatFromList("Delete");
            }

            if (userPreviousChoice == 3)
            {
                this._boatController.delete_Update_View_BoatFromList("View");
            }          

            if (userPreviousChoice == 4)
            {
                this._boatController.listBoatClubBoats();
            }

            if (userPreviousChoice == 5)
            {
                userNavigationChoice = this._menuView.getNavigationMenuInput();
                this.Navigation(userNavigationChoice);
            }

            this.Navigation(1); // Display boat-menu.
        }

        private void memberNavigation(int userPreviousChoice)
        {
            int userNavigationChoice;

            if (userPreviousChoice == 0)
            {
                this._memberController.compactList();
            } 
            
            if (userPreviousChoice == 1)
            {
                this._memberController.verboseList();
            }   
            
            if (userPreviousChoice == 2)
            {
                this._memberController.UpdateMemberOnList();
            }   
            
            if (userPreviousChoice == 3)
            {
                this._memberController.DeleteMemberFromList();
            }  
            
            if (userPreviousChoice == 4)
            {
                userNavigationChoice = this._menuView.getNavigationMenuInput();
                this.Navigation(userNavigationChoice);
            }
            
            this.Navigation(0); // Display member-menu.
        }

        private void guestNavigation(int userPreviousChoice)
        {
            int userNavigationChoice;

            if (userPreviousChoice == 0)
            {
                this._memberController.compactList();
            } 
            
            if (userPreviousChoice == 1)
            {
                this._memberController.verboseList();
            }

            if (userPreviousChoice == 2)
            {
                this._boatController.listBoatClubBoats();
            }
            
            if (userPreviousChoice == 3) // Search
            {
                userNavigationChoice = this._menuView.getSearchMenuInput();
                this.searchNavigation(userNavigationChoice, true);
            }   
            
            if (userPreviousChoice == 4)
            {
                this._menuView.ExitMessage();
            }
            
            this.AuthorizationNavigation(2); // Display guest-menu.
        }
    }
}