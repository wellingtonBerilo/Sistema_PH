using System;
using System.Collections.Generic;

class Reagente
{
    public double PH;
    public string CorHex;
    public string Legenda;
    public string Curiosidade;

    public string Classificacao()
    {
        if (PH < 7.0) return "Ácido";
        if (PH == 7.0) return "Neutro";
        return "Alcalino";
    }
}

class Program
{
    static List<Reagente> reagentes = new List<Reagente>();

    static void Main()
    {
        int opcao;

        do
        {
            Console.Clear();
            Console.WriteLine("===== SISTEMA DE VERIFICAÇÃO POR REAGENTE =====");
            Console.WriteLine("1 - Cadastrar reagente");
            Console.WriteLine("2 - Listar reagentes");
            Console.WriteLine("3 - Ordenar por pH (Bubble Sort)");
            Console.WriteLine("4 - Buscar pH (Busca Binária)");
            Console.WriteLine("5 - Editar reagente");
            Console.WriteLine("6 - Remover reagente");
            Console.WriteLine("7 - Sair");
            Console.Write("\nEscolha: ");

            int.TryParse(Console.ReadLine(), out opcao);

            switch (opcao)
            {
                case 1: Cadastrar(); break;
                case 2: Listar(); break;
                case 3:
                    BubbleSort();
                    Console.WriteLine("\nReagentes ordenados com sucesso.");
                    Pausar();
                    break;
                case 4: Buscar(); break;
                case 5: Editar(); break;
                case 6: Remover(); break;
                case 7: Console.WriteLine("Encerrando..."); break;
                default:
                    Console.WriteLine("Opção inválida.");
                    Pausar();
                    break;
            }

        } while (opcao != 7);
    }

    static void Cadastrar()
    {
        Console.Clear();
        Console.WriteLine("=== CADASTRAR REAGENTE ===");

        double ph;

        do
        {
            Console.Write("Digite o pH (0 a 14): ");
        }
        while (!double.TryParse(Console.ReadLine(), out ph) || ph < 0 || ph > 14);

        Console.Write("Cor hexadecimal: ");
        string cor = Console.ReadLine();

        Console.Write("Legenda: ");
        string legenda = Console.ReadLine();

        Console.Write("Curiosidade/Dica (opcional): ");
        string curiosidade = Console.ReadLine();

        reagentes.Add(new Reagente
        {
            PH = ph,
            CorHex = cor,
            Legenda = legenda,
            Curiosidade = curiosidade
        });

        Console.WriteLine("\nCadastro realizado com sucesso!");
        Pausar();
    }

    static void Listar()
    {
        Console.Clear();

        if (reagentes.Count == 0)
        {
            Console.WriteLine("Nenhum reagente cadastrado.");
        }
        else
        {
            for (int i = 0; i < reagentes.Count; i++)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Registro #" + (i + 1));
                Console.WriteLine("pH: " + reagentes[i].PH);
                Console.WriteLine("Cor: " + reagentes[i].CorHex);
                Console.WriteLine("Legenda: " + reagentes[i].Legenda);
                Console.WriteLine("Classificação: " + reagentes[i].Classificacao());
                Console.WriteLine("Curiosidade: " + reagentes[i].Curiosidade);
            }
        }

        Pausar();
    }

    // Bubble Sort
    static void BubbleSort()
    {
        int n = reagentes.Count;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (reagentes[j].PH > reagentes[j + 1].PH)
                {
                    Reagente temp = reagentes[j];
                    reagentes[j] = reagentes[j + 1];
                    reagentes[j + 1] = temp;
                }
            }
        }
    }

    static void Buscar()
    {
        if (reagentes.Count == 0)
        {
            Console.WriteLine("Cadastre reagentes primeiro.");
            Pausar();
            return;
        }

        BubbleSort();

        Console.Clear();
        Console.Write("Digite o pH procurado: ");

        double valor;
        if (!double.TryParse(Console.ReadLine(), out valor))
        {
            Console.WriteLine("Valor inválido.");
            Pausar();
            return;
        }

        int indice = BuscaBinaria(valor);

        if (indice == -1)
        {
            Console.WriteLine("pH não encontrado.");
        }
        else
        {
            Reagente r = reagentes[indice];

            Console.WriteLine("\n=== REAGENTE ENCONTRADO ===");
            Console.WriteLine("pH: " + r.PH);
            Console.WriteLine("Cor: " + r.CorHex);
            Console.WriteLine("Legenda: " + r.Legenda);
            Console.WriteLine("Classificação: " + r.Classificacao());
            Console.WriteLine("Curiosidade: " + r.Curiosidade);
        }

        Pausar();
    }

    // Busca Binária
    static int BuscaBinaria(double valor)
    {
        int inicio = 0;
        int fim = reagentes.Count - 1;

        while (inicio <= fim)
        {
            int meio = (inicio + fim) / 2;

            if (Math.Abs(reagentes[meio].PH - valor) < 0.0001)
                return meio;

            if (reagentes[meio].PH < valor)
                inicio = meio + 1;
            else
                fim = meio - 1;
        }

        return -1;
    }

    static void Editar()
    {
        if (reagentes.Count == 0)
        {
            Console.WriteLine("Nenhum reagente cadastrado.");
            Pausar();
            return;
        }

        ListarSemPausa();

        Console.Write("\nNúmero do registro para editar: ");

        int indice;
        if (!int.TryParse(Console.ReadLine(), out indice))
            return;

        indice--;

        if (indice < 0 || indice >= reagentes.Count)
        {
            Console.WriteLine("Registro inválido.");
            Pausar();
            return;
        }

        double ph;

        do
        {
            Console.Write("Novo pH (0 a 14): ");
        }
        while (!double.TryParse(Console.ReadLine(), out ph) || ph < 0 || ph > 14);

        Console.Write("Nova cor hexadecimal: ");
        reagentes[indice].CorHex = Console.ReadLine();

        Console.Write("Nova legenda: ");
        reagentes[indice].Legenda = Console.ReadLine();

        Console.Write("Nova curiosidade: ");
        reagentes[indice].Curiosidade = Console.ReadLine();

        reagentes[indice].PH = ph;

        Console.WriteLine("Registro atualizado.");
        Pausar();
    }

    static void Remover()
    {
        if (reagentes.Count == 0)
        {
            Console.WriteLine("Nenhum reagente cadastrado.");
            Pausar();
            return;
        }

        ListarSemPausa();

        Console.Write("\nNúmero do registro para remover: ");

        int indice;
        if (!int.TryParse(Console.ReadLine(), out indice))
            return;

        indice--;

        if (indice < 0 || indice >= reagentes.Count)
        {
            Console.WriteLine("Registro inválido.");
            Pausar();
            return;
        }

        reagentes.RemoveAt(indice);

        Console.WriteLine("Registro removido.");
        Pausar();
    }

    static void ListarSemPausa()
    {
        Console.Clear();

        for (int i = 0; i < reagentes.Count; i++)
        {
            Console.WriteLine((i + 1) + " - pH: " +
                              reagentes[i].PH +
                              " | " +
                              reagentes[i].Legenda);
        }
    }

    static void Pausar()
    {
        Console.WriteLine("\nPressione ENTER para continuar...");
        Console.ReadLine();
    }
}