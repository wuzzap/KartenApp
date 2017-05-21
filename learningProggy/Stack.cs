using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learningProggy
{

    class Stack                              // : List<Karte>
    {

        public static sqlte_connector sc = new sqlte_connector();

        public int iD;
            private int parentID;
        public string stackName;
        public bool gotChild;
        public bool gotParent;
        public bool gotCards;

        public DateTime lastView;
        public List<Card> card;


        public Stack parent;
        public List<int> childIdList;
        public Stack()
        {
            
        }

        public Stack(int ID)
        {
            this.iD = ID;
            this.parentID = sc.getParentId(ID);
            this.stackName = sc.getCategoryName(ID);
            this.gotChild = sc.gotChild(ID);
            this.gotCards = sc.gotCards(ID);
            this.lastView = sc.get_StackLastView(ID);
            this.gotParent = false;
            if (this.parentID > 0) this.gotParent = true;
            this.childIdList = new List<int>(sc.getChildStackIds(0));
        }
/*
        public Stack(int iD, int parentID, string stackName, bool gotChild, bool gotParent, bool gotCards, DateTime lastView, Stack parent, Stack child)
        {
            this.iD = iD;
            this.parentID = 0;
            this.childIdList = new List<int>();
            this.stackName = stackName;
            this.gotChild = gotChild;
            this.gotParent = gotParent;
            this.gotCards = gotCards;
            this.lastView = lastView;
            this.parent = new Stack();
            this.childIdList = new List<int>();
        }
        */
        public Stack createRoot()
        {
            Stack st = new Stack();
            this.iD = 0;
            st.parentID = 0;
            st.stackName = "Root";
            st.gotChild = true;
            st.gotParent = false;
            st.gotCards = false;
            st.lastView = new DateTime();
            st.parentID = 0;
            st.childIdList = new List<int>(sc.getChildStackIds(0));
            return st;
        }

        public List<Stack> createStacks(List<int> list)
        {
            List<Stack> erg = new List<Stack>();
            for(int i = 0; i < list.Count; i++)
            {
                Stack st = new Stack();
                st.iD = list[i];
                st.parentID = sc.getParentId(list[i]);
                st.stackName = sc.getCategoryName(list[i]);
                st.gotChild = sc.gotChild(list[i]);
                st.gotCards = sc.gotCards(list[i]);
                st.lastView = sc.get_StackLastView(list[i]);
                st.gotParent = false;
                if (st.parentID > 0) st.gotParent = true;
                st.childIdList = new List<int>(sc.getChildStackIds(0));
                erg.Add(st);
            }

            return erg;
        }

        public int getQuantityOfChildCards()
        {

            return 5;
        }

        private List<Stack> getChildIDs(int ID = 0)
        {

            return new List<Stack>();
        }
  
    }
}
