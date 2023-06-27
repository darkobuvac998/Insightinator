import * as signalR from '@microsoft/signalr';
import { showErrorToast, showInfoToast } from '../../util/toasts';
import { Store } from 'cx/data';

let connection: signalR.HubConnection;
let RETRY_TIMEOUT;
let retryCount: number = 0;

let store = new Store({
   data: {
      connections: [],
   },
});

const alreadyConnected = (connectionId) => {
   let conn = store.get('connections');
   if (conn && conn.length > 0) {
      return conn.some((c) => c === connectionId);
   }

   return false;
};

const saveConnection = (connectionId) => {
   store.update('connections', (connections) => [...connections, connectionId]);
};

const removeConnection = (connectionId) => {
   store.update('connections', (conns) => conns?.filter((c) => c != connectionId));
};

export const connectToHub = async (hub: string, config: { retry: number }) => {
   connection = new signalR.HubConnectionBuilder()
      .withUrl(`https://localhost:7214/hubs/${hub}`)
      .configureLogging(signalR.LogLevel.Debug)
      .build();

   try {
      if (alreadyConnected(connection.connectionId)) {
         return;
      }
      await connection.start();
      showInfoToast(`Successfully conntected to ${hub} hub.`);
      saveConnection(connection.connectionId);
      console.log(store);
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
      }
   }
};

export const disconnectFromHub = async () => {
   try {
      await connection.stop();
      removeConnection(connection.connectionId);
      retryCount = 0;
      showInfoToast(`Successfully disconntected from hub.`);
   } catch (error) {}
};

export const attachHubHandler = (clientMethod: string, callbackFn: (...args: any) => {}) => {
   connection?.on(clientMethod, callbackFn);
};
