using System.Collections.Generic;

namespace AfterHope.Commands.Menus.Inline
{
    public class InlineMenuRow
    {
        public InlineMenuRow()
        {
            Cells = new List<InlineMenuCell>();
        }

        public List<InlineMenuCell> Cells { get; set; }
    }
}