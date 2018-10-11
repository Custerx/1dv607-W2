using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Controller
{
    public class BoatController : FileController
    {
        private View.BoatView _boatView;

        public BoatController(View.BoatView boatView)
        {
            this._boatView = boatView;
        }

        public void delete_Update_View_BoatFromList(string action) 
        {
            List<Model.Member> memberList = base.LoadMemberList();

            memberList = editBoatDetails(memberList, action);
            base.saveToFile(memberList);
        }
        public void listBoatClubBoats()
        {
            List<Model.Member> memberList = base.LoadMemberList();
            this._boatView.viewAllBoatClubBoats(memberList);
        }
        private void deleteBoat(Model.Member boatOwner)
        {
            this._boatView.viewAllBoats(boatOwner);
            string id = this._boatView.ReadIDInput("Type the ID of the boat you want to delete: ");
            
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
            return;
        }

        private void updateBoatOnList(Model.Member boatOwner)
        {
            this._boatView.viewAllBoats(boatOwner);
            string id = this._boatView.ReadIDInput("Type the ID of the boat you want to update: ");
            
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
            return;
        }

        private void updateBoat(Model.Member boatOwner)
        {
            int boatTypeAsNumber = this._boatView.ReadBoatTypeInput();
            Enums.BoatTypes.Boats boatType = (Enums.BoatTypes.Boats)Convert.ToInt32(boatTypeAsNumber);
            int boatLength = this._boatView.ReadBoatLengthInput();
            boatOwner.Boats.Add(new Model.Boat(boatType, boatLength, base.RandomID()));
            this._boatView.messageForSuccess(boatType + " " + boatLength + "m successfully updated!");
        }
        private List<Model.Member> editBoatDetails(List<Model.Member> viewMemberList, string action)
        {
            string id = this._boatView.ReadIDInput("The boat owner's 6-character ID: ");

            for (int i = 0; i < viewMemberList.Count; i++)
            {
                if (viewMemberList[i].MemberID == id) {
                    this._boatView.messageForSuccess("Member " + viewMemberList[i].Name + " successfully retrieved!");
                    
                    if (action == "Delete")
                    {
                        deleteBoat(viewMemberList[i]);
                    }

                    if (action == "Update")
                    {
                        updateBoatOnList(viewMemberList[i]);
                    }

                    if (action == "View")
                    {
                        this._boatView.viewAllBoats(viewMemberList[i]);
                    }

                    return viewMemberList;
                }
            }

            this._boatView.messageForError("No matching member!");
            return viewMemberList;
        }

        public void registerBoatOnList() 
        {
            string id = this._boatView.ReadIDInput("The boat owner's 6-character ID: ");

            List<Model.Member> MemberList = base.LoadMemberList();

            for (int i = 0; i < MemberList.Count; i++)
            {
                if (MemberList[i].MemberID == id) 
                {
                    int boatTypeAsNumber = this._boatView.ReadBoatTypeInput();
                    Enums.BoatTypes.Boats boatType = (Enums.BoatTypes.Boats)Convert.ToInt32(boatTypeAsNumber);
                    int boatLength = this._boatView.ReadBoatLengthInput();
                    
                    MemberList[i].Boats.Add(new Model.Boat(boatType, boatLength, base.RandomID()));
                    base.saveToFile(MemberList);

                    this._boatView.messageForSuccess(boatType + " " + boatLength + "m successfully added!");

                    return;
                }
            }

            this._boatView.messageForError("No matching member!");
            return;
        }
    }
}