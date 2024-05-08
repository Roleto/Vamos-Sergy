namespace Vamos_Sergy.Models.Items
{
    public class Shield : Equipment
    {
        public Shield() : base() 
        {
            
        }

        public Shield(Item item) : base(item)
        {

        }
        public bool Blocking()
        {
            return (_random.Next(0, 101) < Block);
        }
    }
}
