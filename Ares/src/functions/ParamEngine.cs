namespace Ares;
public class ParamEngineC{
    private Dictionary<string, object> _params = new Dictionary<string, object>();

    public void Add(string name, string defaultValue){
        if(!_params.ContainsKey(name)){
            _params.Add(name, defaultValue);
        }
    }

    public void Add(string name, bool defaultValue){
        if(!_params.ContainsKey(name)){
            _params.Add(name, defaultValue);
        }
    }

    public void Parse(string[] args){
        for(int i = 0; i < args.Length; i++){
            if(_params.ContainsKey(args[i])){
                if(_params[args[i]] is bool){
                    _params[args[i]] = true;
                }else if (i + 1 < args.Length){
                    _params[args[i]] = args[i + 1];
                }
            }
        }
    }

    public T Get<T>(string name){
        if(_params.ContainsKey(name)){
            try{
                return (T)Convert.ChangeType(_params[name], typeof(T));
            }catch (Exception ex){
                throw new ArgumentException($"Error converting parameter {name} to type {typeof(T).Name}: {ex.Message}");
            }
        }else{
            throw new ArgumentException($"Parameter {name} not found");
        }
    }
}
