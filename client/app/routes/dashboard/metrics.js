import { Repeater, Text } from 'cx/src/ui';
import { Metric } from './metric';

export const Metrics = ({ title, metrics }) => (
   <cx>
      <div class="flex flex-col gap-2 p-3 bg-slate-100 rounded m-2">
         <div>
            <h4 class="mb-4 text-sm font-bold text-sky-400">
               <Text value={title} />
            </h4>
         </div>

         <div class="flex flex-row flex-wrap gap-5 w-auto">
            <Repeater visible-expr="{$record.metrics}" records={metrics} recordAlias="$metric">
               <Metric
                  visible-expr="{$metric.value}"
                  title-bind="$metric.name"
                  value-bind="$metric.value"
                  unit-bind="$metric.unit"
                  description-bind="$metric.description"
               />
            </Repeater>
         </div>
      </div>
   </cx>
);
