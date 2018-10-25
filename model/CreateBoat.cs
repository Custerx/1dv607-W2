namespace Model
{
    public class CreateBoat
    {
        private Model.ID _ID;

        public CreateBoat()
        {
            _ID = new Model.ID();
        }

        public Model.Boat create(Enums.BoatTypes.Boats boatType, int boatLength)
        {
            return new Model.Boat(boatType, boatLength, _ID.createID());
        }        
    }
}