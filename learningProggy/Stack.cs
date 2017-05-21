using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learningProggy
{
    public static sql
    class Stack                              // : List<Karte>
    {
        private int iD;
        private int parentID;
        private string stackName;
        private bool gotChild;
        private bool gotParent;
        private bool hasCards;

        private DateTime lastView;
        private List<Card> card;

        public static sqlte_connector sc = new sqlte_connector();
        public Stack parent;
        public List<int> childIdList;
        public Stack()
        {


        }

        public Stack(int iD, int parentID, string stackName, bool gotChild, bool gotParent, bool hasCards, DateTime lastView)
        {
            this.iD = iD;
            this.parentID = parentID;
            this.stackName = stackName;
            this.gotChild = gotChild;
            this.gotParent = gotParent;
            this.hasCards = hasCards;
            this.lastView = lastView;
            this.parent = new Stack();
            this.childIdList = new List<int>();
        }

        public Stack(int iD, int parentID, string stackName, bool gotChild, bool gotParent, bool hasCards, DateTime lalastView, Stack parent, Stack child)
        {
            this.iD = iD;
            this.parentID = 0;
            this.childIdList = new List<int>();
            this.stackName = stackName;
            this.gotChild = gotChild;
            this.gotParent = gotParent;
            this.hasCards = hasCards;
            this.lastView = lastView;
            this.parent = new Stack();
            this.childIdList = new List<int>();
        }

        public Stack createRoot()
        {
            Stack st = new Stack();
            st.iD = 0;
            st.parentID = 0;
            st.stackName = "Root";
            st.gotChild = true;
            st.gotParent = false;
            st.hasCards = false;
            st.lastView = new DateTime();
            st.parentID = 0;
            st.childIdList = new List<int>(sc.getChildStackIds(0));
            return st;
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
