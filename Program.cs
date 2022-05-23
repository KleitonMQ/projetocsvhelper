using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;
using static System.Console;
using Projetocsvhelper.Model;



static void EscreverCsv()
{
    var path = Path.Combine(Environment.CurrentDirectory, "Saida", "usuarios.csv");
    var di = new DirectoryInfo(path);

    if (!di.Exists) di.Create();

    path = Path.Combine(path, "usuarios.csv");

    var pessoas = new List<Pessoa>()
    {
        new Pessoa()
        {
            Nome = "José",
            Email = "zezinho@gmail.com",
            Telefone = 123456,
        },
        new Pessoa()
        {
            Nome = "Hugo",
            Email = "huguinho@gmail.com",
            Telefone = 654321,
        },
        new Pessoa()
        {
            Nome = "Lucrecio",
            Email = "luizinho@gmail.com",
            Telefone = 987654,
        }
    };
    using var sr = new StreamWriter(path);
    using var csvWriter = new CsvWriter(sr, CultureInfo.InvariantCulture);
    csvWriter.WriteRecords(pessoas);

    //Esta configuração usa quando não quer usar a virgula como separador.
    // using var sr = new StreamWriter(path);
    // var csvConfig = new CsvConfiguration(CultureInfo.InstalledUICulture)
    // {
    //     Delimiter = "|"
    // };
    // using var csvWriter = new CsvWriter(sr, csvConfig);
    // csvWriter.WriteRecords(pessoas);
}

static void LerCSVComOutroDelimitador()
{
    var path = Path.Combine(Environment.CurrentDirectory, "Entrada", "preco-livro.csv");

    var fi = new FileInfo(path);
    if (!fi.Exists) throw new FileNotFoundException($"O arquivo {path} não encontrado.");

    using var sr = new StreamReader(fi.FullName);
    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        Delimiter = ";"
    };
    using var csvReader = new CsvReader(sr, csvConfig);
    csvReader.Context.RegisterClassMap<LivroMap>();
    var registros = csvReader.GetRecords<Livro>().ToList();

    foreach (var registro in registros)
    {
        WriteLine($"Nome:{registro.Titulo}");
        WriteLine($"Email:{registro.Preco}");
        WriteLine($"Telefone:{registro.Autor}");
        WriteLine($"Telefone:{registro.Lancamento}");
        WriteLine("--------------------------");
    }
}
//Usando classe é um pouco melhor que usar o dynamic porque evita alguns erros de digitação com autocompeltar.
static void LerCSVComClasse()
{
    var path = Path.Combine(Environment.CurrentDirectory, "Entrada", "novos-usuarios.csv");

    var fi = new FileInfo(path);
    if (!fi.Exists) throw new FileNotFoundException($"O arquivo {path} não encontrado.");

    using var sr = new StreamReader(fi.FullName);
    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
    using var csvReader = new CsvReader(sr, csvConfig);

    var registros = csvReader.GetRecords<Usuario>();

    foreach (var registro in registros)
    {
        WriteLine($"Nome:{registro.Nome}");
        WriteLine($"Email:{registro.Email}");
        WriteLine($"Telefone:{registro.Telefone}");
        WriteLine("--------------------------");
    }

}

static void LerCSVComDynamic()
{
    var path = Path.Combine(Environment.CurrentDirectory, "Entrada", "Produtos.csv");

    var fi = new FileInfo(path);
    if (!fi.Exists) throw new FileNotFoundException($"O arquivo {path} não encontrado.");

    using var sr = new StreamReader(fi.FullName);
    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
    using var csvReader = new CsvReader(sr, csvConfig);

    var registros = csvReader.GetRecords<dynamic>();

    foreach (var registro in registros)
    {
        WriteLine($"Nome:{registro.Produto}");
        WriteLine($"Marca:{registro.Marca}");
        WriteLine($"Preço:{registro.Preco}");
        WriteLine("--------------------------");
    }

}

EscreverCsv();

ReadLine();