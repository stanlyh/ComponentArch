using Microsoft.Extensions.DependencyInjection;

var container = new ServiceCollection()
    //.AddSingleton<IRepository, DefaultRepository>()
    .AddSingleton<IRepository, BeerRepository>()
    .AddTransient<BeerManager>()
    .BuildServiceProvider();

var katManager = container.GetService<BeerManager>();
katManager?.Add("evangeline");
katManager?.Add("aleli");
Console.WriteLine(katManager?.Get());

public class BeerManager
{
    private IRepository _repository;
    public BeerManager(IRepository repository)
        => _repository = repository;

    public void Add(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException("name");
        }
        _repository.Add(name);
    }

    public string Get()
        => "Las mininas son: " + _repository.Get();
}

public interface IRepository
{
    void Add(string beer);
    string Get();
}

public class DefaultRepository : IRepository
{
    public void Add(string name)
    { }

    public string Get()
        => "celeste";
}


//-------------------------------------------------------
public class BeerRepository: IRepository
{
    private List<string> _beers;

    public BeerRepository()
        => _beers = new List<string>();

    public void Add(string name)
        => _beers.Add(name);

    public string Get()
        => _beers.Aggregate("", (miau, purrr) => miau + purrr + ", ");
}