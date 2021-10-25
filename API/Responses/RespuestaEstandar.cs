namespace API.Responses
{
    public class RespuestaEstandar<T>
    {
        public T Data { get; set; }        
        public RespuestaEstandar(T data)
        {
            Data = data;
        }
    }
}
