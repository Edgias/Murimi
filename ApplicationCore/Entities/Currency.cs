using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class Currency : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public string Symbol { get; private set; } = string.Empty;

        public Currency(string name, string symbol)
        {
            SetData(name, symbol);
        }

        public void UpdateDetails(string name, string symbol)
        {
            SetData(name, symbol);
        }

        private void SetData(string name, string symbol)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNullOrEmpty(symbol, nameof(symbol));

            Name = name;
            Symbol = symbol;
        }
    }
}
