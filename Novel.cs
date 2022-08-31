using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Novel : Book
    {
        public uint NumOfVolumes { get; set; }
        public Novel(string title, string authorName,uint numofbooks, uint numOfVolumes, double price) : base(title, authorName, numofbooks,price)
        { NumOfVolumes = numOfVolumes; }
        public override string Type {
            get { return "Novel";}
            }
    }
}
