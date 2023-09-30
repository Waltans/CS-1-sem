using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelAccounting
{
    class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;

        public double Price
        {
            get => price;
            set
            {
                if (value <= 0) throw new ArgumentException();
                
                price = value;
                TotalResultChange();
                Notify(nameof(Price));
            }
        }

        public int NightsCount
        {
            get => nightsCount;
            set
            {
                if (value <= 0) throw new ArgumentException();
                
                nightsCount = value;
                TotalResultChange();
                Notify(nameof(NightsCount));
            }
        }

        public double Discount
        {
            get => discount;
            set
            {
                discount = value;
                if (Math.Abs(discount - ((-1) * Total / (Price * NightsCount) + 1) * 100) > 1e-12)
                    TotalResultChange();
                Notify(nameof(Discount));
            }
        }

        public double Total
        {
            get => total;
            set
            {
                if (value < 0) throw new ArgumentException();
                
                total = value;
                
                if (Math.Abs(total - Price * NightsCount * (1 - Discount / 100)) > 1e-12)
                    Discount = (1 - Total / (Price * NightsCount)) * 100;
                Notify(nameof(Total));
            }   
        }
        
        private void TotalResultChange()
        {
            if( Price * NightsCount * (1 - Discount / 100) < 0)
                throw new ArgumentException();
            total = Price * NightsCount * (1 - Discount / 100);
            Notify(nameof(Total));
        }
    }
}