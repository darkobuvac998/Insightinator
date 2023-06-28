import { Repeater } from 'cx/src/ui';
import Controller from './Controller';
import { Metrics } from './metrics';
import { HttpResponseCode } from './HttpResponseCode';

export default () => (
   <cx>
      <div class="bg-gray-50 overflow-auto" controller={Controller}>
         <Repeater records-bind="$page.metrics">
            <Metrics title-bind="$record.name" metrics-bind="$record.metrics" />
         </Repeater>
         <div visible-expr="{$page.chartMetrics}" class="bg-slate-50 overflow-auto">
            <HttpResponseCode />
         </div>
      </div>
   </cx>
);
