namespace Vamos_Sergy.Models.Items
{
    public class Shield : Equipment
    {
        protected override void GenerateStat()
        {
            base.GenerateStat();
        }

        public bool Block(int p)
        {
            return (_random.Next(0, 101) < p);
        }
    }
}
