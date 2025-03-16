
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public partial class SavesYG
    {
        public int idSave;
        public int money = 0;
        public int SelectedCar = 0;
        public List<int> carOwned = new List<int>();
    }
}
