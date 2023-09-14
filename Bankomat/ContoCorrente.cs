using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankomat
{
    public class ContoCorrente
    {
        private double _saldo {  get; set; }
        private Guid _IBAN {  get; set; }

        public ContoCorrente()
        {
            _saldo = 1000;
            _IBAN = Guid.NewGuid();
        }

        public double saldo { get { return _saldo; } set { _saldo = value; } }
        public Guid iban { get { return _IBAN; } set { iban = _IBAN; } }
    }
}
