using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankomat
{
    public class Bancomat
    {
        private string _nome {  get; set; }
        private Guid _idBancomat {  get; set; }

        public Bancomat()
        {
            _idBancomat = Guid.NewGuid();
            _nome = "unicredit";
        }

        public string nome { get { return _nome; } set { _nome = value; } }

        public bool Prelievo(double importo, Account a)  
        {
            bool esitoPrelievo = _Prelievo(importo, a);
            return esitoPrelievo;
        }

        private bool _Prelievo(double importo, Account a)       
        {
            if(importo < a.contoCorrente.saldo)
            {
                a.contoCorrente.saldo = a.contoCorrente.saldo - importo;
                return true;
            }
            else
            {
                return false;
            }            
        }

        public bool Versamento(double importo, Account a)  
        {
            bool esitoVersamento = _Versamento(importo, a);
            return esitoVersamento;
        }

        private bool _Versamento(double importo, Account a)     
        {            
            if (importo > 0)
            {
                a.contoCorrente.saldo = a.contoCorrente.saldo + importo;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
