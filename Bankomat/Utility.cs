using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bankomat
{
    public static class Utility
    {
        private static List<Account> _accountEsistenti = new List<Account>();
        private static int tentativi = 3;
        private static Bancomat bancomat;

        public static void Avvio()
        {
            _accountEsistenti.Add(new Account("emanuele", "123"));
            _accountEsistenti.Add(new Account("lorenzo", "456"));
            _accountEsistenti.Add(new Account("giorgio", "789"));
            bancomat = new Bancomat();
            MenuLogin();
        }
        public static void AggiungiAccount(Account a)
        {
            _accountEsistenti.Add(a);
        }

        private static void MenuLogin()
        {

            Console.Clear();
            Console.WriteLine("------- LOGIN AREA RISERVATA CLIENTE -------");
            if (_accountEsistenti.Count == 0)
            {
                Console.WriteLine("\nnessun cliente esistente all'interno del database");
                Console.WriteLine("\nPremere 'm' per ritornare al login: ");
                char scelta = char.ToLower(char.Parse(Console.ReadLine()));
                if (scelta == 'm')
                    MenuLogin();
                else
                    return;
            }

            Console.WriteLine("\nUsername: ");
            string _username = Console.ReadLine();
            Console.WriteLine("\nPassword: ");
            string _password = Console.ReadLine();

            Account accountTrovato = _accountEsistenti.FirstOrDefault(Account => Account.username == _username && Account.password == _password);
            Account accountSbagliato = _accountEsistenti.FirstOrDefault(Account => Account.username == _username && Account.password != _password);
           
            if (accountTrovato != null && accountTrovato.bloccoAccount == false)
            {
                Console.Clear();
                MenuAccount(accountTrovato);
            }else if(accountTrovato != null && accountTrovato.bloccoAccount == true)
            {
                Console.WriteLine($"\nL'account: {accountTrovato.username} è attualmente bloccato");
            }else if (accountSbagliato != null)
            {
                accountSbagliato.tentativi--;
                if(accountSbagliato.tentativi <= 0)
                {
                    Console.WriteLine($"\nL'account: {accountSbagliato.username} è attualmente bloccato");
                    accountSbagliato.bloccoAccount = true;
                }
                    else
                Console.WriteLine($"errore durante il login, tentativi rimasti: {accountSbagliato.tentativi}");
            }else if(accountSbagliato == null && accountTrovato == null)
            {
                Console.WriteLine($"\nNessun account trovato con username: {_username}");
            }

            char scelta1 = 'a';
            Console.WriteLine();

            do
            {
                try
                {
                    Console.WriteLine("\nPremere 'm' per ritornare al login: ");
                    scelta1 = char.ToLower(char.Parse(Console.ReadLine()));
                }
                catch
                {
                    Console.WriteLine("\ncarattere non valido");
                }
            } while (scelta1 != 'm');

            if (scelta1 == 'm')
                MenuLogin();
            else
                return;
        }

        public static void MenuAccount(Account a)
        {
            char scelta;
            Console.Clear();
            Console.WriteLine($"------- {a.username.ToUpper()} BENVENUTO IN {bancomat.nome.ToUpper()} -------");
            Console.WriteLine();
            Console.WriteLine("v - effettua un prelievo");
            Console.WriteLine("c - effettua un versamento");
            Console.WriteLine("g - visualizza il saldo corrente");
            Console.WriteLine("e - per effetturare il logout");
            Console.WriteLine("");
            scelta = char.ToLower(char.Parse(Console.ReadLine()));
            Utility.ControlloInputMenuAccount(scelta, a);
        }

        private static void ControlloInputMenuAccount(int scelta, Account a)
        {
            switch (scelta)
            {
                case 'v':
                    {
                        double importo = 0;
                        Console.Clear();
                        Console.WriteLine($"------- {a.username.ToUpper()} - PRELIEVO -------");

                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("\nInserisci l'importo da prelevare: ");
                                importo = double.Parse(Console.ReadLine());

                                bool esitoPrelievo = bancomat.Prelievo(importo, a);
                                if (esitoPrelievo == true)
                                {
                                    Console.WriteLine("\nIl prelievo è stato effettuato con successo");
                                    Console.WriteLine($"SALDO ACCOUNT: {a.contoCorrente.saldo} euro");
                                }
                                else if (esitoPrelievo == false)
                                {
                                    Console.WriteLine("\nIl saldo dell'account è insufficiente per effettuare il prelievo");
                                    Console.WriteLine($"SALDO ACCOUNT: {a.contoCorrente.saldo} euro");
                                }
                                Console.WriteLine("\nPremere 'm' per ritornare al menù dell'account: ");
                                char scelta1 = char.ToLower(char.Parse(Console.ReadLine()));
                                if (scelta1 == 'm')
                                    Utility.MenuAccount(a);
                                else
                                    return;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("\nformato non valido");
                            }
                        }
                    }

                case 'c':
                    {
                        Console.Clear();
                        Console.WriteLine($"------- {a.username.ToUpper()} - VERSAMENTO -------");
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("\nInserisci l'importo da versare: ");
                                double importo = double.Parse(Console.ReadLine());
                                bool esitoVersamento = bancomat.Versamento(importo, a);
                                if (esitoVersamento == true)
                                {
                                    Console.WriteLine("\nIl versamento è stato effettuato con successo");
                                    Console.WriteLine($"SALDO ACCOUNT: {a.contoCorrente.saldo} euro");
                                }
                                else if (esitoVersamento == false)
                                {
                                    Console.WriteLine("importo non valido");
                                }
                                Console.WriteLine("\nPremere 'm' per ritornare al menù dell'account: ");
                                char scelta1 = char.ToLower(char.Parse(Console.ReadLine()));
                                if (scelta1 == 'm')
                                    Utility.MenuAccount(a);
                                else
                                    return;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("\nformato non valido");
                            }
                        }
                    }

                case 'g':
                    {
                        DateTime dateTime = DateTime.Now;
                        Console.Clear();
                        Console.WriteLine($"------- {a.username.ToUpper()} - SALDO -------");
                        Console.WriteLine();
                        Console.WriteLine($"username        | {a.username}");
                        Console.WriteLine($"IBAN:           | {a.contoCorrente.iban}");
                        Console.WriteLine($"saldo corrente: | {a.contoCorrente.saldo} euro");
                        Console.WriteLine($"data e ora:     | {dateTime.ToString()}");
                        Console.WriteLine("\nPremere 'm' per ritornare al menù dell'account: ");
                        char scelta1 = char.ToLower(char.Parse(Console.ReadLine()));
                        if (scelta1 == 'm')
                            Utility.MenuAccount(a);
                        else
                            return;
                        return;
                    }

                case 'e':
                    {
                        a.tentativi = 3;
                        MenuLogin();
                        return;
                    }

                default:
                    {
                        Console.WriteLine("\ncarattere non valido");
                        scelta = char.ToLower(char.Parse(Console.ReadLine()));
                        Utility.ControlloInputMenuAccount(scelta, a);
                        return;
                    }
            }
        }
    }
}
