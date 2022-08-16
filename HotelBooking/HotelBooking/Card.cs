using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking
{
   /// <summary>
   /// Enum cards
   /// </summary>
    public enum CardType
    {
       Visa, VisaElectron, MasterCard
    }
     class Card
     {/// <summary>
     /// declare variables of a card as per OOP
     /// </summary>
        private CardType cardType;
        private string validity;
        private UInt64 cardNo;
        private string cvc;
        private string accountName;

        /// <summary>
        /// default
        /// </summary>
        public Card() : this(CardType.Visa, "", 0, "", "")
        {

        }
        /// <summary>
        /// load constructor
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="accountName"></param>
        /// <param name="cardNo"></param>
        /// <param name="validity"></param>
        public Card(CardType cardType, string accountName, UInt64 cardNo, string validity):this(cardType, accountName,cardNo, string.Empty, validity)
        {
            
        }
        /// <summary>
        /// Load comnstructor with all variables
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="accountName"></param>
        /// <param name="cardNo"></param>
        /// <param name="cvc"></param>
        /// <param name="validity"></param>
        public Card(CardType cardType, string accountName, UInt64 cardNo, string cvc, string validity)
        {
            this.cardType = cardType;
            this.accountName = accountName;
            this.cardNo = cardNo;
            this.cvc = cvc;
            this.validity = validity;
        }

        /// <summary>
        /// gets and sets cardtype
        /// </summary>
        public CardType CardType
        {
            get { return cardType; }
            set { cardType = value; }
        }
        /// <summary>
        /// gets and sets validity
        /// </summary>
        public string Validity
        {
            get { return validity; }
            set { validity = value; }
        }

        //gets and sets cardno
        public UInt64 CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        //gets and sets CVC
        public string CVC
        {
            get { return cvc; }
            set { cvc = value; }
        }

        /// <summary>
        /// gets and sets account name
        /// </summary>
        public string AccountName
        {
            get { return accountName; }
            set { accountName = value; }
        }
    }
}
