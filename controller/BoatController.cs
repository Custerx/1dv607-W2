using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Controller
{
    public class BoatController : Model.Database
    {
        private View.BoatView _boatView;
        private Model.CreateBoat _createBoat;

        public enum Alternatives
        {
            Delete,
            Update,
            View
        }

        public BoatController(View.BoatView boatView)
        {
            this._createBoat = new Model.CreateBoat();
            this._boatView = boatView;
        }

        public void listBoatClubBoats()
        {
            List<Model.Member> memberList = base.LoadMemberList();
            this._boatView.viewAllBoatClubBoats(memberList);
        }

        public void registerBoatOnList() 
        {
            string id = this._boatView.getIDInput("The boat owner's 6-character ID: ");

            List<Model.Member> MemberList = base.LoadMemberList();

            for (int i = 0; i < MemberList.Count; i++)
            {
                if (MemberList[i].MemberID == id) 
                {
                    int boatTypeAsNumber = this._boatView.getBoatTypeInput();
                    Enums.BoatTypes.Boats boatType = (Enums.BoatTypes.Boats)Convert.ToInt32(boatTypeAsNumber);
                    int boatLength = this._boatView.getBoatLengthInput();
                    
                    Model.Boat boat = this._createBoat.create(boatType, boatLength);
                    MemberList[i].Boats.Add(boat);
                    base.saveToFile(MemberList);

                    this._boatView.messageForSuccess(boatType + " " + boatLength + "m successfully added!");

                    return;
                }
            }

            this._boatView.messageForError("No matching member!");
        }

        public void delete_Update_View_BoatFromList(Alternatives action) 
        {
            List<Model.Member> memberList = base.LoadMemberList();

            memberList = delete_Update_View_BoatDetails(memberList, action);
            base.saveToFile(memberList);
        }

        private List<Model.Member> delete_Update_View_BoatDetails(List<Model.Member> viewMemberList, Alternatives action)
        {
            string id = this._boatView.getIDInput("The boat owner's 6-character ID: ");

            for (int i = 0; i < viewMemberList.Count; i++)
            {
                if (viewMemberList[i].MemberID == id) {
                    this._boatView.messageForSuccess("Member " + viewMemberList[i].Name + " successfully retrieved!");
                    
                    if (action == Alternatives.Delete)
                    {
                        this.deleteBoat(viewMemberList[i]);
                    }

                    if (action == Alternatives.Update)
                    {
                        this.updateBoatOnList(viewMemberList[i]);
                    }

                    if (action == Alternatives.View)
                    {
                        this._boatView.viewMemberBoats(viewMemberList[i]);
                    }

                    return viewMemberList;
                }
            }

            this._boatView.messageForError("No matching member!");

            return viewMemberList;
        }

        private void deleteBoat(Model.Member boatOwner)
        {
            this._boatView.viewMemberBoats(boatOwner);
            string id = this._boatView.getIDInput("Type the ID of the boat you want to delete: ");
            
            for (int i = 0; i < boatOwner.Boats.Count; i++)
            {
                if (boatOwner.Boats[i].BoatID == id) 
                {
                    this._boatView.messageForSuccess("Successfully deleted boat: " + boatOwner.Boats[i].BoatType + " length: " + boatOwner.Boats[i].Length + "m id: " + boatOwner.Boats[i].BoatID);
                    boatOwner.Boats.RemoveAt(i);
                    return;
                }
            }

            this._boatView.messageForError("No matching boat!");
        }

        private void updateBoatOnList(Model.Member boatOwner)
        {
            this._boatView.viewMemberBoats(boatOwner);
            string id = this._boatView.getIDInput("Type the ID of the boat you want to update: ");
            
            for (int i = 0; i < boatOwner.Boats.Count; i++)
            {
                if (boatOwner.Boats[i].BoatID == id) 
                {
                    boatOwner.Boats.RemoveAt(i);
                    this.updateBoat(boatOwner);
                    return;
                }
            }

            this._boatView.messageForError("No matching boat!");
        }

        private void updateBoat(Model.Member boatOwner)
        {
            int boatTypeAsNumber = this._boatView.getBoatTypeInput();
            Enums.BoatTypes.Boats boatType = (Enums.BoatTypes.Boats)Convert.ToInt32(boatTypeAsNumber);
            int boatLength = this._boatView.getBoatLengthInput();
            
            Model.Boat boat = this._createBoat.create(boatType, boatLength);
            boatOwner.Boats.Add(boat);

            this._boatView.messageForSuccess(boatType + " " + boatLength + "m successfully updated!");
        }
    }
}