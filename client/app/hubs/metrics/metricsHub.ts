import * as signalR from '@microsoft/signalr';
import { showErrorToast, showInfoToast } from '../../util/toasts';
import { Store } from 'cx/data';

let connection: signalR.HubConnection;
let RETRY_TIMEOUT: any;
let retryCount: number = 0;

let store = new Store({
   data: {
      connections: [],
      conntected: false,
   },
});

const alreadyConnected = (connectionId: string): boolean => {
   let conn = store.get('connections');
   if (conn && conn.length > 0) {
      return conn.some((c) => c === connectionId);
   }

   return false;
};

const saveConnection = (connectionId: string): void => {
   store.update('connections', (connections) => [...connections, connectionId]);
};

const removeConnection = (connectionId): void => {
   store.update('connections', (conns) => conns?.filter((c) => c != connectionId));
};

export const connectToHub = async (hub: string, config: { retry: number }): Promise<void> => {
   connection = new signalR.HubConnectionBuilder()
      .withUrl(`https://localhost:7214/hubs/${hub}`)
      .configureLogging(signalR.LogLevel.Debug)
      .build();

   try {
      if (alreadyConnected(connection.connectionId)) {
         return;
      }
      await connection.start();
      store.toggle('conntected');
      showInfoToast(`Successfully conntected to ${hub} hub.`);
      saveConnection(connection.connectionId);
   } catch (error) {
      showErrorToast(`Error while connecting to ${hub} hub.`);
      if (retryCount < config?.retry) {
         retryCount++;
         RETRY_TIMEOUT = setTimeout(() => {
            connectToHub(hub, config);
         }, 3000);
      } else {
         clearTimeout(RETRY_TIMEOUT);
         retryCount = 0;
         store.toggle('conntected');
      }
   }
};

export const disconnectFromHub = async (): Promise<void> => {
   try {
      await connection.stop();
      removeConnection(connection.connectionId);
      store.toggle('conntected');
      retryCount = 0;
   } catch (error) {}
};

export const attachHubHandler = (clientMethod: string, callbackFn: (...args: any) => {}): void => {
   connection?.on(clientMethod, callbackFn);
};

export default connection;
