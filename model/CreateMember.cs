namespace Model
{
    public class CreateMember
    {
        private Model.ID _ID;

        public CreateMember()
        {
            _ID = new Model.ID();
        }

        public Model.Member create(string username, string personalNumber, string password)
        {
            return new Model.Member(username, personalNumber, _ID.createID(), password);
        }
    }
}