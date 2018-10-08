using System;
using System.Linq;

namespace Model
{
    public class Boat
    {
        private Enums.BoatTypes.Boats _boatType;
        private int _length;
        private string _boatID;

        public Enums.BoatTypes.Boats BoatType
        { 
            get => _boatType; 
            set => _boatType = value;         
        }

        public int Length 
        { 
            get => _length; 
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _length = value;
            }            
        }

        public string BoatID { get { return this._boatID;} set { this._boatID = value; } }

        public Boat(Enums.BoatTypes.Boats boatType, int length, string id) {
            this.BoatType = boatType;
            this.Length = length;
            this._boatID = id;
        }
    }
}