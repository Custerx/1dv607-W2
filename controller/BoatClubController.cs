using System;
using System.IO;

namespace Controller
{
    public class BoatClubController
    {
        private View.MenuView _menuView;
        private Controller.MemberController _memberController;
        private Controller.BoatController _boatController;

        public BoatClubController() {
            this._menuView = new View.MenuView();
            this._memberController = new Controller.MemberController();
            this._boatController = new Controller.BoatController();
        }

        public void run()
        {          
            View.MenuView.MenuChoice userNavigationChoice = this._menuView.getAuthorizationMenuInput();
            this.authorizationNavigation(userNavigationChoice);
        }

        private void authorizationNavigation(View.MenuView.MenuChoice userPreviousChoice)
        {
            Controller.MemberController.Login userNavigationChoice = Controller.MemberController.Login.Invalid;
            View.MenuView.GuestChoice userGuestNavigationChoice = View.MenuView.GuestChoice.Invalid;
            View.MenuView.StartMenuChoice userStartNavigationChoice = View.MenuView.StartMenuChoice.Invalid;
            
            if (userPreviousChoice == View.MenuView.MenuChoice.Login)
            {
                userNavigationChoice = this._memberController.authorization();
            }

            if (userPreviousChoice == View.MenuView.MenuChoice.Register)
            {
                this._memberController.registerMemberOnList();
            }

            if (userPreviousChoice == View.MenuView.MenuChoice.Guest)
            {
                userGuestNavigationChoice = this._menuView.getGuestMenuInput();
                this.guestNavigation(userGuestNavigationChoice);
            }

            if (userPreviousChoice == View.MenuView.MenuChoice.Exit)
            {
                this._menuView.ExitMessage();
            }

            if (userNavigationChoice == Controller.MemberController.Login.Success)
            {
                userStartNavigationChoice = this._menuView.getNavigationMenuInput();
                this.navigation(userStartNavigationChoice);
            } else
            {
                this.run();
            }
        }

        private void navigation(View.MenuView.StartMenuChoice userPreviousChoice)
        {
            View.MenuView.BoatMenuChoice userBoatNavigationChoice = View.MenuView.BoatMenuChoice.Invalid;
            View.MenuView.SearchMenuChoice userSearchNavigationChoice = View.MenuView.SearchMenuChoice.Invalid;
            View.MenuView.MemberMenuChoice userMemberNavigationChoice = View.MenuView.MemberMenuChoice.Invalid;

            if (userPreviousChoice == View.MenuView.StartMenuChoice.Member)
            {
                userMemberNavigationChoice = this._menuView.getMemberMenuInput();
                this.memberNavigation(userMemberNavigationChoice);
            }

            if (userPreviousChoice == View.MenuView.StartMenuChoice.Boat)
            {
                userBoatNavigationChoice = this._menuView.getBoatMenuInput();
                this.boatNavigation(userBoatNavigationChoice);
            }

            if (userPreviousChoice == View.MenuView.StartMenuChoice.Search)
            {
                userSearchNavigationChoice = this._menuView.getSearchMenuInput();
                this.searchNavigation(userSearchNavigationChoice, false);
            }

            if (userPreviousChoice == View.MenuView.StartMenuChoice.Exit)
            {
                this._menuView.ExitMessage();
            }

            this.run(); // Display start menu.
        }

        private void searchNavigation(View.MenuView.SearchMenuChoice userPreviousChoice, bool guest)
        {
            View.MenuView.StartMenuChoice userNavigationChoice = View.MenuView.StartMenuChoice.Invalid;
            View.MenuView.GuestChoice userGuestNavigationChoice = View.MenuView.GuestChoice.Invalid;

            if (userPreviousChoice == View.MenuView.SearchMenuChoice.Username)
            {
                this._memberController.searchAndViewMembersByName(false);
            }

            if (userPreviousChoice == View.MenuView.SearchMenuChoice.Age)
            {
                this._memberController.searchAndViewMembersByAge();
            }

            if (userPreviousChoice == View.MenuView.SearchMenuChoice.UsernameBoatType)
            {
                this._memberController.searchAndViewMembersByNameBoatType();
            }

            if (userPreviousChoice == View.MenuView.SearchMenuChoice.Back)
            {
                if(guest)
                {
                    userGuestNavigationChoice = this._menuView.getGuestMenuInput();
                    this.guestNavigation(userGuestNavigationChoice);
                } else
                {
                    userNavigationChoice = this._menuView.getNavigationMenuInput();
                    this.navigation(userNavigationChoice);
                }
            }

            this.navigation(View.MenuView.StartMenuChoice.Search);
        }

        private void boatNavigation(View.MenuView.BoatMenuChoice userPreviousChoice)
        {
            View.MenuView.StartMenuChoice userNavigationChoice = View.MenuView.StartMenuChoice.Invalid;

            if (userPreviousChoice == View.MenuView.BoatMenuChoice.Register)
            {
                this._boatController.registerBoatOnList();
            }

            if (userPreviousChoice == View.MenuView.BoatMenuChoice.Update)
            {
                this._boatController.delete_Update_View_BoatFromList(Controller.BoatController.Alternatives.Update);
            }

            if (userPreviousChoice == View.MenuView.BoatMenuChoice.Delete)
            {
                this._boatController.delete_Update_View_BoatFromList(Controller.BoatController.Alternatives.Delete);
            }

            if (userPreviousChoice == View.MenuView.BoatMenuChoice.View)
            {
                this._boatController.delete_Update_View_BoatFromList(Controller.BoatController.Alternatives.View);
            }          

            if (userPreviousChoice == View.MenuView.BoatMenuChoice.ClubsBoatlist)
            {
                this._boatController.listBoatClubBoats();
            }

            if (userPreviousChoice == View.MenuView.BoatMenuChoice.Back)
            {
                userNavigationChoice = this._menuView.getNavigationMenuInput();
                this.navigation(userNavigationChoice);
            }

            this.navigation(View.MenuView.StartMenuChoice.Boat);
        }

        private void memberNavigation(View.MenuView.MemberMenuChoice userPreviousChoice)
        {
            View.MenuView.StartMenuChoice userNavigationChoice = View.MenuView.StartMenuChoice.Invalid;

            if (userPreviousChoice == View.MenuView.MemberMenuChoice.Compactlist)
            {
                this._memberController.compactList();
            } 
            
            if (userPreviousChoice == View.MenuView.MemberMenuChoice.Verboselist)
            {
                this._memberController.verboseList();
            }   
            
            if (userPreviousChoice == View.MenuView.MemberMenuChoice.Update)
            {
                this._memberController.updateMemberOnList();
            }   
            
            if (userPreviousChoice == View.MenuView.MemberMenuChoice.Delete)
            {
                this._memberController.deleteMemberFromList();
            }  
            
            if (userPreviousChoice == View.MenuView.MemberMenuChoice.Back)
            {
                userNavigationChoice = this._menuView.getNavigationMenuInput();
                this.navigation(userNavigationChoice);
            }
            
            this.navigation(View.MenuView.StartMenuChoice.Member);
        }

        private void guestNavigation(View.MenuView.GuestChoice userPreviousChoice)
        {
            View.MenuView.SearchMenuChoice userSearchNavigationChoice = View.MenuView.SearchMenuChoice.Invalid;

            if (userPreviousChoice == View.MenuView.GuestChoice.Compactlist)
            {
                this._memberController.compactList();
            } 
            
            if (userPreviousChoice == View.MenuView.GuestChoice.Verboselist)
            {
                this._memberController.verboseList();
            }

            if (userPreviousChoice == View.MenuView.GuestChoice.ClubsBoatlist)
            {
                this._boatController.listBoatClubBoats();
            }
            
            if (userPreviousChoice == View.MenuView.GuestChoice.Search)
            {
                userSearchNavigationChoice = this._menuView.getSearchMenuInput();
                this.searchNavigation(userSearchNavigationChoice, true);
            }   
            
            if (userPreviousChoice == View.MenuView.GuestChoice.Exit)
            {
                this._menuView.ExitMessage();
            }
            
            this.authorizationNavigation(View.MenuView.MenuChoice.Guest); // Display guest-menu.
        }
    }
}