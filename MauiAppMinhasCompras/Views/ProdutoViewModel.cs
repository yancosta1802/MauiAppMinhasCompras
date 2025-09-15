using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.ViewModels;

public class ProdutosViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Produto> TodosProdutos { get; set; }
    public ObservableCollection<Produto> ProdutosFiltrados { get; set; }

    private string _termoBusca;
    public string TermoBusca
    {
        get => _termoBusca;
        set
        {
            if (_termoBusca != value)
            {
                _termoBusca = value;
                OnPropertyChanged();
                FiltrarProdutos(_termoBusca);
            }
        }
    }

    public ProdutosViewModel()
    {
        TodosProdutos = new ObservableCollection<Produto>
        {
            new Produto { Nome = "Caneta", Preco = 2.5m },
            new Produto { Nome = "Caderno", Preco = 15m },
            new Produto { Nome = "Borracha", Preco = 1.2m },
            new Produto { Nome = "Lápis", Preco = 1m }
        };

        ProdutosFiltrados = new ObservableCollection<Produto>(TodosProdutos);
    }

    private void FiltrarProdutos(string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
        {
            ProdutosFiltrados.Clear();
            foreach (var p in TodosProdutos)
                ProdutosFiltrados.Add(p);
        }
        else
        {
            var filtrados = TodosProdutos
                .Where(p => p.Nome.ToLower().Contains(termo.ToLower()))
                .ToList();

            ProdutosFiltrados.Clear();
            foreach (var p in filtrados)
                ProdutosFiltrados.Add(p);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string nome = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
}
