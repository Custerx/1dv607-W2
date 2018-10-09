using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Controller
{
    public class MemberController : FileController
    {
        private View.MemberView _memberView;
        private List<Model.Member> _memberModelList;
        public MemberController(View.MemberView memberView)
        {
            this._memberView = memberView;
            if (base.fileExists(base.filePath()) == false) // No file -> creates a new file and user have to register a new member.
            {
                this._memberView.messageForSuccess("New file created at: " + base.filePath());
                this._memberModelList = new List<Model.Member>();
                this.SaveMemberList();
            }
        }
        public void SaveMemberList() 
        {
            string username = this._memberView.ReadUsernameInput("Chose username: ");
            long personalNumber = this._memberView.ReadPersonalnumberInput("Type the personal-number, example [8907076154]: ");

            if (base.fileExists(base.filePath()) == true)
            {
                this._memberModelList = base.LoadMemberList();
            }

            this._memberModelList.Add(new Model.Member(username, personalNumber, base.RandomID()));

            this._memberView.messageForSuccess("Member successfully registered!");
                
            var json = JsonConvert.SerializeObject(this._memberModelList, Formatting.Indented);
            File.WriteAllText(filePath(), json);
        }

        public void DeleteMemberFromList() 
        {
            string id = this._memberView.ReadMemberIDInput("6-character ID on the member to be removed: ");

            for (int i = 0; i < this._memberModelList.Count; i++)
            {
                if (this._memberModelList[i].MemberID == id) {
                    this._memberModelList.RemoveAt(i);
                    this._memberView.messageForSuccess("Member " + this._memberModelList[i].Name + " successfully deleted!");
                    var json = JsonConvert.SerializeObject(this._memberModelList, Formatting.Indented);
                    File.WriteAllText(base.filePath(), json);
                } else {
                    if (i == this._memberModelList.Count && this._memberModelList.Count != 1) 
                    {
                        this._memberView.messageForError("No matching member!");
                    }
                }
            }
        }
        public void UpdateMemberOnList() {
            string id = this._memberView.ReadMemberIDInput("6-character ID on the member to be updated: ");

            for (int i = 0; i < this._memberModelList.Count; i++)
            {
                if (this._memberModelList[i].MemberID == id) {
                    this._memberModelList.RemoveAt(i);
                    var json = JsonConvert.SerializeObject(this._memberModelList, Formatting.Indented);
                    File.WriteAllText(base.filePath(), json);
                    SaveMemberList();
                    this._memberView.messageForSuccess("Member " + this._memberModelList[i].Name + " successfully updated!");
                } else {
                    if (i == this._memberModelList.Count && this._memberModelList.Count != 1) 
                    {
                        this._memberView.messageForError("Member not found!");
                    }
                }
            }
        }
    }
}