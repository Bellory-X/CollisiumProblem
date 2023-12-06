namespace GodsApi.Models;

public class ExperimentDbModel
{
    public int Id { get; set; }
    public string PlayerColors { get; set; } = null!;
    public string OpponentColors { get; set; } = null!;
    public bool Output { get; set; }
    
    // public int CompareTo(object? o)
    // {
    //     if(o is Experiment experiment) return Id.CompareTo(experiment.Id);
    //     throw new ArgumentException("Некорректное значение параметра");
    // }
}