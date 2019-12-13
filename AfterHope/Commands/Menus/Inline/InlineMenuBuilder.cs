using System.Collections.Generic;

namespace AfterHope.Commands.Menus.Inline
{
    public class InlineMenuBuilder
    {
        private readonly List<InlineMenuRow> rows = new List<InlineMenuRow>();
        private InlineMenuRow currentRow;

        public InlineMenuBuilder AddRow()
        {
            currentRow = new InlineMenuRow();
            rows.Add(currentRow);
            return this;
        }

        public InlineMenuBuilder WithCell(string cellTitle, string commandName, params string[] commandArgs)
        {
            if (currentRow == null)
                AddRow();

            currentRow.Cells.Add(new InlineMenuCell
            {
                Title = cellTitle,
                CommandName = commandName,
                CommandArgs = commandArgs,
            });

            return this;
        }

        public InlineMenu Create()
        {
            return new InlineMenu
            {
                Grid = rows,
            };
        }
    }
}