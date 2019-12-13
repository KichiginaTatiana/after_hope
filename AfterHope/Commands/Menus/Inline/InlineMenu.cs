using System.Collections.Generic;

namespace AfterHope.Commands.Menus.Inline
{
    public class InlineMenu
    {
        public List<InlineMenuRow> Grid { get; set; }

        public static InlineMenuBuilder Build()
        {
            return new InlineMenuBuilder();
        }
    }
}