using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learningProggy
{
    public class Stack                              // : List<Karte>
    {
       static sqlte_connector sc = new sqlte_connector();
        static Card card = new Card();
        // todo private umwandlung ( public int x { get; }; )
        public int iD;
        public int parentID;
        public string stackName;
        public bool gotChild;
        public bool gotParent;
        public bool gotCards;
        public DateTime lastView;
       public List<int> childIdList;
        public List<Stack> childStackList;
        public List<Card> cards;
        public Stack()
        {
            
        }

        public Stack(int ID)
        {
            this.iD = ID;
            this.parentID = sc.get_int_parentID(ID);
            this.stackName = sc.get_string_stackName_stacks(ID);
            this.gotChild = sc.get_bool_gotChild(ID);
            this.gotCards = sc.get_bool_gotCards(ID);
            this.lastView = sc.get_DateTime_stackLastView(ID);
            this.gotParent = false;
            if (this.parentID > 0) this.gotParent = true;
            else this.gotParent = false;
            this.childIdList = new List<int>(sc.get_list_childStackIds(ID));
            this.childStackList = new List <Stack> (createStacks(sc.get_list_childStackIds(ID)));
            this.cards = card.createCards(sc.get_list_cardsOnStackIDs(ID));
        }
        
        public List<Stack> createStacks(List<int> list)
        {
            List<Stack> erg = new List<Stack>();
            for(int i = 0; i < list.Count; i++)
            {
                Stack st = new Stack(list[i]);
                erg.Add(st);
            }
            return erg;
        }
                 
        public List<Stack> createAllStacks()
        {
            List<int> allStackIDs = new List<int>();
            List<Stack> allStacks = new List<Stack>(createStacks(sc.get_list_allStackIds()));
            return allStacks;
        }



    }
}
