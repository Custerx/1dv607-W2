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

        public void DeleteBoatFromList() 
        {
            List<Model.Member> viewMemberList = this._memberController.LoadMemberList();

            viewMemberList = retriveMemberDetails(viewMemberList);

            var json = JsonConvert.SerializeObject(viewMemberList, Formatting.Indented);
            File.WriteAllText(this._memberController.filePath(), json);
        }
        private Model.Boat deleteBoat(Model.Member boatOwner)
        {
            string id = this._boatView.ReadBoatIDInput();
            
            for (int i = 0; i < boatOwner.Boats.Count; i++)
            {
                Console.WriteLine(boatOwner.Boats[i].BoatType);
                if (boatOwner.Boats[i].BoatID == id) 
                {
                    this._boatView.successfullyDeletedBoat(boatOwner.Boats[i].BoatType, boatOwner.Boats[i].Length, boatOwner.Boats[i].BoatID);
                    boatOwner.Boats.RemoveAt(i);
                } else {
                    if (i == boatOwner.Boats.Count) 
                    {
                        this._boatView.noMatchingBoat();
                    }
                }
            }
            return null;
        }
        private List<Model.Member> retriveMemberDetails(List<Model.Member> viewMemberList)
        {
            string id = this._memberView.ReadMemberIDInput("The boat owner's 6-character ID: ");

            for (int i = 0; i < viewMemberList.Count; i++)
            {
                if (viewMemberList[i].MemberID == id) {
                    this._boatView.successfullyRetrieved(viewMemberList[i].Name);
                    deleteBoat(viewMemberList[i]);
                    return viewMemberList;
                } else {
                    if (i+1 == viewMemberList.Count && viewMemberList.Count != 1) {
                        this._boatView.noMatchingMember();
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
                    this._boatView.successfullyAddedBoat(boatType);
                } else {
                    if (i+1 == MemberList.Count && MemberList.Count != 1) 
                    {
                        this._boatView.unsuccessfull();
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
                    this._boatView.successfullyFoundBoat(MemberList[i].Name);
                    return MemberList[i];
                } else {
                    if (i+1 == MemberList.Count && MemberList.Count != 1) 
                    {
                        this._boatView.unsuccessfull();
                    }
                }
            }
            return null;
        }
    }
}