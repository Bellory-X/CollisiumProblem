namespace ColiseumLibrary.Interfaces;

public interface IWebWorker : IWorker
{
    public string PlayerUrl { get; set; }
    public string OpponentUrl { get; set; }
}