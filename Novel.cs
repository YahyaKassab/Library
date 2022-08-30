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
        public Novel(string title, string authorName,uint numofbooks, uint numOfVolumes) : base(title, authorName, numofbooks)
        { NumOfVolumes = numOfVolumes; }
        public override string Type {
            get { return "Novel";}
            }
    }
}
