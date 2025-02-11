using Application.Shared.Repositories;
using Application.UseCases.AccountsFundsTransfer.Models;
using Application.UseCases.AccountsFundsTransfer.Repository;
using Application.UseCases.AccountsFundsTransfer.UseCase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


//Obs: Voce é livre para implementar na linguagem de sua preferência, desde que respeite as funcionalidades e saídas existentes, além de aplicar os conceitos solicitados.

namespace TransacaoFinanceira
{
    class Program
    {
        static void Main(string[] args)
        {
            var execucaoAntiga = false;

            if (execucaoAntiga)
            {
                var TRANSACOES = new[] {
                                     new {correlation_id= 1,datetime="09/09/2023 14:15:00", conta_origem= 938485762L, conta_destino= 2147483649L, VALOR= 150},
                                     new {correlation_id= 2,datetime="09/09/2023 14:15:05", conta_origem= 2147483649L, conta_destino= 210385733L, VALOR= 149},
                                     new {correlation_id= 3,datetime="09/09/2023 14:15:29", conta_origem= 347586970L, conta_destino= 238596054L, VALOR= 1100},
                                     new {correlation_id= 4,datetime="09/09/2023 14:17:00", conta_origem= 675869708L, conta_destino= 210385733L, VALOR= 5300},
                                     new {correlation_id= 5,datetime="09/09/2023 14:18:00", conta_origem= 238596054L, conta_destino= 674038564L, VALOR= 1489},
                                     new {correlation_id= 6,datetime="09/09/2023 14:18:20", conta_origem= 573659065L, conta_destino= 563856300L, VALOR= 49},
                                     new {correlation_id= 7,datetime="09/09/2023 14:19:00", conta_origem= 938485762L, conta_destino= 2147483649L, VALOR= 44},
                                     new {correlation_id= 8,datetime="09/09/2023 14:19:01", conta_origem= 573659065L, conta_destino= 675869708L, VALOR= 150}
            };
                executarTransacaoFinanceira executor = new executarTransacaoFinanceira();
                Parallel.ForEach(TRANSACOES, item =>
                {
                    executor.transferir(item.correlation_id, item.conta_origem, item.conta_destino, item.VALOR);
                });
            }
            else
            {
                var transacoesNovas = new List<Transaction>
                    {
                        new Transaction
                        {
                            Id = Guid.NewGuid(),
                            CorrelationId = 1,
                            DateTime = DateTime.Parse("09/09/2023 14:15:00"),
                            ContaOrigem = 938485762L,
                            ContaDestino = 2147483649L,
                            Valor = 150m
                        },
                        new Transaction
                        {
                            Id = Guid.NewGuid(),
                            CorrelationId = 2,
                            DateTime = DateTime.Parse("09/09/2023 14:15:05"),
                            ContaOrigem = 2147483649L,
                            ContaDestino = 210385733L,
                            Valor = 149m
                        },
                        new Transaction
                        {
                            Id = Guid.NewGuid(),
                            CorrelationId = 3,
                            DateTime = DateTime.Parse("09/09/2023 14:15:29"),
                            ContaOrigem = 347586970L,
                            ContaDestino = 238596054L,
                            Valor = 1100m
                        },
                        new Transaction
                        {
                            Id = Guid.NewGuid(),
                            CorrelationId = 4,
                            DateTime = DateTime.Parse("09/09/2023 14:17:00"),
                            ContaOrigem = 675869708L,
                            ContaDestino = 210385733L,
                            Valor = 5300m
                        },
                        new Transaction
                        {
                            Id = Guid.NewGuid(),
                            CorrelationId = 5,
                            DateTime = DateTime.Parse("09/09/2023 14:18:00"),
                            ContaOrigem = 238596054L,
                            ContaDestino = 674038564L,
                            Valor = 1489m
                        },
                        new Transaction
                        {
                            Id = Guid.NewGuid(),
                            CorrelationId = 6,
                            DateTime = DateTime.Parse("09/09/2023 14:18:20"),
                            ContaOrigem = 573659065L,
                            ContaDestino = 563856300L,
                            Valor = 49m
                        },
                        new Transaction
                        {
                            Id = Guid.NewGuid(),
                            CorrelationId = 7,
                            DateTime = DateTime.Parse("09/09/2023 14:19:00"),
                            ContaOrigem = 938485762L,
                            ContaDestino = 2147483649L,
                            Valor = 44m
                        },
                        new Transaction
                        {
                            Id = Guid.NewGuid(),
                            CorrelationId = 8,
                            DateTime = DateTime.Parse("09/09/2023 14:19:01"),
                            ContaOrigem = 573659065L,
                            ContaDestino = 675869708L,
                            Valor = 150m
                        }
                    };

                var balanceRepo = new BalanceRepository();
                var transferRepo = new AccountsFundsTransferRepository(balanceRepo);

                var executor = new AccountsFundsTransferUseCaseHandler(transferRepo, balanceRepo);

                //executor.CreateTransaction(transacoesNovas[0].CorrelationId, transacoesNovas[0].ContaOrigem, transacoesNovas[0].ContaDestino, transacoesNovas[0].Valor);

                Parallel.ForEach(transacoesNovas, item =>
                {
                    Console.WriteLine(executor.CreateTransaction(item.CorrelationId, item.ContaOrigem, item.ContaDestino, item.Valor).Result.Message.ToString());
                });
            }

        }
    }

    class executarTransacaoFinanceira : acessoDados
    {
        public void transferir(long correlation_id, long conta_origem, long conta_destino, decimal valor)
        {
            contas_saldo conta_saldo_origem = getSaldo<contas_saldo>(conta_origem);
            if (conta_saldo_origem.saldo < valor)
            {
                Console.WriteLine("Transacao numero {0} foi cancelada por falta de saldo", correlation_id);

            }
            else
            {
                contas_saldo conta_saldo_destino = getSaldo<contas_saldo>(conta_destino);
                conta_saldo_origem.saldo -= valor;
                conta_saldo_destino.saldo += valor;
                Console.WriteLine($"Transacao numero {0} foi efetivada com sucesso! Novos saldos: Conta Origem: {1} | Conta Destino: {2}", correlation_id, conta_saldo_origem.saldo, conta_saldo_destino.saldo);
            }
        }
    }
    class contas_saldo
    {
        public contas_saldo(long conta, decimal valor)
        {
            this.conta = conta;
            this.saldo = valor;
        }
        public long conta { get; set; }
        public decimal saldo { get; set; }
    }
    class acessoDados
    {
        Dictionary<int, decimal> SALDOS { get; set; }
        private List<contas_saldo> TABELA_SALDOS;
        public acessoDados()
        {
            TABELA_SALDOS = new List<contas_saldo>();
            TABELA_SALDOS.Add(new contas_saldo(938485762, 180));
            TABELA_SALDOS.Add(new contas_saldo(347586970, 1200));
            //TODO: Investigar o tamanho de dado necessário aqui
            TABELA_SALDOS.Add(new contas_saldo(2147483649, 0));
            TABELA_SALDOS.Add(new contas_saldo(675869708, 4900));
            TABELA_SALDOS.Add(new contas_saldo(238596054, 478));
            TABELA_SALDOS.Add(new contas_saldo(573659065, 787));
            TABELA_SALDOS.Add(new contas_saldo(210385733, 10));
            TABELA_SALDOS.Add(new contas_saldo(674038564, 400));
            TABELA_SALDOS.Add(new contas_saldo(563856300, 1200));


            SALDOS = new Dictionary<int, decimal>();
            this.SALDOS.Add(938485762, 180);

        }
        public T getSaldo<T>(long id)
        {
            return (T)Convert.ChangeType(TABELA_SALDOS.Find(x => x.conta == id), typeof(T));
        }
        public bool atualizar<T>(T dado)
        {
            try
            {
                contas_saldo item = (dado as contas_saldo);
                TABELA_SALDOS.RemoveAll(x => x.conta == item.conta);
                TABELA_SALDOS.Add(dado as contas_saldo);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

    }
}
