using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Controller
{
    public class BoatController
    {
        private View.BoatView _boatView;
        private View.MemberView _memberView;
        private Controller.MemberController _memberController;
        public BoatController(View.BoatView boatView, View.MemberView memberView, Controller.MemberController memberController)
        {
            this._boatView = boatView;
            this._memberView = memberView;
            this._memberController = memberController;
        }

        public void deleteOrUpdateBoatFromList(string action) 
        {
            List<Model.Member> viewMemberList = this._memberController.LoadMemberList();

            viewMemberList = editMemberDetails(viewMemberList, action);

            var json = JsonConvert.SerializeObject(viewMemberList, Formatting.Indented);
            File.WriteAllText(this._memberController.filePath(), json);
        }
        private Model.Boat deleteBoat(Model.Member boatOwner)
        {
            string id = this._boatView.ReadBoatIDInput("Type the ID of the boat you want to delete: ");
            
            for (int i = 0; i < boatOwner.Boats.Count; i++)
            {
                if (boatOwner.Boats[i].BoatID == id) 
                {
                    this._boatView.messageForSuccess("Successfully deleted boat: " + boatOwner.Boats[i].BoatType + " length: " + boatOwner.Boats[i].Length + "m id: " + boatOwner.Boats[i].BoatID);
                    boatOwner.Boats.RemoveAt(i);
                } else {
                    if (i == boatOwner.Boats.Count) 
                    {
                        this._boatView.messageForError("No matching boat!");
                    }
                }
            }
            return null;
        }

        private Model.Boat updateBoat(Model.Member boatOwner)
        {
            string id = this._boatView.ReadBoatIDInput("Type the ID of the boat you want to update: ");
            
            for (int i = 0; i < boatOwner.Boats.Count; i++)
            {
                if (boatOwner.Boats[i].BoatID == id) 
                {
                    boatOwner.Boats.RemoveAt(i);
                    this.addUpdateBoat(boatOwner);
                } else {
                    if (i == boatOwner.Boats.Count) 
                    {
                        this._boatView.messageForError("No matching boat!");
                    }
                }
            }
            return null;
        }

        private Model.Member addUpdateBoat(Model.Member boatOwner)
        {
            int boatTypeAsNumber = this._boatView.ReadBoatTypeInput();
            Enums.BoatTypes.Boats boatType = (Enums.BoatTypes.Boats)Convert.ToInt32(boatTypeAsNumber);
            int boatLength = this._boatView.ReadBoatLengthInput();
            boatOwner.Boats.Add(new Model.Boat(boatType, boatLength, this._memberController.RandomID()));
            this._boatView.messageForSuccess(boatType + " " + boatLength + "m successfully updated!");
            return boatOwner;
        }
        private List<Model.Member> editMemberDetails(List<Model.Member> viewMemberList, string action)
        {
            string id = this._memberView.ReadMemberIDInput("The boat owner's 6-character ID: ");

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
                        updateBoat(viewMemberList[i]);
                    }

                    return viewMemberList;
                } else {
                    if (i+1 == viewMemberList.Count && viewMemberList.Count != 1) {
                        this._boatView.messageForError("No matching member!");
                        return viewMemberList;
                    }
                }
            }
            return viewMemberList;
        }

        public void SaveBoatList() 
        {
            string id = this._memberView.ReadMemberIDInput("Your 6-character ID: ");
            int boatTypeAsNumber = this._boatView.ReadBoatTypeInput();
            Enums.BoatTypes.Boats boatType = (Enums.BoatTypes.Boats)Convert.ToInt32(boatTypeAsNumber);
            int boatLength = this._boatView.ReadBoatLengthInput();

            List<Model.Member> MemberList = this._memberController.fileExists(this._memberController.filePath()) == true ? this._memberController.LoadMemberList() : new List<Model.Member>();

            for (int i = 0; i < MemberList.Count; i++)
            {
                if (MemberList[i].MemberID == id) 
                {
                    MemberList[i].Boats.Add(new Model.Boat(boatType, boatLength, this._memberController.RandomID()));
                    this._boatView.messageForSuccess(boatType + " " + boatLength + "m successfully added!");
                } else {
                    if (i+1 == MemberList.Count && MemberList.Count != 1) 
                    {
                        this._boatView.messageForError("No matching member!");
                    }
                }
            }
                
            var json = JsonConvert.SerializeObject(MemberList, Formatting.Indented);
            File.WriteAllText(this._memberController.filePath(), json);
        }

        public Model.Member LoadMemberBoatList() 
        {
            List<Model.Member> MemberList = this._memberController.LoadMemberList();
            string id = this._memberView.ReadMemberIDInput("Your 6-character ID: ");

            for (int i = 0; i < MemberList.Count; i++)
            {
                if (MemberList[i].MemberID == id) 
                {
                    this._boatView.messageForSuccess("Boat-list successfully retrieved for member: " + MemberList[i].Name);

                    return MemberList[i];
                } else {
                    if (i+1 == MemberList.Count && MemberList.Count != 1) 
                    {
                        this._boatView.messageForError("No matching member!");
                    }
                }
            }
            return null;
        }
    }
}