using Newtonsoft.Json;

namespace Domain;

public class BasicResponse<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set;}
    public BasicResponse(int _status)
    {
        StatusCode = _status;
        Message = GetDefaultMessage(_status);
    }  
    public BasicResponse(int _status, string customMessage)
    {
        StatusCode = _status;
        Message = customMessage;
    }           

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
    private static string GetDefaultMessage(int statusCode)
    {
            string v = statusCode switch
            {
                200 => "Has realizado una petición correcta.",
                400 => "Has realizado una petición incorrecta.",
                401 => "Usuario no autorizado.",
                404 => "El recurso que has intentado solicitar no existe.",
                405 => "Este método HTTP no está permitido en el servidor.",
                500 => "Error en el servidor. Comunícate con el administrador", 
                _ => "Error no controlado en el servidor. Comunícate con el administrador"                                
            };
            return v;
    }
}