using BaseServer;


public interface IClientCallBackInterface
{
    void ClientClose(BaseClient bc);

    void SendCallBack(BaseClient bc,int len);
}

