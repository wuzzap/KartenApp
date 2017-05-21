using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Karteikarten_APP
{
    class Stack                              // : List<Karte>
    {
        private int iD;
        private int parentID;
        private string stackName;
        private bool gotChild;
        private bool gotParent;
        private bool hasCards;

        private DateTime lastUse;
        private List<Karte> card;
        public Stack parent;
        public List<Stack> childIdList;
        public Stack()
        {

            
        }

        public Stack(int iD,int parentID,string stackName,bool gotChild,bool gotParent,bool hasCards,DateTime lastUse){
            this.iD = iD;
            this.parentID = parentID;
            this.stackName = stackName;
            this.gotChild = gotChild;
            this.gotParent = gotParent;
            this.hasCards = hasCards;
            this.lastUse = lastUse;
            this.parent = new Stack();
            this.childIdList = new List<Stack>();
        }

        public Stack(int iD, int parentID, string stackName, bool gotChild, bool gotParent, bool hasCards, DateTime lastUse, Stack parent)
        {
            this.iD = iD;
            this.parentID = 0;
            this.childIdList = new List<Stack>();
            this.stackName = stackName;
            this.gotChild = gotChild;
            this.gotParent = gotParent;
            this.hasCards = hasCards;
            this.lastUse = lastUse;
            this.parent = new Stack();
            
        }

        public Stack createRoot()
        {
            Stack root = new Stack();
            root.iD = 0;
            root.parentID = 0;
            root.stackName = "Root";
            root.gotChild = true;
            root.gotParent = false;
            root.hasCards = false;
            root.lastUse = new DateTime();
            root.parentID = 0;
            root.childIdList = new List<Stack>();


            return root;
        }

        public int getQuantityOfChildCards()
        {

            return 5;
        }


    }
}
