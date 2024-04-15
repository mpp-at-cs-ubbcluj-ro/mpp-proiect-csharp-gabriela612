using System;

namespace Utills.networking;

[Serializable]
public class Response
{
    private ResponseType type;
    private object data;

    private Response() { }

    public ResponseType Type => type;

    public object Data => data;

    private void SetType(ResponseType type)
    {
        this.type = type;
    }

    private void SetData(object data)
    {
        this.data = data;
    }

    public override string ToString()
    {
        return $"Response{{type='{type}', data='{data}'}}";
    }

    public class Builder
    {
        private Response response = new Response();

        public Builder Type(ResponseType type)
        {
            response.SetType(type);
            return this;
        }

        public Builder Data(object data)
        {
            response.SetData(data);
            return this;
        }

        public Response Build()
        {
            return response;
        }
    }
}
