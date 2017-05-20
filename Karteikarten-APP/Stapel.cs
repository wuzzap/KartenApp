using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Karteikarten_APP
{
    class Stapel : List<Karte>
    {
        private int ID;
        private int ParentID;
        private string stackName;
        private bool gotChild;
        private bool gotParent;
        private bool hasCards;
        private DateTime lastUse;

        public Stapel()
        {
            
        }

        public Stapel(int ID,int ParentID,string stackName,bool gotChild,bool gotParent,bool hasCards,DateTime lastUse){
            this.ID = ID;
            this.ParentID = ParentID;
            this.stackName = stackName;
            this.gotChild = gotChild;
            this.gotParent = gotParent;
            this.hasCards = hasCards;
            this.lastUse = lastUse;
        }



    }
}
