import { attachHubHandler, connectToHub, disconnectFromHub } from '../../hubs/metrics/metricsHub';

export default ({ init, get, set }) => {
   return {
      onInit() {
         this.onHubConnect();
         this.handleMetrics();
      },
      onDestroy() {
         this.onHubDisconnect();
      },

      async onHubConnect() {
         await connectToHub('metrics');
      },
      async onHubDisconnect() {
         await disconnectFromHub();
      },
      handleMetrics() {
         attachHubHandler('ReceiveMetrics', (data) => {
            let metrics = JSON.parse(data);
            let simpleMetrics = metrics?.filter((item) => item?.metrics.every((m) => m) && !item?.isChartMetric);
            let chartMetrics = metrics?.filter((item) => item?.metrics.every((m) => m) && item?.isChartMetric);
            console.log(metrics);
            set('$page.metrics', simpleMetrics);
            if (chartMetrics[0]) {
               set('$page.chartMetrics', chartMetrics[0].metrics[0].value);
            }
         });
      },
   };
};
