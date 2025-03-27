
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public partial class SavesYG
    {
        public int idSave;
        public int money2;
        public int level ;
        public int SelectedCar = 0;
        public List<int> carOwned = new List<int>();
        public int tgbonus = 0;
        public bool carFromTG;
        public int playerRating;
    }
}
